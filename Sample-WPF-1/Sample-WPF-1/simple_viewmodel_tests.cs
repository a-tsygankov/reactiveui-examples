using Microsoft.Reactive.Testing;
using ReactiveDomain.Testing;
using ReactiveUI;
using ReactiveUI.Testing;
using System;
using System.Reactive.Concurrency;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Sample_WPF_1
{
    public class simple_viewmodel_tests : ReactiveTest
    {
        private readonly ITestOutputHelper _toh;
        public simple_viewmodel_tests(ITestOutputHelper toh)
        {
            _toh = toh;
            RxApp.MainThreadScheduler = Scheduler.CurrentThread;
        }


        [Fact]
        public void can_create_view_model_class()
        {
            _toh.WriteLine("Test started");

            var vm = new ViewModelClass();
            var notifications = vm.NameList.Changed.CreateCollection();
            _toh.WriteLine($"notifications.Count = {notifications.Count}");

            using (notifications)
            {
                vm.NameList.Add(Name.CreateName("Garfield"));
                Thread.Sleep(1000);
                _toh.WriteLine($"notifications.Count = {notifications.Count}");
            }

            
            _toh.WriteLine("Test completed");
        }

        [Fact]
        public void can_update_can_execute_with_RD()
        {
            var vm = new ViewModelClass();

            using (var notifications = vm.AddName.CanExecute.CreateCollection())
            {

                vm.FirstName = "hgdghfgsdfhjg";
                AssertEx.IsOrBecomesTrue(() => notifications.Count > 1);
                Assert.False(notifications[0]);
                Assert.True(notifications[1]);
            }
                
        }


        //[Fact]
        //public void can_update_can_execute()
        //{
        //    new TestScheduler().With(scheduler =>
        //    {
        //        var vm = new ViewModelClass();

        //        scheduler.Schedule(TimeSpan.FromMilliseconds(100), () => { vm.FirstName = "AAA";
        //            vm.FirstName = ""; 
        //        });

        //        var results = scheduler.Start(
        //            () => vm.WhenAnyValue(x => x.AddName.CanExecute));

        //        //var expected = scheduler.CreateColdObservable(
        //        //    OnNext(100, true),
        //        //    OnNext(200, false));
        //        var expected = new[]
        //        {
        //            OnNext(100, true),
        //            OnNext(200, false)
        //        };

        //        //ReactiveAssert.AreElementsEqual<bool>(expected, results.Messages);
        //        //results.Messages.AssertEqual(expected.Messages);
        //        //IEnumerable<object> expected = new[] { OnNext(20, new Unit()) };
        //        //ReactiveAssert.AreElementsEqual(expected.Messages, results.Messages);
        //    });

        //}

    }
}

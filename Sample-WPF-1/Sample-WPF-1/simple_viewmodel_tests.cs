using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Xunit;
using Xunit.Abstractions;

namespace Sample_WPF_1
{
    public class simple_viewmodel_tests
    {
        private readonly ITestOutputHelper _toh;
        public simple_viewmodel_tests(ITestOutputHelper toh)
        {
            _toh = toh;
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
    }
}

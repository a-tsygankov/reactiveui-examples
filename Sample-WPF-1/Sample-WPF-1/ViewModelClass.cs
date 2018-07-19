using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace Sample_WPF_1
{
    public class ViewModelClass : ReactiveObject
    {
        private string _lastName;
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                this.RaiseAndSetIfChanged(ref _firstName, value);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                this.RaiseAndSetIfChanged(ref _lastName, value);
            }
        }

        public string FullName => _fullName.Value;
        private readonly ObservableAsPropertyHelper<string> _fullName;

        
        public ReactiveList<String> NameList = new ReactiveList<string>();
        public ReactiveCommand AddName { get; }
        public ReactiveCommand <Unit, Unit> AddNameAsync { get; }
        public ReactiveCommand<Unit, Unit> AddNameAsyncCancel { get; }

        public ViewModelClass()
        {
            NameList.Add("John Malkovich");
            NameList.Add("Darth Vader");
            NameList.Add("Tigra");

            _fullName = this.WhenAnyValue(x => x.FirstName, y => y.LastName)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Select(x => $"{x.Item1} {x.Item2}".Trim())
                .ToProperty(this, x => x.FullName, "");

            var canAdd = this.WhenAnyValue(x => x.FullName)
                .Select(x => !string.IsNullOrWhiteSpace(x));

            AddName = ReactiveCommand.Create(
                () => NameList.Add(FullName),
                canAdd);


            AddNameAsync = ReactiveCommand.CreateFromObservable(
                //this.WhenAnyValue(vm => vm.FullName).Select(q => !string.IsNullOrEmpty(q)),
                () => Observable.StartAsync(ct => AddAsync(FullName, ct)).TakeUntil(AddNameAsyncCancel),
                canAdd);

            AddNameAsyncCancel = ReactiveCommand.Create(() =>  {}, AddNameAsync.IsExecuting);


        }

        private async Task AddAsync(string name, CancellationToken token)
        {
            await Task.Delay(1500, token);
            
            NameList.Add(name);
        }
    }
}

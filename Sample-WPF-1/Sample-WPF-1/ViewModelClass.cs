using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        
        public ReactiveList<Name> NameList = new ReactiveList<Name>();
        public ReactiveCommand AddName { get; }
        public ReactiveCommand <Unit, Unit> AddNameAsync { get; }
        public ReactiveCommand<Unit, Unit> AddNameAsyncCancel { get; }

        public ViewModelClass()
        {
            NameList.Add(Name.CreateName("John Malkovich"));
            NameList.Add(Name.CreateName("Darth Vader"));
            NameList.Add(Name.CreateName("Tigra"));

            _fullName = this.WhenAnyValue(x => x.FirstName, y => y.LastName)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Select(x => $"{x.Item1} {x.Item2}".Trim())
                .ToProperty(this, x => x.FullName, "");

            var canAdd = this.WhenAnyValue(x => x.FullName)
                .Select(x => !string.IsNullOrWhiteSpace(x));

            AddName = ReactiveCommand.Create(
                () => NameList.Add(Name.CreateName(FullName)),
                canAdd);


            AddNameAsync = ReactiveCommand.CreateFromObservable(
                () => Observable.StartAsync(ct => AddAsync(FullName, ct)).TakeUntil(AddNameAsyncCancel),
                canAdd);

            AddNameAsyncCancel = ReactiveCommand.Create(() =>  {}, AddNameAsync.IsExecuting);
        }

        private async Task AddAsync(string name, CancellationToken token)
        {
            await Task.Delay(3000, token);
            
            NameList.Add(Name.CreateName(name));
        }
    }
}

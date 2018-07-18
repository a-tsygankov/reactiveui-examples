using System;
using System.Drawing.Text;
using System.Reactive;
using System.Reactive.Linq;
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

            AddName = ReactiveCommand.Create(
                () => NameList.Add(FullName),
                this.WhenAnyValue(x => x.FullName)
                    .Select(x => !string.IsNullOrWhiteSpace(x)));

        }
    }
}

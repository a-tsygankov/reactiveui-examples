using System;
using System.Drawing.Text;
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
        


        public ViewModelClass()
        {
            FirstName = "Jon";
            LastName = "Snow";

            _fullName = this.WhenAnyValue(x => x.FirstName, y => y.LastName)
                //.Throttle(TimeSpan.FromMilliseconds(1000))
                .Select(x => $"{x.Item1} {x.Item2}")
                .ToProperty(this, x => x.FullName);

        }
    }
}

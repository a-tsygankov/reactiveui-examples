using Sample_WPF_1.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sample_WPF_1
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        private string _fullName;
        private string _lastName;
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                UpdateFullName();
            }
        
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                UpdateFullName();
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                if (value == _fullName) return;
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }



        private void UpdateFullName()
        {
            FullName = $"{FirstName} {LastName}";
        }

        public ViewModelClass()
        {
            FirstName = "Jon";
            LastName = "Snow";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

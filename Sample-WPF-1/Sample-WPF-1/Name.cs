using System;
using ReactiveUI;

namespace Sample_WPF_1
{
    public abstract class Name : ReactiveObject
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public static Name CreateName(string name)
        {
            if (name.Split().Length == 1)
            {
                return new PetName { Text = name }; 
            }
            else if (name.Split().Length > 1)
            {
                return new PersonName{ Text = name};
            }
            else
            {
                throw new Exception("Name cannot be empty");
            }
        }
    }

    public class PetName : Name
    {
    }
    public class PersonName : Name
    {

    }
}

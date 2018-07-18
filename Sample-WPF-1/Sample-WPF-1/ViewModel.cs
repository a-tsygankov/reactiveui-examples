using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_WPF_1
{
    public class ViewModel
    {
        private string _fullName;
        private string _lastName;
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string FullName
        {
            get => _fullName;
            set => _fullName = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_practice.ViewModels
{
    internal class PersonalAccountViewModel : BaseViewModel
    {
        private string accessLevel;
        public string AccessLevel
        {
            get => accessLevel;
            set
            {
                accessLevel = value;
                OnPropertyChanged(nameof(AccessLevel));
            }
        }
    }
}

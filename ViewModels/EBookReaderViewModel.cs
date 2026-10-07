using _301434046_eskim__Lab2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.ViewModels
{
    public class EBookReaderViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;

        public EBookReaderViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
    }
}

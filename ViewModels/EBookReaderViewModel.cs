using _301434046_eskim__Lab2.Commands;
using _301434046_eskim__Lab2.Services;
using _301434046_eskim__Lab2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _301434046_eskim__Lab2.ViewModels
{
    public class EBookReaderViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;

        public ICommand LogoutUser { get; }

        public EBookReaderViewModel(NavigationStore navigationStore, AuthenticationService authenticationService)
        {
            _navigationStore = navigationStore;
            LogoutUser = new LogoutCommand(navigationStore, authenticationService);
        }
    }
}

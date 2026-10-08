using _301434046_eskim__Lab2.Services;
using _301434046_eskim__Lab2.Stores;
using _301434046_eskim__Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Commands
{
    public class LoginCommand : CommandBase
    {
        private AuthenticationService _authenticationService;
        private readonly NavigationStore _navigationStore;
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel, NavigationStore navigationStore, AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginViewModel = loginViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            bool authenticated = _authenticationService.Login(
                _loginViewModel.Username,
                _loginViewModel.Password);

            if (authenticated)
            {
                _navigationStore.CurrentViewModel = new EBookReaderViewModel(_navigationStore, _authenticationService);
            }
            else
            {
                _loginViewModel.ErrorMessage = "Invalid username or password.";
            }
        }
    }
}

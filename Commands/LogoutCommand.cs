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
    public class LogoutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly AuthenticationService _authenticationService;

        private readonly AmazonS3PdfService _amazonS3PdfService;

        public LogoutCommand(NavigationStore navigationStore, AuthenticationService authenticationService)
        {
            _navigationStore = navigationStore;
            _authenticationService = authenticationService;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel.Dispose();
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _authenticationService, _amazonS3PdfService);
        }
    }
}

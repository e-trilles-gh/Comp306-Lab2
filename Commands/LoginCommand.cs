using _301434046_eskim__Lab2.Services;
using _301434046_eskim__Lab2.Stores;
using _301434046_eskim__Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _301434046_eskim__Lab2.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly NavigationStore _navigationStore;
        private readonly LoginViewModel _loginViewModel;
        private readonly AmazonS3PdfService _amazonS3PdfService;
        private readonly BookService _bookService;


        public LoginCommand(LoginViewModel loginViewModel,
            NavigationStore navigationStore,
            AuthenticationService authenticationService,
            AmazonS3PdfService amazonS3PdfService,
            BookService bookService)
        {
            _authenticationService = authenticationService;
            _loginViewModel = loginViewModel;
            _navigationStore = navigationStore;
            _amazonS3PdfService = amazonS3PdfService;
            _bookService = bookService;
        }

        public override void Execute(object parameter)
        {
            bool authenticated = _authenticationService.Login(
                _loginViewModel.Username,
                _loginViewModel.Password);

            if (authenticated)
            {
                _navigationStore.CurrentViewModel
                    = new EBookReaderViewModel(
                        _navigationStore,
                        _authenticationService,
                        _amazonS3PdfService,
                        _bookService);
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}

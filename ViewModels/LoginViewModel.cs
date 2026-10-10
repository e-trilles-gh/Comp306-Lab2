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
    public class LoginViewModel : ViewModelBase
    {
        private readonly BookService _bookService;
        public ICommand LoginUser { get; }
        public ICommand ExitProgram { get; }

        private string _username;
        private string _password;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get {  return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel(
            NavigationStore navigationStore,
            AuthenticationService authenticationService,
            AmazonS3PdfService amazonS3PdfService,
            BookService bookService)
        {
            _bookService = bookService;
            LoginUser = new LoginCommand(
                this,
                navigationStore,
                authenticationService,
                amazonS3PdfService,
                _bookService);
            ExitProgram = new ExitCommand();
        }
    }
}

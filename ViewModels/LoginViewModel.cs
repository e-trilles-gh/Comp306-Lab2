using _301434046_eskim__Lab2.Commands;
using _301434046_eskim__Lab2.Services;
using _301434046_eskim__Lab2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //properties
        private readonly BookService _bookService;
        public ICommand LoginUser { get; }
        public ICommand ExitProgram { get; }

        private string _username;
        private string _password;
        private string _errorMessage;

        //setter and getter methods
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

        //constructor with parameters
        public LoginViewModel(
            NavigationStore navigationStore,
            AuthenticationService authenticationService,
            AmazonS3PdfService amazonS3PdfService,
            BookService bookService)
        {
            _bookService = bookService;

            //instantiates the LoginCommand to verify user credentials
            LoginUser = new LoginCommand(
                this,
                navigationStore,
                authenticationService,
                amazonS3PdfService,
                _bookService);

            //instantiates the ExitCommand to terminate the program
            ExitProgram = new ExitCommand();
        }
    }
}

using _301434046_eskim__Lab2.Services;
using _301434046_eskim__Lab2.Stores;
using _301434046_eskim__Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly AuthenticationService _authenticationService;

        private readonly AmazonS3PdfService _amazonS3PdfService;

        private readonly BookService _bookService;

        public LogoutCommand(
            NavigationStore navigationStore,
            AuthenticationService authenticationService,
            AmazonS3PdfService amazonS3PdfService,
            BookService bookService)
        {
            _navigationStore = navigationStore;
            _authenticationService = authenticationService;
            _amazonS3PdfService = amazonS3PdfService;
            _bookService = bookService;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel.Dispose();
            _navigationStore.CurrentViewModel
                = new LoginViewModel(
                    _navigationStore,
                    _authenticationService,
                    _amazonS3PdfService,
                    _bookService);
        }
    }
}

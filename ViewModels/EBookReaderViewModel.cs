using _301434046_eskim__Lab2.Commands;
using _301434046_eskim__Lab2.Models;
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
    public class EBookReaderViewModel : ViewModelBase
    {
        //properties and fields
        private readonly NavigationStore _navigationStore;

        private readonly AuthenticationService _authenticationService;

        private readonly AmazonS3PdfService _amazonS3PdfService;

        private readonly BookListViewModel _bookListViewModel;

        private ViewModelBase _currentContentViewModel;

        private readonly BookService _bookService;

        private string _userName;

        public ICommand LogoutUser { get; }

        public ICommand ShowBookListCommand { get; }

        //getter and setter methods
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        //sets the view for the content control
        public ViewModelBase CurrentContentViewModel
        {
            get { return _currentContentViewModel; }
            set
            {
                if (ReferenceEquals(_currentContentViewModel, value))
                {
                    return;
                }

                _currentContentViewModel = value;
                OnPropertyChanged(nameof(CurrentContentViewModel));
            }
        }

        //constructor with parameter
        public EBookReaderViewModel(
            NavigationStore navigationStore,
            AuthenticationService authenticationService,
            AmazonS3PdfService amazonPdfService,
            BookService bookService)
        {
            _navigationStore = navigationStore;

            _authenticationService = authenticationService;

            _amazonS3PdfService = amazonPdfService;

            _bookService = bookService;

            //instantiates the LogoutCommand for logout button
            LogoutUser
                = new LogoutCommand(
                    _navigationStore,
                    _authenticationService,
                    _amazonS3PdfService,
                    _bookService);

            //instatiates the ActionCommand for ShowBookListCommand
            ShowBookListCommand = new ActionCommand(_ => ShowBookList());

            UserName = _authenticationService.CurrentUserName;

            string userId = _authenticationService.CurrentUserId;
            
            _bookListViewModel = new BookListViewModel(OpenBook, _bookService, userId);

            CurrentContentViewModel = _bookListViewModel;

            _ = _bookListViewModel.LoadBooksAsync();
        }

        private async void ShowBookList()
        {
            _bookListViewModel.ClearSelectedBook();

            //loads all the books
            await _bookListViewModel.LoadBooksAsync();

            //sets the content which shows the list of books
            CurrentContentViewModel = _bookListViewModel;
        }

        private async void OpenBook(Book book)
        {
            //instantiates the viewmodel that manages to view the selected book
            var pdfReaderViewModel = new PdfReaderViewModel(book, _amazonS3PdfService, _bookService);
            await pdfReaderViewModel.LoadAsync();

            if (pdfReaderViewModel.DocumentStream != null)
            {
                //sets the contentview to show the pdf
                CurrentContentViewModel = pdfReaderViewModel;
            }
            else
            {
                System.Windows.MessageBox.Show(
                    pdfReaderViewModel.ErrorMessage ?? "Unable to open the book.");

                pdfReaderViewModel.Dispose();
            }
        }
    }
}

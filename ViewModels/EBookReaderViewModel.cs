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

namespace _301434046_eskim__Lab2.ViewModels
{
    public class EBookReaderViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly AuthenticationService _authenticationService;

        private readonly AmazonS3PdfService _amazonS3PdfService;

        private readonly BookListViewModel _bookListViewModel;

        private ViewModelBase _currentContentViewModel;

        private readonly BookService _bookService;


        public ICommand LogoutUser { get; }

        public ICommand ShowBookListCommand { get; }

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

        public EBookReaderViewModel(
            NavigationStore navigationStore,
            AuthenticationService authenticationService,
            AmazonS3PdfService amazonPdfService,
            BookService bookService)
        {
            _navigationStore = navigationStore;

            _authenticationService = authenticationService;

            _amazonS3PdfService = amazonPdfService;

            string userId = _authenticationService.CurrentUserId;

            LogoutUser
                = new LogoutCommand(
                    navigationStore,
                    authenticationService,
                    amazonPdfService,
                    bookService);

            _bookListViewModel = new BookListViewModel(OpenBook, bookService, userId);

            _ = _bookListViewModel.LoadBooksAsync();

            ShowBookListCommand = new ActionCommand(_ => ShowBookList());

            CurrentContentViewModel = _bookListViewModel;
        }

        private void ShowBookList()
        {
            CurrentContentViewModel = _bookListViewModel;
        }

        private async void OpenBook(Book book)
        {
            var readerViewModel = new PdfReaderViewModel(book, _amazonS3PdfService);
            await readerViewModel.LoadAsync();

            if (readerViewModel.DocumentStream != null)
            {
                CurrentContentViewModel = readerViewModel;
            }
            else
            {
                System.Windows.MessageBox.Show(
                    readerViewModel.ErrorMessage ?? "Unable to open the book.");

                readerViewModel.Dispose();
            }
        }
    }
}

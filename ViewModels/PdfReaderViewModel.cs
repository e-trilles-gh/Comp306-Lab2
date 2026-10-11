using _301434046_eskim__Lab2.Models;
using _301434046_eskim__Lab2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.ViewModels
{
    public class PdfReaderViewModel : ViewModelBase, IDisposable
    {
        //properties
        private readonly AmazonS3PdfService _amazonS3PdfService;

        private MemoryStream _documentStream;

        private bool _isLoading;

        private string _errorMessage;

        private BookService _bookService;

        public Book SelectedBook { get; }

        public int PageToRestore { get; }

        //setter and getter methods
        public int LastReadPage
        {
            get { return SelectedBook.LastReadPage; }
        }

        public MemoryStream DocumentStream
        {
            get { return _documentStream; }
            set
            {
                _documentStream = value;
                OnPropertyChanged(nameof(DocumentStream));
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public PdfReaderViewModel(
            Book selectedBook,
            AmazonS3PdfService amazonS3PdfService,
            BookService bookService)
        {
            SelectedBook = selectedBook ?? throw new ArgumentNullException(nameof(selectedBook));

            _amazonS3PdfService = amazonS3PdfService ?? throw new ArgumentNullException(nameof(amazonS3PdfService));

            _bookService = bookService;

            //retrieves the bookmark of the book, then use that as the starting page when opened
            PageToRestore = SelectedBook.LastReadPage;
        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            ErrorMessage = null;

            try
            {
                //retrieves the selected book from s3
                DocumentStream = await _amazonS3PdfService.GetPdfAsync(SelectedBook);
            }
            catch (Exception error)
            {
                ErrorMessage = "Unable to load the selected book. " + error.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async void UpdateLastReadPage(int page)
        {
            if (page > 0)
            {
                //saves the last page to the s3
                SelectedBook.LastReadPage = page;
                OnPropertyChanged(nameof(LastReadPage));
                await _bookService.UpdateLastReadPageAsync(SelectedBook.UserId, SelectedBook.BookId, page);
            }
        }
    }
}

using _301434046_eskim__Lab2.Models;
using _301434046_eskim__Lab2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.ViewModels
{
    public class PdfReaderViewModel : ViewModelBase, IDisposable
    {
        private readonly AmazonS3PdfService _pdfService;

        private MemoryStream _documentStream;

        private bool _isLoading;

        private string _errorMessage;

        public Book SelectedBook { get; }

        public int PageToRestore { get; }

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

        public PdfReaderViewModel(Book selectedBook, AmazonS3PdfService pdfService)
        {
            SelectedBook = selectedBook ?? throw new ArgumentNullException(nameof(selectedBook));

            _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));

            PageToRestore = SelectedBook.LastReadPage;
        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            ErrorMessage = null;

            try
            {
                DocumentStream = await _pdfService.GetPdfAsync(SelectedBook);
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

        public void UpdateLastReadPage(int page)
        {
            if (page > 0)
            {
                SelectedBook.LastReadPage = page;
                OnPropertyChanged(nameof(LastReadPage));
            }
        }
    }
}

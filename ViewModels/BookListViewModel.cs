using _301434046_eskim__Lab2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.ViewModels
{
    public class BookListViewModel : ViewModelBase
    {
        private Book _selectedBook;
        private readonly Action<Book> _openBook;

        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                if (ReferenceEquals(_selectedBook, value))
                {
                    return;
                }

                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));

                if (_selectedBook != null)
                {
                    _openBook?.Invoke(_selectedBook);
                }
            }
        }

        public BookListViewModel(Action<Book> openBook)
        {
            _openBook = openBook;
        }
    }
}

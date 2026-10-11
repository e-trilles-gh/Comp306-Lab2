using _301434046_eskim__Lab2.Models;
using _301434046_eskim__Lab2.Services;
using System;
using Amazon.DynamoDBv2;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class BookListViewModel : ViewModelBase
    {
        //properties and fields
        private Book _selectedBook;
        private readonly Action<Book> _openBook;
        private readonly BookService _bookService;
        private readonly string _userId;

        //collection of books
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        //setter and getter
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

        //constructor with parameter
        public BookListViewModel(
            Action<Book> openBook,
            BookService bookService,
            string userId)
        {
            _openBook = openBook;
            _bookService = bookService;
            _userId = userId;
        }


        public async Task LoadBooksAsync()
        {
            try
            {
                var books = await _bookService.GetBooksForUserAsync(_userId);

                var sortedBooks = books.OrderByDescending(book => book.LastOpenedAt ?? DateTime.MinValue).ToList();
                
                Books.Clear();

                foreach (Book book in sortedBooks)
                {
                    Books.Add(book);
                }
            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show("Could not load books: " + error.Message);
            }
        }

        public void ClearSelectedBook()
        {
            SelectedBook = null;
        }
    }
}

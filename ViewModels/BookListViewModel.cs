using _301434046_eskim__Lab2.Models;
using _301434046_eskim__Lab2.Services;
using System;
using Amazon.DynamoDBv2;
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
        private readonly BookService _bookService;
        private readonly string _userId;

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

        public BookListViewModel(
            Action<Book> openBook,
            BookService bookService,
            string userId)
        {
            _openBook = openBook;
            _bookService = bookService;
            _userId = userId;
        }

        public void LoadBooks(List<Book> books)
        {
            Books.Clear();

            foreach (Book book in books)
            {
                Books.Add(book);
            }
        }

        public async Task LoadBooksAsync()
        {
            try
            {
                var books = await _bookService.GetBooksForUserAsync(_userId);
                Books.Clear();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show("Could not load books: " + error.Message);
            }
        }

        public void LoadSampleBooks()
        {
            var sampleBooks = new List<Book>
            {
                new Book
                {
                    BookId = "book-001",
                    UserId = _userId,
                    Title = "The Hobbit",
                    Author = "Harry Potter",
                    LastReadPage = 25
                },

                new Book
                {
                    BookId = "book-002",
                    UserId = _userId,
                    Title = "The Cars",
                    Author = "Toy Story",
                    LastReadPage = 8
                },

                new Book
                {
                    BookId = "book-003",
                    UserId = _userId,
                    Title = "The Witcher",
                    Author = "Call of Duty",
                    LastReadPage = 42
                }
            };

            LoadBooks(sampleBooks);
        }
    }
}

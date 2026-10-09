using _301434046_eskim__Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Services
{
    internal interface IBookService
    {
        Task<List<Book>> GetBooksForUserAsync(string userId);
    }
}

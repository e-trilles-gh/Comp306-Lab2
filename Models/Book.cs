using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Models
{
    public class Book
    {
        public string UserId { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string BucketName { get; set; }
        public string KeyName { get; set; }
        public int LastReadPage { get; set; }
        public DateTime? LastOpenedAt { get; set; }
    }
}

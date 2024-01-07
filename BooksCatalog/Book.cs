using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalog
{
    public class Book
    {
        public Book() 
        {
            BookId = Guid.NewGuid().ToString();
        }
        public Book(string title, string author, string isbn, string brief, DateTime date, string genre)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Brief = brief;
            Date = date;
            Genre = genre;
            BookId = Guid.NewGuid().ToString();
        }
        
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string ISBN {get; set; }

        public string Brief { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Genre { get; set; }

    }
}

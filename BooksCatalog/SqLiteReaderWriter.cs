using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalog
{
    public class SqLiteReaderWriter : IReaderWriter
    {
        public BookCatalog ReadBooks()
        {
            using var db = new BooksCatalogContext();
            return new BookCatalog { Books = db.Books.ToList() };
        }

        public void Save(BookCatalog bookCatalog)
        {
            using var db = new BooksCatalogContext();
            foreach (var book in bookCatalog.Books) 
            {
                if(db.Books.Contains(book))
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                }

                db.Books.Add(book);
                db.SaveChanges();
            }
        }
    }
}

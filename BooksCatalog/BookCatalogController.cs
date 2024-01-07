using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksCatalog
{
    public class BookCatalogController
    {
        public BookCatalog BookCatalog { get; set; }

        public BookCatalogController()
        {
            BookCatalog = ReadBooks() ?? new BookCatalog();
        }

        public IReaderWriter GetReaderWriter()
        {
            string path = "settings.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SavingType savingType = JsonConvert.DeserializeObject<SavingType>(json);
                return GetReaderWriterBySavingType(savingType);
            }
            else
            {
                return new JsonReaderWriter();
            }
        }

        public IReaderWriter GetReaderWriterBySavingType(SavingType savingType)
        {
            switch (savingType)
            {
                case SavingType.JSon:
                    return new JsonReaderWriter();
                case SavingType.Xml:
                    return new XmlReaderWriter();
                case SavingType.SqLite:
                    return new SqLiteReaderWriter();
                default:
                    return new JsonReaderWriter();
            }
        }

        public BookCatalogController(IEnumerable<Book> books)
        {
            BookCatalog = new BookCatalog();
            BookCatalog.Books = books.ToList();
        }

        public void Save()
        {
            IReaderWriter readerWriter = GetReaderWriter();
            readerWriter.Save(BookCatalog);

        }
        private BookCatalog ReadBooks()
        {
            return GetReaderWriter().ReadBooks();
        }

        public void AddBook(string title, string author, string isbn, string annotaion, DateTime date, string genre)
        {
            var book = new Book(title, author, isbn, annotaion, date, genre);
            BookCatalog.Books.Add(book);
        }

        public IEnumerable<Book> FindByAuthor(string author)
        {
            return BookCatalog.Books.Where(x => x.Author == author);
        }

        public IEnumerable<Book> FindByISBN(string isbn)
        {
            return BookCatalog.Books.Where(x => x.ISBN == isbn);
        }

        public IEnumerable<Book> FindByTitle(string title)
        {
            return BookCatalog.Books.Where(x => x.Title.Contains(title));
        }

        public IEnumerable<Book> FindByKeyWords(string[] keyWords)
        {
            Dictionary<Book, int> books = new Dictionary<Book, int>();
            foreach (string word in keyWords)
            {
                string keyWord = word.Trim();
                foreach (var book in BookCatalog.Books)
                {
                    if (book.Brief.Contains(keyWord))
                    {
                        if (books.ContainsKey(book))
                        {
                            books[book] = books[book] + 1;
                        }
                        else
                        {
                            books.Add(book, 1);
                        }
                    }
                }
            }

            return books.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
        }

        public IEnumerable<Book> FindBooks(Category category, string value)
        {
            switch (category)
            {
                case Category.Author: return FindByAuthor(value);
                case Category.ISBN: return FindByISBN(value);
                case Category.Title: return FindByTitle(value);
                case Category.Keyword: return FindByKeyWords(value.Split(','));
                default: return new List<Book>();
            }
        }
    }
}

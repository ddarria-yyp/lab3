using BooksCatalog;

namespace BookCatalogTests
{
    public class BookCatalogTests
    {
        [Fact]
        public void NewBookAddedToBooks()
        {
            var books = new List<Book>();
            var catalogController = new BookCatalogController(books);
            int count = catalogController.BookCatalog.Books.Count;
            Assert.Equal(0, count);
            catalogController.AddBook("Title", "Author", "0000-000", "key, work, word", DateTime.Now, "Roman");
            Assert.Single(catalogController.BookCatalog.Books);
        }

        [Theory]
        [InlineData(Category.Title, "Title", 2)]
        [InlineData(Category.Author, "Somebody", 1)]
        [InlineData(Category.Author, "Author2", 1)]
        [InlineData(Category.ISBN, "0200-000", 1)]
        [InlineData(Category.Keyword, "key, work", 2)]
        public void FindBooksReturnsRightResults(Category findByEnum, string keyword, int count)
        {
            var catalog = GetBookCatalog();
            var books = catalog.FindBooks(findByEnum, keyword);
            Assert.True(books.Any());
            Assert.Equal(count, books.Count());
        }

        [Fact]
        public void FindBooksReturnsSortedResults()
        {
            var catalog = GetBookCatalog();
            var books = catalog.FindBooks(Category.Keyword, "work, goods");
            Assert.True(books.Any());
            Assert.Equal(2, books.Count());
            Assert.Equal("Test", books.First().Title);
            Assert.Equal("Title", books.Last().Title);

        }

        [Fact]
        private void GetReaderWriterReturnsCorrectResult()
        {
            var catalog = GetBookCatalog();
            IReaderWriter readerWriter = catalog.GetReaderWriterBySavingType(SavingType.SqLite);
            Assert.True(readerWriter is SqLiteReaderWriter);
            readerWriter = catalog.GetReaderWriterBySavingType(SavingType.Xml);
            Assert.True(readerWriter is XmlReaderWriter);
            readerWriter = catalog.GetReaderWriterBySavingType(SavingType.JSon);
            Assert.True(readerWriter is JsonReaderWriter);
        }

        private BookCatalogController GetBookCatalog()
        {
            var books = new List<Book>();
            var catalog = new BookCatalogController(books);
            catalog.AddBook("Title", "Author", "0000-000", "key, work, word", DateTime.Now, "Roman");
            catalog.AddBook("Title 2", "Author2","0200-000", "sea sky grass", DateTime.Now, "Poem");
            catalog.AddBook("Test", "Somebody", "1000-000", "goods product work", DateTime.Now, "Detective");
            return catalog;
        }
    }
}
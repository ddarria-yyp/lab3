namespace BooksCatalog
{
    public interface IReaderWriter
    {
        void Save(BookCatalog bookCatalog);
        BookCatalog ReadBooks();
    }
}

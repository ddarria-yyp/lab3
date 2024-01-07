using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;


namespace BooksCatalog
{
    public class JsonReaderWriter : IReaderWriter
    {
        public BookCatalog ReadBooks()
        {
            string path = "books.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<BookCatalog>(json);
            }
            else
            {
                return null;
            }
        }

        public void Save(BookCatalog bookCatalog)
        {
            string json = JsonConvert.SerializeObject(bookCatalog);
            File.WriteAllText("books.json", json);
        }
    }
}

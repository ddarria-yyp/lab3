using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BooksCatalog
{
    public class XmlReaderWriter : IReaderWriter
    {
        public BookCatalog? ReadBooks()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BookCatalog));

            using (FileStream fs = new FileStream("books.xml", FileMode.OpenOrCreate))
            {
                BookCatalog? bookCatalog = xmlSerializer.Deserialize(fs) as BookCatalog;
                return bookCatalog;
            }
        }

        public void Save(BookCatalog bookCatalog)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BookCatalog));
            using (FileStream fs = new FileStream("books.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, bookCatalog);
            }
        }
    }
}

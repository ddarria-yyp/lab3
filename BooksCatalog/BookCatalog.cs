using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BooksCatalog
{
    public class BookCatalog
    {
        public BookCatalog() 
        { 
            Books = new List<Book>();
        }

        [XmlElement]
        public List<Book> Books { get; set; }
    }
}

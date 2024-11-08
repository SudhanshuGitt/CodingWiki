using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models.FluentModels
{
    public class Fluent_Book
    {
        // it will tell EF this column will be primary key
        // if we rename to id EF core will assume it as PK we don't need to add KEY annotation
        // or if property ends with Id eg BookId it will also Automatically be PK
        //[Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBM { get; set; }
        public decimal Price { get; set; }
        public decimal PriceRange { get; set; }
        // one to one mappping
        //navigation property to Book Detail table
        public Fluent_BookDetail BookDetail { get; set; }

         //One to Many (One book can have only one publisher but one pulisher can pulish multiple books)
        public int Publisher_Id { get; set; }

        // Navigation prorperty
        public Fluent_Publisher Publisher { get; set; }

        //public List<Fluent_Author> Authors { get; set; }

        ////Author can have mutiple books and books can have multitple authors(many to many)
        public List<Fluent_BookAuthorMap> BookAuthorMap { get; set; }
    }
}

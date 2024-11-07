using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Book
    {
        // it will tell EF this column will be primary key
        // if we rename to id EF core will assume it as PK we don't need to add KEY annotation
        // or if property ends with Id eg BookId it will also Automatically be PK
        //[Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string ISBM { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public decimal PriceRange { get; set; }
        // one to one mappping
        //navigation property to Book Detail table
        public BookDetail BookDetail { get; set; }

        // One to Many (One book can have only one publisher but one pulisher can pulish multiple books)
        [ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }

        // Navigation prorperty
        public Publisher Publisher { get; set; }

        //Author can have mutiple books and books can have multitple authors(many to many)
        public List<Author> Authors { get; set; }
    }
}

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
        [NotMapped] // we do not want to create column for this
        public string PriceRange { get; set; }
        public BookDetail BookDetail { get; set; }
    }
}

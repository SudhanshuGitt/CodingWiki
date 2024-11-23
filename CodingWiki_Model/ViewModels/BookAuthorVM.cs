using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodingWiki_Model.ViewModels
{
    public class BookAuthorVM
    {
        public BookAuthorMap BookAuthor { get; set; }
        public Book Book { get; set; }

        // to dispaly authior of all the currenlty selected book
        // we need the mapping table to find authorid for a paticular book 
        public IEnumerable<BookAuthorMap> BookAuthorList { get; set; }

        public IEnumerable<SelectListItem> AuthorList { get; set; }
    }
}

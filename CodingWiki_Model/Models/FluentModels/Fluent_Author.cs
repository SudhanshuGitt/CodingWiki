using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models.FluentModels
{
    public class Fluent_Author
    {

        public int Author_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        //computed Property
        public string FullName
        {

            get
            {
                return $"{FirstName} {LastName}";
            }

        }


        //Author can have mutiple books and books can have multitple authors(many to many)
        // EF core will create an Intermediate table AuthorBook that has AuthorId and BookId
        // Author book table has 1 to many relation wth Author aswell as Booktable
        // if we combine two one to may relaiton we will have many to many relation(.net cor 5 or above)
        // if not .net 5 we need to create mapping table

        //public List<Fluent_Book> Books { get; set; }
        public List<Fluent_BookAuthorMap> BookAuthorMap { get; set; }

    }
}

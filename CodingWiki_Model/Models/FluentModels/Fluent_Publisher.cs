using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models.FluentModels
{

    public class Fluent_Publisher
    {
        public int Publisher_Id { get; set; }
        public string Publisher_Name { get; set; }
        public string Location { get; set; }

        // if we wan to retrive all books that publisher has published
        //public IList<Fluent_Book> Books { get; set; }

    }
}

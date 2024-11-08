﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models.FluentModels
{
    // if we wan to control on mapping table for eg we wan to Add column in Author book we can crete Manul
    public class Fluent_BookAuthorMap
    {
        // here we need a composite key  
        //[ForeignKey("Book")]
        public int Book_Id { get; set; }
        //[ForeignKey("Author")]
        public int Author_Id { get; set; }

        //public Fluent_Book Book { get; set; }

        //public Fluent_Author Author { get; set; }
    }
}
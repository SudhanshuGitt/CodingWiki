﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class BookDetail
    {
        [Key]
        public int BookDetail_Id { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public string Weight { get; set; }

        // Book_Id is a forgein key to navigation property Book
        [ForeignKey("Book")]
        public int Book_Id { get; set; }
        // we need to add navigation property to define this a FK to Book Table
        public Book Book { get; set; }

    }
}
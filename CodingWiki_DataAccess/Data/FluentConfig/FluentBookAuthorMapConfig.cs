using CodingWiki_Model.Models.FluentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data.FluentConfig
{
    public class FluentBookAuthorMapConfig : IEntityTypeConfiguration<Fluent_BookAuthorMap>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthorMap> modelBuilder)
        {
            //FLuent Book Author
            modelBuilder.HasKey(b => new { b.Author_Id, b.Book_Id });
            //Many to Many One Book can have many Authors and viceversa
            modelBuilder.HasOne(b => b.Book).WithMany(b => b.BookAuthorMap)
                .HasForeignKey(u => u.Book_Id);
            modelBuilder.HasOne(b => b.Author).WithMany(b => b.BookAuthorMap)
                .HasForeignKey(u => u.Author_Id);


        }
    }
}

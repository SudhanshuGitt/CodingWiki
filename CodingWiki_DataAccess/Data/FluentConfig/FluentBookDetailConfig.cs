using CodingWiki_Model.Models.FluentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data.FluentConfig
{
    // add configutation FLuent API it must extend 
    // we need to define entity on which it will be adding configuration
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            // fluent BookDetails
            //Name of table
            // change table name and column name using fluent API
            modelBuilder.ToTable("Fluent_BookDetials");

            //name of columns
            modelBuilder.Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters");

            // Primary key
            //adding required and PK to rproperty
            modelBuilder.HasKey(u => u.BookDetail_Id);

            //other validation
            modelBuilder.Property(u => u.NumberOfChapters).IsRequired();

            //relations
            // One to One Mapping Book to BookDetail ane book can have one book detail and detail can be linked to one book only Book as parent
            // Bookdetail can have one book and book can have(with)  one bookdetial and FK(Book_Id) is in BookDetail 
            modelBuilder.HasOne(b => b.Book).WithOne(b => b.BookDetail).
                HasForeignKey<Fluent_BookDetail>(u => u.Book_Id);

        }
    }
}

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
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            // fluent Book
            modelBuilder.ToTable("Fluent_Books");
            modelBuilder.HasKey(u => u.BookId);
            modelBuilder.Property(u => u.ISBM).HasMaxLength(20).IsRequired();
            modelBuilder.Ignore(u => u.PriceRange);
            // One to Many Mapping Book to Publisher one publisher can have many books Publisher as parent
            // book can have one publisher and publiher can have many books 
            // for on to many we canot define wjhat class will have Fk beacuse only one class can have Fk
            // Publisher cannot have FK 
            modelBuilder.HasOne(b => b.Publisher).WithMany(b => b.Books).
                HasForeignKey(u => u.Publisher_Id);

        }
    }
}

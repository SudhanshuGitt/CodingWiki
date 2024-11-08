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
    public class FluentPublisherConfig : IEntityTypeConfiguration<Fluent_Publisher>
    {
        public void Configure(EntityTypeBuilder<Fluent_Publisher> modelBuilder)
        {
            //fluent Publisher
            modelBuilder.ToTable("Fluent_Publishers");
            modelBuilder.HasKey(u => u.Publisher_Id);
            modelBuilder.Property(u => u.Publisher_Name).HasColumnName("Name").IsRequired();

        }
    }
}

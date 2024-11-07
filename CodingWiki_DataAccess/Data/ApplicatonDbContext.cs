using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data
{
    // we need a class that will intract with evrything we need to do
    // data context class is uded to interact with all the tables

    // in order to interat with EF core it need to extend the DBcontext class
    // db context provide all logic needed for ef core
    // db context provide all logic needed for ef core
    public class ApplicatonDbContext : DbContext
    {
        //Db set is responsile rather be the classes of the the table that we want in our applicaton
        // Name of dbset is the name of the table in the database
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI;Database=CodingWikiEF;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // we need to work on price property in the Book Entity
            modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(10, 5);

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Sipderman Without Duty", ISBM = "123B12", Price = 10.99m },
                new Book { BookId = 2, Title = "Fortune of time", ISBM = "12123B12", Price = 11.99m }
            );

            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "Fake Sunday", ISBM = "77652", Price = 20.99m },
                new Book { BookId = 4, Title = "Cookie Jar", ISBM = "CC12B12", Price = 25.99m },
                new Book { BookId = 5, Title = "Cloudy Forest", ISBM = "90392B33", Price = 40.99m },
            };

            modelBuilder.Entity<Book>().HasData(bookList);
        }


        // update-databse will genrate sql according to the Migration and update Db
        // migration id  is genrated so EF core will check in db Migration table it will only apply that migragtion
        // which is not listed in the tables

        // In Snapshot it keep track on what has been created in Db 
        // for eg if we add new column it will check from snapshot that it need to add column

        // only remove-migration if migration is not applied on Db

        // to revert back to specific migrartion update-datbase (MigrationnamewithoutId)  update-datbase AddBookToDb 

        // get-migration will bring back all the migraiton and show if it is applied or not

        // drop-database to drop db

    }
}

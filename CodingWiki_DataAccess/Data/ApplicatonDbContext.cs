using CodingWiki_Model.Models;
using CodingWiki_Model.Models.FluentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        // we dont need to create Dbset for BookAUthorMap EF Core autmaically know
        // it is a mapping table because of Navigation Property
        // but to retrive record we can create Db Set
        public DbSet<BookAuthorMap> BookAuthorMaps { get; set; }

        // rename to fluent_BookDetails
        public DbSet<Fluent_BookDetail> BookDetails_fluent { get; set; }
        public DbSet<Fluent_Book> Books_fluent { get; set; }
        public DbSet<Fluent_Author> Authors_fluent { get; set; }
        public DbSet<Fluent_Publisher> Publisher_fluent { get; set; }
        public DbSet<Fluent_BookAuthorMap> Fluent_BookAuthorMaps { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI;Database=CodingWikiEF;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent BookDetails
            // change table name and column name using fluent API
            modelBuilder.Entity<Fluent_BookDetail>().ToTable("Fluent_BookDetials");
            modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters");
            //adding required and PK to rproperty
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(u => u.BookDetail_Id);
            modelBuilder.Entity<Fluent_BookDetail>().Property(u => u.NumberOfChapters).IsRequired();
            // One to One Mapping Book to BookDetail ane book can have one book detail and detail can be linked to one book only Book as parent
            // Bookdetail can have one book and book can have(with)  one bookdetial and FK(Book_Id) is in BookDetail 
            modelBuilder.Entity<Fluent_BookDetail>().HasOne(b => b.Book).WithOne(b => b.BookDetail).
                HasForeignKey<Fluent_BookDetail>(u => u.Book_Id);

            // fluent Book
            modelBuilder.Entity<Fluent_Book>().ToTable("Fluent_Books");
            modelBuilder.Entity<Fluent_Book>().HasKey(u => u.BookId);
            modelBuilder.Entity<Fluent_Book>().Property(u => u.ISBM).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Fluent_Book>().Ignore(u => u.PriceRange);
            // One to Many Mapping Book to Publisher one publisher can have many books Publisher as parent
            // book can have one publisher and publiher can have many books 
            // for on to many we canot define wjhat class will have Fk beacuse only one class can have Fk
            // Publisher cannot have FK 
            modelBuilder.Entity<Fluent_Book>().HasOne(b => b.Publisher).WithMany(b => b.Books).
                HasForeignKey(u => u.Publisher_Id);

            //fluent Author
            modelBuilder.Entity<Fluent_Author>().ToTable("Fluent_Authors");
            modelBuilder.Entity<Fluent_Author>().HasKey(u => u.Author_Id);
            modelBuilder.Entity<Fluent_Author>().Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Ignore(u => u.FullName);

            //fluent Publisher
            modelBuilder.Entity<Fluent_Publisher>().ToTable("Fluent_Publishers");
            modelBuilder.Entity<Fluent_Publisher>().HasKey(u => u.Publisher_Id);
            modelBuilder.Entity<Fluent_Publisher>().Property(u => u.Publisher_Name).HasColumnName("Name").IsRequired();

            //FLuent Book Author
            modelBuilder.Entity<Fluent_BookAuthorMap>().HasKey(b => new { b.Author_Id, b.Book_Id });
            //Many to Many One Book can have many Authors and viceversa
            modelBuilder.Entity<Fluent_BookAuthorMap>().HasOne(b => b.Book).WithMany(b => b.BookAuthorMap)
                .HasForeignKey(u => u.Book_Id);
            modelBuilder.Entity<Fluent_BookAuthorMap>().HasOne(b => b.Author).WithMany(b => b.BookAuthorMap)
                .HasForeignKey(u => u.Author_Id);





            // we need to work on price property in the Book Entity
            modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(10, 5);

            // how to set a composite key using fluentAPI
            // code first will give prefrence to fluent API then to Data Annoatation then default convention
            modelBuilder.Entity<BookAuthorMap>().HasKey(b => new { b.Author_Id, b.Book_Id });

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Sipderman Without Duty", ISBM = "123B12", Price = 10.99m, Publisher_Id = 1 },
                new Book { BookId = 2, Title = "Fortune of time", ISBM = "12123B12", Price = 11.99m, Publisher_Id = 1 }
            );

            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "Fake Sunday", ISBM = "77652", Price = 20.99m , Publisher_Id = 2 },
                new Book { BookId = 4, Title = "Cookie Jar", ISBM = "CC12B12", Price = 25.99m , Publisher_Id = 3 },
                new Book { BookId = 5, Title = "Cloudy Forest", ISBM = "90392B33", Price = 40.99m, Publisher_Id = 3  },
            };

            modelBuilder.Entity<Publisher>().HasData(

                new Publisher { Publisher_Id = 1, Publisher_Name = "Pub 1 Jimmy", Location = "Chicago" },
                new Publisher { Publisher_Id = 2, Publisher_Name = "Pub 2 John", Location = "New York" },
                new Publisher { Publisher_Id = 3, Publisher_Name = "Pub 3 Ben", Location = "Hawaii" }
            );

            modelBuilder.Entity<Book>().HasData(bookList);
        }


        // update-databse will genrate sql according to the Migration and update Db
        // migration id  is genrated so EF core will check in db Migration table it will only apply that migragtion
        // which is not listed in the tables

        // In Snapshot it keep track on what has been created in Db 
        // for eg if we add new column it will check from snapshot that it need to add column

        // only remove-migration if migration is not applied on Db

        // to revert back to specific migrartion update-database (MigrationnamewithoutId)  update-database AddBookToDb 

        // get-migration will bring back all the migraiton and show if it is applied or not

        // drop-database to drop db

        //onDelete: ReferentialAction.Cascade)
        //.cascade If parent record which is book is deleted then book detail is also delted
        // .NoAction If parent record which is book is deleted we donot perform any action on book detail
        // .SetNull it will set Book Detail record as null
        // .Restrict if there is book detial it will restrict it will not perform delete

    }
}

//// See https://aka.ms/new-console-template for more information
//using CodingWiki_DataAccess.Data;
//using CodingWiki_Model.Models;
//using Microsoft.EntityFrameworkCore;


Console.WriteLine("Hello, World!");

//using (ApplicatonDbContext context = new())
//{
//    //context.Database.EnsureCreated();
//    //if (context.Database.GetPendingMigrations().Any())
//    //{
//    //    context.Database.Migrate();
//    //}

//}

////AddBook();
////GetAllBooks();
////GetBook();
////UpdateBook();
////UpdateMulBook();
//DeleteBook();

//async void GetAllBooks()
//{
//    using var context = new ApplicatonDbContext();
//    // get bomks
//    // when we have .ToList() it will execute the query there(Defferd Executin)
//    var books = await context.Books.ToListAsync();

//    foreach (var book in books)
//    {
//        Console.WriteLine(book.Title + " - " + book.ISBM);
//    }
//}

//async void DeleteBook()
//{
//    try
//    {
//        using var context = new ApplicatonDbContext();
//        var book = await context.Books.FindAsync(8);
//        context.Books.Remove(book);
//        await context.SaveChangesAsync();

//    }
//    catch (Exception ex)
//    {
//    }

//}

//async void UpdateBook()
//{
//    try
//    {
//        using var context = new ApplicatonDbContext();
//        var book = await context.Books.FindAsync(6);
//        // when we recive any record form EF core it awlays keep track of that
//        // we can directly update and call method save changes
//        book.ISBM = "7777";
//        await context.SaveChangesAsync();

//    }
//    catch (Exception ex)
//    {
//    }
//}

//void UpdateMulBook()
//{
//    try
//    {
//        using var context = new ApplicatonDbContext();
//        var books = context.Books.Where(b => b.Publisher_Id == 1);
//        // when we recive any record form EF core it awlays keep track of that
//        // we can directly update and call method save changes
//        foreach (var book in books)
//        {
//            book.Price = 55.55m;
//        }
//        context.SaveChanges();

//    }
//    catch (Exception ex)
//    {
//    }
//}

//void GetBook()
//{
//    try
//    {
//        using var context = new ApplicatonDbContext();
//        // get bomks
//        // first will always expect a record to return 
//        // if it doesnt return it will give an exception
//        //var book = context.Books.First();
//        // if not found FirstOrDefault will return null not exception
//        string title = "Cookie Jar";
//        //var book = context.Books.FirstOrDefault(b => b.BookId == 1);
//        // here the query gets executed right away no no Deffered execution
//        var book = context.Books.FirstOrDefault(b => b.Title == title);
//        Console.WriteLine(book.Title + " - " + book.ISBM);

//        // find can be used if we want to filter by PK
//        var bookbyPK = context.Books.Find(3);

//        // singel should always reutrn only one record
//        // singl will always expect one recrord but it will fetch 2 records 
//        // if there are two records then it will through exception
//        // if no records are found single will through an exceotion 
//        var bookSingle = context.Books.Single(b => b.Publisher_Id == 2);
//        Console.WriteLine(bookSingle.Title + " - " + bookSingle.ISBM);

//        // if no records are found singleorDefault will return null 
//        var bookSingleOrDefault = context.Books.SingleOrDefault(b => b.Publisher_Id == 100);
//        //Console.WriteLine(bookSingleOrDefault.Title + " - " + bookSingleOrDefault.ISBM);

//        // where will retuen IQueryable<book>
//        // in sql injection passing something hardcoded is not safe we must pass that in parameter
//        var book1 = context.Books.Where(b => b.Title == title);
//        //Query will be executed in DB when we iterate in boo1 
//        foreach (var book3 in book1)
//        {
//            Console.WriteLine($"{book3.Title}");
//        }

//        // WHERE [b].[ISBM] LIKE N'%12%'
//        var bookContains = context.Books.Where(b => b.ISBM.Contains("12"));
//        foreach (var book3 in bookContains)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//        // where has many agggregate like min max and count
//        //var bookContainsStart = context.Books.Where(b => EF.Functions.Like(b.ISBM, "12%")).Min(b => b.Publisher_Id);
//        var bookContainsStart = context.Books.Where(b => EF.Functions.Like(b.ISBM, "12%"));
//        foreach (var book3 in bookContainsStart)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//        //Sorting
//        // by default Asending order
//        var bookOrderAse = context.Books.OrderBy(b => b.Title);
//        foreach (var book3 in bookOrderAse)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//        // here it will ignore first conditionTitle and only order by secondISBM
//        var bookOrderMulAse = context.Books.OrderBy(b => b.Title).OrderByDescending(b => b.ISBM);
//        foreach (var book3 in bookOrderMulAse)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//        var bookOrderMulAse1 = context.Books.Where(b => b.Price > 10).
//            OrderBy(b => b.Title).ThenByDescending(b => b.ISBM);
//        foreach (var book3 in bookOrderMulAse1)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//        // pagination
//        // skip no records and take next two record
//        var bookPagination = context.Books.Skip(0).Take(2);
//        foreach (var book3 in bookPagination)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//        var bookPagination1 = context.Books.Skip(4).Take(1);
//        foreach (var book3 in bookPagination1)
//        {
//            Console.WriteLine($"{book3.Title}-{book3.ISBM}");
//        }

//    }
//    catch (Exception ex)
//    {
//    }
//}

//async void AddBook()
//{
//    using var context = new ApplicatonDbContext();
//    Book bookToAdd = new()
//    {
//        Title = "New EF Core Book",
//        ISBM = "99999",
//        Price = 10.93m,
//        Publisher_Id = 1
//    };

//    // at this point it will track i have to add book in db
//    var Book = await context.Books.FindAsync(bookToAdd);

//    //here it will go to db and create records
//    await context.SaveChangesAsync();

//}


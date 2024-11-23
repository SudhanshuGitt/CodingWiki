using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicatonDbContext _context;

        // .net core is respnsibel for creating the object and even disposing them when done
        public BookController(ApplicatonDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BookIndex()
        {
            // Include directly work on entity
            // if we wan to work on the child entities then we can use then include
            // then include will be working on BookAuthorMap
            List<Book> books = _context.Books.Include(b => b.Publisher).Include(b => b.BookAuthorMap).ThenInclude(b => b.Author).ToList();
            //List<Book> books = await _context.Books.ToListAsync();
            //foreach (var book in books)
            //{
            //    // for each iteration it will got to db and fetch the data so its not efficient way
            //    //book.Publisher = await _context.Publishers.FindAsync(book.Publisher_Id);
            //    // explict loading
            //    // it is going to db just for distnct publisher
            //    // avoide duplicate calls to db for same publisher
            //    // N+1 execution as there are 3 diffrent publihser and one to retrive all the book

            //    // Multilevel explicit loading
            //    await _context.Entry(book).Reference(u => u.Publisher).LoadAsync();
            //    await _context.Entry(book).Collection(u => u.BookAuthorMap).LoadAsync();
            //    foreach (var bookAuth in book.BookAuthorMap)
            //    {
            //        // Book does not have author so we need to load Auhtor from BookAuthorMap
            //        await _context.Entry(bookAuth).Reference(u => u.Author).LoadAsync();
            //    }

            //}
            // Eager loading is the process wherw query for one type eb book load the related entites(publisher)
            return View(books);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            BookVM bookVM = new();

            // projection is a way of converting an entity into C# object with subset of those properties
            // projection increases the efficiency of the project because we select only fields that are needed
            bookVM.PublisherList = _context.Publishers.Select(p => new SelectListItem
            {
                Text = p.Publisher_Name,
                Value = p.Publisher_Id.ToString()
            });
            if (id != null && id != 0)
            {
                bookVM.Book = await _context.Books.FindAsync(id);
                if (bookVM.Book == null)
                {
                    return NotFound();
                }
                return View(bookVM);
            }
            return View(bookVM);

        }

        [HttpPost(Name = "Upsert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BookVM bookVM)
        {
            bookVM.PublisherList = _context.Publishers.Select(p => new SelectListItem
            {
                Text = p.Publisher_Name,
                Value = p.Publisher_Id.ToString()
            });

            if (ModelState.IsValid)
            {
                if (bookVM.Book.BookId == 0)
                {
                    await _context.Books.AddAsync(bookVM.Book);
                }
                else
                {

                    _context.Books.Update(bookVM.Book);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookIndex));
            }

            return View(bookVM);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null && id != 0)
            {
                NotFound();
            }
            BookDetail bookDetail = new();
            bookDetail = await _context.BookDetails.Include(b => b.Book).FirstOrDefaultAsync(b => b.BookDetail_Id == id);

            if (bookDetail == null)
            {
                bookDetail = new()
                {
                    Book_Id = (int)id,
                    Book = await _context.Books.FindAsync(id)
                };
            }

            return View(bookDetail);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(BookDetail bookDetail)
        {
            //ModelState.Remove("Book.ISBM");
            if (ModelState.IsValid)
            {
                if (bookDetail.BookDetail_Id == 0)
                {
                    await _context.BookDetails.AddAsync(bookDetail);
                }
                else
                {

                    _context.BookDetails.Update(bookDetail);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookIndex));
            }

            return View(bookDetail);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Book book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BookIndex));
        }

        public async Task<IActionResult> ManageAuthors(int id)
        {
            BookAuthorVM bookAuthorVM = new()
            {
                BookAuthorList = await _context.BookAuthorMaps.Include(u => u.Author).Include(u => u.Book).Where(b => b.Book_Id == id).ToListAsync(),
                BookAuthor = new()
                {
                    Book_Id = id,
                },
                Book = await _context.Books.FindAsync(id)
            };

            // we need the authors asssigne to the selected book
            List<int> tempListOfAssignedAuthor = bookAuthorVM.BookAuthorList.Select(u => u.Author_Id).ToList();

            // we need all the authors whos id not in temListOfAssignedAuthor
            var tempList = await _context.Authors.Where(u => !tempListOfAssignedAuthor.Contains(u.Author_Id)).ToListAsync();
            bookAuthorVM.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });
            return View(bookAuthorVM);
        }

        [HttpPost]
        public async Task<IActionResult> ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                await _context.BookAuthorMaps.AddAsync(bookAuthorVM.BookAuthor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            BookAuthorMap bookAuthorMap = await _context.BookAuthorMaps.
                FirstOrDefaultAsync(m => m.Author_Id == authorId && m.Book_Id == bookAuthorVM.Book.BookId);

            _context.BookAuthorMaps.Remove(bookAuthorMap);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.Book.BookId });
        }

        public async Task<IActionResult> Playground(int? id)
        {


            // view and sproc
            var viewList = await _context.MainBookDetails.ToListAsync();
            var viewList1 = await _context.MainBookDetails.FirstOrDefaultAsync();
            var viewList2 = _context.MainBookDetails.Where(m => m.Price > 30);

            // raw sql       
            var bookRaw = _context.Books.FromSqlRaw("SELECT * FROM dbo.Books");
            var id1 = 1;
            var bookInterpolated = _context.Books.FromSqlInterpolated($"SELECT * FROM dbo.Books where bookid={id1}");

            // sproc
            var bookproc = _context.Books.FromSqlInterpolated($"EXEC dbo.getAllBookDetailById {id1}").ToList();

            // I queryable inhert from i Enumwrbale


            // Iqueryable is preffered over ienumerbale 
            // as query that we write is filter out the data and reuturn only what we needed

            IEnumerable<Book> booksList1 = _context.Books;
            // I Enumerable fileter in the application
            var filterBook = booksList1.Where(b => b.Price > 50).ToList();
            //I Query able filters the data at db side
            // it will add the where conditon
            // it willl save our server memoryx
            IQueryable<Book> booksList2 = _context.Books;
            var filterBook1 = booksList2.Where(b => b.Price > 50).ToList();

            var bookTemp = _context.Books.FirstOrDefault();
            bookTemp.Price = 100;

            var bookCollection = _context.Books;
            decimal totalPrice = 0;

            foreach (var book in bookCollection)
            {
                totalPrice += book.Price;
            }

            var bookList = _context.Books.ToList();
            foreach (var book in bookList)
            {
                totalPrice += book.Price;
            }

            var bookCollection2 = _context.Books;
            var bookCount1 = bookCollection2.Count();

            var bookCount2 = _context.Books.Count();
            return RedirectToAction(nameof(BookIndex));


        }

    }
}

using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicatonDbContext _context;

        public AuthorController(ApplicatonDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AuthorIndex()
        {
            List<Author> authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Author author = new();
            if (id != null && id != 0)
            {
                author = await _context.Authors.FirstOrDefaultAsync(p => p.Author_Id == id);
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);
            }
            return View(author);

        }

        [HttpPost(Name = "Upsert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.Author_Id == 0)
                {
                    await _context.Authors.AddAsync(author);
                }
                else
                {

                    _context.Authors.Update(author);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AuthorIndex));
            }

            return View(author);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Author author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AuthorIndex));
        }
    }
}

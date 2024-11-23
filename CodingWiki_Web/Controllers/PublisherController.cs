using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicatonDbContext _context;

        public PublisherController(ApplicatonDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> PublisherIndex()
        {
            List<Publisher> publishers = await _context.Publishers.ToListAsync();
            return View(publishers);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Publisher publisher = new();
            if (id != null && id != 0)
            {
                publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Publisher_Id == id);
                if (publisher == null)
                {
                    return NotFound();
                }
                return View(publisher);
            }
            return View(publisher);

        }

        [HttpPost(Name = "Upsert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.Publisher_Id == 0)
                {
                    await _context.Publishers.AddAsync(publisher);
                }
                else
                {

                    _context.Publishers.Update(publisher);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PublisherIndex));
            }

            return View(publisher);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Publisher publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            _context.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PublisherIndex));
        }
    }
}

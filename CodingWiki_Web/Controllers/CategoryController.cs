using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicatonDbContext _context;

        // .net core is respnsibel for creating the object and even disposing them when done
        public CategoryController(ApplicatonDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Ef core will start tracking on moment recieve the entities
            // any changes to entity will be appled to db when we save the chages

            // no tracking queries are useful when we use data for read only purposes
            List<Category> categories = await _context.Categories.AsNoTracking().ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Category category = new();
            if (id != null && id != 0)
            {
                category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            return View(category);

        }

        [HttpPost(Name = "Upsert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Category_Id == 0)
                {
                    await _context.Categories.AddAsync(category);
                }
                else
                {

                    _context.Categories.Update(category);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Remove(category);
            // tracker will track if there is any modified Added or Deleted state is there
            // if there is on savechanges it will aplly the state and updated it to unchanged
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiple(int count)
        {
            List<Category> categories = new();

            for (int i = 0; i < count; i++)
            {
                categories.Add(new Category { CategoryName = Guid.NewGuid().ToString() });
            }
            // it will combine then in a single insert but befot .net5 it was performing indivigial insert hitting db multiple times
            await _context.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveMultiple(int count)
        {
            List<Category> categories = await _context.Categories.OrderByDescending(c => c.Category_Id).Take(count).ToListAsync();

            _context.RemoveRange(categories);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}


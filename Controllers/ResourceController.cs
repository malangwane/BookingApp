using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models; 

namespace BookingApp.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ResourceBookingDbContext _context;

        public ResourceController(ResourceBookingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Resources.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null) return NotFound();

            return View(resource);
        }

        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resource resource)
        {
         
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(resource);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound();

            return View(resource);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Resource resource)
        {
            if (id != resource.Id) return NotFound();

           
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Resources.Any(e => e.Id == resource.Id))
                        return NotFound();
                    else
                        throw;
                }
            
            return View(resource);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null) return NotFound();

            return View(resource);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

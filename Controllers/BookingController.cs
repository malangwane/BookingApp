using BookingApp.Models; // Adjust namespace as needed
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ResourceBookingDbContext _context;

        public BookingController(ResourceBookingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? resourceId)
        {
            var bookingsQuery = _context.Bookings.Include(b => b.Resource).AsQueryable();

            if (resourceId.HasValue)
            {
                bookingsQuery = bookingsQuery.Where(b => b.ResourceId == resourceId.Value);
            }

            var bookings = await bookingsQuery.OrderBy(b => b.StartTime).ToListAsync();

            ViewData["ResourceName"] = resourceId.HasValue
                ? (await _context.Resources.FindAsync(resourceId.Value))?.Name ?? "Unknown Resource"
                : "All Resources";

            ViewData["BookingCount"] = bookings.Count;

            return View(bookings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }


        public IActionResult Create()
        {
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResourceId,StartTime,EndTime,BookedBy,Purpose")] Booking booking)
        {
            if (booking.StartTime >= booking.EndTime)
            {
                ModelState.AddModelError("", "End time must be after start time.");
            }

            bool isConflict = await HasBookingConflictAsync(booking);

            if (isConflict)
            {
                ModelState.AddModelError("", "This resource is already booked during the requested time. Please choose another slot or resource.");
            }

            
                try
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the booking: " + ex.Message);
                }

            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResourceId,StartTime,EndTime,BookedBy,Purpose")] Booking booking)
        {
            if (id != booking.Id)
                return NotFound();


            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("", "End time must be after start time.");
            }

           bool isConflict = await HasBookingConflictAsync(booking);

            if (isConflict)
            {
                ModelState.AddModelError("", "This resource is already booked during the selected time range.");
            }

            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Booking updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.Id))
                    return NotFound();
                else
                    throw;
            }

            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        private async Task<bool> HasBookingConflictAsync(Booking booking)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.Id != booking.Id &&
                b.ResourceId == booking.ResourceId &&
                (
                    (booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                    (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime) ||
                    (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime)
                )
            );
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}

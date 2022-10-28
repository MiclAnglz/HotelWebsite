using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_MajorProject.Data;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Bookings
{
    public class DeleteModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public DeleteModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      [BindProperty(SupportsGet = true)]
      public Booking Booking { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? bid)
        {
            if (bid == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.Include(b => b.theGuest)
                    .Include(b => b.theRoom).FirstOrDefaultAsync(m => m.ID == bid);

            if (booking == null)
            {
                return NotFound();
            }
            else 
            {
                Booking = booking;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? bid)
        {
            if (bid == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(bid);

    
            if (booking != null)
            {
                Booking = booking;
                _context.Booking.Remove(Booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ManageBookings");
        }
    }
}

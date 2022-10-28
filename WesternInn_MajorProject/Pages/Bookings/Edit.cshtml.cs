using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WesternInn_MajorProject.Data;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Guests.Bookings
{
    public class EditModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public EditModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int bid)
        {
            ViewData["GuestEmail"] = new SelectList(_context.Set<Guest>(), "Email", "FullName");
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "ID");
       
            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.ID == bid);

            Booking = booking;

            return Page();
        }

     
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "ID");
            ViewData["GuestEmail"] = new SelectList(_context.Set<Guest>(), "Email", "FullName");

            if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                return Page();
            }

            ViewData["test1"] = "im here1";

            var roomID = new SqliteParameter("roomIDInput", Booking.RoomID);
            var checkin = new SqliteParameter("newCheckIn", Booking.CheckIn);
            var checkout = new SqliteParameter("newCheckOut", Booking.CheckOut);
            var email = new SqliteParameter("newEmail", Booking.GuestEmail);

            var query = await _context.Room.FromSqlRaw("select * from Room where Room.ID = @roomIDInput and Room.ID in " +
                "(select RoomID from Booking WHERE GuestEmail = @newEmail and NOT (CheckOut <= @newCheckIn or @newCheckOut <= CheckIn))"
                , roomID, email, checkin, checkout).ToListAsync();

            int count = query.Count;

            if (query.Count == 0)
            {
             
                _context.Attach(Booking).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(Booking.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./ManageBookings");
            }
            else
            {
                ViewData["test1"] = "This booking is unavailable";
                return Page();
            }
        }

        private bool BookingExists(int id)
        {
          return _context.Booking.Any(e => e.ID == id);
        }
    }
}

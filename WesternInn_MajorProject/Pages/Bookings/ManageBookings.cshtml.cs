using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Bookings
{
    [Authorize(Roles = "Administrator")]
    public class ManageBookingsModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public ManageBookingsModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public WesterInnStats SearchString { get; set; }

        public IList<Guest> Guests { get; set; } = default!;

       
        public async Task<IActionResult> OnGetAsync(string? sortOrder)
        {
           

            if (!String.IsNullOrEmpty(SearchString.GivenName) || !String.IsNullOrEmpty(SearchString.Surname))
            {
                string s1 = SearchString.GivenName;
                string s2 = SearchString.Surname;

                var searchString = new SqliteParameter("search", s1 + "%");
                var searchString1 = new SqliteParameter("search1", s2 + "%");

                var bookings = await _context.Booking.FromSqlRaw("SELECT Booking.* from Booking inner join Guest " +
                     "on Booking.GuestEmail = Guest.Email where " +
                     "Guest.GivenName like @search and Guest.Surname like @search1", 
                     searchString, searchString1).Include(b => b.theGuest).Include(b => b.theRoom).Include(b => b.theRoom).ToListAsync();


                if (bookings.Count != 0)
                {
                    Booking = bookings;
                    return Page();
                }
                else
                {
                    Booking = bookings;
                    return Page();

                }
            }
            
            if (SearchString.BookingId > 0 )
            {
                int s1 = SearchString.BookingId;
               
                var searchString = new SqliteParameter("search2", s1);
               
                var bookings = await _context.Booking.FromSqlRaw("select * from Booking where ID = @search2",
                     searchString).Include(b => b.theGuest).Include(b => b.theRoom).Include(b => b.theRoom).ToListAsync();


                if (bookings.Count != 0)
                {
                    Booking = bookings;
                    return Page();
                }
                else
                {
                    Booking = bookings;
                    return Page();
                }
            }
            else
            {

                if (String.IsNullOrEmpty(sortOrder))
                {
                    sortOrder = "Burger_asc";
                }

                var booking = (IQueryable<Booking>)_context.Booking;

                switch (sortOrder)
                {

                    case "CheckIn_asc":
                        booking = booking.OrderBy(p => p.CheckIn);
                        break;
                    case "CheckIn_desc":
                        booking = booking.OrderByDescending(p => p.CheckIn);
                        break;
                    case "Cost_asc":
                        booking = booking.OrderBy(p => (int)p.Cost);
                        break;
                    case "Cost_desc":
                        booking = booking.OrderByDescending(p => (int)p.Cost);
                        break;
                }

                ViewData["NextCheckInBooking"] = sortOrder != "CheckIn_asc" ? "CheckIn_asc" : "CheckIn_desc";
                ViewData["NextCostBooking"] = sortOrder != "Cost_asc" ? "Cost_asc" : "Cost_desc";

                if (booking.Count() != 0)
                {
                    Booking = await booking.Include(b => b.theGuest)
                        .Include(b => b.theRoom).ToListAsync(); ;
                }
                else
                {
                    Booking = await booking.Include(b => b.theGuest)
                        .Include(b => b.theRoom).ToListAsync();
                    return Page();
                }
            }

            
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Bookings
{
    public class CreatorModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public CreatorModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
     
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "ID");
            ViewData["GuestEmail"] = new SelectList(_context.Set<Guest>(), "Email", "FullName");

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public Booking Bookings { get; set; }

        public Room room { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            var checkinDate = Convert.ToDateTime(Bookings.CheckIn);
            var checkoutDate = Convert.ToDateTime(Bookings.CheckOut);
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "ID");
            ViewData["GuestEmail"] = new SelectList(_context.Set<Guest>(), "Email", "FullName");
            var list = new List<lastID>();

            TimeSpan ts = checkoutDate - checkinDate;
            int days = ts.Days;

            if (!ModelState.IsValid || _context.Booking == null || Bookings == null)
            {
                return Page();
            }

            if (days <= 0)
            {
                ViewData["invalid"] = "True";
                return Page();
            }
            else
            {
                
                var roomID = new SqliteParameter("roomIDInput", Bookings.RoomID);
                var checkin = new SqliteParameter("newCheckIn", Bookings.CheckIn);
                var checkout = new SqliteParameter("newCheckOut", Bookings.CheckOut);

                var query = await _context.Room.FromSqlRaw("select * from Room where Room.ID = @roomIDInput and Room.ID in " +
                    "(select RoomID from Booking WHERE NOT (CheckOut <= @newCheckIn or @newCheckOut <= CheckIn))"
                    , roomID, checkin, checkout).ToListAsync();

                var count = query.Count;

                if (query.Count == 0)
                {
                    var emptyOrder = new Booking();

                    var success = await TryUpdateModelAsync<Booking>(emptyOrder, "Bookings",
                                        s => s.RoomID, s => s.GuestEmail, s => s.CheckIn, s => s.CheckOut);

                    var room = await _context.Room.FindAsync(Bookings.RoomID);
                    emptyOrder.Cost = room.Price;

                    ViewData["test1"] = "Your booking was successful";

                    _context.Booking.Add(emptyOrder);
                    await _context.SaveChangesAsync();

                    list = _context.lastID.FromSqlRaw("select seq from sqlite_sequence where name= 'Booking'").ToList();
                    ViewData["id"] = list[0].seq;

                    return Page();
                }
                else
                {
                    ViewData["test1"] = "This booking is unavailable";
                    return Page();

                }
            }

        }
    }
}

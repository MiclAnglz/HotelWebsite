using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WesternInn_MajorProject.Data;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Rooms
{
    [Authorize(Roles = "Guest")]
    public class IndexModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet= true)]
        public SearchRoom searchroom { get; set; }

             
        public IActionResult OnGet()
        {
            var list = new List<int> { 1, 2, 3 };
            ViewData["maxRooms"] = new SelectList(list);

            return Page();
        }
        public IList<Room> Room { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
            var list = new List<int> { 1, 2, 3 };
            ViewData["maxRooms"] = new SelectList(list);


            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bedcount = new SqliteParameter("bedCountInput", searchroom.BedCount);
            var checkin = new SqliteParameter("newCheckIn", searchroom.CheckIn);
            var checkout = new SqliteParameter("newCheckOut", searchroom.CheckOut);


            var query = _context.Room.FromSqlRaw("select * from [Room] where [Room].BedCount = @bedCountInput and " +
                "[Room].ID not in (select RoomID from Booking where not (CheckOut <= @newCheckIn or @newCheckOut <= CheckIn))"
                , bedcount, checkin, checkout);

            Room = await query.ToListAsync();

            ViewData["checkindate"] = searchroom.CheckIn;
            ViewData["checkoutdate"] = searchroom.CheckOut;
            ViewData["bedCount"] = searchroom.BedCount;

            return Page();
        }
    }
}

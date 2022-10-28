using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Bookings
{
    public class BookingsStatisticsModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public BookingsStatisticsModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public IList<ViewStats> stats { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<WesterInnStats> bookingStats { get; set; }


        [BindProperty(SupportsGet = true)]
        public WesterInnStats Stats { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        List<int> last5Years = new List<int>();

        public Data.ApplicationDbContext Get_context()
        {
            return _context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i < currentYear + 5; i++)
            {
                last5Years.Add(i);
            }

            ViewData["years"] = new SelectList(last5Years);

            var roomIDGroup = _context.Booking.GroupBy(m => m.RoomID);

            bookingStats = await roomIDGroup
                             .Select(g => new WesterInnStats { RoomId = g.Key, NumberOfBookings = g.Count() })
                             .ToListAsync();

            var searchString1 = new SqliteParameter("search1", currentYear.ToString());

            var monthYear = await _context.ViewStats.FromSqlRaw("select * from my where Year = @search1", searchString1).ToListAsync();

            stats = monthYear;

            ViewData["year"] = currentYear.ToString();

            if (!String.IsNullOrEmpty(SearchString))
            {
                ViewData["year"] = SearchString;

                string s2 = SearchString;

                var searchString = new SqliteParameter("search", s2);

                monthYear = await _context.ViewStats.FromSqlRaw("select * from my where Year = @search", searchString).ToListAsync();
            }
            else
            {
                SearchString = currentYear.ToString();
            }
            
           
            if (monthYear != null)
            {
                stats = monthYear;
            }
            return Page();
        }
    }
}

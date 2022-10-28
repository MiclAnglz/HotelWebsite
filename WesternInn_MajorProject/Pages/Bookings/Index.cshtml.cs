using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_MajorProject.Data;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Guests.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync(string? sortOrder)
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

            string _email = User.FindFirst(ClaimTypes.Name).Value;

            booking = booking.Where(s => s.GuestEmail.Contains(_email));
            
            if (_context.Booking != null)
            {
                Booking = await booking
                .Include(b => b.theGuest)
                .Include(b => b.theRoom).ToListAsync();
            }
        }
    }
}

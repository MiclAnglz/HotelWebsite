using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WesternInn_MajorProject.Models;

namespace WesternInn_MajorProject.Pages.Guests
{
    public class MyDetailsModel : PageModel
    {
        private readonly WesternInn_MajorProject.Data.ApplicationDbContext _context;

        public MyDetailsModel(WesternInn_MajorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guest? Myself { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            string _email = User.FindFirst(ClaimTypes.Name).Value;
    
            Guest guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == _email);
        
            if (guest != null)
            {
                // existing user
                ViewData["ExistInDB"] = "true";
                ViewData["Name"] = guest.GivenName + " " + guest.Surname;
                Myself = guest;
            }
            else
            {
                // new user
                ViewData["ExistInDB"] = "false";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Guest guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == _email);

            if (guest != null)
            {

                ViewData["ExistInDB"] = "true";
            }
            else
            {
                // new user
                ViewData["ExistInDB"] = "false";
                guest = new Guest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            guest.Email = _email;

            var success = await TryUpdateModelAsync<Guest>(guest, "Myself",
                                s => s.Surname, s => s.GivenName, s => s.PostCode);
            if (!success)
            {
                return Page();
            }

            if ((string)ViewData["ExistInDB"] == "true")
            {

                _context.Guest.Update(guest);
            }
            else
            {

                _context.Guest.Add(guest);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            ViewData["SuccessDB"] = "success";
            return Page();
        }
    }
}

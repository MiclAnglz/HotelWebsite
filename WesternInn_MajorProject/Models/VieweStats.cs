using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WesternInn_MajorProject.Models
{
    [Keyless]
    public class ViewStats
    {
        public string Month { get; set; }
        public string Year { get; set; }

        [Display(Name = "Total Bookings")]
        public int Amount_Booked { get; set; }
    }
}

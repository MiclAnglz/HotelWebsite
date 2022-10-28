using System.ComponentModel.DataAnnotations;

namespace WesternInn_MajorProject.Models
{
    public class WesterInnStats
    {
        public int ID { get; set; }

        [Display(Name = "Room ID")]
        public int RoomId { get; set; }

        [Display(Name = "Booking ID")]
        public int BookingId { get; set; }

        [Display(Name = "Total Bookings")]
        public int NumberOfBookings { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z-']$", ErrorMessage = "Family name and Given name should only consist of english letters, hyphens, apostrophes and a min length between 2-20")]
        public string GivenName { get; set; } = string.Empty;

        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z-']$", ErrorMessage = "Family name and Given name should only consist of english letters, hyphens, apostrophes and a min length between 2-20")]
        public string Surname { get; set; } = string.Empty;

    }
}

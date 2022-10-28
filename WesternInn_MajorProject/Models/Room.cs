using System.ComponentModel.DataAnnotations;

namespace WesternInn_MajorProject.Models
{
    public class Room
    {
        [Display(Name = "Room ID")]
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[1-3G]$", ErrorMessage = "You can only enter exactly one character of 'G' or a number 1 - 3")]
        [Display(Name = "Level")]
        public string Level { get; set; } = string.Empty;

        [RegularExpression(@"^[1-3]$", ErrorMessage = "You can only have up to 3 beds per room.")]
        [Display(Name = "Bed Count")]
        public int BedCount { get; set; }

        [Range(50.00, 300.00)]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}

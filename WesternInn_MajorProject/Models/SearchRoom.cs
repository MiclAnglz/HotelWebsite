using System.ComponentModel.DataAnnotations;

namespace WesternInn_MajorProject.Models
{
    public class SearchRoom
    {
        [RegularExpression(@"^[1-3]$", ErrorMessage = "You can only have up to 3 beds per room.")]
        public int BedCount { get; set; }

        [Display(Name = "Check-In Date")]
        [DataType(DataType.Date)]
        public string CheckIn { get; set; } = string.Empty;

        [Display(Name = "Check-Out Date")]
        [DataType(DataType.Date)]
        public string CheckOut { get; set; } = string.Empty;
    }
}

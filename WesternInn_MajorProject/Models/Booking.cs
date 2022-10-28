using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WesternInn_MajorProject.CustomValidation;

namespace WesternInn_MajorProject.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public int RoomID { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Guest Email")]
        [Required]
        public string GuestEmail { get; set; } = string.Empty;

        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        [DateVal]
        public string CheckIn { get; set; } = string.Empty;

        [Display(Name = "Check Out Date")]
        [DataType(DataType.Date)]
        [DateVal]
        public string CheckOut { get; set; } = string.Empty;

        [Range(0.00, 10000.00)]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Cost { get; set; }

        [Display(Name = "Bed Count")]
        public Room? theRoom { get; set; }

        [Display(Name = "Guest Name")]
        public Guest? theGuest { get; set; }

    }

}

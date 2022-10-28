using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WesternInn_MajorProject.Models
{
    public class Guest
    {
        [DataType(DataType.EmailAddress)]
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Guest Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Last Name")]

        [RegularExpression(@"^[a-zA-Z-']{2,20}$", ErrorMessage = "Family name and Given name should only consist of english letters, hyphens, apostrophes and a min length between 2-20")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z-']{2,20}$", ErrorMessage = "Family name and Given name should only consist of english letters, hyphens, apostrophes and a min length between 2-20")]
        public string GivenName { get; set; } = string.Empty;

        [RegularExpression(@"^([1-9]\d\d\d)$|(0[8 - 9]\d\d)$", ErrorMessage = "The post code must be a valid Australian postcode. eg: 0800-0999 and 1000-9999")]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; } = string.Empty;

        [NotMapped] // not mapping this property to database, but exist in memory
        public string FullName => $"{GivenName} {Surname}";


        public ICollection<Booking>? Bookings { get; set; }

    }
}

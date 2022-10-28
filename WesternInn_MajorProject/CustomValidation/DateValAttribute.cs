using WesternInn_MajorProject.Models;
using System.ComponentModel.DataAnnotations;


namespace WesternInn_MajorProject.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateValAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => $"The date selected should be ahead of the current date ({DateTime.Now.ToShortDateString()}).";
       protected override ValidationResult? IsValid(object? value, ValidationContext validationcontext)
        {
            var curDate = DateTime.Now;

            if (String.IsNullOrEmpty((string?)value))
            {
                value = curDate.ToString();
            }

            var input = Convert.ToDateTime(value);

            if(curDate.Date.CompareTo(input) > 0)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}

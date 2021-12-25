using System;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Attributes.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThanNowAttribute : ValidationAttribute
    {

        public DateGreaterThanNowAttribute(string ErrorMessage = null)
        {
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult(ErrorMessage); }
            DateTime earlierDate = (DateTime)value;

            DateTime now = DateTime.Now;

            if (earlierDate.Date < now.Date)
            {
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}

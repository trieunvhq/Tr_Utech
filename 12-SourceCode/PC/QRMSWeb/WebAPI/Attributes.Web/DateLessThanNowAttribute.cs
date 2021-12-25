using System;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Attributes.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateLessThanNowAttribute : ValidationAttribute
    {

        public DateLessThanNowAttribute(string ErrorMessage = null)
        {
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try { 
            if (value != null) { return ValidationResult.Success; }
            DateTime earlierDate = (DateTime)value;

            DateTime now = DateTime.Now;

            if (earlierDate <= now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
            }catch(Exception e)
            {
                return new ValidationResult("Có lỗi kiểm tra dữ liệu");
            }
        }
    }
}

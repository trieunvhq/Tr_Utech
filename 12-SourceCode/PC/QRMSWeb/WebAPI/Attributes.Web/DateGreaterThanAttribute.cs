using System;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Attributes.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private string DateToCompareFieldName { get; set; }

        public DateGreaterThanAttribute(string DateToCompareFieldName, string ErrorMessage = null)
        {
            this.DateToCompareFieldName = DateToCompareFieldName;
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                //if (value==null) { return new ValidationResult("Ngày kết thúc hiệu lực không được để trống"); }
                if (value == null) { return ValidationResult.Success; }
                DateTime earlierDate = (DateTime)value;

                DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareFieldName).GetValue(validationContext.ObjectInstance, null);

                if (earlierDate >= laterDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            } catch(Exception e)
            {
                return new ValidationResult("Có lỗi kiểm tra dữ liệu - date");
            }
        }
    }
}

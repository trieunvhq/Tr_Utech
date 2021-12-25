using System;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Attributes.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NumberGreaterThanAttribute : ValidationAttribute
    {
        private string DateToCompareFieldName { get; set; }
        private string ErrorMessage { get; set; }

        public NumberGreaterThanAttribute(string DateToCompareFieldName, string ErrorMessage = null)
        {
            this.DateToCompareFieldName = DateToCompareFieldName;
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                int target = (int)value;

                int compare = (int)validationContext.ObjectType.GetProperty(DateToCompareFieldName).GetValue(validationContext.ObjectInstance, null);

                if (target > compare)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return ErrorMessage != null ? new ValidationResult(ErrorMessage) : new ValidationResult($"{validationContext.DisplayName} must be greater than {DateToCompareFieldName}");
                }
            }
            catch (Exception)
            {
                return ValidationResult.Success;
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Attributes.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinAttribute : ValidationAttribute
    {
        private double Min { get; set; }
        private string ErrorMessage { get; set; }

        public MinAttribute(double Min = 0, string ErrorMessage = null)
        {
            this.Min = Min;
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                double target = Convert.ToDouble(value);

                if (target >= Min)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return ErrorMessage != null ? new ValidationResult(ErrorMessage) : new ValidationResult($"{validationContext.DisplayName} must be greater than {Min}");
                }
            }
            catch (Exception)
            {
                return ValidationResult.Success;
            }
        }
    }
}

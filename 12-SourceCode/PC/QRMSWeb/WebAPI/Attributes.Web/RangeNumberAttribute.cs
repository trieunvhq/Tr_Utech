using System;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Attributes.Web
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeNumberAttribute : ValidationAttribute
    {
        private int Min { get; set; }
        private int Max { get; set; }
        private string ErrorMessage { get; set; }

        public RangeNumberAttribute(int Min, int Max, string ErrorMessage = null)
        {
            this.Min = Min;
            this.Max = Max;
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var isNumeric = int.TryParse(value.ToString(), out int targetValue);
                if (!isNumeric)
                {
                    return ErrorMessage != null ? new ValidationResult(ErrorMessage) : new ValidationResult($"{validationContext.DisplayName} must be {Min} to {Max}");
                }

                if (Min < (int)targetValue && (int)targetValue < Max)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return ErrorMessage != null ? new ValidationResult(ErrorMessage) : new ValidationResult($"{validationContext.DisplayName} must be {Min} to {Max}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ValidationResult.Success;
            }
        }
    }
}

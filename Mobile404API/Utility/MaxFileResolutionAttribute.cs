using System.ComponentModel.DataAnnotations;

namespace testasp1.Utility
{
    public class MaxFileResolutionAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileResolutionAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
            
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null) 
            {
                
                if(file.Length > (1000 * _maxFileSize))
                {
                    return new ValidationResult($"اندازه عکس نهایتا باید {_maxFileSize} کیلوبایت باشد");

                }
            }

            return ValidationResult.Success;

            
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ClothingBrandDashboard.Models
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsLower) ||
                    !password.Any(char.IsDigit) || !password.Any(ch => "!@#$%^&*()_+".Contains(ch)))
                {
                    return new ValidationResult("Password must be at least 8 characters long and include an uppercase letter, a lowercase letter, a digit, and a special character.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

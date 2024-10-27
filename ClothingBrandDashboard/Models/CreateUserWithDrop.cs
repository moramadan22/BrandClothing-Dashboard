using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClothingBrandDashboard.Models
{
    public class CreateUserWithDrop
    {
        public string Name { get; set; }

        [EmailAddress, Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [PasswordComplexity]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm  Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ClothingBrandDashboard.Models
{
    public class CreateUser
    {
        public string Name { get; set; }

        [EmailAddress, Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm  Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

    }
}

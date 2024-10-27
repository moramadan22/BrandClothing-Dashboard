using System.ComponentModel.DataAnnotations;

namespace ClothingBrandDashboard.Models
{
    public class Login
    {
        [EmailAddress, Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

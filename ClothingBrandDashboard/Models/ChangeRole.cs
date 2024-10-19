namespace ClothingBrandDashboard.Models
{
    public class ChangeRole
    {
       
        public record ChangeRoleDto(string UserEmail, string RoleName);

    }
}
//public string UserEmail { get; set; }
//public string RoleName { get; set; }
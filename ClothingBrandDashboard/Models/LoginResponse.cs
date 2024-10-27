namespace ClothingBrandDashboard.Models
{
        public record LoginResponse(bool flag = false, string message = null!, string Token = null!, string RefreshToken = null!, string userId = null);
    
}

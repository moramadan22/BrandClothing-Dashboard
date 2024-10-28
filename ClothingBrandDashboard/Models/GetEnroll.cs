namespace ClothingBrandDashboard.Models
{
    public class GetEnroll
    {
        public string? UserId { get; set; }
        public int? CourseId { get; set; }
        public string? EnrollDate { get; set; }
        public ICollection<string>? Users { get; set; } = new List<string>();
        public ICollection<string>? Courses { get; set; } = new List<string>();
    }
}

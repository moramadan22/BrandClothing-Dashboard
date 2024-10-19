namespace ClothingBrandDashboard.ModelVW
{
    public class Discount
    {
        public string? Code { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

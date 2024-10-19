namespace ClothingBrandDashboard.ModelVW
{
    public class GetDiscount
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        //public virtual ICollection<Product>? Products { get; set; }
    }
}

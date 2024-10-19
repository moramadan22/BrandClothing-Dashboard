namespace ClothingBrandDashboard.ModelVW
{
    public class Product
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int StockQuantity { get; set; }
        public string ISBN { get; set; }

        public int CategoryId { get; set; }

        public int DiscountId { get; set; }
    }
}

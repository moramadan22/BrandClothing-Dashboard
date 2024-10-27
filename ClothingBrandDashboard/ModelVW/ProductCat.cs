using System.ComponentModel;

namespace ClothingBrandDashboard.ModelVW
{
    public class ProductCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Stock Quantity")]
        public int StockQuantity { get; set; }
        public string ISBN { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        public decimal Discount { get; set; }
    }
}

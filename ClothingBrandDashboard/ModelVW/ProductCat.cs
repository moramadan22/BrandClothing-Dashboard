﻿namespace ClothingBrandDashboard.ModelVW
{
    public class ProductCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public string ISBN { get; set; }

        public string CategoryName { get; set; }
        public decimal Discount { get; set; }
    }
}

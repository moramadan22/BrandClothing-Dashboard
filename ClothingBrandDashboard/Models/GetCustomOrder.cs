namespace ClothingBrandDashboard.Models
{
    public class GetCustomOrder
    {
        public int Id { get; set; }
        public string DesignDescription { get; set; }
        public string FabricDetails { get; set; }
        public decimal DepositAmount { get; set; }
        public string CustomOrderStatus { get; set; }
        public double ShoulderWidth { get; set; }
        public double ChestCircumference { get; set; }
        public double WaistCircumference { get; set; }
        public double HipCircumference { get; set; }
        public double WaistLength { get; set; }
        public double ArmLength { get; set; }
        public double BicepSize { get; set; }
        public double ModelLength { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
    }
}

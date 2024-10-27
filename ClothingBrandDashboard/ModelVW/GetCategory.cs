namespace ClothingBrandDashboard.ModelVW
{
    public class GetCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }
        public List<string> Products { get; set; }

    }
}

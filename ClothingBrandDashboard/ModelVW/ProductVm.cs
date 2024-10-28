using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ClothingBrandDashboard.ModelVW
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }
        public int StockQuantity { get; set; }
        public string ISBN { get; set; }

        public int CategoryId { get; set; }
        [DisplayName("Discount")]
        public int DiscountId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Discounts { get; set; }

    }
}

using ClothingBrandDashboard.Models;
using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;

namespace ClothingBrandDashboard.Controllers
{
    public class CustomOrderController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api/CustomClothingOrder";
        public CustomOrderController()
        {

        }
        public async Task<IActionResult> Index()
        {

            return View();

        }
        public async Task<IActionResult> Getdata()
        {
            var CustomOrder = new List<GetCustomOrder>();
            CustomOrder = await client.GetFromJsonAsync<List<GetCustomOrder>>(endpoint);
            return Json(new { data = CustomOrder });

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var CustomOrder= new GetCustomOrder();
            CustomOrder = await client.GetFromJsonAsync<GetCustomOrder>(endpoint + "/" + id);

            //List<GetOrder> Oreders = new List<GetOrder>();
            //Oreders = await client.GetFromJsonAsync<List<GetOrder>>(endpoint);

            return View(CustomOrder);
        }

    }
}

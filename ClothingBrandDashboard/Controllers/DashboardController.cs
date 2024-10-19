using ClothingBrandDashboard.Models;
using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;

namespace ClothingBrandDashboard.Controllers
{
    public class DashboardController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpointCourse = "https://localhost:7108/api/Course";
        public string endpointProduct = "https://localhost:7108/api/Product";
        public string endpointCategory = "https://localhost:7108/api/Category";
        public string endpointOrder = "https://localhost:7108/api/Order";
        public string endpointUsers = "https://localhost:7108/api/Account/identity/user-with-role";
        public string endpointCustomOrders = "https://localhost:7108/api/CustomClothingOrder";
        public string endpointDiscount = "https://localhost:7108/api/Discount";





        public DashboardController()
        {

        }
        public async Task<IActionResult> Index()
        {
            List<Course> Courses = new List<Course>();
            Courses = await client.GetFromJsonAsync<List<Course>>(endpointCourse);

            List<ProductCat> products = new List<ProductCat>();
            products = await client.GetFromJsonAsync<List<ProductCat>>(endpointProduct);

            List<GetCategory> categories = new List<GetCategory>();
            categories = await client.GetFromJsonAsync<List<GetCategory>>(endpointCategory);

            List<GetOrder> orders = new List<GetOrder>();
            orders = await client.GetFromJsonAsync<List<GetOrder>>(endpointOrder);
            orders.OrderByDescending(c=>c.OrderDate).Take(7);

            List<GetUserWithRole> users = new List<GetUserWithRole>();
            users = await client.GetFromJsonAsync<List<GetUserWithRole>>(endpointUsers);

            List<Discount> discounts = new List<Discount>();
            discounts = await client.GetFromJsonAsync<List<Discount>>(endpointDiscount);

            List<GetCustomOrder> customOrders = new List<GetCustomOrder>();
            customOrders = await client.GetFromJsonAsync<List<GetCustomOrder>>(endpointCustomOrders);



            ViewData["courses"] = Courses.Count();
            ViewData["products"] = products.Count();
            ViewData["categories"] = categories.Count();
            ViewData["orders"] = orders.Count();
            ViewData["users"] = users.Count();
            ViewData["discounts"] = discounts.Count();
            ViewData["customOrders"] = customOrders.Count();








            return View(orders);

        }
        //public async Task<IActionResult> Getdata()
        //{
        //    List<Course> Courses = new List<Course>();
        //    Courses = await client.GetFromJsonAsync<List<Course>>(endpoint);
        //    return Json(new { data = Courses });
        //}
    }
}

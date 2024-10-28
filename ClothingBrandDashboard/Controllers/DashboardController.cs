using ClothingBrandDashboard.Models;
using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;

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
        public string endpointEnroll = "https://localhost:7108/api/Enroll";


        





        private readonly string? token;
        public DashboardController(IHttpContextAccessor httpContextAccessor)
        {
            token = httpContextAccessor.HttpContext.Session.GetString("AccessToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // Retrieve the token from session or any secure storage
            //var token = HttpContext.Session.GetString("AccessToken");

            //// Set the token in the Authorization header
            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTW9oYW1lZHJhbWFkYW41NDk2QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Ik1vaGFtZWRyYW1hZGFuNTQ5NkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNWEyOGJhMDQtMzU1Ni00MTBmLWFiMGEtNDQxZTQ4OGFhZjA2IiwiRnVsbE5hbWUiOiJNb2hhbWVkIiwiZXhwIjoxNzI5MzY2MDAyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTA4LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMC8ifQ.u9RktUZioF9-2KpnqghNAPZtYMpiekk3HstM8KWykUw";
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);




        }
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(token))
            {
                // Handle case where the token is not available
                return Unauthorized();
            }


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

            List<GetEnroll> Enrolls = new List<GetEnroll>();
            Enrolls = await client.GetFromJsonAsync<List<GetEnroll>>(endpointEnroll);

            decimal totalRevenue = 0;

            foreach (var item in orders)
            {
                totalRevenue += item.TotalPrice;
            }

            decimal totalQuantity = 0;

            foreach (var item in products)
            {
                totalQuantity += item.StockQuantity;
            }




            ViewData["courses"] = Courses.Count();
            ViewData["products"] = products.Count();
            ViewData["categories"] = categories.Count();
            ViewData["orders"] = orders.Count();
            ViewData["users"] = users.Count();
            ViewData["discounts"] = discounts.Count();
            ViewData["customOrders"] = customOrders.Count();
            ViewData["totalRevenue"] = totalRevenue.ToString("C");
            ViewData["totalCost"] = ((totalRevenue / 100) *80).ToString("C");
            ViewData["totalProfit"] = ((totalRevenue / 100 )* 20).ToString("C");
            ViewData["totalQuantity"] = totalQuantity;
            ViewData["enrolls"] = Enrolls.Count();















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

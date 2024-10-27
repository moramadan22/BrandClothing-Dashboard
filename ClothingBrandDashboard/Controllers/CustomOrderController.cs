using ClothingBrandDashboard.Models;
using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ClothingBrandDashboard.Controllers
{
    public class CustomOrderController : Controller
    {
        HttpClient client = new HttpClient();

        public string endpoint = "https://localhost:7108/api/CustomClothingOrder";
        private readonly string? token;
        public CustomOrderController(IHttpContextAccessor httpContextAccessor)
        {
            token = httpContextAccessor.HttpContext.Session.GetString("AccessToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTW9oYW1lZHJhbWFkYW41NDk2QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Ik1vaGFtZWRyYW1hZGFuNTQ5NkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNWEyOGJhMDQtMzU1Ni00MTBmLWFiMGEtNDQxZTQ4OGFhZjA2IiwiRnVsbE5hbWUiOiJNb2hhbWVkIiwiZXhwIjoxNzI5MzQ4MTM3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTA4LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMC8ifQ.iISt52ilLbjraMxcKPwFs6jAQmsZCr8uctRFO6tGslA";
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<IActionResult> Index()
        {

            return View();

        }
        public async Task<IActionResult> Getdata()
        {

            if (string.IsNullOrEmpty(token))
            {
                // Handle case where the token is not available
                return Unauthorized();

            }

            var CustomOrder = new List<GetCustomOrder>();
            CustomOrder = await client.GetFromJsonAsync<List<GetCustomOrder>>(endpoint);
            return Json(new { data = CustomOrder });

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (string.IsNullOrEmpty(token))
            {
                // Handle case where the token is not available
                return Unauthorized();
            }
            var CustomOrder= new GetCustomOrder();

            CustomOrder = await client.GetFromJsonAsync<GetCustomOrder>(endpoint + "/" + id);

            //List<GetOrder> Oreders = new List<GetOrder>();
            //Oreders = await client.GetFromJsonAsync<List<GetOrder>>(endpoint);

            return View(CustomOrder);
        }

    }
}

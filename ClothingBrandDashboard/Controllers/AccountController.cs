using ClothingBrandDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClothingBrandDashboard.Controllers
{
    public class AccountController : Controller
    {
        public string endpoint = "https://localhost:7108/api/Account/identity/login";

        private readonly HttpClient _httpClient;
        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var loginData = new
                {
                    username = model.Email,
                    password = model.Password
                };

                //var json = JsonConvert.SerializeObject(loginData);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("https://api.example.com/api/login", content);
                var response = await _httpClient.PostAsJsonAsync(endpoint, model);


                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonConvert.DeserializeObject<TokenResponse>(responseData);

                    // Store the token (e.g., in session)
                    HttpContext.Session.SetString("AccessToken", tokenData.Token);

                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }
    }



}

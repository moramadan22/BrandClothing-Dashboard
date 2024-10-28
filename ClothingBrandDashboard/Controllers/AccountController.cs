using Application.DTOs.Response;
using ClothingBrandDashboard.Models;
using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClothingBrandDashboard.Controllers
{
    public class AccountController : Controller
    {
        public string endpoint = "https://localhost:7108/api/Account/identity/";

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
                var response = await _httpClient.PostAsJsonAsync(endpoint + "login", model);


                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonConvert.DeserializeObject<TokenResponse>(responseData);
                    var userIdDate = JsonConvert.DeserializeObject<UserID>(responseData);
                    // Store the token (e.g., in session)
                    var AccessRole = await _httpClient.GetFromJsonAsync<GeneralResponse>(endpoint + "CurrentUserRole?userId=" + userIdDate.userId);
                    if (AccessRole.flag)
                    {

                        HttpContext.Session.SetString("AccessToken", tokenData.Token);

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You are Not Authorized");
                        return View(model);
                    }
                    //var AccessRoleData = await AccessRole.Content.ReadAsStringAsync();
                    // var roleDate = JsonConvert.DeserializeObject<string>(responseData);

                }

                ModelState.AddModelError("", "Email or Password Invalid.");
                return View(model);

            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> logout()
        {
            //  await _httpClient.GetFromJsonAsync<Boolean>(endpoint + "LogOut");
            HttpContext.Session.Remove("AccessToken");
            return RedirectToAction("Login");
        }
    }



}
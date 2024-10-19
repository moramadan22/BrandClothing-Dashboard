using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ClothingBrandDashboard.Controllers
{
    public class OrderController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api/Order";
        private readonly string? token;
        public OrderController(IHttpContextAccessor httpContextAccessor)
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
            List<GetOrder> Oreders = new List<GetOrder>();
            Oreders = await client.GetFromJsonAsync<List<GetOrder>>(endpoint);
            return Json(new { data = Oreders });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var course = new Course();


            return View(course);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {


            // Check if the model state is valid
            if (ModelState.IsValid)
            {

                try
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        // Handle case where the token is not available
                        return Unauthorized();
                    }
                    // Send the POST request to the API
                    var response = await client.PostAsJsonAsync<Course>(endpoint, course);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Log or display the error message for debugging
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Error: {errorMessage}. Please contact administrator.");
                    }
                }
                catch (Exception ex)
                {
                    // Log or display the exception for debugging
                    ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}. Please contact administrator.");
                }
            }

            return View(course);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (string.IsNullOrEmpty(token))
            {
                // Handle case where the token is not available
                return Unauthorized();
            }
            //List<Course> Courses = new List<Course>();
            Course course = await client.GetFromJsonAsync<Course>(endpoint + "/" + id);
            Console.WriteLine(course);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(course);
                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where the token is not available
                    return Unauthorized();
                }

                var response = await client.PutAsJsonAsync(endpoint + "/" + id, course);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (string.IsNullOrEmpty(token))
            {
                // Handle case where the token is not available
                return Unauthorized();
            }
            var Oreder = new GetOrder();
            Oreder = await client.GetFromJsonAsync<GetOrder>(endpoint+"/"+id);

            //List<GetOrder> Oreders = new List<GetOrder>();
            //Oreders = await client.GetFromJsonAsync<List<GetOrder>>(endpoint);

            return View(Oreder);
        }







        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var order = client.DeleteAsync(endpoint + "/" + id+"/"+ "cancel");
            if (order == null)
            {
                return Json(new { success = false, message = "Error while deleting " });
            }
            return Json(new { success = true, message = "Item has deleted successfully" });
        }
    }
}

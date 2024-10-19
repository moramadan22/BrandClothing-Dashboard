using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ClothingBrandDashboard.Controllers
{
    public class CategoryController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api/Category";
        private readonly string? token;
        public CategoryController(IHttpContextAccessor httpContextAccessor)
        {
            token = httpContextAccessor.HttpContext.Session.GetString("AccessToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
            List<GetCategory> categories = new List<GetCategory>();
            categories = await client.GetFromJsonAsync<List<GetCategory>>(endpoint);
            return Json(new { data = categories });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var category = new Category();


            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category )
        {


            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(token))
                {
                    // Handle case where the token is not available
                    return Unauthorized();
                }

                try
                {
                    // Send the POST request to the API
                    var response = await client.PostAsJsonAsync<Category>(endpoint, category);

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

            return View(category);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            //List<Course> Courses = new List<Course>();
            Category category = await client.GetFromJsonAsync<Category>(endpoint + "/" + id);
            //Console.WriteLine(course);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(category);

                var response = await client.PutAsJsonAsync(endpoint + "/" + id, category);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(token))
            {
                // Handle case where the token is not available
                return Unauthorized();
            }
            //var deletedproduct= _context.products.Find(product.Id);
            var category = client.DeleteAsync(endpoint + "?id=" + id);
            if (category == null)
            {
                return Json(new { success = false, message = "Error while deleting " });
            }

            //_context.products.Remove(deletedproduct);


            //_context.SaveChanges();
            //TempData["Deleted"] = "Product has Deleted successfully ";
            return Json(new { success = true, message = "Item has deleted successfully" });

        }

    }
}

using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;

namespace ClothingBrandDashboard.Controllers
{
    public class OrderController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api/Order";
        public OrderController()
        {

        }
        public async Task<IActionResult> Index()
        {

            return View();

        }
        public async Task<IActionResult> Getdata()
        {
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

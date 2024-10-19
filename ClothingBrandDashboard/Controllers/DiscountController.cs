using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ClothingBrandDashboard.Controllers
{
    public class DiscountController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api/Discount";
        public DiscountController()
        {

        }
        public async Task<IActionResult> Index()
        {

            return View();

        }
        public async Task<IActionResult> Getdata()
        {
            List<GetDiscount> Discounts = new List<GetDiscount>();
            Discounts = await client.GetFromJsonAsync<List<GetDiscount>>(endpoint);
            return Json(new { data = Discounts });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var Discount = new Discount();


            return View(Discount);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discount discount )
        {


            // Check if the model state is valid
            if (ModelState.IsValid)
            {

                try
                {
                    // Send the POST request to the API
                    var response = await client.PostAsJsonAsync<Discount>(endpoint, discount);

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

            return View(discount);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //List<Course> Courses = new List<Course>();
            Discount discount = await client.GetFromJsonAsync<Discount>(endpoint + "/" + id);
            //Console.WriteLine(course);
            return View(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Discount discount )
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(discount);

                var response = await client.PutAsJsonAsync(endpoint + "/" + id, discount);

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
            //var deletedproduct= _context.products.Find(product.Id);
            var discount = client.DeleteAsync(endpoint + "?id=" + id);
            if (discount == null)
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

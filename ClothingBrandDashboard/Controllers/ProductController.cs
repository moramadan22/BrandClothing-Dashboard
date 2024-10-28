using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace ClothingBrandDashboard.Controllers
{
    public class ProductController : Controller
    {

        /*
          var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
         
         */
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api/Product";
        public string endpointCat = "https://localhost:7108/api/Category";
        public string endpointDis = "https://localhost:7108/api/Discount";

        private readonly IHttpClientFactory _httpClientFactory;

        List<GetCategory> categories;
        List<GetDiscount> Disccounts;
        //string token {  get; set; }

        private IWebHostEnvironment _webHostEnvironment;



        public ProductController(IWebHostEnvironment webHostEnvironment, IHttpClientFactory httpClientFactory)
        {




            //var token = HttpContext.Session.GetString("AccessToken");


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var 
            //token = HttpContext.Session.GetString("AccessToken");

            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTW9oYW1lZHJhbWFkYW41NDk2QGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Ik1vaGFtZWRyYW1hZGFuNTQ5NkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNWEyOGJhMDQtMzU1Ni00MTBmLWFiMGEtNDQxZTQ4OGFhZjA2IiwiRnVsbE5hbWUiOiJNb2hhbWVkIiwiZXhwIjoxNzI5MzQ4MTM3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTA4LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMC8ifQ.iISt52ilLbjraMxcKPwFs6jAQmsZCr8uctRFO6tGslA";
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            categories = new List<GetCategory>();
            Disccounts = new List<GetDiscount>();

            _webHostEnvironment = webHostEnvironment;
            _httpClientFactory = httpClientFactory;
        }

        //++++++++++++++++++
        public async Task<IActionResult> Index()
        {
            //List<ProductVM> products=new List<ProductVM>();
            // products=await client.GetFromJsonAsync<List<ProductVM>>(endpoint);
            return View();

        }
        public async Task<IActionResult> Getdata()
        {
            //------------
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //----------------------------
            List<ProductCat> products = new List<ProductCat>();
            products = await client.GetFromJsonAsync<List<ProductCat>>(endpoint);
            return Json(new { data = products });
        }


        public async Task<IActionResult> GetCategories()
        {

            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          
            categories = await client.GetFromJsonAsync<List<GetCategory>>(endpointCat);
            return Json(new { categories });

        }






        [HttpGet]
        public async  Task<IActionResult> Create()
        {
            ProductVm productvm = new ProductVm();
            var product =new Product();
            //------------
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //----------------------------

            categories = await client.GetFromJsonAsync<List<GetCategory>>(endpointCat);
            productvm.Categories = categories.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
            Disccounts = await client.GetFromJsonAsync<List<GetDiscount>>(endpointDis);
            productvm.Discounts = Disccounts.Select(s => new SelectListItem
            {
                Text = s.Code,
                Value = s.Id.ToString()
            });

            // return View(productVM);
            return View(productvm);
            //return View(product);

        }

       

        //------------------------------------------------------------------------------------------------
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ProductVm productinput)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var productsave = new Product();
        //        productsave.Name = productinput.Name;
        //        productsave.Description = productinput.Description;
        //        productsave.Price = productinput.Price;
        //        productsave.CategoryId = productinput.CategoryId;
        //        productsave.DiscountId = productinput.DiscountId;
        //        productsave.ISBN = productinput.ISBN;
        //        productsave.StockQuantity = productinput.StockQuantity;
        //        productsave.Image = productinput.Image;

        //        var prod = await client.PostAsJsonAsync<Product>(endpoint, productsave);
        //        var jsonPayload = JsonConvert.SerializeObject(productsave);
        //        Console.WriteLine(jsonPayload);
        //        Console.WriteLine(  "**************");


        //        if (prod.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            Console.WriteLine(client.PostAsJsonAsync(endpoint, productsave).ToString());
        //            Console.WriteLine(client.PostAsJsonAsync(endpoint, productsave));

        //            Console.WriteLine(prod.RequestMessage);
        //            Console.WriteLine(prod.ReasonPhrase);
        //            //Console.WriteLine(prod.re);



        //            ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
        //        }

        //    }
        //    return View(productinput);
        //}
      
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVm productVm)
        {
            //------------
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //----------------------------

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                Product productInput=new Product();
                productInput.Name =productVm.Name;
                productInput.ISBN = productVm.ISBN;
                productInput.Price = productVm.Price;
                productInput.Description = productVm.Description;
                productInput.CategoryId = productVm.CategoryId;
                productInput.DiscountId = productVm.DiscountId;
                productInput.Image = productVm.Image;
                productInput.StockQuantity = productVm.StockQuantity;



                using (var content = new MultipartFormDataContent())
                {
                    // Add the properties to the content
                    content.Add(new StringContent(productInput.Name), "Name");
                    content.Add(new StringContent(productInput.Description), "Description");
                    content.Add(new StringContent(productInput.Price.ToString("G")), "Price"); // Format price if needed
                    content.Add(new StringContent(productInput.StockQuantity.ToString()), "StockQuantity");
                    content.Add(new StringContent(productInput.ISBN), "ISBN");
                    content.Add(new StringContent(productInput.CategoryId.ToString()), "CategoryId");
                    content.Add(new StringContent(productInput.DiscountId.ToString()), "DiscountId");

                    // Add the image if it exists
                    if (productInput.Image != null && productInput.Image.Length > 0)
                    {
                        var fileContent = new StreamContent(productInput.Image.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(productInput.Image.ContentType);
                        content.Add(fileContent, "Image", productInput.Image.FileName);
                    }

                    try
                    {
                        // Send the POST request to the API
                        var response = await client.PostAsync(endpoint, content);

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
            }

            // If the model is invalid or there was an error, return back to the same view
            return View(productVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }



            //------------
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //----------------------------

            //var product = _context.products.Find(id);
            ProductCat productimg = await client.GetFromJsonAsync<ProductCat>(endpoint + "/" + id);

            Product product = await client.GetFromJsonAsync<Product>(endpoint + "/" + id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["img"] = productimg.ImageUrl;
            ProductVm productVM = new ProductVm();
            productVM.Name = product.Name;
            productVM.Id = id;
            productVM.Image = product.Image;
            productVM.CategoryId = product.CategoryId;
            productVM.Description = product.Description;
            productVM.Price = product.Price;
            productVM.DiscountId = product.DiscountId;
            productVM.StockQuantity = product.StockQuantity;
            productVM.ISBN = product.ISBN;
            categories = await client.GetFromJsonAsync<List<GetCategory>>(endpointCat);
            productVM.Categories = categories.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
                Selected = s.Id == productVM.CategoryId
            });

            Disccounts = await client.GetFromJsonAsync<List<GetDiscount>>(endpointDis);
            productVM.Discounts = Disccounts.Select(s => new SelectListItem
            {
                Text = s.Code,
                Value = s.Id.ToString(),
                Selected = s.Id == productVM.DiscountId

            });

            return View(productVM);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(ProductVm productVm)
        //{


        //    // Check if the model state is valid
        //    if (ModelState.IsValid)
        //    {
        //        Product productInput = new Product();
        //        productInput.Name = productVm.Name;
        //        productInput.ISBN = productVm.ISBN;
        //        productInput.Price = productVm.Price;
        //        productInput.Description = productVm.Description;
        //        productInput.CategoryId = productVm.CategoryId;
        //        productInput.DiscountId = productVm.DiscountId;
        //        productInput.Image = productVm.Image;
        //        productInput.StockQuantity = productVm.StockQuantity;



        //        using (var content = new MultipartFormDataContent())
        //        {
        //            // Add the properties to the content

        //            content.Add(new StringContent(productInput.Name), "Name");
        //            content.Add(new StringContent(productInput.Description), "Description");
        //            content.Add(new StringContent(productInput.Price.ToString("G")), "Price"); // Format price if needed
        //            content.Add(new StringContent(productInput.StockQuantity.ToString()), "StockQuantity");
        //            content.Add(new StringContent(productInput.ISBN), "ISBN");
        //            content.Add(new StringContent(productInput.CategoryId.ToString()), "CategoryId");
        //            content.Add(new StringContent(productInput.DiscountId.ToString()), "DiscountId");

        //            // Add the image if it exists
        //            if (productInput.Image != null && productInput.Image.Length > 0)
        //            {
        //                var fileContent = new StreamContent(productInput.Image.OpenReadStream());
        //                fileContent.Headers.ContentType = new MediaTypeHeaderValue(productInput.Image.ContentType);
        //                content.Add(fileContent, "Image", productInput.Image.FileName);
        //            }

        //            try
        //            {
        //                // Send the POST request to the API
        //                var response = await client.PutAsync(endpoint+"/"+productVm.Id, content);

        //                // Check if the response is successful
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    return RedirectToAction(nameof(Index));
        //                }
        //                else
        //                {
        //                    // Log or display the error message for debugging
        //                    var errorMessage = await response.Content.ReadAsStringAsync();
        //                    ModelState.AddModelError(string.Empty, $"Error: {errorMessage}. Please contact administrator.");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Log or display the exception for debugging
        //                ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}. Please contact administrator.");
        //            }
        //        }
        //    }

        //    // If the model is invalid or there was an error, return back to the same view
        //    return View(productVm);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVm productVm)
        {
            if (ModelState.IsValid)
            {
                // Initialize productInput
                Product productInput = new Product
                {
                    Name = productVm.Name,
                    ISBN = productVm.ISBN,
                    Price = productVm.Price,
                    Description = productVm.Description,
                    CategoryId = productVm.CategoryId,
                    DiscountId = productVm.DiscountId,
                    Image = productVm.Image,
                    StockQuantity = productVm.StockQuantity
                };
                if (productVm.Image != null)
                    productInput.Image = productVm.Image;
                else
                    productInput.Image=null;

                using (var content = new MultipartFormDataContent())
                {
                    // Add properties to the content
                    content.Add(new StringContent(productInput.Name), "Name");
                    content.Add(new StringContent(productInput.Description), "Description");
                    content.Add(new StringContent(productInput.Price.ToString("G")), "Price");
                    content.Add(new StringContent(productInput.StockQuantity.ToString()), "StockQuantity");
                    content.Add(new StringContent(productInput.ISBN), "ISBN");
                    content.Add(new StringContent(productInput.CategoryId.ToString()), "CategoryId");
                    content.Add(new StringContent(productInput.DiscountId.ToString()), "DiscountId");


                    if (productInput.Image != null && productInput.Image.Length > 0)
                    {
                        var fileContent = new StreamContent(productInput.Image.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(productInput.Image.ContentType);
                        content.Add(fileContent, "Image", productInput.Image.FileName);
                    }
                    else {
                        content.Add(new StringContent(string.Empty), "Image");
                    }


                    try
                    {

                        //------------
                        var token = HttpContext.Session.GetString("AccessToken");
                        if (string.IsNullOrEmpty(token))
                        {
                            return Unauthorized();
                        }
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        //----------------------------

                        // Send the PUT request to the API
                        var response = await client.PutAsync(endpoint + "/" + productVm.Id, content);

                        // Check if response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            ModelState.AddModelError(string.Empty, $"Error: {errorMessage}. Please contact administrator.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Server error: {ex.Message}. Please contact administrator.");
                    }
                }
            }

            categories = await client.GetFromJsonAsync<List<GetCategory>>(endpointCat);
            productVm.Categories = categories.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
                Selected = s.Id == productVm.CategoryId
            });

            Disccounts = await client.GetFromJsonAsync<List<GetDiscount>>(endpointDis);
            productVm.Discounts = Disccounts.Select(s => new SelectListItem
            {
                Text = s.Code,
                Value = s.Id.ToString(),
                Selected = s.Id == productVm.DiscountId

            });

            // If model is invalid or there was an error, return to the same view
            return View(productVm);
        }






        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            //var deletedproduct= _context.products.Find(product.Id);

            //------------
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //----------------------------
            var deletedproduct = client.DeleteAsync(endpoint +"?id=" + id);
            if (deletedproduct == null)
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

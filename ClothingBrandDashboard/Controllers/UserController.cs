using ClothingBrandDashboard.Models;
using ClothingBrandDashboard.ModelVW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Net.Http.Json;

namespace ClothingBrandDashboard.Controllers
{
    public class UserController : Controller
    {
        HttpClient client = new HttpClient();
        public string endpoint = "https://localhost:7108/api";
        List<Role> roles= new List<Role>();
        public UserController()
        {

        }
        public async Task<IActionResult> Index()
        {

            return View();

        }
        public async Task<IActionResult> Getdata()
        {
            List<GetUserWithRole> Users = new List<GetUserWithRole>();
            Users = await client.GetFromJsonAsync<List<GetUserWithRole>>(endpoint+ "/Account/identity/user-with-role");
            return Json(new { data = Users });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var UserVm = new CreateUserWithDrop();
            roles = await client.GetFromJsonAsync<List<Role>>(endpoint + "/Account/identity/role/list");
            UserVm.Roles = roles.Select(s => new SelectListItem {
                Text = s.Name,
                Value = s.Name,
            });



            return View(UserVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserWithDrop UserVm)
        {
            if (ModelState.IsValid)
            {

                var userDb = new CreateUser { 
                    Name= UserVm .Name,Email= UserVm .Email,Password= UserVm.Password,ConfirmPassword= UserVm .ConfirmPassword,Role= UserVm.Role
                };

                try
                {
                    // Send the POST request to the API
                    var response = await client.PostAsJsonAsync<CreateUser>(endpoint+"/Account/identity/create", userDb);

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
            roles = await client.GetFromJsonAsync<List<Role>>(endpoint + "/Account/identity/role/list");
            UserVm.Roles = roles.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Name,
            });

            return View(UserVm);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //List<Course> Courses = new List<Course>();
            //GetUserWithRole user = await client.GetFromJsonAsync<GetUserWithRole>(endpoint + "/Account/identity/user-with-role").();
            List<GetUserWithRole> Users = new List<GetUserWithRole>();
            Users = await client.GetFromJsonAsync<List<GetUserWithRole>>(endpoint + "/Account/identity/user-with-role");
            var user = new GetUserWithRole();
            //user=Users.Select(c=>c.UserID==id)();
            user=Users.Where(c=>c.UserID==id).FirstOrDefault();
            var senduser = new CreateUserWithDrop();
            senduser.Email = user.Email;
            senduser.Role = user.RoleName;

            roles = await client.GetFromJsonAsync<List<Role>>(endpoint + "/Account/identity/role/list");
            senduser.Roles = roles.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Name,
            });

            //foreach (var use in Users)
            //{
            //    if(use.Email.ToLower()==user.Email.ToLower())
            //        user=us
            //}


            return View(senduser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateUserWithDrop createUserWith )
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createUserWith);

                //var changerole =new ChangeRole { UserEmail=createUserWith.Email,RoleName=createUserWith.Role };
                var changerole = new ChangeRole.ChangeRoleDto(createUserWith.Email, createUserWith.Role);

                //changerole.
                var response = await client.PostAsJsonAsync(endpoint + "/Account/identity/role/change", changerole);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }


        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var user = client.DeleteAsync(endpoint + "/Account/DeleteUser"+ "?id=" + id);
            if (user == null||user.IsFaulted)
            {
                return Json(new { success = false, message = "Error while deleting " });
            }
            return Json(new { success = true, message = "Item has deleted successfully" });
        }

    }
}

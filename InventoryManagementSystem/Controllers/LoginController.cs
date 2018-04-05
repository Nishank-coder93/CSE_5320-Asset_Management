using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            var Model = new LoginViewModel();

            var db = new Context();
            var dbCheck = db.Database.Exists();
            db.Database.CommandTimeout = int.MaxValue;

            // If true, the database does not exist
            if (!dbCheck)
            {
                db.Database.Create();
                // Initializing the database
                Initialize();
            }

            return View(Model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Baseurl = GetURL();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage Res = await client.PostAsJsonAsync("/api/Values/login", Model);
                        if (Res.IsSuccessStatusCode)
                        {
                            var result = await Res.Content.ReadAsStringAsync();

                            if (result.ToLower() == "null")
                            {
                                Model.Login = false;
                                return View(Model);
                            }
                            else
                            {
                                Session["Login"] = true;

                                var response = new ResponseHelper();
                                var output = response.fixResult(result);

                                var user = JsonConvert.DeserializeObject<UserViewModel>(output);

                                Session["LoggedInUserId"] = user.UserId;
                                Session["LoggedInName"] = user.Name;
                                Session["LoggedInUsername"] = user.UserName;
                                Session["Role"] = user.RoleId;

                                if (int.Parse(Session["Role"].ToString()) == 1)
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Dashboard");
                                }                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Model.Login = false;
                    return View(Model);
                }
            }

            Model.Login = false;
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(LoginViewModel Model)
        {
            Session["Login"] = false;

            Session["LoggedInUserId"] = null;
            Session["LoggedInName"] = null;
            Session["LoggedInUsername"] = null;

            return RedirectToAction("Index", "Home");
        }

        public string GetURL()
        {
            var result = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
            return result;
        }

        private void Initialize()
        {
            var db = new Context();

            var listHelper = new ListHelper();

            var users = listHelper.UserList();
            foreach (var u in users)
            {
                var code = "teamseven";
                var hashKey = PasswordHelper.GetHashKey(code);
                var encrypt = PasswordHelper.Encrypt(hashKey, u.Password);

                u.Password = encrypt;
                db.User.Add(u);
            }

            var roles = listHelper.RoleList();
            foreach (var r in roles)
            {
                db.Role.Add(r);
            }

            var userRoles = listHelper.UserRoleList();
            foreach (var r in userRoles)
            {
                db.UserRole.Add(r);
            }

            var facilities = listHelper.FacilityList();
            foreach (var f in facilities)
            {
                db.Facility.Add(f);
            }

            db.SaveChanges();
        }
    }
}
using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.Home;
using CSE_5320.Models.Login;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            var Model = new LoginModel();

            Context db = new Context();
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Baseurl = getURL();
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
                                Model.Error.Message = "Invalid username / password";
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
                                Session["Role"] = user.Role;

                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Model.Error.Message = "Something went wrong. Try again";
                    return View(Model);
                } 
            }

            Model.Error.Message = "Something went wrong. Try again";
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(LoginModel Model)
        {
            Session["Login"] = false;

            Session["LoggedInUserId"] = null;
            Session["LoggedInName"] = null;
            Session["LoggedInUsername"] = null;

            return RedirectToAction("Index", "Home");
        }

        public string getURL()
        {
            var result = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
            return result;
        }

        private void Initialize()
        {
            Context db = new Context();
            var helper = new ListHelper();

            var status = helper.StatusHelper();
            foreach (var s in status)
            {
                db.Status.Add(s);
            }

            var category = helper.CategoryHelper();
            foreach (var c in category)
            {
                db.Category.Add(c);
            }

            var roles = helper.RoleHelper();
            foreach (var r in roles)
            {
                db.Roles.Add(r);
            }

            var users = helper.UserHelper();
            foreach (var u in users)
            {
                db.Users.Add(u);
            }

            var department = helper.DepartmentHelper();
            foreach (var d in department)
            {
                db.Department.Add(d);
            }

            var os = helper.OsHelper();
            foreach (var o in os)
            {
                db.Os.Add(o);
            }

            var cpu = helper.CpuHelper();
            foreach (var c in cpu)
            {
                db.Cpu.Add(c);
            }

            var memory = helper.MemoryHelper();
            foreach (var m in memory)
            {
                db.Memory.Add(m);
            }

            var computer = helper.ComputerHelper();
            foreach (var c in computer)
            {
                db.Computer.Add(c);
            }

            var software = helper.SoftwareHelper();
            foreach (var s in software)
            {
                db.Software.Add(s);
            }

            var asset = helper.AssetHelper();
            foreach (var a in asset)
            {
                db.Assets.Add(a);
            }

            var requests = helper.RequestHelper();
            foreach (var r in requests)
            {
                db.Request.Add(r);
            }

            db.SaveChanges();
        }

    }
}

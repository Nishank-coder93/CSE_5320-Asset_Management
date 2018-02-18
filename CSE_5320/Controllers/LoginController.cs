using CSE_5320.Models.Home;
using CSE_5320.Models.Login;
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
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel Model)
        {
            if (ModelState.IsValid)
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
                        var result = Res.Content.ReadAsStringAsync().Result;

                        switch (result.ToLower())
                        {
                            case "true":
                                Session["Login"] = true;
                                return RedirectToAction("Index", "Home");
                            case "false":
                                Model.Error.Message = "Invalid username / password";
                                return View(Model);
                        }
                    }
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
            return RedirectToAction("Index", "Home");
        }

        public string getURL()
        {
            var result = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
            return result;
        }
    }
}

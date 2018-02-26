using CSE_5320.App_Start;
using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.Home;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index(HomeModel Model)
        {
            if (Session["Login"] == null)
            {
                Session["Login"] = false;
            }

            var login = (bool)Session["Login"];

            if (login)
            {
                var uri = new Uri(Request.Url.AbsoluteUri);
                var api = "api/Values/";
                var method = "";

                var url = uri + api + method;

                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;
                        //var output = JsonConvert.DeserializeObject<HomeModel>(data);

                    }
                }

                // Check for role
                if (int.Parse(Session["Role"].ToString()) == 1)
                {
                    //Admin
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (int.Parse(Session["Role"].ToString()) == 2)
                {
                    //Employee
                    return View();
                } 
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        } 
    }
}

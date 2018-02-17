using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index(HomeModel Model)
        {
            if (Session["Login"] == null)
            {
                Session["Login"] = false;
            }
            
            var login = (bool)Session["Login"];
             
            Context db = new Context();

            var user = db.Users.FirstOrDefault();

            // If true, the database does not exist
            if (user == null)
            {
                // Initializing the database
                Initialize();
            } 

            if (login)
            {
                var uri = new Uri(Request.Url.AbsoluteUri);
                var api = "api/Values/";
                var method = "";

                var url = uri + api+ method;

                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;
                        //var output = JsonConvert.DeserializeObject<HomeModel>(data);
                        
                    }
                }

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
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

            db.SaveChanges(); 
        }
    }
}

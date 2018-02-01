using CSE_5320.Models.Home;
using CSE_5320.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{ 
    [AllowAnonymous]
    public class LoginController : Controller
    { 
        public ActionResult Index()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel Model)
        {
            var homeModel = new HomeModel();
            homeModel.Login = true;

            return RedirectToAction("Index", "Home", homeModel); 
        }
    }
}

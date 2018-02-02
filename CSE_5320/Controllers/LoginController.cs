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
            var Model = new LoginModel();
            return View(Model);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel Model)
        {
            Session["Login"] = true; 
            return RedirectToAction("Index", "Home"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(LoginModel Model)
        {
            Session["Login"] = false;
            return RedirectToAction("Index", "Home");
        }
    }
}

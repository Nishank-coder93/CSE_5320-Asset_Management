using CSE_5320.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Context db = new Context();

            var user = new User();
            user.Id = 1;
            user.Name = "Neil";

            db.Users.Add(user);
            db.SaveChanges();

            return View();
        }
    }
}

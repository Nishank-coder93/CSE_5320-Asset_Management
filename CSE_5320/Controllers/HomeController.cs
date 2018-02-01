using CSE_5320.Helper;
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

            var user = db.Users.FirstOrDefault();

            // If true, the database does not exist
            if (user == null)
            {
                // Initializing the database
                Initialize();
            }

            return View();
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

            var location = helper.LocationHelper();
            foreach (var l in location)
            {
                db.Locations.Add(l);
            }

            var departments = helper.DepartmentHelper();
            foreach (var d in departments)
            {
                db.Departments.Add(d);
            }

            var categories = helper.CategoryHelper();
            foreach (var c in categories)
            {
                db.Categories.Add(c);
            } 

            var maintainance = helper.MaintainanceHelper();
            foreach (var m in maintainance)
            {
                db.Maintainance.Add(m);
            }

            var asset = helper.AssetHelper();
            foreach (var a in asset)
            {
                db.Assets.Add(a);
            }

            var departmentAssets = helper.DepartmentAssetHelper();
            foreach (var da in departmentAssets)
            {
                db.DepartmentAssets.Add(da);
            }

            db.SaveChanges(); 
        }
    }
}

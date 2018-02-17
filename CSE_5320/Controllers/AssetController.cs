using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    public class AssetController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Return()
        {
            return View();
        }
    }
}
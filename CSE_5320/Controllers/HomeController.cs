using CSE_5320.App_Start;
using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.Home;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
                    return View(Model);
                } 
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public async Task<ActionResult> getUserAssetRequests()
        {
            var UserId = Session["LoggedInUserId"].ToString();

            var model = new HomeModel();
            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getAssetRequestsByUserId";

                HttpResponseMessage Res = await client.PostAsJsonAsync(apiURL, UserId);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    var requestList = JsonConvert.DeserializeObject<List<Request>>(output);

                    foreach (var r in requestList)
                    {
                        var asset = new AssetInformationViewModel();
                        asset.AsserRequestId = r.Id;
                        asset.AssetName = r.Asset.Name;
                        asset.Duration = r.FromDate.ToShortDateString() + " - " + r.ToDate.ToShortDateString();
                        model.UserAssets.Add(asset);
                    }

                }
            }

            return PartialView("PartialViews/_assetData", model);
        }

        public async Task<JsonResult> loadAssets(string Type)
        {
            var model = new RequestModel();

            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = string.Empty;

                switch (Type)
                {
                    case "computer":
                        apiURL = "/api/Values/getComputers";
                        break;
                    case "software":
                        apiURL = "/api/Values/getSoftwares";
                        break;
                } 

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    var request = JsonConvert.DeserializeObject<List<Request>>(output);

                    
                }
            }

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/_requestModal", model)
            },
              JsonRequestBehavior.AllowGet
            );
        }

        //------- Helper Methods --------
        public string getURL()
        {
            var result = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
            return result;
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}

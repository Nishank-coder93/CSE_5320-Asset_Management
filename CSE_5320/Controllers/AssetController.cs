using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    public class AssetController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Requests(AssetViewModel model)
        {
            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getOpenAssetRequests";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    var requestList = JsonConvert.DeserializeObject<List<Request>>(output);

                    foreach (var r in requestList)
                    {
                        var asset = new AssetDetails();
                        asset.AssetId = r.Id;
                        asset.AssetName = r.Asset.Name;
                        asset.AssignedUserName = r.User.Name;
                        asset.Duration = r.FromDate.ToShortDateString() + " - " + r.ToDate.ToShortDateString();
                        model.AssetDetails.Add(asset);
                    }

                }
            }

            return View(model);
        }

        public async Task<JsonResult> Assetinfo(string id)
        {
            var model = new AssetInformationViewModel();

            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getAssetRequestById/" + id;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixResult(result);

                    var request = JsonConvert.DeserializeObject<Request>(output);

                    model.AsserRequestId = request.Id;
                    model.AssetName = request.Asset.Name;

                    if (request.Asset.ComputerId.HasValue)
                    {
                        model.AssetType = "Computer";
                        model.CpuName = request.Asset.Computer.Cpu.Name;
                        model.CpuVersion = request.Asset.Computer.Cpu.Version;
                        model.Memory = request.Asset.Computer.Memory.Name;
                        model.OsName = request.Asset.Computer.Os.Name + " " + request.Asset.Computer.Os.Version;
                        model.SerialNumber = request.Asset.Computer.SerialNumber;

                        if (request.Asset.Computer.TechnicalContact.HasValue)
                        {
                            model.TechnicalContact = request.Asset.Computer.Technical.Name;
                        }

                        model.WarrantyStatus = request.Asset.Computer.Status.Name;

                    }
                    else
                    {
                        model.AssetType = "Software";
                    }

                    model.RequestingUser = request.User.Name;
                    model.Duration = request.FromDate.ToShortDateString() +" - "+ request.ToDate.ToShortDateString();
                }
            }

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/_assetModal", model)
            },
              JsonRequestBehavior.AllowGet
            );
        }
        
        public async Task<ActionResult> confirmRequest(string id)
        {
            var result = false;

            var Baseurl = getURL();

            var model = new AssetViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/confirmRequests";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = id;

                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync(apiURL, content);
                if (Res.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            
            switch (result)
            {
                case true:
                    model.successMessage = true;
                    break;
                case false:
                    model.deniedMessage = true;
                    break;
                default:
                    model.errorMessage = true;
                    break;
            }

            return RedirectToAction("Requests", "Asset", model);
        }

        public async Task<ActionResult> denyRequest(string id)
        {
            var result = false;

            var Baseurl = getURL();

            var model = new AssetViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/denyRequests";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = id;

                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync(apiURL, content);
                if (Res.IsSuccessStatusCode)
                {
                    result = true;
                }
            }

            switch (result)
            {
                case true:
                    model.successMessage = true;
                    break;
                case false:
                    model.deniedMessage = true;
                    break;
                default:
                    model.errorMessage = true;
                    break;
            }

            return View("Requests", model);
        }

        public ActionResult Return()
        {
            return View();
        }

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
using CSE_5320.App_Start;
using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.Dashboard;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    [AuthorizationFilter]
    public class DashboardController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = new DasboardViewModel();
            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getDashboard";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixResult(result);

                    model = JsonConvert.DeserializeObject<DasboardViewModel>(output);
                }
            }

            if (TempData["Success"] != null && bool.Parse(TempData["Success"].ToString()) == true)
            {
                model.SuccessMessage = true;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsset(NewAssetModel model)
        {
            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/createAsset";

                var parameters = new Dictionary<string, string>();
                parameters["Name"] = model.Name;
                parameters["CPU"] = model.CPU;
                parameters["OS"] = model.OS;
                parameters["Memory"] = model.Memory;
                parameters["Date"] = null;
                if (model.ExpirationDateStatus)
                {
                    parameters["Date"] = model.ExpirationDate;
                }

                parameters["SerialNumber"] = model.SerialNumber;

                switch (model.Category)
                {
                    case true:
                        parameters["Category"] = "1";
                        break;
                    case false:
                        parameters["Category"] = "2";
                        break;
                }

                var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync(apiURL, content);
                if (Res.IsSuccessStatusCode)
                {
                    TempData["Success"] = true;
                }
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAsset(string Id)
        {
            var Baseurl = getURL();
            var result = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/deleteAsset";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = Id;

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
                    return Json("'Success':'true'");
                case false:
                    return Json("'Success':'false'");
                default:
                    return Json("'Success':'false'");
            }
        }

        public async Task<ActionResult> loadAssets()
        {
            var model = new DasboardViewModel();

            var result_model = new List<Asset>();
            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getAssets";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    result_model = JsonConvert.DeserializeObject<List<Asset>>(output);
                }
            }

            foreach (var r in result_model)
            {
                if(r.StatusId != 2)
                {
                    var asset = new AssetInformation();
                    asset.AssetId = r.Id;
                    asset.AssetName = r.Name;

                    model.AssetInformation.Add(asset);
                }
                
            }

            return PartialView("PartialViews/_assetList", model);
        }

        public async Task<ActionResult> newAsset()
        {
            var model = new NewAssetModel();
            var Baseurl = getURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getNewAsset";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixResult(result);

                    model = JsonConvert.DeserializeObject<NewAssetModel>(output);
                }
            }

            var result_cpu = new List<string>();
            var result_os = new List<string>();
            var result_memory = new List<string>();

            foreach (var c in model.CpuData)
            {
                result_cpu.Add(c.Name);
            }

            foreach (var o in model.OsData)
            {
                result_os.Add(o.Name);
            }

            foreach (var m in model.MemoryData)
            {
                result_memory.Add(m.Name);
            }

            model.CpuList = JsonConvert.SerializeObject(result_cpu);
            model.OsList = JsonConvert.SerializeObject(result_os);
            model.MemoryList = JsonConvert.SerializeObject(result_memory);

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/_newAssetModal", model)
            },
              JsonRequestBehavior.AllowGet
            );
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

                var apiURL = "/api/Values/getAssetById?Id=" + id;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixResult(result);

                    var request = JsonConvert.DeserializeObject<Asset>(output);

                    model.AsserRequestId = request.Id;
                    model.AssetName = request.Name;

                    if (request.ComputerId.HasValue)
                    {
                        model.AssetType = "Computer";
                        model.CpuName = request.Computer.Cpu.Name;
                        model.Memory = request.Computer.Memory.Name;
                        model.OsName = request.Computer.Os.Name;
                        model.SerialNumber = request.Computer.SerialNumber;
                    }
                    else
                    {
                        model.AssetType = "Software";
                        model.CpuName = request.Software.Cpu.Name;
                        model.Memory = request.Software.Memory.Name;
                        model.OsName = request.Software.Os.Name;
                        model.SerialNumber = request.Software.SerialNumber;
                    }
                }
            }

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/_assetInfo", model)
            },
              JsonRequestBehavior.AllowGet
            );
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

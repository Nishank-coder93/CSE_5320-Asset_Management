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

                var apiURL = "/api/Values/getAssetRequestsByUserId/"+ UserId;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
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

                        if (r.Asset.ComputerId.HasValue)
                        {
                            asset.AssetType = "Computer";
                        }
                        else if (r.Asset.SoftwareId.HasValue)
                        {
                            asset.AssetType = "Software";
                        }
                        
                        var fromDate = string.Empty;
                        var toDate = string.Empty;
                        var date = string.Empty;

                        if (r.FromDate.HasValue)
                        {
                            fromDate = r.FromDate.Value.ToShortDateString();
                            date = fromDate;
                            asset.FromDate = fromDate;
                        }

                        if (r.ToDate.HasValue)
                        {
                            toDate = r.ToDate.Value.ToShortDateString();
                            date += " - " + toDate;
                            asset.ToDate = toDate;
                        }
                        asset.Duration = date;
                        asset.StatusId = r.statusId;

                        if (asset.StatusId == 5 || asset.StatusId == 6)
                        {
                            model.UserAssets.Add(asset); 
                        }
                        else
                        {
                            model.UserAssetsHistory.Add(asset);
                        }

                    }

                }
            }

            return PartialView("PartialViews/_assetData", model);
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

                var apiURL = "/api/Values/getAssetRequestById?Id=" + id;

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
                        model.Memory = request.Asset.Computer.Memory.Name;
                        model.OsName = request.Asset.Computer.Os.Name;
                        model.SerialNumber = request.Asset.Computer.SerialNumber;

                        if (request.statusId != 5)
                        {
                            model.View = false;
                        }
                        else
                        {
                            model.View = true;
                        }
                    }
                    else
                    {
                        model.AssetType = "Software";
                        model.CpuName = request.Asset.Software.Cpu.Name;
                        model.Memory = request.Asset.Software.Memory.Name;
                        model.OsName = request.Asset.Software.Os.Name;
                        model.SerialNumber = request.Asset.Software.SerialNumber;
                        
                        if (request.statusId != 5)
                        {
                            model.View = false;
                        }
                        else
                        {
                            model.View = true;
                        }
                    }

                    var fromDate = string.Empty;
                    var toDate = string.Empty;
                    var date = string.Empty;

                    if (request.FromDate.HasValue)
                    {
                        fromDate = request.FromDate.Value.ToShortDateString();
                        date = fromDate;
                    }

                    if (request.ToDate.HasValue)
                    {
                        toDate = request.ToDate.Value.ToShortDateString();
                        date += " - " + toDate;
                    }

                    if (request.statusId == 5)
                    {
                        model.Duration = "Requested till "+toDate;
                    }
                    else
                    {
                        model.Duration = date;
                    } 
                    
                }
            }

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/_assetModal", model)
            },
              JsonRequestBehavior.AllowGet
            );
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
                        model.AssetType = "Computers";
                        apiURL = "/api/Values/getComputers";
                        break;
                    case "software":
                        model.AssetType = "Softwares";
                        apiURL = "/api/Values/getSoftwares";
                        break;
                } 

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = await Res.Content.ReadAsStringAsync();

                        var response = new ResponseHelper();
                        var output = response.fixListResult(result);

                        var request = JsonConvert.DeserializeObject<List<Asset>>(output);

                        foreach (var r in request)
                        {
                            var d = new Models.Home.AssetDetails();

                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            // Check only if the Asset is active
                            if (r.StatusId == 1)
                            {
                                var apiURL_request = "/api/Values/getAssetRequestById/" + r.Id;
                                Res = await client.GetAsync(apiURL_request);
                                if (Res.IsSuccessStatusCode)
                                {
                                    var result_asset = await Res.Content.ReadAsStringAsync();
                                    var response_asset = new ResponseHelper();
                                    var output_asset = response_asset.fixResult(result_asset);

                                    if (output_asset == "{ul}")
                                    {
                                        d.Available = true;
                                    }
                                    else
                                    {
                                        var request_asset = JsonConvert.DeserializeObject<Request>(output_asset);

                                        if (request_asset.statusId == 6)
                                        {
                                            d.Available = false;

                                            if (request_asset.ToDate.HasValue)
                                            {
                                                d.AvailableFrom = request_asset.ToDate.Value.ToShortDateString();
                                            }
                                            else
                                            {
                                                d.AvailableFrom = "Contact the administrator";
                                            }
                                        }
                                        else
                                        {
                                            d.Available = true;
                                        }
                                    }
                                }
                            } 

                            if (r.ComputerId.HasValue)
                            {
                                d.AssetId = r.Id;
                                d.AssetName = r.Name;

                                d.CpuName = r.Computer.Cpu.Name;
                                d.Memory = r.Computer.Memory.Name;
                                d.OsName = r.Computer.Os.Name;
                                d.SerialNumber = r.Computer.SerialNumber;

                            }
                            else if (r.SoftwareId.HasValue)
                            {
                                d.AssetId = r.Id;
                                d.AssetName = r.Name;

                                d.CpuName = r.Software.Cpu.Name;
                                d.Memory = r.Software.Memory.Name;
                                d.OsName = r.Software.Os.Name;
                                d.SerialNumber = r.Software.SerialNumber;
                            }

                            if (r.StatusId != 5)
                            {
                                d.View = false;
                            }
                            else
                            {
                                d.View = true;
                            }

                            model.AssetDetails.Add(d);
                        }
                    }
                    catch (Exception ex)
                    {

                    } 
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

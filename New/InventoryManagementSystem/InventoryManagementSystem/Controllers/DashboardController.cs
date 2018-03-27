using CSE_5320.App_Start;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models.Tables;
using InventoryManagementSystem.Models.ViewModels;
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
using System.Web.Script.Serialization;

namespace InventoryManagementSystem.Controllers
{
    [AuthorizationFilter]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        { 
            return View();
        }

        public async Task<ActionResult> LoadResourcesByFacility()
        {
            var model = new DashboardViewModel();
            var userId = int.Parse(Session["LoggedInUserId"].ToString());

            var Baseurl = GetURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetUserFacilitiesByUserId/" + userId;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    var data = JsonConvert.DeserializeObject<List<UserFacility>>(output);

                    foreach (var d in data)
                    {
                        var fr = new FacilityReport();
                        fr.FacilityId = d.FacilityId;
                        fr.FacilityName = d.Facility.Name;

                        using (var client_new = new HttpClient())
                        {
                            client_new.BaseAddress = new Uri(Baseurl);
                            client_new.DefaultRequestHeaders.Clear();
                            client_new.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var apiURL_new = "/api/Values/GetResourceVerificationByFacilityId/" + d.FacilityId;

                            HttpResponseMessage Res_new = await client_new.GetAsync(apiURL_new);
                            if (Res_new.IsSuccessStatusCode)
                            {
                                var result_new = await Res_new.Content.ReadAsStringAsync();

                                var response_new = new ResponseHelper();
                                var output_new = response_new.fixListResult(result_new);

                                var data_new = JsonConvert.DeserializeObject<List<ResourceReport>>(output_new);

                                foreach (var r in data_new)
                                {
                                    if (!r.Verified && !r.Missing)
                                    {
                                        fr.ResourceReport.Add(r);
                                    } 
                                }
                            }
                        }

                        model.FacilityReport.Add(fr);
                        
                    }
                }
            } 

            return PartialView("PartialViews/_data", model);
        }

        public async Task<ActionResult> UpdateQuantity(string Id)
        {
            var model = new DashboardViewModel();

            var Baseurl = GetURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetResourceById/" + Id;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixResult(result);

                    var data = JsonConvert.DeserializeObject<Resource>(output);
                    model.Quantity = data.Quantity;
                    model.Id = data.Id;
                }
            }

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/_updateQuantity", model)
            },
              JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public async Task<JsonResult> Update(string data)
        {
            var result = false;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var obj = serializer.Deserialize<Dictionary<string, object>>(data);

            var Id = string.Empty;
            var quantity = string.Empty;

            foreach (var o in obj)
            {
                switch (o.Key)
                {
                    case "Id":
                        Id = o.Value.ToString();
                        break;
                    case "quantity":
                        quantity = o.Value.ToString();
                        break;
                }
            }

            var Baseurl = GetURL();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/UpdateResource";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = Id;
                parameters["quantity"] = quantity;
                parameters["UserId"] = Session["LoggedInUserId"].ToString();

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

        [HttpPost]
        public async Task<JsonResult> ConfirmQuantity(string Id)
        {
            var result = false;

            var Baseurl = GetURL();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/ConfirmResource";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = Id;
                parameters["UserId"] = Session["LoggedInUserId"].ToString();

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

        [HttpPost]
        public async Task<JsonResult> MissingQuantity(string Id)
        {
            var result = false;

            var Baseurl = GetURL();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/MissingResource";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = Id;
                parameters["UserId"] = Session["LoggedInUserId"].ToString();

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

        public string GetURL()
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
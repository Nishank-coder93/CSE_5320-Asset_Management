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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

                                var data_new = JsonConvert.DeserializeObject<List<Report>>(output_new);

                                foreach (var r in data_new)
                                {
                                    var rr = new ResourceReport();
                                    rr.ResourceId = r.ResourceId;
                                    rr.Verified = r.Verify;
                                    rr.Missing = r.Missing;

                                    fr.ResourceReport.Add(rr);
                                }
                            }
                        }

                        model.FacilityReport.Add(fr);
                        
                    }
                }
            } 

            return PartialView("PartialViews/_data", model);
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
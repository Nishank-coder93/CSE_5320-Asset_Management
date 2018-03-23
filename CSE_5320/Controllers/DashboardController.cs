using CSE_5320.App_Start;
using CSE_5320.Helper;
using CSE_5320.Models.Dashboard;
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

            return View(model);
        }

        public async Task<ActionResult> loadAssets()
        {
            var model = new DasboardViewModel();

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

            foreach (var c in model.Cpu)
            {
                result_cpu.Add(c.Name);
            }

            foreach (var o in model.Os)
            {
                result_os.Add(o.Name);
            }

            foreach (var m in model.Memory)
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

using CSE_5320.Models;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    public class AssetController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var Baseurl = getURL();

            var model = new AssetViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/getAssetByUserId/?UserId=" + Session["LoggedInUserId"];

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();
                    var assetList = JsonConvert.DeserializeObject<List<Asset>>(result);

                    foreach (var a in assetList)
                    {
                        var asset = new AssetDetails();
                        asset.AssetId = a.Id;
                        asset.AssetName = a.Name;
                        asset.AssignedUserId = a.AssignedTo;

                        if (a.AssignedTo.HasValue)
                        {
                            asset.AssignedUserName = a.User.Name;
                        }
                        else
                        {
                            asset.AssignedUserName = string.Empty;
                        }

                        if (a.ReturnDate.HasValue)
                        {
                            asset.ReturnDate = a.ReturnDate.Value.ToShortDateString();
                        }

                        model.AssetDetails.Add(asset);
                    }
                }
            }

            return View(model);
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
    }
}
using CSE_5320.Models.Home;
using CSE_5320.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Web.Mvc;
using CSE_5320.Models.Login;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System.Web;
using CSE_5320.Models.Dashboard;

namespace CSE_5320.Controllers
{
    public class ValuesController : ApiController
    {
        public string login(LoginModel model)
        {
            var db = new Context();
            var user = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                var result = new UserViewModel();

                result.UserId = user.Id;
                result.UserName = user.Username;
                result.Name = user.Name;

                var response = JsonConvert.SerializeObject(result);

                return response;
            }
            else
            {
                return null;
            }
        }

        public string getDashboard()
        {
            var db = new Context();
            var assetRequestCount = db.Assets.Where(x => x.StatusId == 5).ToList().Count();
            var assetReturnConfirmationCount = db.Request.Where(x => x.statusId == 5).ToList().Count();

            var result = new DasboardViewModel();
            result.AssetRequestCount = assetRequestCount;
            result.AssetReturnConfirmationCount = assetReturnConfirmationCount;

            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getAssets()
        {
            var db = new Context();
            var result = db.Assets.ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getAssetById(int Id)
        {
            var db = new Context();
            var result = db.Assets.Where(x => x.Id == Id).FirstOrDefault();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getAssetRequestById(int Id)
        {
            var db = new Context();
            var result = db.Request.Where(x => x.Id == Id).FirstOrDefault();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getOpenAssetRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x=>x.statusId == 5).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getConfirmedAssetRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x => x.statusId == 6).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getDeniedAssetRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x => x.statusId == 7).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        [System.Web.Mvc.HttpPost]
        public bool confirmRequests()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);

            var Id = int.Parse(request_parse.FirstOrDefault().Value);
            var db = new Context();
            var confirm = db.Request.Where(x => x.Id == Id).FirstOrDefault();

            if (confirm != null)
            {
                confirm.statusId = 6;
                db.SaveChanges();

                return true;
            }

            return false;
        }

        [System.Web.Mvc.HttpPost]
        public bool denyRequests()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);

            var Id = int.Parse(request_parse.FirstOrDefault().Value);
            var db = new Context();
            var confirm = db.Request.Where(x => x.Id == Id).FirstOrDefault();

            if (confirm != null)
            {
                confirm.statusId = 7;
                db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}

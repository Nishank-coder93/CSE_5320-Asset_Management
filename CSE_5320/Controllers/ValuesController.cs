using CSE_5320.Helper;
using CSE_5320.Models;
using CSE_5320.Models.Dashboard;
using CSE_5320.Models.Login;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace CSE_5320.Controllers
{
    public class ValuesController : ApiController
    {
        public string login(LoginModel model)
        {
            var db = new Context();

            var user = db.Users.Where(x => x.Username == model.Username).FirstOrDefault();

            if (user != null)
            {
                var code = "teamseven";
                var hashKey = PasswordHelper.GetHashKey(code);
                var decrypt = PasswordHelper.Decrypt(hashKey, user.Password);
                if (decrypt == model.Password)
                {
                    var result = new UserViewModel();

                    result.UserId = user.Id;
                    result.UserName = user.Username;
                    result.Name = user.Name;
                    result.Role = user.RoleId;

                    var response = JsonConvert.SerializeObject(result);

                    return response;
                }

                return null;                
            }
            else
            {
                return null;
            }
        }

        public string getDashboard()
        {
            var db = new Context();
            var assetRequestCount = db.Request.Where(x => x.statusId == 5).ToList().Count();
            var assetReturnConfirmationCount = db.Request.Where(x => x.statusId == 8).ToList().Count();

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

        public string getComputers()
        {
            var db = new Context();
            var result = db.Assets.Where(x=>x.ComputerId.HasValue && x.StatusId != 2).ToList();
            var response = JsonConvert.SerializeObject(result); 
            return response;
        }

        public string getSoftwares()
        {
            var db = new Context();
            var result = db.Assets.Where(x => x.SoftwareId.HasValue && x.StatusId != 2).ToList();
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

        //--------- API's related to User requests ---------

        public string getAssetRequestsByUserId(string Id)
        {
            var db = new Context();
            var userId = int.Parse(Id);
            var result = db.Request.Where(x => x.RequestedUser == userId).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public void createAssetRequest(Request request)
        {
            var db = new Context();
            db.Request.Add(request);
            db.SaveChanges();
        }

        //--------- API's related to Asset requests ---------
        public string getAssetRequestById(string Id)
        {
            var db = new Context();
            int reqId = int.Parse(Id);
            var result = db.Request.Where(x => x.Id == reqId).FirstOrDefault();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getOpenAssetRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x => x.statusId == 5).ToList();
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
                confirm.FromDate = DateTime.Now;
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

        //--------- API's related asset returns ----------
        public string getOpenAssetReturnRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x => x.statusId == 8).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getConfirmedAssetReturnRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x => x.statusId == 9).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        public string getDeniedAssetReturnRequests()
        {
            var db = new Context();
            var result = db.Request.Where(x => x.statusId == 10).ToList();
            var response = JsonConvert.SerializeObject(result);

            return response;
        }

        [System.Web.Mvc.HttpPost]
        public void returnAsset()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);

            var Id = int.Parse(request_parse.FirstOrDefault().Value);
            var db = new Context();
            var asset = db.Request.Where(x => x.AssetId == Id).FirstOrDefault();
            if (asset != null)
            {
                asset.statusId = 8; 
            }

            db.SaveChanges();
        }

        //-----------API for a new Asset------------------

        public string getNewAsset()
        {
            var result = new NewAssetModel();

            var db = new Context();

            result.Cpu = db.Cpu.ToList();
            result.Os = db.Os.ToList();
            result.Memory = db.Memory.ToList();

            var response = JsonConvert.SerializeObject(result);

            return response;
        }


    }
}

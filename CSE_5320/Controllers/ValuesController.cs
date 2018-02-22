using CSE_5320.Models.Home;
using CSE_5320.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Web.Mvc;
using CSE_5320.Models.Login;
using CSE_5320.Models.ViewModels;
using Newtonsoft.Json;

namespace CSE_5320.Controllers
{
    public class ValuesController : ApiController
    {
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


        public string getAssetByUserId(int UserId)
        {
            var db = new Context();
            var assets = db.Assets.ToList();

            var result = new List<Asset>();

            foreach (var a in assets)
            {
                if (a.AssignedTo.HasValue)
                {
                    if (a.AssignedTo.Value == UserId)
                    {
                        result.Add(a);
                    }
                }
            }

            var response = JsonConvert.SerializeObject(result);

            return response;
        }

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
    }
}

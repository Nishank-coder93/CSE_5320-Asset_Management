using CSE_5320.Models.Home;
using CSE_5320.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq; 
using System.Web.Mvc;
using CSE_5320.Models.Login;

namespace CSE_5320.Controllers
{
    public class ValuesController : ApiController
    {
        public List<Asset> getAssets()
        {
            var db = new Context();
            return db.Assets.ToList();
        }

        public Asset getAssetById(int Id)
        {
            var db = new Context();
            return db.Assets.Where(x=>x.Id == Id).FirstOrDefault();
        }

        public bool login(LoginModel model)
        {
            var db = new Context();
            var user = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

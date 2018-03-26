using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagementSystem.Controllers
{
    public class ValuesController : ApiController
    {
        public string Login(LoginViewModel model)
        {
            var db = new Context();

            var user = db.User.Where(x => x.UserName == model.Username).FirstOrDefault();

            if (user != null)
            {
                var code = "teamseven";
                var hashKey = PasswordHelper.GetHashKey(code);
                var decrypt = PasswordHelper.Decrypt(hashKey, user.Password);
                if (decrypt == model.Password)
                {
                    var result = new UserViewModel();

                    result.UserId = user.Id;
                    result.UserName = user.UserName;
                    result.Name = user.Name;

                    var role = db.UserRole.Where(x => x.UserId == user.Id).FirstOrDefault();

                    if (role != null)
                    {
                        result.RoleId = role.RoleId;
                    }

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
    }
}

﻿using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.Tables;
using InventoryManagementSystem.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
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

        public string GetUsers()
        {
            var db = new Context();
            var result = new List<UserManagementModel>();

            var users = db.User.ToList();

            foreach(var u in users)
            {
                var user = new UserManagementModel();
                user.Id = u.Id;
                user.Name = u.Name;
                user.Email = u.Email;
                user.Status = u.Active;

                var roles = db.UserRole.Where(x => x.UserId == u.Id).ToList();

                foreach(var r in roles)
                {
                    if(user.Roles == string.Empty)
                    {
                        user.Roles = r.Role.Name;
                    }
                    else
                    {
                        user.Roles += ", "+r.Role.Name;
                    }
                }

                var facilities = db.UserFacility.Where(x => x.UserId == u.Id).ToList();

                foreach (var f in facilities)
                {
                    if (user.Facilities == string.Empty)
                    {
                        user.Facilities = f.Facility.Name;
                    }
                    else
                    {
                        user.Facilities += ", " + f.Facility.Name;
                    }
                }

                result.Add(user);
            }

            var response = JsonConvert.SerializeObject(result);
            return response;
        }

        public string GetFacilities()
        {
            var db = new Context();

            var result = db.Facility.ToList();

            var response = JsonConvert.SerializeObject(result);
            return response;
        }

        [HttpPost]
        public bool NewUser()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);

            var db = new Context();

            var user = new User();
            var facilities = new ArrayList();
            var roles = new ArrayList();

            foreach (var r in request_parse)
            {
                switch (r.Key)
                {
                    case "name":
                        user.Name = r.Value;
                        break;
                    case "email":
                        user.Email = r.Value;
                        break;
                    case "facilityList":
                        facilities = JsonConvert.DeserializeObject<ArrayList>(r.Value); 
                        break;
                    case "roleList":
                        roles = JsonConvert.DeserializeObject<ArrayList>(r.Value);
                        break;
                }
            }

            user.Password = GeneratePassword(); 
            db.User.Add(user);
            SendEmail(user.Email,user.Password);

            foreach (var f in facilities)
            {
                var uf = new UserFacility();
                uf.UserId = user.Id;
                uf.FacilityId = int.Parse(f.ToString());

                db.UserFacility.Add(uf);
            }

            foreach (var r in roles)
            {
                var ur = new UserRole();
                ur.UserId = user.Id;
                ur.RoleId = int.Parse(r.ToString());

                db.UserRole.Add(ur);
            }

            db.SaveChanges();

            return true;
        }

        private string GeneratePassword()
        {
            var result = string.Empty;

            return result;
        }

        private void SendEmail(string email, string password)
        {
            //todo
        }
    }
}

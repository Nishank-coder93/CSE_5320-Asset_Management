﻿using CSE_5320.App_Start;
using InventoryManagementSystem.Helpers;
using InventoryManagementSystem.Models.Tables;
using InventoryManagementSystem.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace InventoryManagementSystem.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LoadUserManagement()
        {
            var model = new HomeViewModel();

            var Baseurl = GetURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetUsers";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    model.UserManagementData = JsonConvert.DeserializeObject<List<UserManagementModel>>(output);
                }
            }

            return PartialView("PartialViews/User_Management/_data", model);
        }

        public async Task<ActionResult> NewUser()
        {
            var model = new NewUserModel();
            var Baseurl = GetURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetFacilities";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    model.Facilities = JsonConvert.DeserializeObject<List<Facility>>(output);
                }
            }

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/User_Management/_newUserModal", model)
            },
              JsonRequestBehavior.AllowGet
            );
        }

        public async Task<ActionResult> EditUser(string Id)
        {
            var model = new NewUserModel();
            var Baseurl = GetURL();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetFacilities";

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    model.Facilities = JsonConvert.DeserializeObject<List<Facility>>(output);
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetUser/"+Id;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixResult(result);

                    var userDetails = JsonConvert.DeserializeObject<User>(output);
                    model.Id = userDetails.Id;
                    model.Email = userDetails.Email;
                    model.Name = userDetails.Name;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetUserFacilitiesByUserId/" + Id;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    var userDetails = JsonConvert.DeserializeObject<List<UserFacility>>(output);
                    foreach (var u in userDetails)
                    {
                        model.SelectedFacilities.Add(u.FacilityId);
                    } 
                }
            }

            //model.SelectedFacilities_json = JsonConvert.SerializeObject(model.SelectedFacilities);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/GetUserRolesByUserId/" + Id;

                HttpResponseMessage Res = await client.GetAsync(apiURL);
                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();

                    var response = new ResponseHelper();
                    var output = response.fixListResult(result);

                    var userDetails = JsonConvert.DeserializeObject<List<UserRole>>(output);
                    foreach (var u in userDetails)
                    {
                        model.SelectedRoles.Add(u.RoleId);
                        model.RoleId = u.RoleId;
                    }
                }
            }

            //model.SelectedRoles_json = JsonConvert.SerializeObject(model.SelectedRoles);

            return Json(new
            {
                LocationModal = RenderRazorViewToString("PartialViews/User_Management/_editUserModal", model)
            },
              JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public async Task<JsonResult> CreateUser(string data)
        {
            var result = false;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var obj = serializer.Deserialize<Dictionary<string, object>>(data);

            var name = string.Empty;
            var email = string.Empty;
            var roleList = new ArrayList();
            var facilityList = new ArrayList();

            foreach (var o in obj)
            {
                switch (o.Key)
                {
                    case "name":
                        name = o.Value.ToString();
                        break;
                    case "email":
                        email = o.Value.ToString();
                        break;
                    case "list_facilities":
                        facilityList = (ArrayList)o.Value;
                        break;
                    case "list_roles":
                        roleList = (ArrayList)o.Value;
                        break;
                }
            }

            var Baseurl = GetURL();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/NewUser";

                var parameters = new Dictionary<string, string>();
                parameters["name"] = name;
                parameters["email"] = email;
                parameters["roleList"] = JsonConvert.SerializeObject(roleList);
                parameters["facilityList"] = JsonConvert.SerializeObject(facilityList);

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
        public async Task<JsonResult> UpdateUser(string data)
        {
            var result = false;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var obj = serializer.Deserialize<Dictionary<string, object>>(data);

            var id = string.Empty;
            var name = string.Empty;
            var email = string.Empty;
            var roleList = new ArrayList();
            var facilityList = new ArrayList();

            foreach (var o in obj)
            {
                switch (o.Key)
                {
                    case "Id":
                        id = o.Value.ToString();
                        break;
                    case "name":
                        name = o.Value.ToString();
                        break;
                    case "email":
                        email = o.Value.ToString();
                        break;
                    case "list_facilities":
                        facilityList = (ArrayList)o.Value;
                        break;
                    case "list_roles":
                        roleList = (ArrayList)o.Value;
                        break;
                }
            }

            var Baseurl = GetURL();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiURL = "/api/Values/UpdateUser";

                var parameters = new Dictionary<string, string>();
                parameters["Id"] = id;
                parameters["name"] = name;
                parameters["email"] = email;
                parameters["roleList"] = JsonConvert.SerializeObject(roleList);
                parameters["facilityList"] = JsonConvert.SerializeObject(facilityList);

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

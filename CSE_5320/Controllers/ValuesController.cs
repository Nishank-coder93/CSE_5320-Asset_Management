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
            var result = db.Assets.Where(x => x.ComputerId.HasValue && x.StatusId != 2).ToList();
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
        public bool createAsset()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);

            var db = new Context();
            var asset = new Asset();

            var cpuId = 0;
            var osId = 0;
            var memoryId = 0;
            int? computerId = null;
            int? softwareId = null;
            var serialNumber = string.Empty;

            foreach (var r in request_parse)
            {
                switch (r.Key)
                {
                    case "Name":
                        asset.Name = r.Value;
                        break;
                    case "CPU":
                        var cpu = db.Cpu.Where(x => x.Name == r.Key).FirstOrDefault();
                        if (cpu != null)
                        {
                            cpuId = cpu.Id;
                        }
                        else
                        {
                            var newCpu = new Cpu();
                            newCpu.Name = r.Value;
                            db.Cpu.Add(newCpu);
                            db.SaveChanges();

                            cpuId = newCpu.Id;
                        }
                        break;
                    case "OS":
                        var os = db.Os.Where(x => x.Name == r.Key).FirstOrDefault();
                        if (os != null)
                        {
                            osId = os.Id;
                        }
                        else
                        {
                            var newOs = new Os();
                            newOs.Name = r.Value;
                            db.Os.Add(newOs);
                            db.SaveChanges();

                            osId = newOs.Id;
                        }
                        break;
                    case "Memory":
                        var memory = db.Memory.Where(x => x.Name == r.Key).FirstOrDefault();
                        if (memory != null)
                        {
                            memoryId = memory.Id;
                        }
                        else
                        {
                            var newMemory = new Memory();
                            newMemory.Name = r.Value;
                            db.Memory.Add(newMemory);
                            db.SaveChanges();

                            memoryId = newMemory.Id;
                        }
                        break;
                    case "SerialNumber":
                        serialNumber = r.Value;
                        break;
                    case "Category":
                        switch (r.Value)
                        {
                            case "1":
                                var Computer = new Computer();
                                Computer.SerialNumber = serialNumber;
                                Computer.MemoryId = memoryId;
                                Computer.OsId = osId;
                                Computer.CategoryId = 1;
                                Computer.CpuId = cpuId;

                                db.Computer.Add(Computer);
                                db.SaveChanges();

                                computerId = Computer.Id;

                                break;
                            case "2":
                                var Software = new Software();
                                Software.MemoryId = memoryId;
                                Software.OsId = osId;
                                Software.CpuId = cpuId;
                                Software.CategoryId = 2;

                                db.Software.Add(Software);
                                db.SaveChanges();

                                softwareId = Software.Id;
                                break;
                        }
                        break;
                }
            }

            asset.ComputerId = computerId;
            asset.SoftwareId = softwareId;
            asset.StatusId = 1;

            db.Assets.Add(asset);
            db.SaveChanges();

            return true;
        }

        [System.Web.Mvc.HttpPost]
        public bool editAsset()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);

            var db = new Context();
            var asset = new Asset();

            var cpuId = 0;
            var osId = 0;
            var memoryId = 0;
            var serialNumber = string.Empty;
            var assetId = 0;

            foreach (var r in request_parse)
            {
                switch (r.Key)
                {
                    case "Id":
                        assetId = int.Parse(r.Value);
                        break;
                    case "CPU":
                        var cpu = db.Cpu.Where(x => x.Name == r.Key).FirstOrDefault();
                        if (cpu != null)
                        {
                            cpuId = cpu.Id;
                        }
                        else
                        {
                            var newCpu = new Cpu();
                            newCpu.Name = r.Value;
                            db.Cpu.Add(newCpu);
                            db.SaveChanges();

                            cpuId = newCpu.Id;
                        }
                        break;
                    case "OS":
                        var os = db.Os.Where(x => x.Name == r.Key).FirstOrDefault();
                        if (os != null)
                        {
                            osId = os.Id;
                        }
                        else
                        {
                            var newOs = new Os();
                            newOs.Name = r.Value;
                            db.Os.Add(newOs);
                            db.SaveChanges();

                            osId = newOs.Id;
                        }
                        break;
                    case "Memory":
                        var memory = db.Memory.Where(x => x.Name == r.Key).FirstOrDefault();
                        if (memory != null)
                        {
                            memoryId = memory.Id;
                        }
                        else
                        {
                            var newMemory = new Memory();
                            newMemory.Name = r.Value;
                            db.Memory.Add(newMemory);
                            db.SaveChanges();

                            memoryId = newMemory.Id;
                        }
                        break;
                    case "SerialNumber":
                        serialNumber = r.Value;
                        break;
                }
            }

            var editAsset = db.Assets.Where(x => x.Id == assetId).FirstOrDefault();
            if (editAsset != null)
            {
                if (editAsset.ComputerId.HasValue)
                {
                    var editComputer = db.Computer.Where(x => x.Id == editAsset.ComputerId.Value).FirstOrDefault();
                    if(editComputer != null)
                    {
                        editComputer.CpuId = cpuId;
                        editComputer.OsId = osId;
                        editComputer.SerialNumber = serialNumber;
                        editComputer.MemoryId = memoryId;
                    }
                }
                else
                {
                    var editSoftware = db.Software.Where(x => x.Id == editAsset.SoftwareId.Value).FirstOrDefault();
                    if (editSoftware != null)
                    {
                        editSoftware.CpuId = cpuId;
                        editSoftware.OsId = osId;
                        editSoftware.SerialNumber = serialNumber;
                        editSoftware.MemoryId = memoryId;
                    }
                }

                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        [System.Web.Http.HttpPost]
        public bool deleteAsset()
        {
            var request = Request.Content.ReadAsStringAsync().Result;
            var request_parse = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var id = 0;
            foreach (var r in request_parse)
            {
                switch (r.Key)
                {
                    case "Id":
                        id = int.Parse(r.Value);
                        break;

                }
            }

            var db = new Context();
            var asset = db.Assets.Where(x => x.Id == id).FirstOrDefault();
            if (asset != null)
            {
                asset.StatusId = 2;
            }

            db.SaveChanges();

            return true;
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

            result.CpuData = db.Cpu.ToList();
            result.OsData = db.Os.ToList();
            result.MemoryData = db.Memory.ToList();

            var response = JsonConvert.SerializeObject(result);

            return response;
        }


    }
}

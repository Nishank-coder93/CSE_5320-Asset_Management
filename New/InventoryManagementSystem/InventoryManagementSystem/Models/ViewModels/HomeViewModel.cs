using InventoryManagementSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            UserManagementData = new List<UserManagementModel>();
            FacilityManagementData = new List<FacilityManagementModel>();
        }

        public List<UserManagementModel> UserManagementData { get; set; }
        public List<FacilityManagementModel> FacilityManagementData { get; set; }
    }

    public class UserManagementModel
    {
        public UserManagementModel()
        {
            Roles = string.Empty;
            Facilities = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Facilities { get; set; }
        public bool Status { get; set; }
    }

    public class NewUserModel
    {
        public NewUserModel()
        {
            Facilities = new List<Facility>();
        }

        public string Name { get; set;}
        public string Email { get; set; }
        public List<Facility> Facilities { get; set; }
    }

    public class FacilityManagementModel
    {

    }
}
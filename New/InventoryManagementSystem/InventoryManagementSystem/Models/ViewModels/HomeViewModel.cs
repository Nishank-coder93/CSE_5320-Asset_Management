using InventoryManagementSystem.Models.Tables;
using System;
using System.Collections;
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
            SelectedFacilities = new List<int>();
            SelectedRoles = new List<int>();
        }

        public int Id { get; set; }
        public string Name { get; set;}
        public string Email { get; set; }
        public int RoleId { get; set; }
        public List<Facility> Facilities { get; set; }
        public List<int> SelectedFacilities { get; set; }
        public List<int> SelectedRoles { get; set; }
    }

    public class NewFacilityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set;}
    }

    public class FacilityManagementModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
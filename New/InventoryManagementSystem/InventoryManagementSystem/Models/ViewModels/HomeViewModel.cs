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
            UserListData = new List<String>();
            ReportData = new List<ReportModel>();
            UserManagementData = new List<UserManagementModel>();
            FacilityManagementData = new List<FacilityManagementModel>();
            ResourceManagmentData = new List<ResourceModel>();
            FacilityId = 0;
        }

        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public List<String> UserListData { get; set; }
        public List<ReportModel> ReportData { get; set; }
        public List<ResourceModel> ResourceManagmentData { get; set; }
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
        public int NumberOfResources { get; set; }
    }

    public class ResourceModel
    {
        public int FacilityId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class ReportModel
    {
        public ReportModel()
        {
            ResourceReport = new List<ResourceReportModel>();
        }

        public int FacilityId { get; set; }
        public List<ResourceReportModel> ResourceReport { get; set; }
    }

    public class ResourceReportModel
    {
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string Status { get; set; }
        public bool Verified { get; set; }
        public bool Missing { get; set; }
        public int? QuantityChange { get; set; }
        public int? MissingQuantity { get; set; }
        public string UserName { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            FacilityReport = new List<FacilityReport>();
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public List<FacilityReport> FacilityReport { get; set; }
    }

    public class FacilityReport
    {
        public FacilityReport()
        {
            ResourceReport = new List<ResourceReport>();
        }

        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public List<ResourceReport> ResourceReport { get; set; }

    }

    public class ResourceReport
    {
        public string Message { get; set; }
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public bool Verified { get; set; }
        public bool Missing { get; set; }
        public int Quantity { get; set; }
    }
}
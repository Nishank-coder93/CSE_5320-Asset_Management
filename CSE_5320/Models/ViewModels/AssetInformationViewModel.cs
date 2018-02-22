using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.ViewModels
{
    public class AssetInformationViewModel
    { 
        public string AssetName { get; set; }

        public string AssetType { get; set; }

        public string CpuName { get; set; }

        public string CpuVersion { get; set; }

        public string Memory { get; set; }

        public string OsName { get; set; }

        public string SerialNumber { get; set; }

        public string Status { get; set; }

        public string TechnicalContact { get; set; }

        public string WarrantyStatus { get; set; }

        public string RequestingUser { get; set; }

        public string Duration { get; set; }
    }
    
}
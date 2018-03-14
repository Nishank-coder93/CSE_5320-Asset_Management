using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.Home
{
    public class RequestModel
    {
        public RequestModel()
        {
            AssetDetails = new List<AssetDetails>();
        }

        public string AssetType { get; set; }

        public List<AssetDetails> AssetDetails { get; set; }
    }

    public class AssetDetails
    {
        public AssetDetails()
        {
            Available = false;
        }

        public int AssetId { get; set; }

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

        public bool View { get; set; }

        public bool Available { get; set; }

        public string AvailableFrom { get; set; }
    }
}
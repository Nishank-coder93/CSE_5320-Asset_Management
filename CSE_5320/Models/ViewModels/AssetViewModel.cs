using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.ViewModels
{
    public class AssetViewModel
    {
        public AssetViewModel()
        {
            AssetDetails = new List<AssetDetails>();
        }

        public bool successMessage { get; set; }

        public bool deniedMessage { get; set; }

        public bool errorMessage { get; set; }

        public List<AssetDetails> AssetDetails { get; set; }
    }

    public class AssetDetails
    {
        public int AssetId { get; set; }

        public string AssetName { get; set; }

        public string Duration { get; set; }

        public string AssignedUserName { get; set; }
    }
}
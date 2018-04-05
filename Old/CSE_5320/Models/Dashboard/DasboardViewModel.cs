﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.Dashboard
{
    public class DasboardViewModel
    {
        public DasboardViewModel()
        {
            AssetInformation = new List<AssetInformation>();

        }
        public int AssetRequestCount { get; set; }

        public int AssetReturnConfirmationCount { get; set; }

        public List<AssetInformation> AssetInformation { get; set; }

        public bool SuccessMessage { get; set; }

        public bool EditSuccessMessage { get; set; }

        public bool DeleteSuccessMessage { get; set; }

    }

    public class AssetInformation
    {
        public int AssetId { get; set; }

        public string AssetName { get; set; }
    }
}
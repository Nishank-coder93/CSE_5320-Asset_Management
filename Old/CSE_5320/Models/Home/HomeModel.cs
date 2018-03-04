using CSE_5320.Models.Error;
using CSE_5320.Models.ViewModels;
using System.Collections.Generic;

namespace CSE_5320.Models.Home
{
    public class HomeModel : ErrorModel
    {
        public HomeModel()
        {
            UserAssets = new List<AssetInformationViewModel>();
            UserAssetsHistory = new List<AssetInformationViewModel>();
        }

        public List<AssetInformationViewModel> UserAssets { get; set; }
        public List<AssetInformationViewModel> UserAssetsHistory { get; set; }
    }
}
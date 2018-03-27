using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.Dashboard
{
    public class EditAssetModel
    {
        public EditAssetModel()
        {
            CpuData = new List<Cpu>();
            OsData = new List<Os>();
            MemoryData = new List<Memory>();
        }

        public List<Cpu> CpuData { get; set; }
        public List<Os> OsData { get; set; }
        public List<Memory> MemoryData { get; set; }

        public string CpuList { get; set; }
        public string OsList { get; set; }
        public string MemoryList { get; set; }

        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string SerialNumber { get; set; }
        public string Cpu { get; set; }
        public string OS { get; set; }
        public string Memory { get; set; }
    }
}
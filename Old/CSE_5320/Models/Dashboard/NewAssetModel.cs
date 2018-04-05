using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.Dashboard
{
    public class NewAssetModel
    {
        public NewAssetModel()
        {
            CpuData = new List<Cpu>();
            OsData = new List<Os>();
            MemoryData = new List<Memory>();

            Category = true;
        }

        public List<Cpu> CpuData { get; set; }
        public List<Os> OsData { get; set; }
        public List<Memory> MemoryData { get; set; }

        public string CpuList { get; set; }
        public string OsList { get; set; }
        public string MemoryList { get; set; }

        public string CPU { get; set; }
        public string OS { get; set; }
        public string Memory { get; set; }
        public string Name { get; set; }
        public bool Category { get; set; }
        public bool ExpirationDateStatus { get; set; }
        public string SerialNumber { get; set; }
        public string ExpirationDate { get; set; }
    }

}
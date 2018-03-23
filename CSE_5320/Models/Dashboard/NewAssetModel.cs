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
            Cpu = new List<Cpu>();
            Os = new List<Os>();
            Memory = new List<Memory>();
        }

        public List<Cpu> Cpu { get; set; }
        public List<Os> Os { get; set; }
        public List<Memory> Memory { get; set; }

        public string CpuList { get; set; }
        public string OsList { get; set; }
        public string MemoryList { get; set; }
    }

}
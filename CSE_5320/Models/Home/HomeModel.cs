using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.Home
{
    public class HomeModel
    {
        public HomeModel()
        {
            Map = new List<Map>();
        }
        public List<Map> Map { get; set; }
    }

    public class Map
    {
        public string id { get; set; }
        public int value { get; set; }
    }
}
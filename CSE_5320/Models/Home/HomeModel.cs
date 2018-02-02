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
            MapLocations = new List<MapLocations>();
        }
        public List<Map> Map { get; set; }
        public List<MapLocations> MapLocations { get; set; }
    }

    public class Map
    {
        public string id { get; set; }
        public int value { get; set; }
    }

    public class MapLocations
    {
        public MapLocations()
        {
            type = "circle";
            alpha = 0.5;
        }
        public string type { get; set; }
        public double alpha { get; set; }
        public string title { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
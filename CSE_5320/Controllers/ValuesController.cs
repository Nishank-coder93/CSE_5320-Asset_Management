using CSE_5320.Models.Home;
using CSE_5320.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq; 
using System.Web.Mvc;

namespace CSE_5320.Controllers
{
    public class ValuesController : ApiController
    {
        public HomeModel getMapLocations()
        {
            var result = new HomeModel();

            Context db = new Context();
            var states = db.State.ToList(); 
            var locations = db.Locations.ToList();

            foreach(var s in states)
            {
                var m = new Map();
                m.id = s.Code;
                m.value = 0;
                result.Map.Add(m);
            }

            foreach (var l in locations)
            {
                var ml = new MapLocations();
                ml.title = l.Name;
                ml.latitude = l.Latitude;
                ml.longitude = l.Longitude; 
                result.MapLocations.Add(ml);

                foreach (var r in result.Map)
                {
                    if (r.id == l.State.Code)
                    {
                        r.value += 1;
                    }
                }
            }

            return result;
        } 
    }
}

using CSE_5320.Models.Error;
using System.Collections.Generic;

namespace CSE_5320.Models.Home
{
    public class HomeModel : ErrorModel
    {
        public HomeModel()
        {
           
        }

        public ErrorModel Error { get; set; }
    }
}
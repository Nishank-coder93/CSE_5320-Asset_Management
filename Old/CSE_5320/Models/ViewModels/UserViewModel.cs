using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public int Role { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class User : Base
    { 
        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }
    }
}
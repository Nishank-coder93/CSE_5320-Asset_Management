using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class User : Base
    { 
        public string Username { get; set; }

        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
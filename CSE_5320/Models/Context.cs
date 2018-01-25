using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class Context : DbContext
    {
        public Context() : base("name=Cse5320") { }

        public virtual DbSet<User> Users { get; set; }
    }
}
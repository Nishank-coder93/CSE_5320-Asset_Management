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

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentAsset> DepartmentAssets { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Maintainance> Maintainance { get; set; }
    }
}
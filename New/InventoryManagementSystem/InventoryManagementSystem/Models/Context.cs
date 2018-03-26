using InventoryManagementSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Context : DbContext
    {
        public Context() : base("name=Cse5320") { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        public virtual DbSet<Facility> Facility { get; set; }
        public virtual DbSet<UserFacility> UserFacility { get; set; }

        public virtual DbSet<Resource> Resource { get; set; }
        public IEnumerable<object> Users { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
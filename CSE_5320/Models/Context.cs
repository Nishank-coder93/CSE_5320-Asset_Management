using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CSE_5320.Models
{
    public class Context : DbContext
    {
        public Context() : base("name=Cse5320") { }

        public virtual DbSet<Asset> Assets { get; set; } 
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Cpu> Cpu { get; set; }
        public virtual DbSet<Os> Os { get; set; }
        public virtual DbSet<Memory> Memory { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Computer> Computer { get; set; }
        public virtual DbSet<Software> Software { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
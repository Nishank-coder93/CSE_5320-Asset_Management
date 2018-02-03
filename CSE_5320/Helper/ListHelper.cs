using CSE_5320.Models;
using CSE_5320.Models.Home;
using System.Collections.Generic;

namespace CSE_5320.Helper
{
    public class ListHelper
    { 
        public List<Status> StatusHelper()
        {
            var result = new List<Status>();

            var status_1 = new Status();
            status_1.Id = 1;
            status_1.Name = "Active";
            result.Add(status_1);

            var status_2 = new Status();
            status_2.Id = 2;
            status_2.Name = "InActive";
            result.Add(status_2);

            return result;
        }

        public List<Category> CategoryHelper()
        {
            var result = new List<Category>();

            return result;
        }

        public List<Department> DepartmentHelper()
        {
            var result = new List<Department>();
            
            return result;
        }

        public List<Asset> AssetHelper()
        {
            var result = new List<Asset>();

            return result;
        }

        public List<Os> OsHelper()
        {
            var result = new List<Os>();

            return result;
        }

        public List<Cpu> CpuHelper()
        {
            var result = new List<Cpu>();

            return result;
        }

        public List<Memory> MemoryHelper()
        {
            var result = new List<Memory>();

            return result;
        }

        public List<Computer> ComputerHelper()
        {
            var result = new List<Computer>();

            return result;
        }

        public List<Software> SoftwareHelper()
        {
            var result = new List<Software>();

            return result;
        }

        public List<Role> RoleHelper()
        {
            var result = new List<Role>();

            var role_1 = new Role();
            role_1.Id = 1;
            role_1.Name = "Admin";
            result.Add(role_1);

            return result;
        }

        public List<User> UserHelper()
        {
            var result = new List<User>();

            var user_1 = new User();
            user_1.Id = 1;
            user_1.Name = "Admin";
            user_1.Username = "Admin";
            user_1.Password = "Admin"; // Need to hash
            user_1.RoleId = 1;
            result.Add(user_1);

            return result;
        }
    }
}
 
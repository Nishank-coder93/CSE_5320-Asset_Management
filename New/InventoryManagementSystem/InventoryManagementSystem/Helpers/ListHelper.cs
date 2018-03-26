using InventoryManagementSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Helpers
{
    public class ListHelper
    {
        public List<User> UserList()
        {
            var result = new List<User>();

            var user_1 = new User();
            user_1.Id = 1;
            user_1.Name = "Admin";
            user_1.UserName = "admin";
            user_1.Password = "admin";
            user_1.Active = true;
            result.Add(user_1);

            return result;
        }

        public List<Role> RoleList()
        {
            var result = new List<Role>();

            var role_1 = new Role();
            role_1.Id = 1;
            role_1.Name = "Admin";
            result.Add(role_1);

            return result;
        }

        public List<UserRole> UserRoleList()
        {
            var result = new List<UserRole>();

            var userRole_1 = new UserRole();
            userRole_1.Id = 1;
            userRole_1.UserId = 1;
            userRole_1.RoleId = 1;
            result.Add(userRole_1);

            return result;
        }
    }
}

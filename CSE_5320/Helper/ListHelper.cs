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

            var cat_1 = new Category();
            cat_1.Id = 1;
            cat_1.Name = "Computer";
            cat_1.StatusId = 1;
            result.Add(cat_1);

            return result;
        }

        public List<Asset> AssetHelper()
        {
            var result = new List<Asset>();

            var asset_1 = new Asset();
            asset_1.Id = 1;
            asset_1.Name = "Dell 15R2";
            asset_1.CategoryId = 1;
            asset_1.StatusId = 1;
            result.Add(asset_1);

            return result;
        }

        public List<Role> RoleHelper()
        {
            var result = new List<Role>();

            var role_1 = new Role();
            role_1.Id = 1;
            role_1.Name = "Admin";
            role_1.StatusId = 1;
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
            user_1.StatusId = 1;
            result.Add(user_1);

            return result;
        }
    }
}
 
using CSE_5320.Models;
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
        public List<Location> LocationHelper()
        {
            var result = new List<Location>();

            var loc_1 = new Location();
            loc_1.Id = 1;
            loc_1.Name = "University of Texas at Arlington";
            loc_1.Latitude = (float)32.7299;
            loc_1.Longitude = (float)97.1140;
            loc_1.StatusId = 1;
            result.Add(loc_1);

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

        public List<Department> DepartmentHelper()
        {
            var result = new List<Department>();

            var dep_1 = new Department();
            dep_1.Id = 1;
            dep_1.Name = "Engineering";
            dep_1.LocationId = 1;
            dep_1.StatusId = 1;
            result.Add(dep_1);

            return result;
        }

        public List<DepartmentAsset> DepartmentAssetHelper()
        {
            var result = new List<DepartmentAsset>();

            var da_1 = new DepartmentAsset();
            da_1.Id = 1;
            da_1.Name = "Front Desk";
            da_1.DepartmentId = 1;
            da_1.AssetId = 1;
            da_1.StatusId = 1;
            result.Add(da_1);

            return result;
        }

        public List<Role> RoleHelper()
        {
            var result = new List<Role>();

            var role_1 = new Role();
            role_1.Id = 1;
            role_1.Name = "IT Asset Manager";
            role_1.StatusId = 1;
            result.Add(role_1);

            var role_2 = new Role();
            role_2.Id = 2;
            role_2.Name = "Employee";
            role_2.StatusId = 1;
            result.Add(role_2);

            var role_3 = new Role();
            role_3.Id = 3;
            role_3.Name = "Maintance";
            role_3.StatusId = 1;
            result.Add(role_3);

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

        public List<Maintainance> MaintainanceHelper()
        {
            var result = new List<Maintainance>();

            return result;
        }
    }
}
 
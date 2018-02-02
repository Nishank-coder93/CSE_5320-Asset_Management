﻿using CSE_5320.Models;
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

        public List<Map> MapHelper()
        {
            var result = new List<Map>();

            var state_1 = new Map();
            state_1.id = "US-AL";
            state_1.value = 0;
            result.Add(state_1);

            var state_2 = new Map();
            state_2.id = "US-AK";
            state_2.value = 0;
            result.Add(state_2);

            var state_3 = new Map();
            state_3.id = "US-AZ";
            state_3.value = 0;
            result.Add(state_3);

            var state_4 = new Map();
            state_4.id = "US-AR";
            state_4.value = 0;
            result.Add(state_4);

            var state_5 = new Map();
            state_5.id = "US-CA";
            state_5.value = 0;
            result.Add(state_5);

            var state_6 = new Map();
            state_6.id = "US-CO";
            state_6.value = 0;
            result.Add(state_6);

            var state_7 = new Map();
            state_7.id = "US-CT";
            state_7.value = 0;
            result.Add(state_7);

            var state_8 = new Map();
            state_8.id = "US-DE";
            state_8.value = 0;
            result.Add(state_8);

            var state_9 = new Map();
            state_9.id = "US-FL";
            state_9.value = 0;
            result.Add(state_9);

            var state_10 = new Map();
            state_10.id = "US-GA";
            state_10.value = 0;
            result.Add(state_10);

            var state_11 = new Map();
            state_11.id = "US-HI";
            state_11.value = 0;
            result.Add(state_11);

            var state_12 = new Map();
            state_12.id = "US-ID";
            state_12.value = 0;
            result.Add(state_12);

            var state_13 = new Map();
            state_13.id = "US-IL";
            state_13.value = 0;
            result.Add(state_13);

            var state_14 = new Map();
            state_14.id = "US-IN";
            state_14.value = 0;
            result.Add(state_14);

            var state_15 = new Map();
            state_15.id = "US-IA";
            state_15.value = 0;
            result.Add(state_15);

            var state_16 = new Map();
            state_16.id = "US-KS";
            state_16.value = 0;
            result.Add(state_16);

            var state_17 = new Map();
            state_17.id = "US-KY";
            state_17.value = 0;
            result.Add(state_17);

            var state_18 = new Map();
            state_18.id = "US-LA";
            state_18.value = 0;
            result.Add(state_18);

            var state_19 = new Map();
            state_19.id = "US-ME";
            state_19.value = 0;
            result.Add(state_19);

            var state_20 = new Map();
            state_20.id = "US-MD";
            state_20.value = 0;
            result.Add(state_20);

            var state_21 = new Map();
            state_21.id = "US-MA";
            state_21.value = 0;
            result.Add(state_21);

            var state_22 = new Map();
            state_22.id = "US-MI";
            state_22.value = 0;
            result.Add(state_22);

            var state_23 = new Map();
            state_23.id = "US-MN";
            state_23.value = 0;
            result.Add(state_23);

            var state_24 = new Map();
            state_24.id = "US-MS";
            state_24.value = 0;
            result.Add(state_24);

            var state_25 = new Map();
            state_25.id = "US-MO";
            state_25.value = 0;
            result.Add(state_25);

            var state_26 = new Map();
            state_26.id = "US-MT";
            state_26.value = 0;
            result.Add(state_26);

            var state_27 = new Map();
            state_27.id = "US-NE";
            state_27.value = 0;
            result.Add(state_27);

            var state_28 = new Map();
            state_28.id = "US-NV";
            state_28.value = 0;
            result.Add(state_28);

            var state_29 = new Map();
            state_29.id = "US-NH";
            state_29.value = 0;
            result.Add(state_29);

            var state_30 = new Map();
            state_30.id = "US-NJ";
            state_30.value = 0;
            result.Add(state_30);

            var state_31 = new Map();
            state_31.id = "US-NM";
            state_31.value = 0;
            result.Add(state_31);

            var state_32 = new Map();
            state_32.id = "US-NY";
            state_32.value = 0;
            result.Add(state_32);

            var state_33 = new Map();
            state_33.id = "US-NC";
            state_33.value = 0;
            result.Add(state_33);

            var state_34 = new Map();
            state_34.id = "US-ND";
            state_34.value = 0;
            result.Add(state_34);

            var state_35 = new Map();
            state_35.id = "US-OH";
            state_35.value = 0;
            result.Add(state_35);

            var state_36 = new Map();
            state_36.id = "US-OK";
            state_36.value = 0;
            result.Add(state_36);

            var state_37 = new Map();
            state_37.id = "US-OR";
            state_37.value = 0;
            result.Add(state_37);

            var state_38 = new Map();
            state_38.id = "US-PA";
            state_38.value = 0;
            result.Add(state_38);

            var state_39 = new Map();
            state_39.id = "US-RI";
            state_39.value = 0;
            result.Add(state_39);

            var state_40 = new Map();
            state_40.id = "US-SC";
            state_40.value = 0;
            result.Add(state_40);

            var state_41 = new Map();
            state_41.id = "US-SD";
            state_41.value = 0;
            result.Add(state_41);

            var state_42 = new Map();
            state_42.id = "US-TN";
            state_42.value = 0;
            result.Add(state_42);

            var state_43 = new Map();
            state_43.id = "US-TX";
            state_43.value = 0;
            result.Add(state_43);

            var state_44 = new Map();
            state_44.id = "US-UT";
            state_44.value = 0;
            result.Add(state_44);

            var state_45 = new Map();
            state_45.id = "US-VT";
            state_45.value = 0;
            result.Add(state_45);

            var state_46 = new Map();
            state_46.id = "US-VA";
            state_46.value = 0;
            result.Add(state_46);

            var state_47 = new Map();
            state_47.id = "US-WA";
            state_47.value = 0;
            result.Add(state_47);

            var state_48 = new Map();
            state_48.id = "US-WV";
            state_48.value = 0;
            result.Add(state_48);

            var state_49 = new Map();
            state_49.id = "US-WI";
            state_49.value = 0;
            result.Add(state_49);

            var state_50 = new Map();
            state_50.id = "US-WY";
            state_50.value = 0;
            result.Add(state_50);

            return result;
        }
    }
}
 
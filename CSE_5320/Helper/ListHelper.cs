using CSE_5320.Models;
using CSE_5320.Models.Home;
using System;
using System.Collections.Generic;
using System.Text;

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
            status_2.Name = "In Active";
            result.Add(status_2);

            var status_3 = new Status();
            status_3.Id = 3;
            status_3.Name = "Available";
            result.Add(status_3);

            var status_4 = new Status();
            status_4.Id = 4;
            status_4.Name = "Returned";
            result.Add(status_4);

            var status_5 = new Status();
            status_5.Id = 5;
            status_5.Name = "Requested";
            result.Add(status_5);

            var status_6 = new Status();
            status_6.Id = 6;
            status_6.Name = "Approved";
            result.Add(status_6);

            var status_7 = new Status();
            status_7.Id = 7;
            status_7.Name = "Denied";
            result.Add(status_7);

            var status_8 = new Status();
            status_8.Id = 8;
            status_8.Name = "Asset Return requested";
            result.Add(status_8);

            var status_9 = new Status();
            status_9.Id = 9;
            status_9.Name = "Asset Return Confirmed";
            result.Add(status_9);

            var status_10 = new Status();
            status_10.Id = 10;
            status_10.Name = "Asset Return Denied";
            result.Add(status_10);

            var status_11 = new Status();
            status_11.Id = 11;
            status_11.Name = "canceled";
            result.Add(status_11);

            return result;
        }

        public List<Category> CategoryHelper()
        {
            var result = new List<Category>();

            var c1 = new Category();
            c1.Id = 1;
            c1.Name = "Computer";
            result.Add(c1);

            var c2 = new Category();
            c2.Id = 2;
            c2.Name = "Software";
            result.Add(c2);

            return result;
        }

        public List<Department> DepartmentHelper()
        {
            var result = new List<Department>();
            
            return result;
        } 

        public List<Os> OsHelper()
        {
            var result = new List<Os>();

            var os_1 = new Os();
            os_1.Id = 1;
            os_1.Name = "Windows 10";
            result.Add(os_1);

            var os_2 = new Os();
            os_2.Id = 2;
            os_2.Name = "Mac X";
            result.Add(os_2);

            return result;
        }

        public List<Cpu> CpuHelper()
        {
            var result = new List<Cpu>();

            var cpu_1 = new Cpu();
            cpu_1.Id = 1;
            cpu_1.Name = "Intel Core i3";
            result.Add(cpu_1);

            var cpu_2 = new Cpu();
            cpu_2.Id = 2;
            cpu_2.Name = "Intel Core i5";
            result.Add(cpu_2);

            var cpu_3 = new Cpu();
            cpu_3.Id = 3;
            cpu_3.Name = "Intel Core i7";
            result.Add(cpu_3);

            return result;
        }

        public List<Memory> MemoryHelper()
        {
            var result = new List<Memory>();

            var m1 = new Memory();
            m1.Id = 1;
            m1.Name = "1 Gb";
            result.Add(m1);

            var m2 = new Memory();
            m2.Id = 2;
            m2.Name = "2 Gb";
            result.Add(m2);

            var m3 = new Memory();
            m3.Id = 3;
            m3.Name = "3 Gb";
            result.Add(m3);

            var m4 = new Memory();
            m4.Id = 4;
            m4.Name = "4 Gb";
            result.Add(m4);

            var m5 = new Memory();
            m5.Id = 5;
            m5.Name = "6 Gb";
            result.Add(m5);

            var m6 = new Memory();
            m6.Id = 6;
            m6.Name = "8 Gb";
            result.Add(m6);

            var m7 = new Memory();
            m7.Id = 7;
            m7.Name = "16 Gb";
            result.Add(m7);

            var m8 = new Memory();
            m8.Id = 8;
            m8.Name = "32 Gb";
            result.Add(m8);

            return result;
        }

        public List<Computer> ComputerHelper()
        {
            var result = new List<Computer>();

            var c1 = new Computer();
            c1.Id = 1;
            c1.SerialNumber = serialNumberGenerator();
            c1.MemoryId = 8;
            c1.OsId = 1;
            c1.CategoryId = 1;
            c1.CpuId = 3;
            result.Add(c1);

            var c2 = new Computer();
            c2.Id = 2;
            c2.SerialNumber = serialNumberGenerator();
            c2.MemoryId = 6;
            c2.OsId = 2;
            c2.CategoryId = 1;
            c2.CpuId = 2;
            result.Add(c2);

            return result;
        }

        public List<Software> SoftwareHelper()
        {
            var result = new List<Software>();

            var s1 = new Software();
            s1.Id = 1;
            s1.MemoryId = 1;
            s1.OsId = 1;
            s1.CpuId = 1;
            s1.CategoryId = 2;
            s1.ExpiryDate = null;
            result.Add(s1);


            return result;
        }

        public List<Asset> AssetHelper()
        {
            var result = new List<Asset>();

            var a1 = new Asset();
            a1.Id = 1;
            a1.Name = "Dell 15r2";
            a1.TimesUsed = 0;
            a1.ComputerId = 1;
            a1.SoftwareId = null;
            a1.StatusId = 1;
            result.Add(a1);

            var a2 = new Asset();
            a2.Id = 2;
            a2.Name = "Dell XPS";
            a2.TimesUsed = 0;
            a2.ComputerId = 2;
            a2.SoftwareId = null;
            a2.StatusId = 1;
            result.Add(a2);

            var a3 = new Asset();
            a3.Id = 3;
            a3.Name = "HP";
            a3.TimesUsed = 0;
            a3.ComputerId = 2;
            a3.SoftwareId = null;
            a3.StatusId = 1;
            result.Add(a3);

            var a4 = new Asset();
            a4.Id = 4;
            a4.Name = "Adobe Photoshop";
            a4.TimesUsed = 0;
            a4.ComputerId = null;
            a4.SoftwareId = 1;
            a4.StatusId = 1;
            result.Add(a4);

            return result;
        }

        public List<Role> RoleHelper()
        {
            var result = new List<Role>();

            var role_1 = new Role();
            role_1.Id = 1;
            role_1.Name = "Admin";
            result.Add(role_1);

            var role_2 = new Role();
            role_2.Id = 2;
            role_2.Name = "Employee";
            result.Add(role_2);

            return result;
        }

        public List<User> UserHelper()
        {
            var result = new List<User>();

            var user_1 = new User();
            user_1.Id = 1;
            user_1.Name = "Admin";
            user_1.Username = "admin";
            user_1.Password = "admin";
            user_1.RoleId = 1;
            result.Add(user_1);

            var user_2 = new User();
            user_2.Id = 2;
            user_2.Name = "Neil";
            user_2.Username = "Neil";
            user_2.Password = "neil";
            user_2.RoleId = 2;
            result.Add(user_2);

            var user_3 = new User();
            user_3.Id = 3;
            user_3.Name = "Nishank";
            user_3.Username = "Nishank";
            user_3.Password = "nishank";
            user_3.RoleId = 2;
            result.Add(user_3);

            var user_4 = new User();
            user_4.Id = 4;
            user_4.Name = "Sid";
            user_4.Username = "Sid";
            user_4.Password = "sid";
            user_4.RoleId = 2;
            result.Add(user_4);

            var user_5 = new User();
            user_5.Id = 5;
            user_5.Name = "Vibhor";
            user_5.Username = "Vibhor";
            user_5.Password = "vibhor";
            user_5.RoleId = 2;
            result.Add(user_5);

            return result;
        }

        public List<Request> RequestHelper()
        {
            var result = new List<Request>();

            var request_1 = new Request();
            request_1.Id = 1;
            request_1.RequestedUser = 2;
            request_1.AssetId = 1;
            request_1.FromDate = null;
            request_1.ToDate = new DateTime(2018,8,12);
            request_1.statusId = 5;
            result.Add(request_1);

            var request_2 = new Request();
            request_2.Id = 2;
            request_2.RequestedUser = 3;
            request_2.AssetId = 2;
            request_2.FromDate = new DateTime(2018, 8, 12);
            request_2.ToDate = new DateTime(2018, 8, 20);
            request_2.statusId = 6;
            result.Add(request_2);

            return result;
        }

        public string serialNumberGenerator()
        {
            Guid theGuid;
            var guidString = string.Empty; 

            theGuid = Guid.NewGuid();
            guidString = theGuid.ToString().Replace("-", "").Substring(0, 16);

            string s1, s2, s3, s4;
            s1 = guidString.Substring(0, 4);
            s2 = guidString.Substring(4, 4);
            s3 = guidString.Substring(8, 4);
            s4 = guidString.Substring(12, 4);
            string serial = s1 + "-" + s2 + "-" + s3 + "-" + s4;

            return serial;
        }

        public string AssetCodeGenerator()
        {
            Guid theGuid;
            var guidString = string.Empty;

            theGuid = Guid.NewGuid();
            guidString = theGuid.ToString().Replace("-", "").Substring(0, 16);

            string s1, s2, s3, s4;
            s1 = guidString.Substring(0, 4);
            s2 = guidString.Substring(4, 4);
            s3 = guidString.Substring(8, 4);
            s4 = guidString.Substring(12, 4);

            string serial = "ASSET_"+ s1;

            return serial;
        }

        public string PasswordHash(string input)
        {
            return input;
        }
    }
}
 
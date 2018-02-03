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
            status_2.Name = "InActive";
            result.Add(status_2);

            var status_3 = new Status();
            status_3.Id = 3;
            status_3.Name = "Expired";
            result.Add(status_3);

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
            os_1.Name = "Windows";
            os_1.Version = "10";
            result.Add(os_1);

            var os_2 = new Os();
            os_2.Id = 2;
            os_2.Name = "Mac";
            os_2.Version = "X";
            result.Add(os_2);

            return result;
        }

        public List<Cpu> CpuHelper()
        {
            var result = new List<Cpu>();

            var cpu_1 = new Cpu();
            cpu_1.Id = 1;
            cpu_1.Name = "Intel";
            cpu_1.Version = "Core i3";
            result.Add(cpu_1);

            var cpu_2 = new Cpu();
            cpu_2.Id = 2;
            cpu_2.Name = "Intel";
            cpu_2.Version = "Core i5";
            result.Add(cpu_2);

            var cpu_3 = new Cpu();
            cpu_3.Id = 3;
            cpu_3.Name = "Intel";
            cpu_3.Version = "Core i7";
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
            c1.Name = "Dell 15";
            c1.Id = 1;
            c1.SerialNumber = serialNumberGenerator();
            c1.MemoryId = 8;
            c1.OsId = 1;
            c1.CategoryId = 1;
            c1.CpuId = 3;
            c1.WarrantyStatus = 1;
            c1.AssignedTo = null;
            c1.TechnicalContact = 2;
            result.Add(c1);

            var c2 = new Computer();
            c2.Name = "Mac";
            c2.Id = 2;
            c2.SerialNumber = serialNumberGenerator();
            c2.MemoryId = 6;
            c2.OsId = 2;
            c2.CategoryId = 1;
            c2.CpuId = 2;
            c2.WarrantyStatus = 3;
            c2.AssignedTo = null;
            c2.TechnicalContact = 2;
            result.Add(c2);

            return result;
        }

        public List<Software> SoftwareHelper()
        {
            var result = new List<Software>();

            var s1 = new Software();
            s1.Id = 1;
            s1.Name = "";
            s1.MemoryId = 1;
            s1.OsId = 1;
            s1.CpuId = 1;
            s1.CategoryId = 2;
            s1.AssignedTo = null;
            s1.TechnicalContact = 2;
            s1.StatusId = 1;
            s1.ExpiryDate = null;
            result.Add(s1);


            return result;
        }

        public List<Asset> AssetHelper()
        {
            var result = new List<Asset>();

            var a1 = new Asset();
            a1.Id = 1;
            a1.Name = AssetCodeGenerator();
            a1.TimesUsed = 0;
            a1.ComputerId = 1;
            a1.SoftwareId = null;
            a1.StatusId = 1;
            result.Add(a1);

            var a2 = new Asset();
            a2.Id = 2;
            a2.Name = AssetCodeGenerator();
            a2.TimesUsed = 0;
            a2.ComputerId = 2;
            a2.SoftwareId = null;
            a2.StatusId = 1;
            result.Add(a1);

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
            user_1.Username = "Admin";
            user_1.Password = PasswordHash("Admin");
            user_1.RoleId = 1;
            result.Add(user_1);

            var user_2 = new User();
            user_2.Id = 2;
            user_2.Name = "Neil";
            user_2.Username = "Neil";
            user_2.Password = PasswordHash("Neil");
            user_2.RoleId = 2;
            result.Add(user_2);

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
 
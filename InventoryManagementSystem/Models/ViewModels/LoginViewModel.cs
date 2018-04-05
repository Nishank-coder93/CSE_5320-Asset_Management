using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            Login = true;
        }
        public string Username { get; set; }

        public string Password { get; set; }

        public bool Login { get; set; }
    }
}
using CSE_5320.Models.Error;

namespace CSE_5320.Models.Login
{
    public class LoginModel : ErrorModel
    {
        public LoginModel()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
        public string Username { get; set; }

        public string Password { get; set; }

        public ErrorModel Error { get; set; }
    }
}
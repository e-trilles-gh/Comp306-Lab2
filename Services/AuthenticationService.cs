using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public string CurrentUserId { get; private set; }

        public string CurrentUserName { get; private set; }

        public bool Login(string username, string password)
        {
            CurrentUserName = username;

            if (username == "eskim@gmail.com" && password == "eskim123")
            {
                CurrentUserId = "user-001";
                return true;
            }

            if (username == "john@gmail.com" && password == "john123")
            {
                CurrentUserId = "user-002";
                return true;
            }

            if (username == "jane@gmail.com" && password == "jane123")
            {
                CurrentUserId = "user-003";
                return true;
            }

            CurrentUserName = null;
            CurrentUserId = null;
            return false;
        }

        public void Logout()
        {
            CurrentUserId = null;
        }
    }
}

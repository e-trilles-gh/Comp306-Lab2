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

        public bool Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                CurrentUserId = "user-001";
                return true;
            }

            if (username == "bob" && password == "bob123")
            {
                CurrentUserId = "user-002";
                return true;
            }

            return false;
        }

        public void Logout()
        {
            CurrentUserId = null;
        }
    }
}

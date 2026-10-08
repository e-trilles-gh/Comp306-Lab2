using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                return true;
            }
            return false;
        }
    }
}

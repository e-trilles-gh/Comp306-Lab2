using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Services
{
    public interface IAuthenticationService
    {
        bool Login(string username, string password);
    }
}

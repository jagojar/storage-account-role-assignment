using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aadSignUpApiConnectorApp.Services
{
    public static class AuthenticationService
    {
        public static bool ValidateUser(string username, string password)
        {
            if (username == "uservalidator" && password == "P@ssW0rd987!!")
            {
                return true;
            }

            return false;
        }

        public static bool UserInValidDomain(string email)
        {
            string domain = email.Split('@')[1];

            if (domain == "yopmail.com")
                return true;

            return false;
        }
    }
}

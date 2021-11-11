using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aadSignUpApiConnectorApp.Models
{
    public class UserValidationModel
    {
        public string email { get; set; }
        public string displayName { get; set; }
        public string country { get; set; }
    }
}

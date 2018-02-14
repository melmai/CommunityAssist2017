using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityAssist2017.Models
{
    public class LoginClass
    {
        // Properties
        public string UserName { get; set; }
        public string Password { get; set; }

        // Emtpy Constructor
        // Required by MVC
        public LoginClass() { }

        // Constructor with username and password as arguments
        public LoginClass(string user, string password)
        {
            UserName = user;
            Password = password;
        }
    }
}
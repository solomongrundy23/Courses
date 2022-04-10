using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Auth
    {
        public Auth() { }
        public Auth(string username, string password) 
        { 
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public static Auth Admin => new Auth("admin", "secret");
        public static Auth Random => new Auth((new Faker()).Internet.UserName(), (new Faker()).Internet.Password());
    }
}

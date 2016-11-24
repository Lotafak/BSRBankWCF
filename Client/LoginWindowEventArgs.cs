using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class LoginWindowEventArgs : EventArgs
    {
        public LoginWindowEventArgs(string password, string username)
        {
            Password = password;
            Username = username;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}

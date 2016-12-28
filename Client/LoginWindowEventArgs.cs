using System;

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
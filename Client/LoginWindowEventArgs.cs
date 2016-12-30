using System;

namespace Client
{
    public class LoginWindowEventArgs : EventArgs
    {
        public LoginWindowEventArgs(string credentials)
        {
            Credentials = credentials;
        }

        public string Credentials { get; private set; }
    }
}
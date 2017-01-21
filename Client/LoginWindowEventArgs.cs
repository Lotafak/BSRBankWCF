using System;

namespace Client
{
    /// <summary>
    /// Event args for handling login
    /// </summary>
    public class LoginWindowEventArgs : EventArgs
    {
        /// <summary>
        /// Initializing <see cref="LoginWindowEventArgs"/> with credential string
        /// </summary>
        /// <param name="credentials">Base64 envoded users credentials</param>
        public LoginWindowEventArgs(string credentials)
        {
            Credentials = credentials;
        }

        /// <summary>
        /// Base64 encoded users credentials
        /// </summary>
        public string Credentials { get; private set; }
    }
}
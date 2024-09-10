using System;

namespace Steam_Desktop_Authenticator.Exceptions
{
    internal class LoginException : Exception
    {
        public bool MoveAuthenticator { get; set; }
    }
}

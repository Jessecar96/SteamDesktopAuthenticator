using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Steam_Desktop_Authenticator
{
    public class DeleteAllCookiesVisitor : ICookieVisitor
    {
        public void Dispose()
        {

        }

        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            deleteCookie = true;
            return true;
        }
    }
}

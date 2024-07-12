using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Constants
{
    public static class CookieKey
    {
        public static class Autentication
        {
            public static string UserProfile { get { return "Cookie.Autentication.UserProfile"; } }            
        }
        public static class General
        {            
            public static string ProjectID { get { return "Cookie.Autentication.ProjectID"; } }
        }
    }
}

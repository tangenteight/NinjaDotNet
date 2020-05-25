using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDotNet.Client.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:44395/";

        public static string BlogsEndpoint = $"{BaseUrl}api/Blogs/";
        public static string BlogCommentsEndpoint = $"{BaseUrl}api/BlogComments/";

        public static string RegisterEndpoint = $"{BaseUrl}api/users/register/";
        public static string LoginEndpoint = $"{BaseUrl}api/users/login/";
        public static string LogoutEndpoint = $"{BaseUrl}api/users/logout/";
    }
}

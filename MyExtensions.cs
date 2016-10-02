using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtensionMethods
{

    /// <summary>
    /// Summary description for MyExtensions
    /// </summary>

        public static class MyExtensions
        {
            public static string userHtmlEncode(this Object str)
            {
                return HttpContext.Current.Server.HtmlEncode(str.ToString());
            }
            public static string userHtmlEncode(this string str)
            {
                return HttpContext.Current.Server.HtmlEncode(str);
            }
        }
   
}
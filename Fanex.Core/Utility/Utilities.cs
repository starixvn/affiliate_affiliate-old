using System.Web;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace Fanex.Core.Utility
{
    public static class Utilities
    {       
        public static string GetIPAddressOfClient()
        {
            string ip;
            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }
            return ip;
        }
        public static Int64 GetIPValue()
        {
            string ip = GetIPAddressOfClient();
            if (string.IsNullOrEmpty(ip) || ip == "127.0.0.1")
            {
                return 0;
            }
            else
            {
                IList<string> parts = GetStringIList(ip, ".");
                return 16777216 * int.Parse(parts[0]) + 65536 * int.Parse(parts[1]) + 256 * int.Parse(parts[2]) + int.Parse(parts[3]);
            }
        }
        public static IList<string> GetStringIList(string stringArray, string sep)
        {
            string[] separator = { sep };
            string[] result = stringArray.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            IList<string> data = new List<string>();
            foreach (var item in result)
            {
                data.Add(item);
            }
            return data;
        }
        public static string GetKeyword(Uri urlReferrer)
        {
            if (urlReferrer != null)
            {
                System.Collections.Specialized.NameValueCollection parameters = HttpUtility.ParseQueryString(urlReferrer.Query);
                if (urlReferrer.Host.Contains("google.com") ||
                    urlReferrer.Host.Contains("bing.com") ||
                    urlReferrer.Host.Contains("ask.com"))
                {
                    return parameters["q"];
                }
                else if (urlReferrer.Host.Contains("yahoo.com"))
                {
                    return parameters["p"];
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static RegionInfo ResolveCountry()
        {
            CultureInfo culture = ResolveCulture();
            if (culture != null)
                return new RegionInfo(culture.LCID);

            return null;
        }

        public static CultureInfo ResolveCulture()
        {
            string[] languages = HttpContext.Current.Request.UserLanguages;

            if (languages == null || languages.Length == 0)
                return null;

            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static string GetDeviceName(string userAgent)
        {
            string device = "Computer/Laptop";
            if (string.IsNullOrEmpty(userAgent))
                return device;

            if (userAgent.Contains("iPhone"))
            {
                device = "iPhone";
            }
            if (userAgent.Contains("iPad"))
            {
                device = "iPad";
            }
            if (userAgent.Contains("iPod"))
            {
                device = "iPod";
            }

            if (userAgent.Contains("BlackBerry"))
            {
                device = "BlackBerry";
            }

            if (userAgent.Contains("HTC"))
            {
                device = "HTC";
            }

            if (userAgent.Contains("Motorola"))
            {
                device = "Motorola";
            }

            if (userAgent.Contains("Nokia"))
            {
                device = "Nokia";
            }

            return device;
        }

        public static string GetOSName(string userAgent)
        {
            string os = string.Empty;
            if (string.IsNullOrEmpty(userAgent))
                return os;
            //microsoft
            if (userAgent.Contains("Windows NT 6.1"))
            {
                os = "Windows 7";
            }
            else if (userAgent.Contains("Windows NT 6.0"))
            {
                os = "Windows Vista";
            }
            else if (userAgent.Contains("Windows NT 5.1"))
            {
                os = "Windows XP";
            }
            else if (userAgent.Contains("Windows NT 5.0"))
            {
                os = "Windows 2000";
            }
            else if (userAgent.Contains("Windows NT 4.1"))
            {
                os = "Windows 98";
            }
            //apple
            else if (userAgent.Contains("iPhone") ||
                        userAgent.Contains("iPad") ||
                        userAgent.Contains("iPod"))
            {
                os = "iOS";
            }
            else if (userAgent.Contains("Mac OS"))
            {
                os = "Mac OS";
            }
            //RIM
            else if (userAgent.Contains("BlackBerry"))
            {
                os = "BlackBerry";
            }
            //Google
            else if (userAgent.Contains("Android"))
            {
                os = "Android";
            }
            //Linux
            else if (userAgent.Contains("Linux"))
            {
                os = "Linux";
            }
            else if (userAgent.Contains("Ubuntu"))
            {
                os = "Ubuntu";
            }
            return os;

        }
    }
}

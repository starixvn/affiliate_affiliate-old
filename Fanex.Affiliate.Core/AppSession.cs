// -----------------------------------------------------------------------
// <copyright file="Session.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System.Web;
    using DNAide.Core;

    #region ---------- for Session-------------

    public static class AppSession
    {
     
        public static string Currency
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["Currency"], "USD");
            }
            set
            {
                HttpContext.Current.Session["Currency"] = value;
            }
        }

        public static string Language
        {
            get
            {                
                
                return CastAide.AsString(HttpContext.Current.Session["Culture"], AppSettings.DefaultLanguage);
            }
            set
            {
                HttpContext.Current.Session["Culture"] = value;
                HttpContext.Current.Session["Language"] = value;            
            }
        }

        public static string Flag
        {
            get
            {
                string f = CastAide.AsString(HttpContext.Current.Session["Flag"], AppSettings.DefaultLanguage);
                return f;
            }
            set
            {
                HttpContext.Current.Session["Flag"] = value;
            }
        }

        public static string CaptchaCode
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["CaptchaCode"]);
            }
            set
            {
                HttpContext.Current.Session["CaptchaCode"] = value;
            }
        }
        
        public static int CustID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["CustID"], -1);
            }
            set
            {
                HttpContext.Current.Session["CustID"] = value;
            }
        }

        public static int OnUser
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["On_User"], -1);
            }
            set
            {
                HttpContext.Current.Session["On_User"] = value;
            }
        }
        public static bool IsAuthenticated
        {
            get
            {
                return (HttpContext.Current.Session["CustID"] != null);
            }
        }
        public static string UserName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["UserName"]); }
            set { HttpContext.Current.Session["UserName"] = value; }
        }
        public static string FirstName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["FirstName"]); }
            set { HttpContext.Current.Session["FirstName"] = value; }
        }
        public static string LastName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["LastName"]); }
            set { HttpContext.Current.Session["LastName"] = value; }
        }
        public static string Email
        {
            get { return CastAide.AsString(HttpContext.Current.Session["Email"]); }
            set { HttpContext.Current.Session["Email"] = value; }
        }

         public static string Prefix//Prefix before money
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["Prefix"], "$");
            }
            set
            {
                HttpContext.Current.Session["Prefix"] = value;
            }
        }
        


    }


    #endregion
}


// -----------------------------------------------------------------------
// <copyright file="Session.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Customer
{
    using System.Web;
    using DNAide.Core;
    using System;

    #region ---------- for Session-------------

    public static class AffSession
    {
        public static string TrackingCode
        {
            get
            {
                if (HttpContext.Current.Session["TrackingCode"] != null)
                    return CastAide.AsString(HttpContext.Current.Session["TrackingCode"]);
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["TrackingCode"] = value;
            }
        }

       
        public static bool IsInternalSite
        {
            get
            {
                try
                {
                    return CastAide.AsBoolean(HttpContext.Current.Session["IsInternalSite"]);
                }
                catch
                {
                    return false;
                }
            }
            set { HttpContext.Current.Session["IsInternalSite"] = value; }
        }

        /*20090202@DungTong add to check where the this aff has secSuriry question yet */
        public static bool HasSecurityQuestion
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["HasSecurityQuestion"]); }
            set { HttpContext.Current.Session["HasSecurityQuestion"] = value; }
        }
        static bool fromHome = false;
        public static bool IsFromHome
        {
            get { return fromHome; }
            set { fromHome = value; }
        }
        static bool firsTimeLoginSucc = false;
        public static bool FirsTimeLoginSucc
        {
            get { return firsTimeLoginSucc; }
            set { firsTimeLoginSucc = value; }
        }
        //20100305@Johnny add to get mail option of aff(plain text or html format)
        public static bool MailOption
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["MailOption"]); }
            set { HttpContext.Current.Session["MailOption"] = value; }
        }
        public static string PreferLang
        {
            get { return CastAide.AsString(HttpContext.Current.Session["PreferLang"], "en-US"); }
            set { HttpContext.Current.Session["PreferLang"] = value; }
        }
        public static Int16 AffType
        {
            get { return CastAide.AsInt16(HttpContext.Current.Session["AffType"], 1); }
            set { HttpContext.Current.Session["AffType"] = value; }
        }
       
    }


    #endregion
}

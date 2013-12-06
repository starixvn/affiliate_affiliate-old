// -----------------------------------------------------------------------
// <copyright file="Session.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin
{
    using System.Web;
    using DNAide.Core;
    using System;


    #region ---------- for Session-------------

    public static class AdminSession
    {

        public static string AffCurrency
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["AffCurrency"], String.Empty);

            }
            set
            {
                HttpContext.Current.Session["AffCurrency"] = value;
            }

        }

        public static int UserID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["UserID"], -1);

            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }

        }

        public static int RoleID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["RoleID"], -1);

            }
            set
            {
                HttpContext.Current.Session["RoleID"] = value;
            }

        }

        public static string RoleAccess
        {
            get
            {
                return  CastAide.AsString(HttpContext.Current.Session["RoleAccess"],string.Empty);
            }
            set
            {
                HttpContext.Current.Session["RoleAccess"] = value;
            }
        }
        public static int AffId
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["AffId"]);
            }
            set
            {
                HttpContext.Current.Session["AffId"] = value;
            }
        }
        public static string AffUsername
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["AffUsername"], string.Empty);
            }
            set
            {
                HttpContext.Current.Session["AffUsername"] = value;
            }
        }
        public static int AffType
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["AffType"], 1);
            }
            set
            {
                HttpContext.Current.Session["AffType"] = value;
            }
        }
     

        public static bool IsReportAccess
        {
            get
            {
                string strRole = HttpContext.Current.Session["RoleAccess"].ToString();
                return (strRole.IndexOf("[AffReport]-1") >= 0);
            }
        }

        public static bool IsIT
        {
            get
            {
                return CastAide.AsBoolean(HttpContext.Current.Session["IsIT"]);
            }
            set
            {
                HttpContext.Current.Session["IsIT"] = value;
            }
        }

    }

    #endregion
}

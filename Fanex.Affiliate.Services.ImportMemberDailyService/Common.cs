// -----------------------------------------------------------------------
// <copyright file="Common.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Services
{
    using System;
    using System.Configuration;
    using System.Xml;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Common
    {


        public static string UsernamePrefix
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["UsernamePrefix"]); }
        }

        public static string SpSuffix
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["SP_Suffix"]); }
        }
        
        public static string FullAffiliateDBName         
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["AffiliateDB"]); }
        }
        public static int AdminID
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["adminID"]); }
        }
        public static int Interval
        {
            get { return  Convert.ToInt32(ConfigurationManager.AppSettings["interval"]); }
        }
        public static int DueTime
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DueTime"]); }
        }
        public static string IsSendMail
        {
            get { return ConfigurationManager.AppSettings["isSendMail"]; }
        }
        public static DateTime? FromDate
        {
            get {
                var s = ConfigurationManager.AppSettings["fromDate"];
                if (String.IsNullOrWhiteSpace(s))
                    return null;
                return Convert.ToDateTime(s, null);
            }
         
        }
        public static DateTime? ToDate
        {
            get
            {
                var s = ConfigurationManager.AppSettings["toDate"];
                if (String.IsNullOrWhiteSpace(s))
                    return null;
                return Convert.ToDateTime(s, null);
            }
        }
     
        public static string SubjectMailError
        {
            get { return ConfigurationManager.AppSettings["Environment"] + "-" + ConfigurationManager.AppSettings["subjectMailError"]; }
        }
       
    }
}

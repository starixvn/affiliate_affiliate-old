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
    using DNAide.Core;    
    using System.Globalization;
    using System.Collections.Generic;
    using DNAide.Web;
    using System.Data;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Common
    {
       // private static char CSV_SEPARATOR = '\t';        

        public static int Interval
        {
            get { return CastAide.AsInt32(ConfigurationManager.AppSettings["interval"]); }
        }

        public static int DueTime
        {
            get { return CastAide.AsInt32(ConfigurationManager.AppSettings["DueTime"]); }
        }

        public static DateTime? FromDate
        {
            get
            {
                var s = ConfigurationManager.AppSettings["fromDate"];
                if (String.IsNullOrWhiteSpace(s))
                    return null;
                return CastAide.AsDateTime(s, null);
            }
        }
        public static DateTime? ToDate
        {
            get
            {
                var s = ConfigurationManager.AppSettings["toDate"];
                if (String.IsNullOrWhiteSpace(s))
                    return null;
                return CastAide.AsDateTime(s, null);
            }
        }

        public static string IsSendMail
        {
            get { return ConfigurationManager.AppSettings["isSendMail"]; }
        }
        public static string FromMail
        {
            get { return ConfigurationManager.AppSettings["fromMail"]; }
        }
        public static string ToMail
        {
            get { return ConfigurationManager.AppSettings["toMail"]; }
        }
        public static string CCMail
        {
            get { return ConfigurationManager.AppSettings["CCMail"]; }
        }
        public static string BCCMail
        {
            get { return ConfigurationManager.AppSettings["BCCMail"]; }
        }

        public static string SubjectMailError
        {
            get { return ConfigurationManager.AppSettings["Environment"] + "-" +  ConfigurationManager.AppSettings["subjectMailError"]; }
        }

        public static int AdminID
        {
            get { return CastAide.AsInt32(ConfigurationManager.AppSettings["adminID"]); }
        }

        //public static void SendEmail(string Content)
        //{

        //    //SendMailService.SendMail2(Common.MailServer, Common.ToMail, Common.FromMail, Common.CCMail, Common.BCCMail, Common.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]), Common.PasswordMail);
        //    MailRecord mailItem = new MailRecord
        //    {
        //        MailTo = Common.ToMail,
        //        Subject = Common.SubjectMailError,
        //        Title = Common.SubjectMailError,
        //        Body = Content, //IsBodyHtml ready
        //        MailBcc = Common.BCCMail,
        //        MailCc = Common.CCMail
        //    };
        //    using (MailDeliver client = new MailDeliver())
        //    {
        //        client.SendMail(mailItem);
        //    }
        //}
    }
}

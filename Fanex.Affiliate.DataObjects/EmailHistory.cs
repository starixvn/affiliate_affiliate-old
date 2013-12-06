
namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using System.Web;
    using DNAide.Data;
    using System;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// class to insert a new email into the system
    /// Author : DungTong on 20090317
    /// </summary>

    public class EmailHistory
    {
        public static string InsertEmail(string content,string sender,string affiliate,string email,string subject)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("mid", 0);
            hsParam.Add("creator",HttpContext.Current.Session["CustID"]);
            hsParam.Add("content",HttpContext.Current.Server.HtmlEncode(content));
            hsParam.Add("sender", HttpContext.Current.Server.HtmlEncode(sender));
            hsParam.Add("affiliate", HttpContext.Current.Server.HtmlEncode(affiliate));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(email));
            hsParam.Add("subject", HttpContext.Current.Server.HtmlEncode(subject));
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("browser", HttpContext.Current.Request.Browser.Browser);
            hsParam.Add("sessionid", HttpContext.Current.Session.SessionID);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_EmailHistoryIns", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]); 
        }
        public static DbResult EmailHistorySel(object mid, string senderName, string email, string affiliateName, object fromdate, object todate, int pagesize, int pageindex)
        {
            Hashtable hsParam = new Hashtable();
            if (fromdate != null && todate != null)
            {
                hsParam.Add("fromdate", fromdate);
                hsParam.Add("todate", Convert.ToDateTime(todate).AddDays(1).AddMilliseconds(-3));
            }
            if (mid != null)
            {
                hsParam.Add("mid", mid);
            }
            if (!string.IsNullOrEmpty(senderName))
            {
                hsParam.Add("sendername", HttpContext.Current.Server.HtmlEncode(senderName));
            }
            if (!string.IsNullOrEmpty(email))
            {
                hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(email));
            }
            if (!string.IsNullOrEmpty(affiliateName))
            {
                hsParam.Add("affiliateusername", HttpContext.Current.Server.HtmlEncode(affiliateName));
            }
            hsParam.Add("pagesize", Convert.ToInt32(pagesize));
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            /* hsParam.Add("language", Common.GetLangChar());*/

            return DbHelper.Execute("Aff_EmailHistorySel", hsParam, DBType.AFFILIATE);
        }
        /// <summary>
        /// Delete list of selected email id by @DungTong
        /// </summary>
        /// <param name="mailIDList">list of id separated by a comma</param>
        /// <returns>Error Message</returns>
        public static string DeleteMailHistory(string mailIDList, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("listmid", mailIDList);
            hsParam.Add("userid", HttpContext.Current.Session["CustID"]);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_EmailHistoryDel", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }

    }
}

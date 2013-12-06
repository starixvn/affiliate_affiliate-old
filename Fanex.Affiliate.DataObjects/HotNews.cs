
namespace Fanex.Affiliate.DataObjects
{
    /// <summary>
    /// class for getting hot news information
    /// Author : DungTong on 20090317
    /// </summary>
    using System.Web;
    using System.Globalization;
    using System.Collections;
    using DNAide.Data;
    using DNAide.Core;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;    

    public class HotNews
    {
        /// <summary>
        /// Get the latest hot news
        /// </summary>
        /// <returns>string for the news</returns>
        public static DbResult GetLatestHotNews()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("msgid", -1);
            return DNAide.Data.DbHelper.Execute("Aff_HotNewsDetail_New", hsParam, DBType.AFFILIATE);            
        }
        /// <summary>
        ///  Search Hot News
        /// </summary>
        /// <param name="important">important = -1 : don't search on this column, = 0: search unimportant message, -1: search important message</param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="pageindex"></param>
        /// <param name="totalpages"></param>
        /// <param name="nrows"></param>
        /// <returns>list of selected hot news</returns>
        public static DbResult HotNewsSel(int important, object fromdate, object todate, int pageindex, int intPageSize,string language = null)
        {            
            Hashtable hsParam = new Hashtable();
            if (fromdate != null && todate != null)
            {
                CultureInfo info = new CultureInfo("en-US");
                hsParam.Add("fromdate", CastAide.AsDateTime(fromdate, info));
                hsParam.Add("todate", CastAide.AsDateTime(todate, info).AddDays(1).AddMilliseconds(-3));
            }
            if (important >= 0)
            {
                hsParam.Add("important", important);
            }
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            //HttpContext.Current.Session["Language"]
            hsParam.Add("language", language); //System.Threading.Thread.CurrentThread.CurrentCulture.Name

            return DbHelper.Execute("Aff_HotNewsSel_New", hsParam, DBType.AFFILIATE);
        }
        /// <summary>
        /// Get hot news information by msgid
        /// </summary>
        /// <returns>result set for this msgid</returns>
        public static DbResult GetHotNews(int id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("msgid", id);
            return DbHelper.Execute("Aff_HotNewsDetail_New", hsParam, DBType.AFFILIATE);
        }
       
        /// <summary>
        /// Delete list of selected messages by @DungTong
        /// </summary>
        /// <param name="hotNewsIDList">list of id separated by a comma</param>
        /// <returns>Error Message</returns>
        public static string DeleteHotNews(string hotNewsIDList, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("listmsgid", hotNewsIDList);
            hsParam.Add("userid", HttpContext.Current.Session["CustID"]);
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_HotNewsDel", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
        /// <summary>
        /// Add a hot news into the system.
        /// </summary>        
        /// <param name="xml"></param>
        /// <param name="important">The hot news is marked by red color or not</param>
        /// <returns>Error message</returns>
        public static string AddHotNews(int important, string xml, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("important", important);
            hsParam.Add("xml", xml);          
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());            
            hsParam.Add("userid", HttpContext.Current.Session["CustID"]);
           // hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_HotNewsIns", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
        /// <summary>
        /// Update an existing hot news in the system.
        /// </summary>
        /// <param name="msgid">id of this hot news</param>
        /// <param name="xml"></param>
        /// <param name="important">The hot news is marked by red color or not</param>
        /// <returns>Error message</returns>
        public static string UpdateHotNews(int id, int important, string xml, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);
            hsParam.Add("important", important);
            hsParam.Add("xml", xml); 
            hsParam.Add("userid",HttpContext.Current.Session["CustID"]);
            //hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);                        
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            DbResult result = DbHelper.Execute("Aff_HotNewsUpd", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
    }
}

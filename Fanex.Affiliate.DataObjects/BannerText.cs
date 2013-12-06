// -----------------------------------------------------------------------
// <copyright file="Banner.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Collections;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BannerText
    {

        //public static DbResult GetListText(string category, string language, string currency, int pageIndex, int pageSize)
        //{
        //    Hashtable hsParam = new Hashtable();
        //    if (!string.IsNullOrEmpty(category))
        //    {
        //        hsParam.Add("category", category);
        //    }
        //    if (!string.IsNullOrEmpty(language))
        //    {
        //        hsParam.Add("language", language);
        //    }
        //    if (!string.IsNullOrEmpty(currency))
        //    {
        //        hsParam.Add("currency", currency);
        //    }
        //    hsParam.Add("pagesize", pageSize);
        //    hsParam.Add("pageindex", pageIndex);
        //    hsParam.Add("totalpages", 0);
        //    hsParam.Add("nrows", 0);

        //    return DbHelper.Execute("Aff_TextSel", hsParam, DBType.AFFILIATE);

        //}
        public static int InsText(string textvalue, string category, string language, string currency, ref int errcode, ref string errmsg)
        {
            DbResult dbr = null;
            int result = 0;
            Hashtable htb = new Hashtable();
            htb.Add("bannerid", 0);
            htb.Add("message", HttpContext.Current.Server.HtmlEncode(textvalue));
            htb.Add("category", HttpContext.Current.Server.HtmlEncode(category));
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("currency", HttpContext.Current.Server.HtmlEncode(currency));
            htb.Add("userid", HttpContext.Current.Session["CustID"]);
            htb.Add("ip", Utilities.GetIPAddressOfClient());
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");

            dbr = DbHelper.Execute("Aff_TextIns", htb, DBType.AFFILIATE);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            result = CastAide.AsInt32(dbr.Outputs["bannerid"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            return result;
        }

        public static string UpdText(int id, string textvalue, string category, string language, string currency, string lang = "en-US")
        {
            Hashtable htb = new Hashtable();
            htb.Add("bannerid", id);
            htb.Add("message", HttpContext.Current.Server.HtmlEncode(textvalue));
            htb.Add("category", HttpContext.Current.Server.HtmlEncode(category));
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("currency", HttpContext.Current.Server.HtmlEncode(currency));
            htb.Add("userid", HttpContext.Current.Session["CustID"]);
            htb.Add("ip", Utilities.GetIPAddressOfClient());
            htb.Add("language", lang);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            DbResult dbr = DbHelper.Execute("Aff_TextUpd", htb, DBType.AFFILIATE);
            return dbr.Outputs["errmsg"].ToString();
        }

        public static string DelDataText(string listBannerID, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("listbannerid", listBannerID);
            hsParam.Add("userid", HttpContext.Current.Session["CustID"]);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_TextDel", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
        /*
        public static DbResult GetListText(string category, string language, string currency, int pageIndex, int pageSize)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_TextSel_1", hsParam, DBType.AFFILIATE_12BET);

        }*/

        public static DbResult GetTextDetail(int textId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("textid", textId);
            return DbHelper.Execute("Aff_TextSelDetail", hsParam, DBType.AFFILIATE);
        }
    }
}

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
    using System;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Banner
    {
        public static int InsertBannerText(string textvalue, int category, string language, 
            int currency, ref int errcode, ref string errmsg)
        {
            return InsertBanner(textvalue, category, language, currency, 0, null, 0, ref errcode, ref errmsg);
        }
        public static int InsertBanner(string content, int category,
            string language, int currency, int imagetype, string size,int bannertype,
            ref int errcode, ref string errmsg)
        {
            DbResult dbr = null;
            int result = 0;
            Hashtable htb = new Hashtable();

            //if (type == Common.NormalBanner)
            //{
            //    spName = "Aff_BannerIns";
            //    htb.Add("fileextension", HttpContext.Current.Server.HtmlEncode(fileNameExtension));
            //}

            htb.Add("content", HttpContext.Current.Server.HtmlEncode(content));

            htb.Add("bannerid", 0);
            htb.Add("category", category);
            htb.Add("language", language);
            htb.Add("currency", currency);
            htb.Add("imagetype", imagetype);
            htb.Add("bannertype", bannertype);
            htb.Add("size", HttpContext.Current.Server.HtmlEncode(size));
            htb.Add("userid", HttpContext.Current.Session["CustID"]);
            htb.Add("ip", Utilities.GetIPAddressOfClient());
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            
            dbr = DbHelper.Execute("Aff_BannerIns", htb, DBType.AFFILIATE);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            result = CastAide.AsInt32(dbr.Outputs["bannerid"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            return result;
        }

        public static string UpdateBannerText(int id, string content, int category, string language,
              int currency)
        {
            return UpdateBanner(id, content, category, language, currency, 0, null, 0);
        }
        public static string UpdateBanner(int id, string content, int category, string language, 
            int currency, int imagetype, string size,int bannertype)
        {
            Hashtable htb = new Hashtable();
            htb.Add("bannerid", id);
            htb.Add("content", HttpContext.Current.Server.HtmlEncode(content));
            htb.Add("category", category);
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("imagetype", imagetype);

            if(!String.IsNullOrEmpty(size))
                htb.Add("size", size);  
            htb.Add("currency", currency);
            htb.Add("bannertype", bannertype);
            htb.Add("userid", HttpContext.Current.Session["CustID"]);
            htb.Add("ip", Utilities.GetIPAddressOfClient());
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            DbResult dbr = DbHelper.Execute("Aff_BannerUpd", htb, DBType.AFFILIATE);
            return dbr.Outputs["errmsg"].ToString();
        }
      
        //Delete Banner
        public static string DelDataBanner(string listBannerID, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("listbannerid", listBannerID);
            hsParam.Add("userid", HttpContext.Current.Session["CustID"]);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_BannerDel", hsParam, DBType.AFFILIATE);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
        public static DbResult GetBannerListText(int category, string language, int currency, int pageIndex, int pageSize)
        {
            return GetBannerList(category, language, 0, 0, currency, pageIndex, pageSize, 0);
        }

        public static DbResult GetBannerList(int category, string language, int imageType, int size, int currency, int pageIndex, int pageSize, int type)
        {
            Hashtable hsParam = new Hashtable();
            if (category>0)
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (size>0)
            {
                hsParam.Add("size", size);
            }
            if (imageType>0)
            {
                hsParam.Add("imagetype", imageType);
            }
            if (currency>0)
            {
                hsParam.Add("currency", currency);
            }                        
            
            hsParam.Add("type", type);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSel", hsParam, DBType.AFFILIATE);

        }
        public static DbResult GetBannerDetail(int bannerid)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("bannerid", bannerid);
            return DbHelper.Execute("Aff_BannerSelDetail", hsParam, DBType.AFFILIATE);
        }
        public static DbResult GetBannerType(string typeName)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(typeName))
                hsParam.Add("typename", typeName);
            hsParam.Add("language", "en-US");

            return DbHelper.Execute("Aff_BannerSelType", hsParam, DBType.AFFILIATE);
        }



    }
}

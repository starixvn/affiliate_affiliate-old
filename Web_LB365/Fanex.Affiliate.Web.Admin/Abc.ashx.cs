namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Resources;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Model;

    public class Abc : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //return;//Disable Edit exchange rate
            //context.Response.ContentType = "text/html";
            //try
            //{
            //    var langcodes = new string[] { "en-US", "es-ES", "id-ID", "th-TH", "vi-VN", "zh-CN", "zh-TW" };
            //    foreach (var item in langcodes)
            //    {
            //        var code = item;
            //        var culture = CultureInfo.GetCultureInfo(code);
            //        ResourceSet resourceSet = Lang.ResourceManager.GetResourceSet(culture, true, true);
            //        foreach (DictionaryEntry entry in resourceSet)
            //        {
                        
            //            if (entry.Key.ToString() != "Creator")
            //                continue;
            //            var hsParams = new Hashtable();
            //            hsParams.Add("languagecode", code);
            //            hsParams.Add("resourcekey", entry.Key.ToString());
            //            hsParams.Add("resourcevalue", entry.Value.ToString());
            //            hsParams.Add("errcode", 0);
            //            hsParams.Add("errmsg", string.Empty);
            //            var rs = Fanex.Affiliate.DataObjects.DbHelper.Execute("Aff_AddTranslation", hsParams, DBType.AFFILIATE);

            //        }
            //    }
               
            //}
            //catch (Exception ex)
            //{
            //    context.Response.Write(ex.ToString());
            //}
            //context.Response.Write("ok");                  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}

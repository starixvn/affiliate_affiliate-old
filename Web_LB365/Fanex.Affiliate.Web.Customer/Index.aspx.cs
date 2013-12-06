namespace Fanex.Web.LiveBet365.Customer
{
    using System;
    using System.Web;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Utility;

    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            string lang = (Request.QueryString["lang"] ?? string.Empty);
            switch (lang.ToLower())
            {
                case "ch":
                    AppSession.Flag = "ct";
                    lang = "zh-TW";
                    break;
                case "cs":
                    AppSession.Flag = "cs";
                    lang = "zh-CN";
                    break;
                case "es":
                    AppSession.Flag = "es";
                    lang = "es-ES";
                    break;
                case "vi":
                    AppSession.Flag = "vi";
                    lang = "vi-VN";
                    break;
                case "id":
                    AppSession.Flag = "id";
                    lang = "id-ID";
                    break;
                case "th":
                    AppSession.Flag = "th";
                    lang = "th-TH";
                    break;
                default:
                    AppSession.Flag = "e";
                    lang = "en-US";
                    break;
            }
            AppSession.Language = lang;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
            Response.Redirect(AppSettings.FullDomainName);
        }
    }
}
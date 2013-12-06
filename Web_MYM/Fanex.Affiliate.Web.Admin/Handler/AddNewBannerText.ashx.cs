namespace Fanex.Web.MYM.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;
    using DNAide.Core;

    public class AddNewBannerText : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/html";

                
                string textvalue = context.Request.Params["textvalue"];
                var category = CastAide.AsInt32(context.Request.Params["slCategory"],0);
                var language = context.Request.Params["slLanguage"];
                var currency = CastAide.AsInt32(context.Request.Params["slCurrency"], 0);                
                var err = string.Empty;
                int errCode = 0;

                int bannerID = CastAide.AsInt32(context.Request["bannerid"], 0);

                var isupdate = bannerID > 0;
                if (isupdate)
                {
                    errCode = 1;
                    err = Banner.UpdateBannerText(bannerID, textvalue, category, language, currency);
                }
                else
                {
                    bannerID = Banner.InsertBannerText(textvalue, category, language, currency, ref errCode, ref err);

                }
                if (!string.IsNullOrEmpty(err))
                {
                    context.Response.Write("<script type=\"text/javascript\">alert('" + err + "');</script>");
                    return;
                }
                context.Response.Write("<script type=\"text/javascript\">alert('" + (isupdate ? Fanex.Affiliate.Resources.Admin.UpdateSucc : Fanex.Affiliate.Resources.Admin.SuccAddText) + "');parent.Cancelfunction();</script>");
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.Core.Culture;
using System.Web.SessionState;
using Fanex.Affiliate.DataObjects;

namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    /// <summary>
    /// Summary description for GetEditedBannerText
    /// </summary>
    public class UpdateBannerText : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/html";
                string idbanner = context.Request.Params["hdBannerID"];
                string textvalue = context.Request.Params["textvalue"];
                string category = context.Request.Params["slCategory"];
                string language = context.Request.Params["slLanguage"];
                string currency = context.Request.Params["slCurrency"];

                string err = BannerText.UpdText(Convert.ToInt32(idbanner), textvalue, category, language, currency);
                if (!string.IsNullOrEmpty(err))
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                    return;
                }
                //Upload new file
                context.Response.Write("<script type=\"text/javascript\">parent.CancelEdit();</script>");
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
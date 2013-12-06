using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.Core.Culture;
using System.Web.SessionState;
using Fanex.Affiliate.DataObjects;

namespace Fanex.Web.Aoncash.Admin.Handler
{
    /// <summary>
    /// Summary description for GetEditedBannerText
    /// </summary>
    public class DelBannerText : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string listIdBanner = context.Request.Params["params"];
                string result = BannerText.DelDataText(listIdBanner, Fanex.Affiliate.Core.AppSession.Language);
                context.Response.Write(result);
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
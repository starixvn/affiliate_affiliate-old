using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.Core.Culture;
using System.Web.SessionState;

using System.Text;
using Fanex.Affiliate.Core.Admin.GenerateHTML;
using DNAide.Core;

namespace Fanex.Web.Aoncash.Admin.Handler
{
    /// <summary>
    /// Summary description for GetEditedBannerText
    /// </summary>
    public class GetDataBannerText : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                var category = CastAide.AsInt32(context.Request["cate"], 0);
                var lang = (context.Request["lang"].Equals("")) ? null : context.Request["lang"];
                var currency =  CastAide.AsInt32(context.Request["currency"], 0);

                int page = string.IsNullOrEmpty(context.Request["page"]) ? 1 : CastAide.AsInt32(context.Request["page"]);
                int pageSize = (context.Request["PageSize"].Equals("")) ? 20 : CastAide.AsInt32(context.Request["PageSize"]);
                int minNo = 0, maxNo = 0, totalPage = 0, totalBanner = 0;
                StringBuilder result = new StringBuilder();
                string content = Banner.GetListText(category, lang, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize);
                result.Append(minNo.ToString());
                result.Append("$$$");
                result.Append(maxNo.ToString());
                result.Append("$$$");
                result.Append(content);

                context.Response.Write(result.ToString());
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
namespace Fanex.Web.i668bet.Admin.Handler
{

    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using System.Globalization;
    using DNAide.Core;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Culture;

    public class GetStatisticsDetail_New : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                CultureInfo culture = new CultureInfo("en-US");
                DateTime fDate = CastAide.AsDateTime(context.Request["fdate"], culture);
                DateTime tDate = CastAide.AsDateTime(context.Request["tdate"], culture);
                string domain = context.Request["domain"];
                string productCode = context.Request["productCode"];
                string languageCode = context.Request["languageCode"];
                int pageSize = CastAide.AsInt32(context.Request["pageSize"]);
                int pageIndex = CastAide.AsInt32(context.Request["pageIndex"]);
                StringBuilder result = new StringBuilder();
                
                result.Append(Fanex.Affiliate.Core.Admin.GenerateHTML.TrackingSystem.GetStatisticsDetail_New(fDate, tDate, domain, productCode, languageCode, pageSize, pageIndex));

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
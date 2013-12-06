namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core.Admin.GenerateHTML;

    public class GetOnlineList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string param = context.Request["params"];
            int page = CastAide.AsInt32(param.Split(',')[0]);
            int pagesize = CastAide.AsInt32(param.Split(',')[1]);
            string ret = OnlineList.GenerateOnlineListTable(page, pagesize);
            context.Response.Write(ret);
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

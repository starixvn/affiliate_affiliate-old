namespace Fanex.Web.MYM.Admin.Handler
{
    using System.Web.SessionState;
    using System.Web;
    using DNAide.Core;
    using DNAide.Data;
    using Fanex.Affiliate.DataObjects;

    public class Favorite : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int AffId = CastAide.AsInt32(context.Request["affid"], -1);
            int intAction = CastAide.AsInt32(context.Request["action"], -1);
            if (AffId == -1 || intAction == -1)
            {
                context.Response.Write("1");
                return;
            }
            DbResult rs = null;
            if (intAction == 0)
                rs = Affiliate.AffFavoriteDel(AffId, Fanex.Affiliate.Core.AppSession.Language);
            if (intAction == 1)
                rs = Affiliate.AffFavoriteIns(AffId, Fanex.Affiliate.Core.AppSession.Language);
            context.Response.Write("" + rs.Outputs["errcode"] + ";" + rs.Outputs["errmsg"]);
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

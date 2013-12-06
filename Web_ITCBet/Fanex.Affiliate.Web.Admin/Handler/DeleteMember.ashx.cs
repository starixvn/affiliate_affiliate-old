namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;

    public class DeleteMember : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string username = context.Request.Params["username"].Trim();
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write(Fanex.Affiliate.Resources.Translator.Get("EmptyIBCCustID"));
                return;
            }

            context.Response.Write(Member.DeleteMember(username));
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

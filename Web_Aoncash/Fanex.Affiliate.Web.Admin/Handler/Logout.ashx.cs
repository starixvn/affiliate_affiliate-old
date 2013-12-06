namespace Fanex.Web.Aoncash.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using System.Web.Security;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    public class Logout : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            try
            {
                Auth.Logout(AppSession.OnUser);
            }
            catch { }
            context.Session.Clear();
            FormsAuthentication.SignOut();
            context.Response.Write("<html><head></head><body><script type='text/javascript'>top.location.href='" + FormsAuthentication.LoginUrl + "';</script></body></html>");
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

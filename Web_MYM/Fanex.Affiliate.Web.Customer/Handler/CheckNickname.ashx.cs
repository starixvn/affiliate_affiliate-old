namespace Fanex.Web.MYM.Customer.Handler
{
    using System;
    using System.Web;
    using Fanex.Affiliate.DataObjects;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core;

    public class CheckNickname : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                string strNickname = CastAide.AsString(context.Request["nickname"]).Trim();
                if (UserAff.IsAvailableNickname(strNickname))
                    context.Response.Write("ok");
                else
                    context.Response.Write("error");
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
                AppCommon.SendEmail(ex);
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
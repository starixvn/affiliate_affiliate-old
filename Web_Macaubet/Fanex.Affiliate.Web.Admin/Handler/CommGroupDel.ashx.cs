namespace Fanex.Web.Macaubet.Admin.Handler
{
    using System;
    using System.Web;
    using DNAide.Data;
    using System.Web.SessionState;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;

    public class CommGroupDel : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string groupID = context.Request.Params["params"];

                if (string.IsNullOrEmpty(groupID))
                {
                    context.Response.Write("Invalid group ID");
                    return;
                }

                try
                {
                    DbResult result = CommGroup.CommGroupDel(Convert.ToInt32(groupID), context.Request.UserHostAddress, Fanex.Affiliate.Core.AppSession.Language);
                    context.Response.Write(result.Outputs["errmsg"]);

                }
                catch (Exception e)
                {
                    context.Response.Write(e.ToString());
                    Fanex.Affiliate.Core.AppCommon.SendEmail(e);
#if DEBUG
                    throw;

#else
                DNAide.Web.DNAideLog.Write(e.ToString());
#endif

                }


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

namespace Fanex.Web.Macaubet.Admin.Handler
{
    using System;
    using System.Web;
    using DNAide.Data;
    using System.Web.SessionState;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;

    public class CommGroupAssign : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string[] arr = context.Request.Params["params"].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {

                    DbResult result = CommGroup.CommGroupAssign(Convert.ToInt32(arr[0]), arr[1], context.Request.UserHostAddress, Fanex.Affiliate.Core.AppSession.Language);
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

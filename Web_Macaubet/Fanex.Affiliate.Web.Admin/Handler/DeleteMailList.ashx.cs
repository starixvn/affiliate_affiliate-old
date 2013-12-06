namespace Fanex.Web.Macaubet.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;

    public class DeleteMailList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                string midlist = context.Request.Params["params"].Trim();
                if (string.IsNullOrEmpty(midlist))
                {
                    context.Response.Write("Null data is sent to server");
                    return;
                }
                //bool isHost = Common.isHost(Fanex.Affiliate.Core.AppSession.CustID, "[HotNews]");
                //if (isHost)
                //{
                //Drop the comma at the end of message
                midlist = midlist.Substring(0, midlist.Length - 1);
                StringBuilder result = new StringBuilder();
                result.Append(EmailHistory.DeleteMailHistory(midlist, Fanex.Affiliate.Core.AppSession.Language));
                context.Response.Write(result.ToString());
                //}
                //else
                //{
                //    context.Response.Write(Fanex.Affiliate.Resources.Translator.Get("UpdateRolePolicyErr);
                //}
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

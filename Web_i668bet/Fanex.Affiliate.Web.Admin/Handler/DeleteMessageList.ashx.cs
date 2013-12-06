namespace Fanex.Web.i668bet.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;

    public class DeleteMessageList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                string msgidlist = context.Request.Params["params"].Trim();
                if (string.IsNullOrEmpty(msgidlist))
                {
                    context.Response.Write("Null data is sent to server");
                    return;
                }
                //Drop a comma at the end of message
                msgidlist = msgidlist.Substring(0, msgidlist.Length - 1);

                StringBuilder result = new StringBuilder();
                result.Append(HotNews.DeleteHotNews(msgidlist, Fanex.Affiliate.Core.AppSession.Language));
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

namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Net;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;

    public class ResourceHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var action = context.Request["action"];
            var key = HttpUtility.UrlDecode(context.Request["key"]);
            try
            {
                switch (action)
                {
                    case "add":
                        var type = HttpUtility.UrlDecode(context.Request["type"]);
                        var lang = HttpUtility.UrlDecode(context.Request["lang"]);
                        var description = HttpUtility.UrlDecode(context.Request["description"]);
                        var value = HttpUtility.UrlDecode(context.Request["value"]);
                        if (context.Request["plaintext"] == "true")
                        {
                            value = Common.HtmlToPlainText(value);
                        }
                        var r = Resource.AddResource(key, value, type, lang, description, Fanex.Affiliate.Core.AppSession.Language);
                        if (r == "True")
                        {
                            ReCache();
                        }
                        context.Response.Write(r);
                        break;
                    case "delete":
                        context.Response.Write(Resource.DeleteResources(key));
                        break;
                    case "update":
                        var id = new Guid(context.Request["id"]);
                        var updatedvalue = HttpUtility.UrlDecode(context.Request["value"]);
                        var updateddescription = HttpUtility.UrlDecode(context.Request["description"]);
                        //if (context.Request["plaintext"] == "true")
                        //{
                        //    updatedvalue = Common.HtmlToPlainText(updatedvalue);
                        //}
                        var result = Resource.UpdateResource(id, updatedvalue, updateddescription, Fanex.Affiliate.Core.AppSession.Language);

                        if (result == "True")
                        {
                            ReCache();
                        }

                        context.Response.Write(result);
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
            }

        }
        private static void ReCache()
        {
            var domains = Common.GetStringIList(System.Configuration.ConfigurationManager.AppSettings["RecacheUrls"], ",");
            foreach (var request in domains.Select(url => (HttpWebRequest)WebRequest.Create(url)))
            {
                try
                {
                    request.Method = "GET";
                    request.BeginGetResponse(RespCallback, null);

                }
                catch
                {
                    return;
                }
            }
        }

        private static void RespCallback(IAsyncResult asynchronousResult)
        {
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
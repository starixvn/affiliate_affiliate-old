namespace Fanex.Web.Macaubet.Admin.Handler
{
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;
    using System;

    public class SettingHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["action"];
            string key = HttpUtility.UrlDecode(context.Request["key"]);
            try
            {
                switch (action)
                {
                    case "add":
                        string type = HttpUtility.UrlDecode(context.Request["type"]);
                        string description = HttpUtility.UrlDecode(context.Request["description"]);
                        string value = HttpUtility.UrlDecode(context.Request["value"]);
                        if (context.Request["plaintext"] == "true")
                        {
                            value = Common.HtmlToPlainText(value);
                        }
                        context.Response.Write(Settings.AddSetting(key, description, value, type, Fanex.Affiliate.Core.AppSession.Language));
                        break;
                    case "delete":
                        context.Response.Write(Settings.DeleteSettings(key));
                        break;
                    case "update":
                        int id = int.Parse(context.Request["id"]);
                        string updateddescription = HttpUtility.UrlDecode(context.Request["description"]);
                        string updatedvalue = HttpUtility.UrlDecode(context.Request["value"]);
                        context.Response.Write(Settings.UpdateSetting(id, updateddescription, updatedvalue, Fanex.Affiliate.Core.AppSession.Language));
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
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

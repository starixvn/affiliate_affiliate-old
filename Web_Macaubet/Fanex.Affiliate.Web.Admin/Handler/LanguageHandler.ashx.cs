namespace Fanex.Web.Macaubet.Admin.Handler
{
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;
    using System.Collections;
    using Fanex.Affiliate.Core;

    public class LanguageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["action"];
           
            var hsParam = new Hashtable();
            switch (action)
            {
                case "add":

                    hsParam.Add("languageid",  context.Request["id"]);
                    hsParam.Add("languagename", context.Request["name"]);
                    hsParam.Add("countryid", context.Request["countryid"]);

                    context.Response.Write(Language.Add(hsParam, Fanex.Affiliate.Core.AppSession.Language));
                    break;
                case "delete":
                    context.Response.Write(Language.Delete(context.Request["id"]));
                    break;
                case "update":
                    
                    hsParam.Add("languageid",  context.Request["id"]);
                    hsParam.Add("languagename", context.Request["name"]);
                    hsParam.Add("countryid", context.Request["countryid"]);

                    context.Response.Write(Language.Update(hsParam, Fanex.Affiliate.Core.AppSession.Language));
                    break;
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

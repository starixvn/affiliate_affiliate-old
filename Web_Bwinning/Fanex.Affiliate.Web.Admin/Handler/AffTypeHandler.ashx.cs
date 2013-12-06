namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;
    using System.Collections;
    using Fanex.Affiliate.Core;

    public class AffTypeHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["action"];
           
            var hsParam = new Hashtable();
            switch (action)
            {
                case "add":
                    
                    hsParam.Add("name", context.Request["name"]);
                    hsParam.Add("description", context.Request["description"]);                    

                    context.Response.Write(AffType.Add(hsParam));
                    break;
                case "delete":
                    context.Response.Write(AffType.Delete(context.Request["id"]));
                    break;
                case "update":
                    
                    hsParam.Add("id",  context.Request["id"]);
                    hsParam.Add("name", context.Request["name"]);
                    hsParam.Add("description", context.Request["description"]);

                    context.Response.Write(AffType.Update(hsParam, Fanex.Affiliate.Core.AppSession.Language));
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

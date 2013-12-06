namespace Fanex.Web.Aoncash.Admin.Handler
{
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;
    using System.Collections;
    using Fanex.Affiliate.Core;
    using System;

    public class ProductHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["action"];
            try
            {
                var hsParam = new Hashtable();
                switch (action)
                {
                    case "add":
                        
                        hsParam.Add("productname", context.Request["name"]);
                        hsParam.Add("description", context.Request["description"]);
                        hsParam.Add("bettypelist", context.Request["bettypelist"]);

                        context.Response.Write(Product.Add(hsParam));
                        break;
                    case "delete":
                        context.Response.Write(Product.Delete(context.Request["id"]));
                        break;
                    case "update":

                        hsParam.Add("productid", context.Request["id"]);
                        hsParam.Add("productname", context.Request["name"]);
                        hsParam.Add("description", context.Request["description"]);
                        hsParam.Add("bettypelist", context.Request["bettypelist"]);

                        context.Response.Write(Product.Update(hsParam));
                        break;
                }
            }
            catch (Exception ex)
            {
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

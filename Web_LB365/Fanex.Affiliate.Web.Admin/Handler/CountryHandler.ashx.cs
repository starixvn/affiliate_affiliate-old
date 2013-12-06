namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.DataObjects;
    using System.Collections;

    public class CountryHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["action"];
            
            var hsParam = new Hashtable();
            switch (action)
            {
                case "add":                    

                    hsParam.Add("countryid",  context.Request["id"]);
                    hsParam.Add("countryname", context.Request["name"]);
                    hsParam.Add("description", context.Request["description"]);
                    hsParam.Add("icon", context.Request["icon"]);
                    hsParam.Add("displayorder", string.IsNullOrWhiteSpace(context.Request["displayorder"]) ? 0 : int.Parse(context.Request["displayorder"]));
                    hsParam.Add("isenabled", string.IsNullOrWhiteSpace(context.Request["isenabled"]) ? 0 : int.Parse(context.Request["isenabled"]));

                    context.Response.Write(Country.Add(hsParam));
                    break;
                case "delete":
                    context.Response.Write(Country.Delete(context.Request["id"]));
                    break;
                case "update":

                    
                    hsParam.Add("countryid",  context.Request["id"]);
                    hsParam.Add("countryname", context.Request["name"]);
                    hsParam.Add("description", context.Request["description"]);
                    hsParam.Add("icon", context.Request["icon"]);
                    hsParam.Add("displayorder", context.Request["displayorder"] != null ? int.Parse(context.Request["displayorder"]) : 0);
                    hsParam.Add("isenabled", context.Request["isenabled"] != null ? int.Parse(context.Request["isenabled"]) : 0);

                    context.Response.Write(Country.Update(hsParam));
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

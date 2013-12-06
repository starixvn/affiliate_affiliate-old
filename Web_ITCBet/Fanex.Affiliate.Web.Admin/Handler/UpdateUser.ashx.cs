namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Data;
    using Fanex.Affiliate.DataObjects;
    public class UpdateUser : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int custid = (string.IsNullOrEmpty(context.Request.Form["custid"])) ? 0 : CastAide.AsInt32(context.Request.Form["custid"]);
            string fn = string.IsNullOrEmpty(context.Request.Form["fn"]) ? "" : context.Request.Form["fn"];
            string ln = string.IsNullOrEmpty(context.Request.Form["ln"]) ? "" : context.Request.Form["ln"];
            int depid = string.IsNullOrEmpty(context.Request.Form["depid"]) ? 0 : CastAide.AsInt32(context.Request.Form["depid"]);

            DbResult dbr = null;
            dbr = UserControl.UpdateUser(custid, fn, ln, depid);
            if (CastAide.AsInt32(dbr.Outputs["errcode"]) == 0)
            {
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write(dbr.Outputs["errmsg"]);
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

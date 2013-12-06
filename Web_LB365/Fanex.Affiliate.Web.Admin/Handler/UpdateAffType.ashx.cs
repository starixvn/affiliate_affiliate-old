namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Data;
    using Fanex.Affiliate.Core.Admin.GenerateHTML;
    using Fanex.Affiliate.DataObjects;

    public class UpdateAffType : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            var affid = CastAide.AsInt32(context.Request["affid"]);
            var affType = CastAide.AsInt32(context.Request["afftype"]);

            int errCode = 0;
            string errMsg = null;
            DbResult dbr = null;

            Affiliate.UpdateAffType(affid, affType, ref errCode, ref errMsg);


            if (errCode == 0)
            {
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write(errMsg);
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
namespace Fanex.Web.Macaubet.Admin.Handler
{
    using System;
    using System.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;

    public class Adjustment : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int custid = CastAide.AsInt32(context.Request["custid"]);
            decimal dcAdjust = CastAide.AsDecimal(context.Request["adj"], 0);
            string description = context.Request["des"];
            if (dcAdjust == 0)
                return;
            try
            {
                Int64 transId = 0;
                string errMsg = string.Empty;              
                int errCode = 0;
                errCode = Affiliate.Adjustment(dcAdjust, custid, Fanex.Affiliate.Core.AppSession.CustID, description, ref transId, ref errMsg);
                errMsg = (string.IsNullOrEmpty(errMsg) ? Fanex.Affiliate.Resources.Translator.Get("UpdateSucc") : errMsg);
                context.Response.Write(errCode + "@" + errMsg);
            }
            catch (Exception ex)
            {
                context.Response.Write("1@" + ex.Message);
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

namespace Fanex.Web.i668bet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.DataObjects;

    public class EditRate : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //return;//Disable Edit exchange rate
            context.Response.ContentType = "text/html";
            try
            {
                int exId = CastAide.AsInt32(context.Request["id"]);
                decimal rate = CastAide.AsDecimal(context.Request["rate"]);
                var tocurrency = context.Request["tocurrency"];
                DbResult rs = ExchangeRate.Exchange_Update(exId, rate, tocurrency);
                if (CastAide.AsInt32(rs.Outputs["errcode"]) == 0)
                {
                    //This SP is no need to run(because exchange rate on one month can have multiple value)
                    //DbResult rs2 = DataHandler.ExchangeRate.Exchange_Update_RptDepWinlost(exId);
                    context.Response.Write("1");
                }
                else
                {
                    context.Response.Write(rs.Outputs["errmsg"]);
                }
            }
            catch (Exception ex)
            {
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
#if DEBUG
                throw;
#endif
                DNAideLog.Write(ex.ToString());
                context.Response.Write(ex.Message);
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

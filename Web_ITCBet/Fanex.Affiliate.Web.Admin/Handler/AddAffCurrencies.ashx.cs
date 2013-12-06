namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;

    public class AddAffCurrencies : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //return;//Disable Edit exchange rate
            context.Response.ContentType = "text/html";
            try
            {
                
                string data = CastAide.AsString(context.Request["data"],String.Empty);
                
                DbResult rs = ExchangeRate.AddAffCurrencies(data);
                if (CastAide.AsInt32(rs.Outputs["errcode"]) == 0)
                {
                    //This SP is no need to run(because exchange rate on one month can have multiple value)
                    //DbResult rs2 = DataHandler.ExchangeRate.Exchange_Update_RptDepWinlost(exId);
                    context.Response.Write("1");

                    AppSettings.RefreshAffiliateCurrencies();                    
                }
                else
                {
                    context.Response.Write(rs.Outputs["errmsg"]);
                }
            }
            catch (Exception ex)
            {
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

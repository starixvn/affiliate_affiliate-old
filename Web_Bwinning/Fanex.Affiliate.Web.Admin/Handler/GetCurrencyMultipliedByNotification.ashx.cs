
using System;
using System.Web;
using System.Web.SessionState;
using System.Text;
using DNAide.Data;
using System.Data;
using System.Linq;
using Fanex.Core.Culture;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Model;
using DNAide.Core;

public class GetCurrencyMultipliedByNotification : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //  if (context.Request.UrlReferrer != null)
        // {

        CultureHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/plain";


        var currencycode = context.Request.Params["currencycode"];
        var multipliedby = CastAide.AsInt32(context.Request.Params["multipliedby"], 1);

        // var c = AppSettings.SupportedBaseCurrencies.Where(x => x.Value == currencycode).FirstOrDefault();

        if (multipliedby > 0)
            context.Response.Write(AdminCommon.CreateCurrencyNotificationLine(currencycode, multipliedby));
        else
            context.Response.Write(String.Empty);

        //  }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

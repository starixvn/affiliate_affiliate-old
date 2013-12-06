namespace Fanex.Web.Bwinning.Customer.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Culture;

    public class SetLanguage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            try
            {
                string strLang = context.Request["lang"];                
                AppSession.Flag = strLang;
                AppSession.Language = strLang;
                //HttpContext.Current.Session["Language"] = strLang;
                context.Response.Write(strLang);
            }
            catch (Exception ex)
            {
                DNAide.Web.DNAideLog.Write(ex.Message);
                context.Response.Write("");
                AppCommon.SendEmail(ex);
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
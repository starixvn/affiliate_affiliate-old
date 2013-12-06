namespace Fanex.Web.Aoncash.Customer.Authorization
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.Security;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;

    public class LogOut : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string CurrentLang = AppSession.Language;
            try
            {
                //clear online list
                if (AppSettings.HasLogin) Auth.Logout(AppSession.OnUser);
                context.Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message);
            }
            AppSession.Language = CurrentLang;
            AppSession.Flag = CurrentLang;
            context.Response.Redirect(FormsAuthentication.LoginUrl);
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
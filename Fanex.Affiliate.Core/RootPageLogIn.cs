namespace Fanex.IndoPaul.Core.Affiliate
{
    using System;
    using System.Web;
    using Fanex.IndoPaul.DataObjects;
    
    /// <summary>
    /// Summary description for RootPageLogIn
    /// </summary>
    public class RootPageLogIn :  RootPage
    {
        protected override void InitializeCulture()
        {
            if (!AppSettings.Session.IsAuthenticated)
            {
                HttpContext.Current.Response.Redirect(Common.RootURL());
            }
            if (!AppSettings.Session.HasSecurityQuestion && !HttpContext.Current.Request.RawUrl.Contains("ChangeSecurityQuestion.aspx"))
            {
                HttpContext.Current.Response.Redirect("ChangeSecurityQuestion.aspx", false);
            }
            //InitializeCulture();
            AffHandler.InitializeCulture(HttpContext.Current);
        }
    }
}
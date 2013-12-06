namespace Fanex.Affiliate.Core.Customer
{
    using System.Web;
    using Fanex.Affiliate.Core;

    /// <summary>
    /// Summary description for BasePageLogIn
    /// </summary>
    public class BasePageLogIn :  BasePage
    {
        protected override void InitializeCulture()
        {
            if (!Fanex.Affiliate.Core.AppSession.IsAuthenticated)
            {
                HttpContext.Current.Response.Redirect(AppSettings.FullDomainName);
            }
            //if (!AffSession.HasSecurityQuestion && !HttpContext.Current.Request.RawUrl.Contains("ChangeSecurityQuestion.aspx"))
            //{
            //    HttpContext.Current.Response.Redirect("ChangeSecurityQuestion.aspx", false);
            //}
            Fanex.Core.Culture.CultureHandler.InitializeCulture(HttpContext.Current);
            
            
        }
    }
}
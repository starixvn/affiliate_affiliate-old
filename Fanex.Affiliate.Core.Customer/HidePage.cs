namespace Fanex.Affiliate.Core.Customer
{
    /// <summary>
    /// Summary description for IBCPage2
    /// </summary>
    public class HidePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            Response.Redirect(Fanex.Core.Utility.Common.RootURL());
        }
    }
}
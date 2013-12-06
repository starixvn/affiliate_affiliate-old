namespace Fanex.IndoPaul.Core.Affiliate
{
    using System;
    using System.Web;
    using Fanex.IndoPaul.DataObjects;
    /// <summary>
    /// Summary description for IBCPage2
    /// </summary>
    public class HidePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            Response.Redirect(Common.RootURL());
        }
    }
}
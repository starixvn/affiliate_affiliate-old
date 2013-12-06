namespace Fanex.Web.Bwinning.Customer
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Utility;

    public partial class Master2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.IsAuthenticated)
                Response.Redirect(Page.ResolveUrl("~/UserInfo/Account_Overview.aspx"));
            string strCssLink1 = AppSettings.FullDomainName + "/App_Themes/Aff.ico?v=" + AppSettings.JsVersion;
            string strCssLink2 = AppSettings.FullDomainName + "/App_Themes/Aff.png?v=" + AppSettings.JsVersion;
            string link1 = "<link href=\"" + strCssLink1 + "\" rel=\"shortcut icon\"/>";
            string link2 = "<![if ! IE]><link href=\"" + strCssLink2 + "\" rel=\"shortcut icon\"/><![endif]>";
            Page.Header.Controls.AddAt(2, new LiteralControl(link1));
            Page.Header.Controls.AddAt(3, new LiteralControl(link2));
        }
    }
}
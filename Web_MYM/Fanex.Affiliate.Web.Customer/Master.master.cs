namespace Fanex.Web.MYM.Customer
{
    using System;
    using System.Text;
    using System.Web.UI;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Core;

    public partial class Master : System.Web.UI.MasterPage
    {
        protected StringBuilder strScript = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCssLink1 = AppSettings.FullDomainName + "/App_Themes/Aff.ico?v=" + AppSettings.JsVersion;
            string strCssLink2 = AppSettings.FullDomainName + "/App_Themes/Aff.png?v=" + AppSettings.JsVersion;
            string link1 = "<link href=\"" + strCssLink1 + "\" rel=\"shortcut icon\"/>";
            string link2 = "<![if ! IE]><link href=\"" + strCssLink2 + "\" rel=\"shortcut icon\"/><![endif]>";
            Page.Header.Controls.AddAt(2, new LiteralControl(link1));
            Page.Header.Controls.AddAt(3, new LiteralControl(link2));
            //strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + Common.RootURL() + "/App_Js/jquery/jquery-1.4.2.min.js\"></script>");
            strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + AppSettings.FullDomainName + "/App_Js/prototype.js?vers=" + AppSettings.JsVersion + "\"></script>");
            strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + AppSettings.FullDomainName + "/App_Js/Common.js?vers=" + AppSettings.JsVersion + "\"></script>");
            strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + AppSettings.FullDomainName + "/App_Js/login.js?vers=" + AppSettings.JsVersion + "\"></script>");
            strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + AppSettings.FullDomainName + "/App_Js/Scroller.js?vers=" + AppSettings.JsVersion + "\"></script>");
            strScript.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + AppSettings.FullDomainName + "/App_Js/swfobject.js?vers=" + AppSettings.JsVersion + "\"></script>");
        }
    }
}
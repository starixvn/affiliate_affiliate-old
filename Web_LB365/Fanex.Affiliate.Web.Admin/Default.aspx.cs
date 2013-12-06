using System;
using Fanex.Affiliate.Core;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string strCssLink = Common.RootURL() + "/App_Themes/Aff12bet.ico?v=" + AppSettings.JsVersion;
        IconICO.Attributes.Add("href", "App_Themes/Aff.ico?v=" + AppSettings.JsVersion);
        IconPNG.Attributes.Add("href", "App_Themes/Aff.png?v=" + AppSettings.JsVersion);
    }
}

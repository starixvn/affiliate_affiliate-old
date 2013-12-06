using System;
using DNAide.Web;
using Fanex.Affiliate.Core;
public partial class SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UMTime_From = string.Empty, UMTime_To = string.Empty;
        bool isDownTime = AppCommon.GetDownTime(out UMTime_From, out UMTime_To);

        if (isDownTime)
        {
            Response.Redirect("~/UnderMaintenance.aspx");
            Response.End();
            return;
        }

        IconICO.Attributes.Add("href", "App_Themes/Aff.ico?v=" + AppSettings.JsVersion);
        IconPNG.Attributes.Add("href", "App_Themes/Aff.png?v=" + AppSettings.JsVersion);
        //string temp=TemplateHandler.LoadTemplate("Login.htm");
        //TemplateHandler.Assign(ref temp, "Login", Fanex.Affiliate.Resources.Translator.Get("Login);
        //TemplateHandler.Assign(ref temp, "Username", Fanex.Affiliate.Resources.Translator.Get("Username);
        //TemplateHandler.Assign(ref temp, "Password", Fanex.Affiliate.Resources.Translator.Get("Password);
        //TemplateHandler.Assign(ref temp, "ForgetPassword", Fanex.Affiliate.Resources.Translator.Get("ForgetPassword);
        //TemplateHandler.Assign(ref temp, "Help", Fanex.Affiliate.Resources.Translator.Get("Help);
        //TemplateHandler.Assign(ref temp, "Logon", Fanex.Affiliate.Resources.Translator.Get("LogOn);
        //TemplateHandler.Assign(ref temp, "now", DateTime.Now.Year.ToString());
        //this.HtmlContent = temp; 
    }
}

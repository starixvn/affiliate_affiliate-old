namespace Fanex.Web.i668bet.Customer
{
    using System;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.Resources;

    public partial class Controls_RightPanel : BaseUserControl
    {
        protected string strUsername, strPwd, strValidate, strLogin, strSignup, strForgetPwd, strTitleLogin, strLang;
        protected string signupImg;
        public string script = "";
        public string strURL = string.Empty;
        public string strURL2 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            strLang = AffCommon.GetLanguage();

            GenURL();
            if (AppSession.IsAuthenticated)
            {
                ltrUserName.Text = AppSession.UserName;
                LoginPanel.Visible = false;
                //aSignup.Visible = false;
            }
            else
            {
                divAfterLogin.Visible = false;
            }

            //script += "if($('us')) $('txtusername').value = $('us').value;";
            //script += "if($('pw')) $('txtpassword').value = $('pw').value;";
            script += "if($('val')) $('txtvalidate').value = $('val').value;";

            strUsername = Translator.Get("Username");
            strPwd = Translator.Get("Password");
            strValidate = Translator.Get("Validation");
            strLogin = Translator.Get("Login");
            strSignup = Translator.Get("SignUp");
            strForgetPwd = Translator.Get("ForgetPassword");
            strTitleLogin = Translator.Get("TitleLogin");
        }

        void GenURL()
        {
            string strTmp = Request.Url.AbsolutePath;
            strURL = "Flash/" + AffCommon.GetLanguage() + "/Ads_Aff.swf?v=" + AppSettings.JsVersion;
            strURL2 = "Flash/" + AffCommon.GetLanguage() + "/";
            if (strTmp.ToLower().IndexOf("userinfo") >= 0)
            {
                strURL = "../" + strURL;
                strURL2 = "../" + strURL2;
            }
        }
    }
}
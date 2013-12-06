using System;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Customer;

public partial class ChangePass : BasePageLogIn
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ltrChangePass.Text = Translator.Get("ChangePass") + " " + AppSettings.Session.UserName;
        string strUserName = Request["username"];
        ltrTextRemind.Text = Translator.Get("ChangePassText");
        //ltrTextRemind.Text = Translator.Get("PassCheck");
        ltrCurrPass.Text = Translator.Get("OldPass");
        ltrNewPass.Text = Translator.Get("NewPass");
        ltrComfimPass.Text = Translator.Get("RePass");        
    }
}

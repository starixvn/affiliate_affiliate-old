using System;
using System.Web.Security;
using DNAide.Utils;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;

public partial class Test_TestInternalLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rndStr = Encryption.RandomString(6);
        string accKey = FormsAuthentication.HashPasswordForStoringInConfigFile(Session.SessionID + rndStr, "MD5");
        string custName = Request["username"];
        string accID = Fanex.Affiliate.Core.AppSession.CustID.ToString();
        string accOnId = AppSession.OnUser.ToString();
        string _url = AdminSettings.InternalURL;
        _url += "?chalengeStr=" + rndStr;
        _url += "&accKey=" + accKey;
        _url += "&custName=" + custName;
        _url += "&accId=" + Fanex.Affiliate.Core.AppSession.CustID.ToString(); ;
        _url += "&accOnId=" + accOnId;
        Response.Redirect(_url);
    }
}

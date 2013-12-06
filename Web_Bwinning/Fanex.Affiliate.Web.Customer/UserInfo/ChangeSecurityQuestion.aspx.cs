using System;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;
using Fanex.Core.Utility;
using Fanex.Affiliate.Core;

public partial class ChangeSecurityQuestion : BasePageLogIn
{
    protected string strSecureQuestion = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AffSession.HasSecurityQuestion)
        {
            ltrChangeSecureQuestion.Text = Translator.Get("UpdateSecurityQuestion");
            strSecureQuestion = DNAide.Web.TemplateHandler.LoadTemplate("UserInfo/SecureQuestion_" + AppSession.Language + ".htm");
        }
        else
        {
            Response.Redirect(AppSettings.FullDomainName);
        }

    }
}

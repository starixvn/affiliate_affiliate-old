using System;
using DNAide.Web;
using Fanex.Affiliate.Core;


public partial class ChangeAdminPass : BasePageTpl
{
    protected int pendingAffNo = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContent = TemplateHandler.LoadTemplate("ChangeAdminPass.htm");
        TemplateHandler.Assign(ref strContent, "username", AppSession.UserName);
        TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strContent, "oldpass", Fanex.Affiliate.Resources.Translator.Get("OldPass"));
        TemplateHandler.Assign(ref strContent, "password", Fanex.Affiliate.Resources.Translator.Get("PassNew"));
        TemplateHandler.Assign(ref strContent, "confirmpass", Fanex.Affiliate.Resources.Translator.Get("ConfirmPass"));
        TemplateHandler.Assign(ref strContent, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
        TemplateHandler.Assign(ref strContent, "cancel", Fanex.Affiliate.Resources.Translator.Get("Reset"));
        TemplateHandler.Assign(ref strContent, "oldrequire", Fanex.Affiliate.Resources.Translator.Get("OldRequire"));
        TemplateHandler.Assign(ref strContent, "passrequire", Fanex.Affiliate.Resources.Translator.Get("PassRequire"));
        TemplateHandler.Assign(ref strContent, "passcheck", Fanex.Affiliate.Resources.Translator.Get("PassCheck"));
        TemplateHandler.Assign(ref strContent, "passconfirm", Fanex.Affiliate.Resources.Translator.Get("PassConfirm"));

        //Get pendingAffNo
        //DataHandler.Home.GetPendingMember(ref pendingAffNo);
        //TemplateHandler.Assign(ref strContent, "pending-aff-reg", Fanex.Affiliate.Resources.Translator.Get("PendingAffReg);
        //TemplateHandler.Assign(ref strContent, "pending-aff-no", pendingAffNo);

        TemplateHandler.Assign(ref strContent, "change-my-pwd", Fanex.Affiliate.Resources.Translator.Get("ChangeMyPwd"));

        this.HtmlContent = strContent;
    }
}

using System;
using DNAide.Web;
using DNAide.Core;
using Fanex.Affiliate.Core;

public partial class ChangePass : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContent = TemplateHandler.LoadTemplate("ChangePass.htm");
        string isAdmin = CastAide.AsString(Request["isAdmin"], "0");
        //TemplateHandler.Assign(ref strContent, "submenu", Common.GenSubMenu(new string[] { "#" }, new string[] { Request["username"] }, 322, 0));
        
        TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);

        AppCommon.AssignToAdminTemplate(ref strContent,
        "password", "confirmpass", "passrequire", "passconfirm", "submit", "cancel");

        TemplateHandler.Assign(ref strContent, "username", Request["username"]);
        TemplateHandler.Assign(ref strContent, "custid", Request["custid"]);
        TemplateHandler.Assign(ref strContent, "isAdmin", isAdmin);
        this.HtmlContent = strContent;
    }
}

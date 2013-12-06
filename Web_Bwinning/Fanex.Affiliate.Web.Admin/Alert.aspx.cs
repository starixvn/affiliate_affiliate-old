using System;
using DNAide.Web;
using DNAide.Core;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;

public partial class Alert : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strContent = TemplateHandler.LoadTemplate("Alert.htm");
        int intAlertCode = CastAide.AsInt32(Request["code"]);
        string strLink = (string.IsNullOrEmpty(Request["link"]) ? "Listbanner.aspx" : Request["link"]);
        if (intAlertCode > 1 && intAlertCode < 5)
        {
            TemplateHandler.Assign(ref strContent, "user","The " +  Request["user"]);
        }
        else if (intAlertCode == 5)
        {
            TemplateHandler.Assign(ref strContent, "user", string.Empty);
        }
        else
        {
            TemplateHandler.Assign(ref strContent, "user", Request["user"]);
        }
        TemplateHandler.Assign(ref strContent, "message", AdminCommon.GetAlertMsg(intAlertCode));
        TemplateHandler.Assign(ref strContent, "link", strLink);
        TemplateHandler.Assign(ref strContent, "clickhere", Admin.ClickHere);
        TemplateHandler.Assign(ref strContent, "redirect", Admin.Redirect);
        this.HtmlContent = strContent;
    }
}

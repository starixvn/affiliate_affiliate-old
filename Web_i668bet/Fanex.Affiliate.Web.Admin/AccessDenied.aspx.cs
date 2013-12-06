using System;
using DNAide.Web;
using Fanex.Affiliate.Core;

public partial class AccessDenied : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var strTpl = TemplateHandler.LoadTemplate("AccessDenied.html");
        TemplateHandler.Assign(ref strTpl,"YouAreNotAuthorized",Fanex.Affiliate.Resources.Admin.YouAreNotAuthorized);
        this.HtmlContent = strTpl;
    }
}

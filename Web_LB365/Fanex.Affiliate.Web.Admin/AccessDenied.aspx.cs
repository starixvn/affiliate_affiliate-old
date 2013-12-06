using System;
using DNAide.Web;
using Fanex.Affiliate.Core;

public partial class AccessDenied : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.HtmlContent = TemplateHandler.LoadTemplate("AccessDenied.html");
    }
}

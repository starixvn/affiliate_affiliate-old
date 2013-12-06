namespace Fanex.Web.Bwinning.Admin.Domain
{
    using System;
    using System.Web;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;

    public partial class AddAffiliateDomain : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            string list = TemplateHandler.LoadTemplate("Domain/AddAffiliateDomain.html");
            AppCommon.AssignToAdminTemplate(ref list,
                "Domain", "Status", "Add", "Cancel","Submit");
            
            this.HtmlContent = list;

        }
    }
}
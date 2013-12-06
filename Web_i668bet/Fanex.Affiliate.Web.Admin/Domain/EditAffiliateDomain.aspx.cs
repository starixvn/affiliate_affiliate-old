namespace Fanex.Web.i668bet.Admin.Domain
{
    using Fanex.Affiliate.Core;
    using System;
    using System.Web;
    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;
    public partial class EditAffiliateDomain : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            string list = TemplateHandler.LoadTemplate("Domain/EditAffiliateDomain.html");
            string affDomainId = Request["affDomainID"];
            string affDomainName = Request["affDomainName"];

            AppCommon.AssignToAdminTemplate(ref list,
               "Domain", "Status", "Add", "Cancel", "Submit");
            
            TemplateHandler.Assign(ref list, "affdomainid", affDomainId);
            TemplateHandler.Assign(ref list, "AffDomainName", affDomainName);
            TemplateHandler.Assign(ref list, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");

            this.HtmlContent = list;
        }
    }
}
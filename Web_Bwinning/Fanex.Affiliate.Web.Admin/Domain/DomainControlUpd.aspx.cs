namespace Fanex.Web.Bwinning.Admin.Domain
{
    using System;
    using DNAide.Core;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin.GenerateHTML;

    public partial class Domain_DomainControlUpd : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControlUpd.htm");
            AppCommon.AssignToAdminTemplate(ref strTpl,
               "Market", "AffDomain", "RedirectDomain", "Status");
            TemplateHandler.Assign(ref strTpl, "slMarket", Domain.GenCountryList(Request["market"]));
            TemplateHandler.Assign(ref strTpl, "slAffDomain", Domain.GenAffDomainList(Request["affdomain"]));
            TemplateHandler.Assign(ref strTpl, "slRedirectDomain", Domain.Gen12BetDomainList(Request["redirectdomain"]));
            TemplateHandler.Assign(ref strTpl, "submit", Fanex.Affiliate.Resources.Admin.Submit);
            TemplateHandler.Assign(ref strTpl, "cancel", Fanex.Affiliate.Resources.Admin.Cancel);
            TemplateHandler.Assign(ref strTpl, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");
            TemplateHandler.Assign(ref strTpl, "id", Request["id"]);
            this.HtmlContent = strTpl;
        }
    }
}
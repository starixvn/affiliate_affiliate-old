namespace Fanex.Web.ITCBet.Admin.Domain
{
    using System;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin.GenerateHTML;

    public partial class Domain_DomainControlIns : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControlIns.htm");
            AppCommon.AssignToAdminTemplate(ref strTpl,
                "Market", "AffDomain", "RedirectDomain", "Status");
            TemplateHandler.Assign(ref strTpl, "slMarket", Domain.GenCountryList(string.Empty));
            TemplateHandler.Assign(ref strTpl, "slAffDomain", Domain.GenAffDomainList(string.Empty));
            TemplateHandler.Assign(ref strTpl, "slRedirectDomain", Domain.Gen12BetDomainList(string.Empty));
            TemplateHandler.Assign(ref strTpl, "submit", Fanex.Affiliate.Resources.Admin.Add);
            TemplateHandler.Assign(ref strTpl, "cancel", Fanex.Affiliate.Resources.Admin.Cancel);
            this.HtmlContent = strTpl;

        }
    }
}
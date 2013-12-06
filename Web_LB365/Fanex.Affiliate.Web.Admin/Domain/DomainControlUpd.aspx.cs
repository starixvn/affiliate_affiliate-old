namespace Fanex.Web.LiveBet365.AffiliateAdmin.Domain
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

            TemplateHandler.Assign(ref strTpl, "slMarket", Domain.GenCountryList(Request["market"]));
            TemplateHandler.Assign(ref strTpl, "slAffDomain", Domain.GenAffDomainList(Request["affdomain"]));
            TemplateHandler.Assign(ref strTpl, "slRedirectDomain", Domain.Gen12BetDomainList(Request["redirectdomain"]));
            TemplateHandler.Assign(ref strTpl, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
            TemplateHandler.Assign(ref strTpl, "cancel", Fanex.Affiliate.Resources.Translator.Get("Cancel"));
            TemplateHandler.Assign(ref strTpl, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");
            TemplateHandler.Assign(ref strTpl, "id", Request["id"]);
            this.HtmlContent = strTpl;
        }
    }
}
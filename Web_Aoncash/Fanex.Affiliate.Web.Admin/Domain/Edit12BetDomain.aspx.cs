namespace Fanex.Web.Aoncash.Admin.Domain
{
    using System;
    using Fanex.Affiliate.Core;
    using System.Web;
    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;
    public partial class Edit12BetDomain : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            string list = TemplateHandler.LoadTemplate("Domain/Edit12BetDomain.html");
            string _12BetDomainID = Request["12BetDomainID"];
            string _12BetDomainName = Request["12BetDomainName"];

            TemplateHandler.Assign(ref list, "12BetDomain", Fanex.Affiliate.Resources.Admin.Domain);
            TemplateHandler.Assign(ref list, "Status", Fanex.Affiliate.Resources.Translator.Get("Status"));
            TemplateHandler.Assign(ref list, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
            TemplateHandler.Assign(ref list, "cancel", Fanex.Affiliate.Resources.Translator.Get("Cancel"));
            TemplateHandler.Assign(ref list, "12betdomainid", _12BetDomainID);
            TemplateHandler.Assign(ref list, "12BetDomainName", _12BetDomainName);
            TemplateHandler.Assign(ref list, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");

            this.HtmlContent = list;
        }
    }
}
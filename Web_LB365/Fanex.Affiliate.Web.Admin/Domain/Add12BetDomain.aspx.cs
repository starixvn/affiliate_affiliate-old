namespace Fanex.Web.LiveBet365.AffiliateAdmin.Domain
{
    using System;
    using System.Web;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;

    public partial class Add12BetDomain : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.Private);
            string list = TemplateHandler.LoadTemplate("Domain/Add12BetDomain.html");
            AppCommon.AssignToTemplate(ref list,
                "Domain", "Status", "Add", "Cancel", "Submit");
            
            this.HtmlContent = list;

        }
    }
}
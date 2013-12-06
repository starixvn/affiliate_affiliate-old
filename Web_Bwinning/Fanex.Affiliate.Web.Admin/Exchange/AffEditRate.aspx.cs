namespace Fanex.Web.Bwinning.Admin.Exchange
{
    using System;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;

    public partial class AffEditRate : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Exchange/AffEditRate.htm");
            TemplateHandler.Assign(ref strTpl, "Currency", Fanex.Affiliate.Resources.Translator.Get("Currency"));
            TemplateHandler.Assign(ref strTpl, "rate", Request["tocurrency"] + " rate");
            TemplateHandler.Assign(ref strTpl, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
            if (string.IsNullOrEmpty(Request["exid"]) || string.IsNullOrEmpty(Request["curr"]) || string.IsNullOrEmpty(Request["rate"]))
                this.HtmlContent = "Invalid information";
            int exId = CastAide.AsInt32(Request["exid"], -1);
            TemplateHandler.Assign(ref strTpl, "curr-val", Request["curr"]);
            TemplateHandler.Assign(ref strTpl, "rate-val", Request["rate"]);
            TemplateHandler.Assign(ref strTpl, "exid-val", exId.ToString());
            this.HtmlContent = strTpl;
        }
    }
}

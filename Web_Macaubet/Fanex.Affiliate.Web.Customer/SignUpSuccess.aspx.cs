namespace Fanex.Web.Macaubet.Customer
{
    using System;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;

    public partial class SignUpSuccess : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strContent = TemplateHandler.LoadTemplate("RegistrationSuccess.htm");
            TemplateHandler.Assign(ref strContent, "openacc", Translator.Get("UnderReview"));
            TemplateHandler.Assign(ref strContent, "thankscontent", Translator.Get("RegisSuccess"));
            TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
            ltrSuccess.Text = strContent;
        }
    }
}
namespace Fanex.Web.Bwinning.Admin.Exchange
{
    using System;
    using System.Text;
    using System.Linq;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;


    public partial class AddAffCurrencies : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Exchange/AddAffCurrencies.htm");            
            var str = new StringBuilder();
            var aff = AppSettings.AffCurrencies.Select(c => c.Value);
            var currencies = AppSettings.SupportedBaseCurrencies.Select(c => c.Value).Where(c => !aff.Contains(c));

           
           foreach (var item in currencies)
           {
            str.AppendFormat("<INPUT NAME='options' TYPE='CHECKBOX' VALUE='{0}'>{0}<BR>", item);
           }
            
            
            TemplateHandler.Assign(ref strTpl, "content", str.ToString());
            TemplateHandler.Assign(ref strTpl, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
            
            this.HtmlContent = strTpl;
        }
    }
}

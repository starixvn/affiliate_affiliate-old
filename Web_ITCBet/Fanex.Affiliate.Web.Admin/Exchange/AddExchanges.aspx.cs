namespace Fanex.Web.ITCBet.Admin.Exchange
{
    using System;
    using System.Text;
    using System.Linq;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;


    public partial class AddExchanges : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Exchange/AddExchanges.htm");            
            var strTable = new StringBuilder();
            // table header
            strTable.Append("<tr><th></th><th>Code</th><th>Name</th>");
            foreach (var item in AppSettings.AffCurrencies)
	        {
                strTable.AppendFormat("<th>{0}</th>",item.Value);
	        }
            strTable.Append("</tr>");
            // table body
            var currencies = Currency.GetCurrenciesNotInExchangeTable();
            
            foreach (var item in currencies)
            {
                strTable.AppendFormat("<tr><td><input data-id='{0},${1}$,${2}$' type='checkbox'/></td><td>{1}</td><td>{2}</td>", item.Id, item.Value, item.Name);
                for (int i = 0; i < AppSettings.AffCurrencies.Count; i++)
                {
                    strTable.Append("<td><input type='number' disabled style='width:50px'/></td>");
                }
                strTable.Append("</tr>");
            }

            TemplateHandler.Assign(ref strTpl, "affcurrencies", String.Join(",",AppSettings.AffCurrencies.Select(c => c.Value)));
            TemplateHandler.Assign(ref strTpl, "table_content", strTable.ToString());
            TemplateHandler.Assign(ref strTpl, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
            
            this.HtmlContent = strTpl;
        }
    }
}

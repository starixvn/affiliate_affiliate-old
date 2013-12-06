using System;
using System.Linq;
using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;
using Fanex.Affiliate.Model;
using System.Text;

public partial class UserInfo_Adjustment : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrencyUtils.SetDefaultCurrency(AdminSession.AffCurrency);
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/Adjustment.htm");
        AppCommon.AssignToAdminTemplate(ref strTpl,
        "affiliate", "BalanceForward", "Description", "submit", "Adjustment");

        string strContent = TemplateHandler.CopyBlock(strTpl, "content");
        int custid = AdminSession.AffId;
        TemplateHandler.Assign(ref strTpl, "affiliate-val", AdminSession.AffUsername);
        TemplateHandler.Assign(ref strTpl, "custid", custid);
        var maxAmount = Settings.GetMaxAdjustmentAmount(AdminSession.AffCurrency);
        TemplateHandler.Assign(ref strTpl, "maxamount-val", maxAmount);
        TemplateHandler.Assign(ref strTpl, "AdjustAmountUpTo", String.Format(Fanex.Affiliate.Resources.Admin.AdjustAmountUpTo, CurrencyUtils.Format(maxAmount)));

        var currency = AdminSession.AffCurrency;
        var currencyObj = AppSettings.AffSystemSupportedCurrencies.Where(x => x.Value == currency).FirstOrDefault() as CurrencyObj;
        if (currencyObj != null && currencyObj.MultipliedBy > 1)
        {                        
            TemplateHandler.Assign(ref strTpl, "CurrencyNotifications",
                AdminCommon.CreateCurrencyNotificationLine(currency, currencyObj.MultipliedBy));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "CurrencyNotifications", null);
        }
        
        DbResult rs = Affiliate.SelBalanceBF(custid);

        decimal dcBF = CastAide.AsDecimal(rs.Outputs["balbroughtforward"]);
        TemplateHandler.Assign(ref strTpl, "balforward-val", CurrencyUtils.Format(dcBF)); 
        TemplateHandler.Assign(ref strTpl, "bf-val", NumberFormat.DecFormat(dcBF, 2));
        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 4, 0, Fanex.Affiliate.Resources.Translator.Get("Adjustment"), "i_customer.png"));
        //string subPage = Common.GenSubPage(6);
        //TemplateHandler.Assign(ref subPage, "ContentPage", strTpl);
        #endregion

        this.HtmlContent = strTpl;

    }
}

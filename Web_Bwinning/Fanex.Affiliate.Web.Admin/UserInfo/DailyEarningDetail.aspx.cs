using System;
using System.Data;
using System.Text;
using DNAide.Web;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;
using Fanex.Affiliate.Resources;

public partial class DailyEarningDetail : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime _date = (Request["monthdate"] != null) ? CastAide.AsDateTime(Request["monthdate"]) : DateTime.Now.AddDays(-1);
        int custID = CastAide.AsInt32(Request["custid"]);
        int productId = CastAide.AsInt32(Request["slProduct"], 0);
        int step1 = CastAide.AsInt32(Request["step1"], 0) + 1;
        int step2 = CastAide.AsInt32(Request["step2"], 0) + 1;

        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDailyDetail.htm");
        AppCommon.AssignToAdminTemplate(ref strTemplate, "SearchBy");
        int width = 780;
        if (productId > 1)
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "sportsbook");
            width = 420;
        }

        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "custid", custID);
        TemplateHandler.Assign(ref strTemplate, "step1", step1);
        TemplateHandler.Assign(ref strTemplate, "step2", step2);
        TemplateHandler.Assign(ref strTemplate, "slProduct", AdminCommon.GenListProduct(productId));
        TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);

        TemplateHandler.Assign(ref strTemplate, "detailearnings", Fanex.Affiliate.Resources.Translator.Get("DetailEarning"));
        TemplateHandler.Assign(ref strTemplate, "date", _date.ToString("dd MMMM yyyy"));
        
        TemplateHandler.Assign(ref strTemplate, "username", Fanex.Affiliate.Resources.Translator.Get("Bet12Username"));

        TemplateHandler.Assign(ref strTemplate, "deposit", Fanex.Affiliate.Resources.Translator.Get("Deposit"));
        TemplateHandler.Assign(ref strTemplate, "withdrawal", Fanex.Affiliate.Resources.Translator.Get("Withdrawal"));
        TemplateHandler.Assign(ref strTemplate, "bonus", Fanex.Affiliate.Resources.Translator.Get("Bonus"));
        TemplateHandler.Assign(ref strTemplate, "winlost", Fanex.Affiliate.Resources.Translator.Get("Winlost"));
        TemplateHandler.Assign(ref strTemplate, "turnover", Fanex.Affiliate.Resources.Translator.Get("Turnover"));
        TemplateHandler.Assign(ref strTemplate, "adjustment", Fanex.Affiliate.Resources.Translator.Get("Adjustment"));
        TemplateHandler.Assign(ref strTemplate, "psp", Fanex.Affiliate.Resources.Translator.Get("PSP"));
        TemplateHandler.Assign(ref strTemplate, "total", Fanex.Affiliate.Resources.Translator.Get("Total"));
        TemplateHandler.Assign(ref strTemplate, "back", Fanex.Affiliate.Resources.Translator.Get("Back"));
        TemplateHandler.Assign(ref strTemplate, "StatementHistory", Fanex.Affiliate.Resources.Translator.Get("StatementHistory"));
        TemplateHandler.Assign(ref strTemplate, "earningMonth", _date.ToString("MMMM yyyy"));
        TemplateHandler.Assign(ref strTemplate, "user", CastAide.AsString(Request["user"]));

        TemplateHandler.Assign(ref strTemplate, "isreport", Request["isreport"]);

        DNAide.Data.DbResult rs = Affiliate.SelAffEarningDetail(custID, productId, _date);
        DataTable dtbResult = rs.ResultSet.Tables[0];
        if (dtbResult.Rows.Count > 0)
        {
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            string currencycode = rs.Outputs["affcurrency"].ToString();

            CurrencyUtils.SetDefaultCurrency(currencycode);
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            decimal dcDeposit = 0, dcWinlost = 0, dcTurnover = 0, dcBonus = 0, dcAdjustment = 0, dcPSP = 0, dcWithdrawal = 0;
            foreach (DataRow dr in dtbResult.Rows)
            {
                string strTmp = strContent;
                intNo++;
                dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                dcWinlost += CastAide.AsDecimal(dr["winlost"]);
                dcTurnover += CastAide.AsDecimal(dr["turnover"]);
                dcBonus += CastAide.AsDecimal(dr["bonus"]);
                dcAdjustment += CastAide.AsDecimal(dr["adjustment"]);
                dcPSP += CastAide.AsDecimal(dr["psp"]);
                dcWithdrawal += CastAide.AsDecimal(dr["withdrawal"]);

                TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd); 
                TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                  

                string strUsername2 = dr["username"].ToString();//make sure remove 12bet$ in the first position of strUsername2                    
                //strUsername2 = (strUsername2.IndexOf("12Bet$") == 0 ? strUsername2.Substring(6, strUsername2.Length - 6) : strUsername2);

                TemplateHandler.Assign(ref strTmp, "username-val", strUsername2);
                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", CurrencyUtils.Format(dr["withdrawal"]));
                TemplateHandler.Assign(ref strTmp, "winlost-val", CurrencyUtils.Format(dr["winlost"]));
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["turnover"]));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dr["bonus"]));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dr["adjustment"]));
                TemplateHandler.Assign(ref strTmp, "psp-val", CurrencyUtils.Format(dr["psp"]));
                strBuild.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTemplate, "totaldeposit", CurrencyUtils.Format(dcDeposit));
            TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", CurrencyUtils.Format(dcWithdrawal));
            TemplateHandler.Assign(ref strTemplate, "totalwinlost", CurrencyUtils.Format(dcWinlost));
            TemplateHandler.Assign(ref strTemplate, "totalturnover", CurrencyUtils.Format(dcTurnover));
            TemplateHandler.Assign(ref strTemplate, "totalbonus", CurrencyUtils.Format(dcBonus));
            TemplateHandler.Assign(ref strTemplate, "totaladjustment", CurrencyUtils.Format(dcAdjustment));
            TemplateHandler.Assign(ref strTemplate, "totalpsp", CurrencyUtils.Format(dcPSP));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", AdminCommon.NoInfo(8));
            TemplateHandler.RemoveBlock(ref strTemplate, "total");
        }
        #region Load SubPage
        if (CastAide.AsString(Request["isreport"]) == "1")
        {
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 4, 0, SubMenu.ReportText[4], "i_report.png"));
            //TemplateHandler.RemoveBlock(ref strTemplate, "isreport");
            TemplateHandler.Assign(ref strTemplate, "divCss", "padl0");
            TemplateHandler.RemoveBlock(ref strTemplate, "notreport");
            TemplateHandler.Assign(ref strTemplate, "commission", Translator.Get("Commission"));
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0, Fanex.Affiliate.Resources.Translator.Get("StatementHistory"), "i_customer.png"));
            TemplateHandler.Assign(ref strTemplate, "divCss", string.Empty);
            TemplateHandler.RemoveBlock(ref strTemplate, "isreport");
        }

        //TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0, Fanex.Affiliate.Resources.Translator.Get("StatementHistory, "i_customer.png"));
        //string subPage = Common.GenSubPage(1);
        //TemplateHandler.Assign(ref subPage, "ContentPage", strTemplate);
        #endregion

        this.HtmlContent = strTemplate;
    }
}

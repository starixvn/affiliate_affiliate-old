using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;
using Fanex.Core.Utility;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;

public partial class DailyEarning : BasePageTpl
{
    private System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDaily.htm");


        AppCommon.AssignToTemplate(ref strTemplate,
            "dailyearnings", "date", "winlost", "bonus", "deposit", "withdrawal",
            "adjustment", "psp", "total", "StatementHistory","turnover");


        DateTime _date = CastAide.AsDateTime(Request["monthdate"], DateTime.Now.AddDays(-1), cult);
        

        int step = CastAide.AsInt32(Request["hdStep"], 0) + 1;
        int custID = CastAide.AsInt32(Request["custid"]);
        
        int productId = CastAide.AsInt32(Request["slProduct"], 0);

        int width = 600;
        //if (productId > 1)
        //{
        ////    TemplateHandler.RemoveBlock(ref strTemplate, "sportsbook");
        //    width = 250;
        //}

        TemplateHandler.Assign(ref strTemplate, "custid", custID);
        TemplateHandler.Assign(ref strTemplate, "productid", productId);
        TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);
        TemplateHandler.Assign(ref strTemplate, "step", step);        
        TemplateHandler.Assign(ref strTemplate, "earningMonth", _date.ToString("MMMM yyyy"));
        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "isreport", Request["isreport"]);

       // CurrencyUtils.SetDefaultCurrency(AdminSettings.SystemCurrency);

        TemplateHandler.Assign(ref strTemplate, "user-val", CastAide.AsString(Request["user"]));

        TemplateHandler.Assign(ref strTemplate, "slProducts", AdminCommon.GenListProduct(productId));
        DNAide.Data.DbResult rs = Affiliate.SelAffEarningDaily(custID, productId, _date);
        //rs.ResultSet.Tables[0]
        DataTable dtbResult = rs.ResultSet.Tables[0];
        if (dtbResult.Rows.Count > 0)
        {
            string currencycode = rs.Outputs["affcurrency"].ToString();
            CurrencyUtils.SetDefaultCurrency(currencycode);
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            decimal dcWinlost = 0, dcDeposit = 0, dcBonus = 0,dcAdjust = 0, dcPSP = 0,dcWithdrawal=0,dcTurnover=0;
            foreach (DataRow dr in dtbResult.Rows)
            {
                string strTmp = strContent;
                intNo++;
                dcWinlost += CastAide.AsDecimal(dr["MemberWinlost"]);
                dcTurnover+= CastAide.AsDecimal(dr["turnover"]);
                dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                dcBonus += CastAide.AsDecimal(dr["bonus"]);
                dcAdjust += CastAide.AsDecimal(dr["adjustment"]);
                dcPSP += CastAide.AsDecimal(dr["psp"]);
                dcWithdrawal += CastAide.AsDecimal(dr["withdrawal"]);


                TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                TemplateHandler.Assign(ref strTmp, "date-val", CastAide.AsDateTime(dr["winlostdate"]).ToString("dd MMMM yyyy"));
                TemplateHandler.Assign(ref strTmp, "winlost-val", CurrencyUtils.Format(dr["MemberWinlost"]));
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["Turnover"]));
                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", CurrencyUtils.Format(dr["withdrawal"]));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dr["bonus"]));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dr["adjustment"]));
                TemplateHandler.Assign(ref strTmp, "psp-val", CurrencyUtils.Format(dr["psp"]));
                TemplateHandler.Assign(ref strTmp, "custid", custID.ToString());
                TemplateHandler.Assign(ref strTmp, "monthdate-val", CastAide.AsDateTime(dr["winlostdate"]).ToShortDateString());
                strBuild.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTemplate, "totalwinlost", CurrencyUtils.Format(dcWinlost));
            TemplateHandler.Assign(ref strTemplate, "totalturnover", CurrencyUtils.Format(dcTurnover));
            TemplateHandler.Assign(ref strTemplate, "totaldeposit", CurrencyUtils.Format(dcDeposit));
            TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", CurrencyUtils.Format(dcWithdrawal));
            TemplateHandler.Assign(ref strTemplate, "totalbonus", CurrencyUtils.Format(dcBonus));
            TemplateHandler.Assign(ref strTemplate, "totaladjustment", CurrencyUtils.Format(dcAdjust));
            TemplateHandler.Assign(ref strTemplate, "totalpsp", CurrencyUtils.Format(dcPSP));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", AdminCommon.NoInfo(7));
            TemplateHandler.RemoveBlock(ref strTemplate, "total");
        }
        #region Load SubPage 

        if (CastAide.AsString(Request["isreport"]) == "1")
        {
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 4, 0, SubMenu.ReportText[4], "i_report.png"));
          //  TemplateHandler.RemoveBlock(ref strTemplate, "isreport");
            TemplateHandler.Assign(ref strTemplate, "divCss", "padl0");
            TemplateHandler.Assign(ref strTemplate, "commission",Translator.Get("Commission"));
            TemplateHandler.RemoveBlock(ref strTemplate, "notreport");
        }
        else
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "isreport");
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0, Fanex.Affiliate.Resources.Translator.Get("StatementHistory"), "i_customer.png"));
            TemplateHandler.Assign(ref strTemplate, "divCss", string.Empty);
        }
        #endregion

        this.HtmlContent = strTemplate;
    }
}

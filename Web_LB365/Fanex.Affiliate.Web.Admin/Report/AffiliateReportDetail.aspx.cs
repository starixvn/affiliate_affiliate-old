using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Utils;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;

public partial class Report_AffiliateReportDetail : BasePageTpl
{
    string strTpl = string.Empty;
    string affCurrency = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateReportDetail.htm");

        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 1, 0, SubMenu.ReportText[1], "i_report.png"));

        GenerateHTML();
        AssignHeader();

        this.HtmlContent = strTpl;
    }


    void GenerateHTML()
    {
        
        string strAffName = CastAide.AsString(Request["affname"], string.Empty);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);

        TemplateHandler.Assign(ref strTpl, "affname", strAffName);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        //TemplateHandler.Assign(ref strTpl, "turnover-total", CurrencyUtils.Format(dcTotalTurnover, 2));
            
        //TemplateHandler.Assign(ref strTpl, "affid", intAffId);

        DataTable dtb = new DataTable();
        DbResult rs = null;
        rs = Affiliate.SelAffiliateReportByAff(strAffName, dtmFDate, dtmTDate, isGetAll);

        if (CastAide.AsInt32(rs.Outputs["affid"]) != 0 || CastAide.AsString(Request["isclk"]) == "1")
            TemplateHandler.Assign(ref strTpl, "err-display", "none");
        else
                TemplateHandler.Assign(ref strTpl, "err-display", "block");

        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        if (dtb.Rows.Count > 0)
        {
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strDate = string.Empty;
            string strCheck = string.Empty;
            string strColor = AdminCommon.RowBgEven;
            string strRows = string.Empty;
            int intCount = 0;
            affCurrency = rs.Outputs["affcurrency"].ToString();

            decimal dcTotalDeposit = 0, dcTotalWidthrawal = 0, dcTotalTurnover = 0, dcTotalWinloss = 0, dcTotalBonus = 0, dcTotalPsp = 0, dcTotalAdjustment = 0, dcTotalAffBF = 0, dcTotalAffWithdrawal = 0, dcTotalAffAdjustment = 0;
            StringBuilder strListRow = new StringBuilder();
            StringBuilder strRowByDate = new StringBuilder();

            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;
                    

                decimal dcAffExRate = CastAide.AsDecimal(dr["AffExRate"]);
                decimal dcDeposit = 0, dcWidthrawal = 0, dcTurnover = 0, dcWinloss = 0, dcBonus = 0, dcPsp = 0, dcAdjustment = 0, dcAffBF = 0, dcAffWithdrawal = 0, dcAffAdjustment = 0;

                dcDeposit = CastAide.AsDecimal(dr["deposit"]);
                dcWidthrawal = CastAide.AsDecimal(dr["withdrawal"]);
                dcTurnover = CastAide.AsDecimal(dr["turnover"]);
                dcWinloss = CastAide.AsDecimal(dr["winlost"]);
                dcBonus = CastAide.AsDecimal(dr["bonus"]);
                dcPsp = CastAide.AsDecimal(dr["psp"]);
                dcAdjustment = CastAide.AsDecimal(dr["adjustment"]);
                dcAffAdjustment = CastAide.AsDecimal(dr["affadjustment"]);
                dcAffWithdrawal = CastAide.AsDecimal(dr["affwithdrawal"]);
                dcAffBF = CastAide.AsDecimal(dr["affbf"]);
                //string strPrefix = dr["Prefix"].ToString();

                var currency = dr["Currency"].ToString();

                TemplateHandler.Assign(ref strTmp, "date-val", CastAide.AsDateTime(dr["Winlostdate"]).ToString("d-MMM-yy ddd"));
                TemplateHandler.Assign(ref strTmp, "currency-val", dr["Currency"]);

                TemplateHandler.Assign(ref strTmp, "units-val", NumberFormat.DecFormat(dcAffExRate, 5));

                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dcDeposit,currency,null));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", CurrencyUtils.Format(dcWidthrawal, currency, null));
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dcTurnover, currency, null));
                TemplateHandler.Assign(ref strTmp, "memwinloss-val", CurrencyUtils.Format(dcWinloss, currency, null));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dcBonus, currency, null));
                TemplateHandler.Assign(ref strTmp, "psp-val", CurrencyUtils.Format(dcPsp, currency, null));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dcAdjustment, currency, null));
                TemplateHandler.Assign(ref strTmp, "affadjustment-val", CurrencyUtils.Format(dcAffAdjustment, currency, null));
                TemplateHandler.Assign(ref strTmp, "affwithdrawal-val", CurrencyUtils.Format(dcAffWithdrawal, currency, null));
                TemplateHandler.Assign(ref strTmp, "affbf-val", CurrencyUtils.Format(dcAffBF, currency, null));

                TemplateHandler.Assign(ref strTmp, "depositeqv-val", CurrencyUtils.Format(dcDeposit * dcAffExRate,affCurrency,null));
                TemplateHandler.Assign(ref strTmp, "withdrawaleqv-val", CurrencyUtils.Format(dcWidthrawal * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "turnovereqv-val", CurrencyUtils.Format(dcTurnover * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "memwinlosseqv-val", CurrencyUtils.Format(dcWinloss * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "bonuseqv-val", CurrencyUtils.Format(dcBonus * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "pspeqv-val", CurrencyUtils.Format(dcPsp * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "adjustmenteqv-val", CurrencyUtils.Format(dcAdjustment * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "affadjustmenteqv-val", CurrencyUtils.Format(dcAffAdjustment * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "affwithdrawaleqv-val", CurrencyUtils.Format(dcAffWithdrawal * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "affbfeqv-val", CurrencyUtils.Format(dcAffBF * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "affwithdrawaleqv-val", CurrencyUtils.Format(dcAffAdjustment * dcAffExRate, affCurrency, null));
                TemplateHandler.Assign(ref strTmp, "affadjustmenteqv-val", CurrencyUtils.Format(dcAffWithdrawal * dcAffExRate, affCurrency, null));
                ////NEW//

                if (string.Compare(strCheck, dr["Winlostdate"].ToString(), true) != 0)
                {
                    if (string.IsNullOrEmpty(strCheck))
                    {
                        intCount++;
                    }
                    else
                    {
                        strRows = strRowByDate.ToString();
                        TemplateHandler.Assign(ref strRows, "numrow", intCount);

                        strColor = (strColor == AdminCommon.RowBgOdd ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                        TemplateHandler.Assign(ref strRows, "color", strColor);

                        strListRow.Append(strRows);
                        intCount = 1;
                        strRowByDate.Remove(0, strRowByDate.Length);
                    }
                    strRowByDate.Append(strTmp);

                    strCheck = dr["Winlostdate"].ToString();
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTmp, "date", string.Empty);
                    strRowByDate.Append(strTmp);
                    intCount++;
                }

                dcTotalDeposit += dcDeposit * dcAffExRate;
                dcTotalWidthrawal += dcWidthrawal * dcAffExRate;
                dcTotalTurnover += dcTurnover * dcAffExRate;
                dcTotalWinloss += dcWinloss * dcAffExRate;
                dcTotalBonus += dcBonus * dcAffExRate;
                dcTotalPsp += dcPsp * dcAffExRate;
                dcTotalAdjustment += dcAdjustment * dcAffExRate;
                dcTotalAffAdjustment += dcAffAdjustment * dcAffExRate;
                dcTotalAffWithdrawal += dcAffWithdrawal * dcAffExRate;
                dcTotalAffBF += dcAffBF * dcAffExRate;
            }
            //Append last row
            strRows = strRowByDate.ToString();
            TemplateHandler.Assign(ref strRows, "numrow", intCount);
            strColor = (strColor == AdminCommon.RowBgOdd ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
            TemplateHandler.Assign(ref strRows, "color", strColor);
            strListRow.Append(strRows);

            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            
            CurrencyUtils.SetDefaultCurrency(affCurrency);
            TemplateHandler.Assign(ref strTpl, "deposit-total", CurrencyUtils.Format(dcTotalDeposit));
            TemplateHandler.Assign(ref strTpl, "withdrawal-total", CurrencyUtils.Format(dcTotalWidthrawal));
            TemplateHandler.Assign(ref strTpl, "turnover-total", CurrencyUtils.Format(dcTotalTurnover));
            TemplateHandler.Assign(ref strTpl, "memwinloss-total", CurrencyUtils.Format(dcTotalWinloss));
            TemplateHandler.Assign(ref strTpl, "bonus-total", CurrencyUtils.Format(dcTotalBonus));
            TemplateHandler.Assign(ref strTpl, "psp-total", CurrencyUtils.Format(dcTotalPsp));
            TemplateHandler.Assign(ref strTpl, "adjustment-total", CurrencyUtils.Format(dcTotalAdjustment));
            TemplateHandler.Assign(ref strTpl, "affadjustment-total", CurrencyUtils.Format(dcTotalAffAdjustment));
            TemplateHandler.Assign(ref strTpl, "affwithdrawal-total", CurrencyUtils.Format(dcTotalAffWithdrawal));
            TemplateHandler.Assign(ref strTpl, "affbf-total", CurrencyUtils.Format(dcTotalAffBF));
            ////NEW//
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(21));
        }
    }
    

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "aff-txt", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Fanex.Affiliate.Resources.Translator.Get("From"));
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Fanex.Affiliate.Resources.Translator.Get("To"));
        TemplateHandler.Assign(ref strTpl, "search", Fanex.Affiliate.Resources.Translator.Get("Search"));
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "memstatistics", Fanex.Affiliate.Resources.Translator.Get("MemberStatistics"));
        TemplateHandler.Assign(ref strTpl, "date", Fanex.Affiliate.Resources.Translator.Get("Date"));
        TemplateHandler.Assign(ref strTpl, "currency", Fanex.Affiliate.Resources.Translator.Get("Currency"));
        TemplateHandler.Assign(ref strTpl, "units", Fanex.Affiliate.Resources.Translator.Get("Units") + "/" + affCurrency);
        TemplateHandler.Assign(ref strTpl, "deposit", Fanex.Affiliate.Resources.Translator.Get("Deposit"));
        TemplateHandler.Assign(ref strTpl, "turnover", Fanex.Affiliate.Resources.Translator.Get("Turnover"));
        TemplateHandler.Assign(ref strTpl, "bonus", Fanex.Affiliate.Resources.Translator.Get("Bonus"));
        TemplateHandler.Assign(ref strTpl, "withdrawal", Fanex.Affiliate.Resources.Translator.Get("Withdrawal"));
        TemplateHandler.Assign(ref strTpl, "adjustment", Fanex.Affiliate.Resources.Translator.Get("Adjustment"));
        TemplateHandler.Assign(ref strTpl, "affbf", Fanex.Affiliate.Resources.Translator.Get("BFComm"));
        TemplateHandler.Assign(ref strTpl, "winlost", Fanex.Affiliate.Resources.Translator.Get("Winlost"));
        TemplateHandler.Assign(ref strTpl, "original", Fanex.Affiliate.Resources.Translator.Get("Original"));
        TemplateHandler.Assign(ref strTpl, "usdeqv", affCurrency + " " + Fanex.Affiliate.Resources.Translator.Get("UsdEqv"));
        TemplateHandler.Assign(ref strTpl, "psp", Fanex.Affiliate.Resources.Translator.Get("PSP"));
        TemplateHandler.Assign(ref strTpl, "total", Fanex.Affiliate.Resources.Translator.Get("Total"));
        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("AffiliateReportDetail") + " [" + Request["affname"] + "]");
        TemplateHandler.Assign(ref strTpl, "affstatistics", Fanex.Affiliate.Resources.Translator.Get("AffiliateStatistics"));
        TemplateHandler.Assign(ref strTpl, "err-msg", Fanex.Affiliate.Resources.Translator.Get("InvalidAffiliate"));
        //Assign color
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
        //TemplateHandler.Assign(ref strTpl, "color", AdminCommon.RowBgOdd);
        TemplateHandler.Assign(ref strTpl, "colormonthly", AdminCommon.RowBgPending);

        //Hung added
        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

    }
}

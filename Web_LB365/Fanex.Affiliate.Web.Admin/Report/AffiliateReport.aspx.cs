using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.Core.Helper;
using Fanex.Affiliate.DataObjects;

public partial class Report_AffiliateReport : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateReport.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        AppCommon.AssignToTemplate(ref strTpl,
        "From", "To", "search", "memberstatistics", "affiliatestatistics", "date", "currency",
        "deposit", "turnover", "bonus", "withdrawal", "adjustment", "BFComm", "winlost", "affiliate",
        "total", "psp", "back", "currency","afftype");

        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("LimitDate") + AppSettings.SystemStartDate.ToString("d-MMM-yy"));
        //Assign color
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);

        //Hung added
        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

    }

    
    void GenerateHTML()
    {
        
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        string strPreferLang = Request["slPreferLang"];
        int currency = CastAide.AsInt32(Request["slCurrency"],0);

        int afftype = CastAide.AsInt32(Request["afftypelist"], -1);

        var hideAfftype = AppSettings.BaseAffTypes.Count < 2;

        if (hideAfftype)
        {
            TemplateHandler.RemoveBlock(ref strTpl, "afftype");
        }
        else
        TemplateHandler.Assign(ref strTpl, "afftypelist",
            HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypelist", afftype, hideAfftype ? null : "Select Affiliate Type", null, hideAfftype));  

        TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        //TemplateHandler.Assign(ref strTpl, "turnover-total", NumberFormat.DecFormat(dcTotalTurnover, 2));

        //Begin list Language
        //TemplateHandler.Assign(ref strTpl, "listlang", AdminCommon.GetListSupportedLang(true, false));
        var htLang = AppSettings.BaseLanguages; // AdminCommon.GetListSupportedLang();
        TemplateHandler.Assign(ref strTpl, "listlang",
            HtmlHelper.DropDownList(htLang, "slPreferLang", strPreferLang, "Select Language"));
        //1  Common.GenDropdownList((IList<string>)htLang["key"], (IList<string>)htLang["value"], "slPreferLang", false, "Select Language", ""));
        //End list Language

        //Begin list currency
        var lsCurrency = AppSettings.AffCurrencies; //AdminCommon.GetListSupportedCurrency();
            //TemplateHandler.Assign(ref strTpl, "lscurrency", Common.GenDropdownList(lsCurrency, lsCurrency, "slCurrency", false, "Select Currency", currency));
            TemplateHandler.Assign(ref strTpl, "lscurrency",
                HtmlHelper.DropDownList(lsCurrency, "slCurrency", currency, "Select Currency", null, false));
                // Common.GenDropdownList(lsCurrency, lsCurrency, "slCurrency", false, "Select Currency", currency));
            //End list currency
 DataTable dtb = new DataTable();
            DbResult rs = null;
            rs = Affiliate.SelAffiliateReport(strPreferLang, dtmFDate, dtmTDate, isGetAll,afftype, currency);

        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        if (dtb.Rows.Count > 0)
        {
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strDate = string.Empty;
            int intCount = 0;                

            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 1, dtb.Rows.Count, SubMenu.ReportText[1], "i_report.png"));

            strListRow = new StringBuilder();
            foreach (DataRow dr in dtb.Rows)
            {
                var affcurrency =  dr["Currency"].ToString();
                CurrencyUtils.SetDefaultCurrency(affcurrency);
                string strTmp = strRowTpl;
                intCount++;
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
                string strPrefix = dr["Prefix"].ToString();

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "affid", dr["affid"]);
                TemplateHandler.Assign(ref strTmp, "aff-val",  dr["username"]);
                TemplateHandler.Assign(ref strTmp, "afftype-val", dr["afftypename"]);
                TemplateHandler.Assign(ref strTmp, "currency-val", dr["Currency"]);


                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dcDeposit));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", CurrencyUtils.Format(dcWidthrawal));
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dcTurnover));
                TemplateHandler.Assign(ref strTmp, "winlost-val", CurrencyUtils.Format(dcWinloss));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dcBonus));
                TemplateHandler.Assign(ref strTmp, "psp-val", CurrencyUtils.Format(dcPsp));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dcAdjustment));
                TemplateHandler.Assign(ref strTmp, "affadjustment-val", CurrencyUtils.Format(dcAffAdjustment));
                TemplateHandler.Assign(ref strTmp, "affwithdrawal-val", CurrencyUtils.Format(dcAffWithdrawal));
                TemplateHandler.Assign(ref strTmp, "affbf-val", CurrencyUtils.Format(dcAffBF));
                

                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(12));
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 1, 0, SubMenu.ReportText[1], "i_report.png"));
        }
    }
}

using System;
using System.Collections;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Utils;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Helper;
using Fanex.Affiliate.Resources;

public partial class MemberReport : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    string _systemCurrency = AdminSettings.SystemCurrency.Value;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrencyUtils.SetDefaultCurrency(_systemCurrency);

        strTpl = TemplateHandler.LoadTemplate("Report/MemberReport.htm");
        
        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }
    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);

        AppCommon.AssignToAdminTemplate(ref strTpl,
            "From", "To", "search", "date", "currency", "deposit", "turnover", "bonus",
            "MemberWinlost", "affwinlost", "housewinlost", "AffNetWinLoss", "housenetwinlost",
            "original", "subtotal", "back","afftype");

        TemplateHandler.Assign(ref strTpl, "units", Admin.Units + "/" + AdminSettings.SystemCurrency.Value);
        TemplateHandler.Assign(ref strTpl, "usdeqv", AdminSettings.SystemCurrency.Value + " " + Fanex.Affiliate.Resources.Admin.UsdEqv);
        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("LimitDate") + AppSettings.SystemStartDate.ToString("d-MMM-yy"));
        //Assign color
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "color", AdminCommon.RowBgOdd);
        TemplateHandler.Assign(ref strTpl, "colormonthly", AdminCommon.RowBgPending);        
        
        //Hung added
        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        
    }

    void GenerateHTML()
    {
        int intCustId = 0;
        string strMenuText = string.Empty, strUsername = string.Empty;
        if (string.Compare(Request["isaff"], "1", true) == 0)
        {
            if (!string.IsNullOrEmpty(Request["custid"]) && !string.IsNullOrEmpty(Request["username"]))
            {
                intCustId = CastAide.AsInt32(Request["custid"]);
                strUsername = CastAide.AsString(Request["username"]);
            }
            else
            {
                intCustId = AdminSession.AffId;
                strUsername = AdminSession.AffUsername;
            }
            TemplateHandler.Assign(ref strTpl, "pagepadd", string.Empty);
            TemplateHandler.Assign(ref strTpl, "username", strUsername);
            TemplateHandler.Assign(ref strTpl, "username-val", strUsername + " --- ");
            TemplateHandler.Assign(ref strTpl, "custid", intCustId);
            strMenuText = Fanex.Affiliate.Resources.Translator.Get("ReportDetails") + " [" + strUsername + "]";
            //TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 1, 0, Fanex.Affiliate.Resources.Translator.Get("Report, "i_customer.png"));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "pagepadd", "padl0");
            TemplateHandler.Assign(ref strTpl, "username", string.Empty);
            TemplateHandler.Assign(ref strTpl, "username-val", string.Empty);
            TemplateHandler.Assign(ref strTpl, "custid", string.Empty);
            strMenuText = Fanex.Affiliate.Resources.Translator.Get("ReportDetails");
        }
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 0, 0, SubMenu.ReportText[0], "i_report.png"));

        TemplateHandler.Assign(ref strTpl, "isaff-val", Request["isaff"]);
                       
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        if (dtmFDate < AppSettings.SystemStartDate)
            dtmFDate = AppSettings.SystemStartDate;
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

        int afftype = CastAide.AsInt32(Request["afftypelist"], -1);

        var hideAfftype = AppSettings.BaseAffTypes.Count < 2;

        var subtotal_colspan = hideAfftype ? 3 : 4;
        var month_colspan = hideAfftype ? 1 : 2;
        if (hideAfftype)
        {
            TemplateHandler.RemoveBlock(ref strTpl, "afftype");

        }
        else
            TemplateHandler.Assign(ref strTpl, "afftypelist",
                HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypelist", afftype, hideAfftype ? null : "All types", null, hideAfftype));
        TemplateHandler.Assign(ref strTpl, "subtotal_colspan", subtotal_colspan);
        TemplateHandler.Assign(ref strTpl, "month_colspan", month_colspan);
        DataTable dtb = new DataTable();
        DbResult rs = null;
        if (intCustId != 0)
        {
            TemplateHandler.Assign(ref strTpl, "hwidth", "170");
            rs = Affiliate.SelMemberReportByAff(intCustId, dtmFDate, dtmTDate,afftype);
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "username", string.Empty);
            TemplateHandler.Assign(ref strTpl, "hwidth", "100");
            rs = Affiliate.SelMemberReport(dtmFDate, dtmTDate,afftype);
        }
        if(rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];
        if (dtb.Rows.Count > 0)
        {
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strSubTpl = TemplateHandler.ReplaceBlock(ref strTpl, "subtotal", "");
            string strTotal = TemplateHandler.ReplaceBlock(ref strTpl, "total", "");
            string strDate = string.Empty;
            int intCount = 0, numRow = 0;
            decimal dcDeposit = 0, dcTurnover = 0, dcBonus = 0, dcMemWinloss = 0, dcAffWinloss = 0, dcHouseWinloss = 0, dcAffNetPos = 0, dcHouseNetWinloss = 0;
            Hashtable hsTotal = new Hashtable();//Use for Total monthly
            ArrayList arr = new ArrayList();
            strListRow = new StringBuilder();
            StringBuilder strRow = new StringBuilder();
            int intMonth = 0;
            foreach (DataRow dr in dtb.Rows)
            {
                string strTmpDate = CastAide.AsDateTime(dr["Winlostdate"]).ToString("d-MMM-yy ddd");
                string strTmp = TemplateHandler.CopyBlock(strRowTpl, "currency");
                string strNewRow = string.Empty;
                //Load 1 row without date value
                decimal dcActualRate = CastAide.AsDecimal(dr["actualrate"]);

                var currencySymbol = dr["Currency"].ToString();


                //Add new 1 column here
                TemplateHandler.Assign(ref strTmp, "afftype-val", dr["afftypename"]);
                TemplateHandler.Assign(ref strTmp, "currency-val", currencySymbol);
                TemplateHandler.Assign(ref strTmp, "units-val", NumberFormat.DecFormat(dcActualRate, 5));
                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dr["deposit"],currencySymbol,null));
                TemplateHandler.Assign(ref strTmp, "depositeqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["deposit"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["Turnover"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "turnovereqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["Turnover"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dr["Bonus"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "bonuseqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["Bonus"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "memwinloss-val", CurrencyUtils.Format(dr["MemberWinLost"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "memwinlosseqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "affwinloss-val", CurrencyUtils.Format(dr["AffiliateWinLost"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "affwinlosseqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "housewinloss-val", CurrencyUtils.Format(dr["HouseWinLost"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "housewinlosseqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "AffNetWinLoss-val", CurrencyUtils.Format(dr["AffiliateNetWinLost"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "AffNetWinLosseqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate, _systemCurrency, null));
                TemplateHandler.Assign(ref strTmp, "housenetwinloss-val", CurrencyUtils.Format(dr["HouseNetWinLost"], currencySymbol, null));
                TemplateHandler.Assign(ref strTmp, "housenetwinlosseqv-val", CurrencyUtils.Format(CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate, _systemCurrency, null));

                numRow++;


                
                if (string.Compare(strDate, strTmpDate, true) != 0)//New row
                {
                    //hsTotal = new Hashtable();
                    #region insert old row to list rows
                    string strOldRow = strRow.ToString();
                    TemplateHandler.Assign(ref strOldRow, "numrow", intCount.ToString());
                    strRow = new StringBuilder();
                    strListRow.Append(strOldRow);
                    #endregion

                    if (!string.IsNullOrEmpty(strDate))//insert subtotal row for list rows before
                    {
                        GenerateSubTotal(strRow, strSubTpl, intCount, ref dcDeposit, ref dcTurnover, ref dcBonus, ref dcMemWinloss, ref dcAffWinloss, ref dcHouseWinloss, ref dcAffNetPos, ref dcHouseNetWinloss);//Sub total                            
                    }
                    strNewRow = strRowTpl;

                    TemplateHandler.Assign(ref strNewRow, "date-val", strTmpDate);
                    
                    TemplateHandler.ReplaceBlock(ref strNewRow, "currency", strTmp);
                    strRow.Append(strNewRow);

                    strDate = strTmpDate;
                    intCount = 0;
                }
                
                dcDeposit += CastAide.AsDecimal(dr["deposit"]) * dcActualRate;
                dcTurnover += CastAide.AsDecimal(dr["Turnover"]) * dcActualRate;
                dcBonus += CastAide.AsDecimal(dr["Bonus"]) * dcActualRate;
                dcMemWinloss += CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate;
                dcAffWinloss += CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate;
                dcHouseWinloss += CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate;
                dcAffNetPos += CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate;
                dcHouseNetWinloss += CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate;

                int intCurrMonth = CastAide.AsDateTime(dr["Winlostdate"]).Month;
                if (intMonth != intCurrMonth)
                {
                    //Assign Monthly row
                    if(intMonth != 0)
                        GenerateMonthlyRow(strTotal, hsTotal);
                    intMonth = intCurrMonth;
                    hsTotal = new Hashtable();
                }
                if (hsTotal.ContainsKey(dr["Currency"]))
                {
                    Hashtable hsCurrency = (Hashtable)hsTotal[dr["Currency"]];
                    hsCurrency["Deposit"] = CastAide.AsDecimal(hsCurrency["Deposit"]) + (CastAide.AsDecimal(dr["Deposit"]) * dcActualRate);
                    hsCurrency["Turnover"] = CastAide.AsDecimal(hsCurrency["Turnover"]) + (CastAide.AsDecimal(dr["Turnover"]) * dcActualRate);
                    hsCurrency["Bonus"] = CastAide.AsDecimal(hsCurrency["Bonus"]) + (CastAide.AsDecimal(dr["Bonus"]) * dcActualRate);
                    hsCurrency["MemberWinLost"] = CastAide.AsDecimal(hsCurrency["MemberWinLost"]) + (CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate);
                    hsCurrency["AffiliateWinLost-val"] = CastAide.AsDecimal(hsCurrency["AffiliateWinLost"]) + (CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate);
                    hsCurrency["HouseWinLost"] = CastAide.AsDecimal(hsCurrency["HouseWinLost"]) + (CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate);
                    hsCurrency["AffiliateNetWinLost-val"] = CastAide.AsDecimal(hsCurrency["AffiliateNetWinLost"]) + (CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate);
                    hsCurrency["HouseNetWinLost"] = CastAide.AsDecimal(hsCurrency["HouseNetWinLost"]) + (CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate);

                    hsTotal[dr["Currency"]] = hsCurrency;
                }
                else
                {
                    Hashtable hsCurrency = new Hashtable();
                    hsCurrency.Add("Deposit", CastAide.AsDecimal(dr["Deposit"]) * dcActualRate);
                    hsCurrency.Add("Turnover", CastAide.AsDecimal(dr["Turnover"]) * dcActualRate);
                    hsCurrency.Add("Bonus", CastAide.AsDecimal(dr["Bonus"]) * dcActualRate);
                    hsCurrency.Add("MemberWinLost", CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate);
                    hsCurrency.Add("AffiliateWinLost", CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate);
                    hsCurrency.Add("HouseWinLost", CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate);
                    hsCurrency.Add("AffiliateNetWinLost", CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate);
                    hsCurrency.Add("HouseNetWinLost", CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate);
                    hsCurrency["Currency"] = dr["Currency"];
                    //hsCurrency.Add("Rate", CastAide.AsDecimal(dr["actualrate"]));
                    hsTotal.Add(dr["Currency"], hsCurrency);
                }

                if (string.IsNullOrEmpty(strNewRow))//New row but is not first row of date
                    strRow.Append("<tr class=\"nowrap\" bgcolor=\"" + AdminCommon.RowBgOdd + "\" onmouseout=\"bgColor='" + AdminCommon.RowBgOdd + "'\" onmouseover=\"bgColor='" + AdminCommon.RowBgOver + "'\">" + strTmp + "</tr>");
                intCount++;
                if (numRow == dtb.Rows.Count)
                {
                    GenerateSubTotal(strRow, strSubTpl, intCount, ref dcDeposit, ref dcTurnover, ref dcBonus, ref dcMemWinloss, ref dcAffWinloss, ref dcHouseWinloss, ref dcAffNetPos, ref dcHouseNetWinloss);//Sub total
                    GenerateMonthlyRow(strTotal, hsTotal);//Monthly for last row
                }
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "content", AdminCommon.NoInfo(19));
            TemplateHandler.ReplaceBlock(ref strTpl, "subtotal", string.Empty);
            TemplateHandler.ReplaceBlock(ref strTpl, "total", string.Empty);
        }
    }

    void GenerateMonthlyRow(string strTotal, Hashtable hsTotal)
    {
        string strMonthly = strTotal;
        string strMonthlyTotal = TemplateHandler.ReplaceBlock(ref strMonthly, "monthly-total", string.Empty);
        TemplateHandler.Assign(ref strMonthly, "monthly-val", Fanex.Affiliate.Resources.Translator.Get("TotalMonthly"));
        TemplateHandler.Assign(ref strMonthly, "numrow", hsTotal.Keys.Count.ToString());
        decimal dcDeposit = 0, dcTurnoverTotal = 0, dcBonusTotal = 0, dcMemTotal = 0, dcAffTotal = 0, dcHouseTotal = 0, dcAffNetTotal = 0, dcHouseNetTotal = 0;
        
        ArrayList arrItem = new ArrayList();
        foreach (DictionaryEntry de in hsTotal)
        {
            arrItem.Add(de.Key);
        }
        arrItem.Sort();
        
        for (int i = 0; i < arrItem.Count; i++)
        {
            string strTmpMonthly = strMonthly;
            if (i != 0)
                TemplateHandler.RemoveBlock(ref strTmpMonthly, "monthly");
            //string strKey = hsTotal.;
            Hashtable hsCurrency = (Hashtable)hsTotal[arrItem[i]];
            //decimal dcRate = CastAide.AsDecimal(hsCurrency["Rate"]);
            TemplateHandler.Assign(ref strTmpMonthly, "currency-val", hsCurrency["Currency"].ToString());
            //TemplateHandler.Assign(ref strTmpMonthly, "rate-val", NumberFormat.DecFormat(hsCurrency["Rate"], 5));

            dcDeposit += CastAide.AsDecimal(hsCurrency["Deposit"]);
            dcTurnoverTotal += CastAide.AsDecimal(hsCurrency["Turnover"]);
            dcBonusTotal += CastAide.AsDecimal(hsCurrency["Bonus"]);
            dcMemTotal += CastAide.AsDecimal(hsCurrency["MemberWinLost"]);
            dcAffTotal += CastAide.AsDecimal(hsCurrency["AffiliateWinLost"]);
            dcHouseTotal += CastAide.AsDecimal(hsCurrency["HouseWinLost"]);
            dcAffNetTotal += CastAide.AsDecimal(hsCurrency["AffiliateNetWinLost"]);
            dcHouseNetTotal += CastAide.AsDecimal(hsCurrency["HouseNetWinLost"]);

            TemplateHandler.Assign(ref strTmpMonthly, "deposit-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["Deposit"])));
            TemplateHandler.Assign(ref strTmpMonthly, "turnover-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["Turnover"])));
            TemplateHandler.Assign(ref strTmpMonthly, "bonus-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["Bonus"])));
            TemplateHandler.Assign(ref strTmpMonthly, "memwinloss-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["MemberWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "affwinloss-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["AffiliateWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "housewinloss-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["HouseWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "AffNetWinLoss-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["AffiliateNetWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "housenetwinloss-val", CurrencyUtils.Format(CastAide.AsDecimal(hsCurrency["HouseNetWinLost"])));
            strListRow.Append(strTmpMonthly);
        }
        TemplateHandler.Assign(ref strMonthlyTotal, "total", Fanex.Affiliate.Resources.Translator.Get("Total"));
        TemplateHandler.Assign(ref strMonthlyTotal, "deposit-total", CurrencyUtils.Format(dcDeposit));
        TemplateHandler.Assign(ref strMonthlyTotal, "turnover-total", CurrencyUtils.Format(dcTurnoverTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "bonus-total", CurrencyUtils.Format(dcBonusTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "memwinloss-total", CurrencyUtils.Format(dcMemTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "affwinloss-total", CurrencyUtils.Format(dcAffTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "housewinloss-total", CurrencyUtils.Format(dcHouseTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "AffNetWinLoss-total", CurrencyUtils.Format(dcAffNetTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "housenetwinloss-total", CurrencyUtils.Format(dcHouseNetTotal));
        strListRow.Append(strMonthlyTotal);
    }

    void GenerateSubTotal(StringBuilder strRow, string strSubTpl, int intCount, ref decimal dcDeposit, ref decimal dcTurnover, ref decimal dcBonus, ref decimal dcMemWinloss, ref decimal dcAffWinloss, ref decimal dcHouseWinloss, ref decimal dcAffNetPos, ref decimal dcHouseNetWinloss)
    {        
        string strOldRow = strRow.ToString();
        TemplateHandler.Assign(ref strOldRow, "numrow", intCount.ToString());
        strListRow.Append(strOldRow);
        string strSubTmp = strSubTpl;
        TemplateHandler.Assign(ref strSubTmp, "deposit-val", CurrencyUtils.Format(dcDeposit,_systemCurrency,null));
        TemplateHandler.Assign(ref strSubTmp, "turnover-val", CurrencyUtils.Format(dcTurnover));
        TemplateHandler.Assign(ref strSubTmp, "bonus-val", CurrencyUtils.Format(dcBonus));
        TemplateHandler.Assign(ref strSubTmp, "memwinloss-val", CurrencyUtils.Format(dcMemWinloss));
        TemplateHandler.Assign(ref strSubTmp, "affwinloss-val", CurrencyUtils.Format(dcAffWinloss));
        TemplateHandler.Assign(ref strSubTmp, "housewinloss-val", CurrencyUtils.Format(dcHouseWinloss));
        TemplateHandler.Assign(ref strSubTmp, "AffNetWinLoss-val", CurrencyUtils.Format(dcAffNetPos));
        TemplateHandler.Assign(ref strSubTmp, "housenetwinloss-val", CurrencyUtils.Format(dcHouseNetWinloss));
        strListRow.Append(strSubTmp);
        dcTurnover = 0;
        dcBonus = 0;
        dcMemWinloss = 0;
        dcAffWinloss = 0;
        dcHouseWinloss = 0;
        dcAffNetPos = 0;
        dcHouseNetWinloss = 0;
        dcDeposit = 0;
    }
}

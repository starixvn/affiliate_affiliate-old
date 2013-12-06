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
using Fanex.Affiliate.Resources;

public partial class Report_Commission : BasePageTpl
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/Commission.htm");
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        AppCommon.AssignToAdminTemplate(ref strTpl, "Affiliate", "month", "year", "search", "Earning","afftype");
        //TemplateHandler.Assign(ref strTpl, "commission", Fanex.Affiliate.Resources.Translator.Get("Commission);
        


        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("LimitDate") + AppSettings.SystemStartDate.ToString("d-MMM-yy"));

        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "currencys", Fanex.Affiliate.Resources.Translator.Get("Currency"));

    }
    private DateTime GetLastDayOfMonth(DateTime dtDate)
    {

        // set return value to the last day of the month

        // for any date passed in to the method



        // create a datetime variable set to the passed in date

        DateTime dtTo = dtDate;



        // overshoot the date by a month

        dtTo = dtTo.AddMonths(1);



        // remove all of the days in the next month

        // to get bumped down to the last day of the

        // previous month

        dtTo = dtTo.AddDays(-(dtTo.Day));



        // return the last day of the month

        return dtTo;

    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "AdminCommon.GetAffUsername(1166364)"

            string strUsername = CastAide.AsString(Request["affname"]).Trim();
            string gmt = String.Empty;
            var defaulDate = ServerDateTime.GetCurrentDBTime(out gmt);

            int month = CastAide.AsInt32(Request["slmonth"], defaulDate.Month);
            int year = CastAide.AsInt32(Request["slyear"], defaulDate.Year);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 50);
            int intPage = CastAide.AsInt32(Request["page"], 1);
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

            TemplateHandler.Assign(ref strTpl, "slmonth", AppCommon.GenMonthDropDown("slmonth", month, "r"));
            TemplateHandler.Assign(ref strTpl, "slyear", AppCommon.GenYearDropDown("slyear", year, "r"));

            TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
            TemplateHandler.Assign(ref strTpl, "month-val", month);
            TemplateHandler.Assign(ref strTpl, "year-val", year);
            if (String.IsNullOrEmpty(strUsername))
                TemplateHandler.Assign(ref strTpl, "affname", "[" + Fanex.Affiliate.Resources.Translator.Get("All") + "]");
            else
                TemplateHandler.Assign(ref strTpl, "affname", strUsername);
            TemplateHandler.Assign(ref strTpl, "all-val", "[" + Fanex.Affiliate.Resources.Translator.Get("All") + "]");
            //Begin list Language
            var htLang = AppSettings.BaseLanguages; // AdminCommon.GetListSupportedLang();
            TemplateHandler.Assign(ref strTpl, "listlang",
                HtmlHelper.DropDownList(htLang, "slPreferLang", strPreferLang, Admin.Language));
            //AdminCommon.GenDropdownList((IList<string>)htLang["key"], (IList<string>)htLang["value"], "slPreferLang", false, "Select Language", ""));
            //End list Language
        //Begin list currency
            var lsCurrency = AppSettings.AffCurrencies;// AdminCommon.GetListSupportedCurrency();
        TemplateHandler.Assign(ref strTpl, "lscurrency",
            HtmlHelper.DropDownList(lsCurrency, "slCurrency", currency, Admin.Currency));
        //Common.GenDropdownList(lsCurrency, lsCurrency, "slCurrency", false, "Select Currency", currency));
        //End list currency
        DbResult rs = Affiliate.SelAffiliateCommissionReport(strUsername, strPreferLang,afftype, month, year,currency, intPage, intPageSize);

  DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];
        int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
        int nummem = CastAide.AsInt32(rs.Outputs["nrows"]);
        DateTime LastDay = GetLastDayOfMonth(new DateTime(year, month, 1));
        if (dtb.Rows.Count > 0)
        {
            int intCount = (intPage - 1) * intPageSize;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            StringBuilder strListRow = new StringBuilder();
            //Hashtable hsTotal = new Hashtable();


            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;
                intCount++;

                if (CastAide.AsBoolean(dr["isclosed"]))
                    TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgClosed);   
                else
                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "monthdate_val", month.ToString() + "/" + LastDay.Day + "/" + year.ToString());
                    
                var affcurrency = dr["Currency"].ToString();
                TemplateHandler.Assign(ref strTmp, "num-val", intCount);
                TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "afftype-val", dr["afftypename"]);
                TemplateHandler.Assign(ref strTmp, "currency-val",affcurrency );
                TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                //TemplateHandler.Assign(ref strTmp, "commission-val", DNAide.Core.DataFormat.FormatNumber2(dr["commission"]));
                TemplateHandler.Assign(ref strTmp, "affcomm-val", CurrencyUtils.Format(dr["affComm"],affcurrency,null));
                    

                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(4));
        }
        //Assign Paging template
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", 550, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", 550, false, false));
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 4, nummem, SubMenu.ReportText[3], "i_report.png"));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 4, 0, SubMenu.ReportText[3], "i_report.png"));
        }
    }
}

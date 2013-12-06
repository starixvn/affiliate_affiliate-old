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

public partial class Report_AffiliateQMReport : BasePageTpl
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {        
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateQMReport.htm");
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);

        AppCommon.AssignToAdminTemplate(ref strTpl, "Affiliate", "Month", "Year",
            "search", "QualifiedMembers", "Product", "total", "afftype", "Submit");
        //TemplateHandler.Assign(ref strTpl, "horseracing", Fanex.Affiliate.Resources.Translator.Get("HorseRacing);

        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("LimitDate") + AppSettings.SystemStartDate.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);

    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "AdminCommon.GetAffUsername(1166364)"
        int width = 350;
        TemplateHandler.Assign(ref strTpl, "width", width);

        string strUsername = CastAide.AsString(Request["affname"],"").Trim();
        string gmt = String.Empty;
        var defaulDate = ServerDateTime.GetCurrentDBTime(out gmt);        

        int month = CastAide.AsInt32(Request["slmonth"], defaulDate.Month);
        int year = CastAide.AsInt32(Request["slyear"], defaulDate.Year);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 50);
        int intPage = CastAide.AsInt32(Request["page"], 1);
        string strPreferLang = Request["slPreferLang"];
        int currency =CastAide.AsInt32(Request["slCurrency"], 0);
        int productid = CastAide.AsInt32(Request["slProduct"], 0);

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

        TemplateHandler.Assign(ref strTpl, "slProducts", AdminCommon.GenListProduct(productid,false,Admin.Product));

        DbResult rs = Affiliate.SelAffiliateQMReport(productid, strUsername, afftype, strPreferLang, month, year, currency, intPage, intPageSize);

        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
        int nummem = CastAide.AsInt32(rs.Outputs["nrows"]);
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

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                TemplateHandler.Assign(ref strTmp, "num-val", intCount);

                TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                TemplateHandler.Assign(ref strTmp, "afftype-val", dr["afftypename"]);
                TemplateHandler.Assign(ref strTmp, "product-val", dr["product"]);
                TemplateHandler.Assign(ref strTmp, "qualifiedmember-val", dr["qualifiedmember"]);
                TemplateHandler.Assign(ref strTmp, "productid", productid);
          
                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(12));
        }
        //Assign Paging template
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", width, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", width, false, false));
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, nummem, SubMenu.ReportText[3], "i_report.png"));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, 0, SubMenu.ReportText[3], "i_report.png"));
        }
    }
}

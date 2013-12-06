using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;

public partial class Report_AffiliateQMReportDetail : BasePageTpl
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateQMReportDetail.htm");
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        AppCommon.AssignToTemplate(ref strTpl, "Product","PlayerUserName","turnover","IsQualifiedMember");


        TemplateHandler.Assign(ref strTpl, "ibcusername", Fanex.Affiliate.Resources.Translator.Get("IBCUsername"));
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "affiliate", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("LimitDate") + AppSettings.SystemStartDate.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "spturnover", Fanex.Affiliate.Resources.Translator.Get("SPTurnover"));
        
        TemplateHandler.Assign(ref strTpl, "ishrqualified", Fanex.Affiliate.Resources.Translator.Get("IsHrTurnoverQualified"));
        TemplateHandler.Assign(ref strTpl, "qmreportdetailof", Fanex.Affiliate.Resources.Translator.Get("QMReportDetailOf"));
        TemplateHandler.Assign(ref strTpl, "in", Fanex.Affiliate.Resources.Translator.Get("In")); 

    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "AdminCommon.GetAffUsername(1166364)"
        int width = 400;
        int affid = CastAide.AsInt32(Request["affid"]);
        string affname = CastAide.AsString(Request["affname"]);
        int month = CastAide.AsInt32(Request["month"], DateTime.Now.Month);
        int year = CastAide.AsInt32(Request["year"], DateTime.Now.Year);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 50);
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int productid= CastAide.AsInt32(Request["productid"], 1);

        TemplateHandler.Assign(ref strTpl, "width", width);
        TemplateHandler.Assign(ref strTpl, "year-val", year);
        TemplateHandler.Assign(ref strTpl, "month-val", month);
        TemplateHandler.Assign(ref strTpl, "affid-val", affid);
        TemplateHandler.Assign(ref strTpl, "affname-val", affname);
        TemplateHandler.Assign(ref strTpl, "productid-val", productid);

        DbResult rs = Affiliate.SelAffiliateQMReportDetail(productid,affid, month, year, intPage, intPageSize);

        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
        int nummem = CastAide.AsInt32(rs.Outputs["nrows"]);
        string currencycode = rs.Outputs["affcurrency"].ToString();
        CurrencyUtils.SetDefaultCurrency(currencycode);
        if (dtb.Rows.Count > 0)
        {
            int intCount = (intPage - 1)*intPageSize;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            StringBuilder strListRow = new StringBuilder();
            //Hashtable hsTotal = new Hashtable();


            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;
                intCount++;

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                TemplateHandler.Assign(ref strTmp, "num-val", intCount);
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "product-val", dr["product"]);
                ////NEW//TemplateHandler.Assign(ref strTmp, "spturnover-val", CurrencyUtils.Format(dr["spturnover"], strPrefix));
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["turnover"]));
                TemplateHandler.Assign(ref strTmp, "isqualifiedmember-val", (CastAide.AsInt32(dr["isqualifiedmember"]) == 0 ? "" : "checked='true'"));                

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
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, nummem, SubMenu.ReportText[3], "i_report.png"));
        }
    }
}

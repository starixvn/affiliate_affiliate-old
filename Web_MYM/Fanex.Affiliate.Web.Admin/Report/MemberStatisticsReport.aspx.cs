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

public partial class Report_MemberStatisticsReport : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/MemberStatisticsReport.htm");

        AssignHeader();
        GenerateHTML();
        

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "datelimit", Fanex.Affiliate.Resources.Translator.Get("LimitDate") + AppSettings.SystemStartDate.ToString("d-MMM-yy"));

        AppCommon.AssignToAdminTemplate(ref strTpl,
        "Affiliate", "From", "To", "search","memberstatistics","affiliate","All","active","dormant",
        "deposit","nondeposit","firstdeposit","nondeposit","totalregister","total","afftype","GetAll");
        
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "AdminCommon.GetAffUsername(1166364)"
        int width = 700;
        TemplateHandler.Assign(ref strTpl, "width", width);
           

        string strUsername = CastAide.AsString(Request["affname"]).Trim();
        string strPreferLang = Request["slPreferLang"];
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int currency =CastAide.AsInt32(Request["slCurrency"],0);

        int afftype = CastAide.AsInt32(Request["afftypelist"], -1);

        var hideAfftype = AppSettings.BaseAffTypes.Count < 2;

        if (hideAfftype)
        {
            TemplateHandler.RemoveBlock(ref strTpl, "afftype");
        }else
        TemplateHandler.Assign(ref strTpl, "afftypelist",
            HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypelist", afftype, hideAfftype ? null : "Select Affiliate Type", null, hideAfftype));  


        TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "affname", strUsername);
        

        //Begin list Language
        var htLang = AppSettings.BaseLanguages;// AdminCommon.GetListSupportedLang();
        TemplateHandler.Assign(ref strTpl, "listlang",
            HtmlHelper.DropDownList(htLang, "slPreferLang",strPreferLang, Admin.Language));
        //Common.GenDropdownList((IList<string>)htLang["key"], (IList<string>)htLang["value"], "slPreferLang", false, "Select Language", ""));
        //End list Language

            //Begin list currency
            var lsCurrency = AppSettings.AffCurrencies; // AdminCommon.GetListSupportedCurrency();
            TemplateHandler.Assign(ref strTpl, "lscurrency",
                HtmlHelper.DropDownList(lsCurrency, "slCurrency", currency, Admin.Currency));
           // TemplateHandler.Assign(ref strTpl, "lscurrency",
            // Common.GenDropdownList(lsCurrency, lsCurrency, "slCurrency", false, "Select Currency", currency));
            //End list currency

        DbResult rs = Affiliate.SelAffiliateStatistics(strUsername, strPreferLang, dtmFDate, dtmTDate, isGetAll, afftype,currency,intPage, intPageSize);

        DataTable dtb = new DataTable();
        if(rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
        int nummem = CastAide.AsInt32(rs.Outputs["nrows"]);
            
        if (dtb.Rows.Count > 0)
        {
            int intCount = (intPage - 1) * intPageSize + 1;
           // int intActive = 0, intTotal = 0, intTotalRegister = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;


            StringBuilder strListRow = new StringBuilder();
            //Hashtable hsTotal = new Hashtable();


            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                //int active = CastAide.AsInt32(dr["active"]);
                //int total = CastAide.AsInt32(dr["total"]);

                //intActive += active;
                //intTotal += total;

                TemplateHandler.Assign(ref strTmp, "no", intCount);
                intCount++;

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "afftype-val", dr["afftypename"]);
                //TemplateHandler.Assign(ref strTmp, "activeall-val", dr["activeall"]);
                //TemplateHandler.Assign(ref strTmp, "fdmall-val", dr["fdmall"]);
                TemplateHandler.Assign(ref strTmp, "active-val", dr["active"]);
                TemplateHandler.Assign(ref strTmp, "dormant-val", dr["dormant"]);
                TemplateHandler.Assign(ref strTmp, "total-val", dr["totalfdm"]);
                //TemplateHandler.Assign(ref strTmp, "nondeposit-val", dr["nondeposit"]);
                TemplateHandler.Assign(ref strTmp, "totalregister-val", dr["TotalRegistered"]);
                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(9));
        }
        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 2, nummem, SubMenu.ReportText[2], "i_report.png"));
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", width, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", width, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
    }
}

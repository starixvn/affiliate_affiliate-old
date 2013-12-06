using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Helper;

public partial class UserInfo_MediaStatistics_LandingPage : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_LandingPage.htm");

        AppCommon.AssignToAdminTemplate(ref strTpl, "Referrers", "LandingPages", "From", "To",
            "SiteURLs", "TotalClicks", "UniqueClicks", "Signups", "FDM", "SiteUrl", "TotalClicks",
            "UniqueClicks", "SignUp", "Submit","Page","Language");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
    }

    void GenerateHTML()
    {
        #region Generate select list
        string productCode = CastAide.AsString(Request["productCode"]);
        string languageCode = CastAide.AsString(Request["languageCode"]);

        //TemplateHandler.Assign(ref strTpl, "slPage", Common.GenerateSelectObj("productCode", arrValue, arrTarget, productCode));
        TemplateHandler.Assign(ref strTpl, "slPage", AppCommon.DropdownCustomerPage("productCode", productCode, true));

        //DataView dv = Fanex.Affiliate.DataObjects.DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE).ResultSet.Tables[0].DefaultView;
        //TemplateHandler.Assign(ref strTpl, "slLanguage",
        //    AdminCommon.GetDataOptionBanner(dv, "languageCode", "Language", languageCode, true, true));
        TemplateHandler.Assign(ref strTpl, "slLanguage",
            HtmlHelper.DropDownList(AppSettings.BaseLanguages,"languageCode",languageCode,true,null,false,
            "style='margin-right:20px;' "));
        #endregion

        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddMonths(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);

        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

        DbResult rs = Fanex.Affiliate.DataObjects.TrackingSystem.SelClickStatisticsByPage(AdminSession.AffId, dtmFDate, dtmTDate, productCode, languageCode);

        //DbResult rs = null;

        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];


        if (dtb.Rows.Count > 0)
        {

            int intCount = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;

            StringBuilder strListRow = new StringBuilder();
            int totalSignups = 0, totalDeposits = 0;
            //Hashtable hsTotal = new Hashtable();
            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                intCount++;
                totalSignups += CastAide.AsInt32(dr["Signups"]);
                totalDeposits += CastAide.AsInt32(dr["FDM"]);

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AppCommon.RowBgEven : AppCommon.RowBgOdd);

                TemplateHandler.Assign(ref strTmp, "Page-val", dr["sportcode"]);
                TemplateHandler.Assign(ref strTmp, "LangCode-val", dr["languagecode"]);
                TemplateHandler.Assign(ref strTmp, "Language-val", GetLanguageOriginal(dr["languagecode"].ToString()));
                TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());

                strListRow.Append(strTmp);
            }
            TemplateHandler.Assign(ref strRowTpl, "color", (intCount % 2 == 0) ? AppCommon.RowBgOdd : AppCommon.RowBgEven);
            //Assign total signups for Old URL
            //strListRow.Append(TrackingSystem.GenRowOldURL(strRowTpl, totalSignups, totalDeposits, dtmFDate, dtmTDate));

            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());

        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(6));
        }
        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 5, 0, SubMenu.AffApprovedText[5], "i_report.png"));
    }

    string GetLanguageOriginal(string strLangCode)
    {
        switch (strLangCode.ToLower())
        {
            case "en-us": return "English";
            case "eneuro": return "English_Euro";
            case "zh-tw": return "Chinese Traditional";
            case "zh-cn": return "Chinese Simplified";
            case "th-th": return "Thai";
            case "es-es": return "Spanish";
            case "vi-vn": return "Tiếng Việt";
            case "id-id": return "Indonesian";
            default:
                return "";
        }
    }
}

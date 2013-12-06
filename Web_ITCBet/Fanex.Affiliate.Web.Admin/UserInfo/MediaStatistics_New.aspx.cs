using System.Xml.Linq;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using System.Text;
using System;
using System.Data;
using System.Linq;
using Fanex.Affiliate.Core;
using Fanex.Core.Utility;
using Fanex.Affiliate.Core.Admin;

public partial class Report_MediaStatistics_New : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_New.htm");

        AssignHeader();
        GenerateHTML();

        AppCommon.AssignToAdminTemplate(ref strTpl, "Referrers", "LandingPages","From","To",
            "SiteURLs","TotalClicks","UniqueClicks","Signups","FDM","SiteUrl","TotalClicks",
            "UniqueClicks","SignUp","Search");
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "over", AdminCommon.RowBgOver);
    }

    void GenerateHTML()
    {
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddMonths(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);

        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

        DbResult rs = Fanex.Affiliate.DataObjects.TrackingSystem.SelClickStatisticsBySite(AdminSession.AffId, dtmFDate, dtmTDate);
        // DbResult rs = null;
        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];


        if (dtb.Rows.Count > 0)
        {

            int intCount = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;

            StringBuilder strListRow = new StringBuilder();
            //Hashtable hsTotal = new Hashtable();
            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                intCount++;

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                TemplateHandler.Assign(ref strTmp, "Site-val", dr["DomainReferrer"]);
                TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());

                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());


        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(5));
        }
        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 5, 0, SubMenu.AffApprovedText[5], "i_report.png"));            
    }
}
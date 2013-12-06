
namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using System;
    /// <summary>
    /// Generate content for Tracking System
    /// Written by : Tong Hoang Dung on 19/03/2009
    /// </summary>
    public class TrackingSystem
    {
        public static string GetStatisticsAllMonthsContentList(int affiliateID,string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            DbResult result = DataObjects.TrackingSystem.SelClickStatisticsAllMonths(affiliateID,trackingcode);
            int cols = 4;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataTable dtbResult in result.ResultSet.Tables)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string strTmp = strContent;
                        TemplateHandler.Assign(ref strTmp, "No-val", ++intNo);
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Month-val", Convert.ToDateTime(dr["date"]).ToString("MMMM-yyyy"));
                        TemplateHandler.Assign(ref strTmp, "MonthFull-val", dr["date"].ToString());
                        TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                        strBuild.Append(strTmp);

                    }
                }
                if (intNo > 0)
                    return strBuild.ToString();
                return AdminCommon.NoInfo(cols);
            }
            return AdminCommon.NoInfo(cols);

        }

        public static string GetStatisticsMonthDetailContent(int affiliateID,int month, int year, string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetailContent");
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            DbResult result = DataObjects.TrackingSystem.SelClickStatisticsDetail(affiliateID,month, year, trackingcode);
            int cols = 4;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataTable dtbResult in result.ResultSet.Tables)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string strTmp = strContent;
                        TemplateHandler.Assign(ref strTmp, "No-val", ++intNo);
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Date-val", Convert.ToDateTime(dr["date"]).ToString("MM-dd-yyyy"));
                        TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                        strBuild.Append(strTmp);

                    }
                }
                if (intNo > 0)
                    return strBuild.ToString();
                return AdminCommon.NoInfo(cols);
            }
            return AdminCommon.NoInfo(cols);

        }


        public static string GetTrackingCodeList(int affiliateID, bool isGenAll)
        {
            DbResult trackingCodeList = DataObjects.TrackingSystem.TrackingCodeSel(affiliateID);
            StringBuilder result = new StringBuilder();
            string opt = TemplateHandler.LoadTemplate("Option.htm");
            TemplateHandler.Assign(ref opt, "id", "slTrackingCode");
            string temp = TemplateHandler.CopyBlock(opt, "optionList");
            string tempopt;
            string selected = "selected";
            string noneValue = string.Empty;
            if (isGenAll)
            {
                tempopt = temp;
                TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
                TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Admin.All + "]");
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
                result.Append(tempopt);
                selected = string.Empty;
                noneValue = "[None]";
            }
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", noneValue);
            TemplateHandler.Assign(ref tempopt, "Optname", "[None]");
            TemplateHandler.Assign(ref tempopt, "selected", selected);
            result.Append(tempopt);
            if (trackingCodeList.ResultSet.Tables.Count > 0)
            {
                DataTable dt = trackingCodeList.ResultSet.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    tempopt = temp;
                    if (dr["trackingcode"].ToString() != "[None]")
                    {
                        TemplateHandler.Assign(ref tempopt, "Optvalue", dr["trackingcode"].ToString());
                        TemplateHandler.Assign(ref tempopt, "Optname", dr["trackingcode"].ToString());
                        TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
                        result.Append(tempopt);
                    }
                }
            }

            TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
            return opt;

        }
        public static string GetStatisticsDetail_New(DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageNum)
        {
            DbResult result = DataObjects.TrackingSystem.SelStatisticsDetail_New(AdminSession.AffId, fDate, tDate, domain, sportCode, languageCode, pageSize, pageNum);
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_New.htm");
            string strContentTable = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetail");

            AppCommon.AssignToAdminTemplate(ref strContentTable,
                "SiteUrl", "TotalClicks", "UniqueClicks", "SignUp", "FDM");

            int totalPage = CastAide.AsInt32(result.Outputs["totalpages"]);
            TemplateHandler.Assign(ref strContentTable, "paging-top", AppCommon.GenPaging(totalPage, pageNum, pageSize, 0, "aPageTop", "txtPageTop", 600, false, true));
            TemplateHandler.Assign(ref strContentTable, "paging-bottom", AppCommon.GenPaging(totalPage, pageNum, pageSize, 0, "aPageBottom", "txtPageBottom", 600, false, false));
            TemplateHandler.Assign(ref strContentTable, "over", AppCommon.RowBgOver);
            TemplateHandler.Assign(ref strContentTable, "page", pageNum);
            //TemplateHandler.Assign(ref strContentTable, "Page-text", Fanex.Affiliate.Resources.Admin.Page);
            strContentTable = strContentTable.Replace("LoadData", "LoadDetail");
            strContentTable = strContentTable.Replace("ChangePage", "submitPage");

            string strContent = TemplateHandler.CopyBlock(strContentTable, "StatisticsDetailContent");
            AppCommon.AssignToAdminTemplate(ref strContent,
                "SiteUrl", "TotalClicks", "UniqueClicks", "SignUp", "FDM");
            int cols = 5;
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            if (result.ResultSet.Tables.Count > 0)
            {
                DataTable dtbResult = result.ResultSet.Tables[0];
                foreach (DataRow dr in dtbResult.Rows)
                {
                    intNo++;
                    string strTmp = strContent;
                    var shortUrl = CastAide.AsString(dr["UrlReferrer"],"");
                    if (shortUrl.Length > 40)
                        shortUrl = shortUrl.Substring(0, 40) + "...";

                    TemplateHandler.Assign(ref strTmp, "FullSiteUrl-val", dr["UrlReferrer"]);
                    TemplateHandler.Assign(ref strTmp, "Site-val", shortUrl);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
                    TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? AppCommon.RowBgEven : AppCommon.RowBgOdd);


                    strBuild.Append(strTmp);

                }
            }
            if (intNo > 0)
            {
                TemplateHandler.ReplaceBlock(ref strContentTable, "StatisticsDetailContent", strBuild.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strContentTable, "StatisticsDetailContent", AppCommon.NoInfo(cols));
            }
            return strContentTable;
        }
    }

}

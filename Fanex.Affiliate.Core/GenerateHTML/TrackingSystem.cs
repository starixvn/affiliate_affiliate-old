using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Configuration;
using System.Web;
using Fanex.IndoPaul.DataObjects;
namespace Fanex.IndoPaul.Core.Affiliate.GenerateHTML
{
    /// <summary>
    /// Generate content for Tracking System
    /// Written by : Tong Hoang Dung on 19/03/2009
    /// </summary>
    public class TrackingSystem
    {
        public static string GetStatisticsAllMonthsContentList(string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics.htm");            
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");
            DbResult result = DataObjects.TrackingSystem.SelClickStatisticsAllMonths(trackingcode);                       
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
                        
                        TemplateHandler.Assign(ref strTmp, "Month-val", Common.ConvertDateTime(dr["date"], "-", false, false));
                        TemplateHandler.Assign(ref strTmp, "MonthFull-val", dr["date"].ToString());
                        if ((intNo % 2) == 0)
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                        }

                        strBuild.Append(strTmp);

                    }
                }
                if(intNo>0)
                    return strBuild.ToString();
                return Common.NoInfo(cols);
            }
            return Common.NoInfo(cols);

        }

        public static string GetStatisticsMonthDetailContent(int month,int year,string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetailContent");
            DbResult result = DataObjects.TrackingSystem.SelClickStatisticsDetail(month,year,trackingcode);
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
                        TemplateHandler.Assign(ref strTmp, "Date-val", Common.ConvertDateTime(dr["date"], "-", true, false));                        
                        if ((intNo % 2) == 0)
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                        }

                        strBuild.Append(strTmp);

                    }
                }
                if (intNo > 0)
                    return strBuild.ToString();
                return Common.NoInfo(cols);
            }
            return Common.NoInfo(cols);

        }

        
        public static string GetTrackingCodeList(bool isGenAll, string activedValue)
        {
            DbResult trackingCodeList = DataObjects.TrackingSystem.TrackingCodeSel();
            StringBuilder result = new StringBuilder();
            string opt = TemplateHandler.LoadTemplate("Option.htm");
            TemplateHandler.Assign(ref opt, "id", "slTrackingCode");
            string temp = TemplateHandler.CopyBlock(opt, "optionList");
            string tempopt;
            string selected="selected";
            string noneValue = string.Empty;
            if (isGenAll)
            {
                tempopt = temp;
                TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
                TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Lang.All + "]");
                if(string.IsNullOrEmpty(activedValue))
                    TemplateHandler.Assign(ref tempopt, "selected", "selected");
                result.Append(tempopt);
                selected=string.Empty;
                noneValue = "[None]";
            }
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", noneValue);
            TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Lang.none + "]");
            if (string.IsNullOrEmpty(activedValue) && !isGenAll)
                TemplateHandler.Assign(ref tempopt, "selected", "selected");            
            result.Append(tempopt);
            if (trackingCodeList.ResultSet.Tables.Count > 0)
            {
                DataTable dt = trackingCodeList.ResultSet.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    tempopt = temp;
                    if (dr["trackingcode"].ToString() != "[None]")
                    {
                        if (!string.IsNullOrEmpty(activedValue) && string.Compare(activedValue, dr["trackingcode"].ToString(), true) == 0)
                            TemplateHandler.Assign(ref tempopt, "selected", "selected"); 
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
    }

}

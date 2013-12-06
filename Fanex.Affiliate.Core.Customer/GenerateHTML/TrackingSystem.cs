namespace Fanex.Affiliate.Core.Customer.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using System;
    using System.Globalization;

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
                        
                        TemplateHandler.Assign(ref strTmp, "Month-val", AffCommon.ConvertDateTime(dr["date"], "-", false, false));
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
                return AffCommon.NoInfo(cols);
            }
            return AffCommon.NoInfo(cols);

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
                        TemplateHandler.Assign(ref strTmp, "Date-val", AffCommon.ConvertDateTime(dr["date"], "-", true, false));                        
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
                return AffCommon.NoInfo(cols);
            }
            return AffCommon.NoInfo(cols);

        }

        
        public static string GetTrackingCodeList(bool isGenAll, string activedValue)
        {
            DbResult trackingCodeList = DataObjects.TrackingSystem.TrackingCodeSel(AppSession.CustID);
            //DbResult trackingCodeList = DataObjects.TrackingSystem.TrackingCodeSel();
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
                TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Translator.Get("All") + "]");
                if(string.IsNullOrEmpty(activedValue))
                    TemplateHandler.Assign(ref tempopt, "selected", "selected");
                result.Append(tempopt);
                selected=string.Empty;
                noneValue = "[None]";
            }
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", noneValue);
            TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Translator.Get("none") + "]");
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

        public static string GetStatisticsBySite(object fromdate, object todate)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_New.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");

            TemplateHandler.Assign(ref strContent, "fdate", AffCommon.ConvertDateTime(fromdate, "/", true, false));
            TemplateHandler.Assign(ref strContent, "tdate", AffCommon.ConvertDateTime(todate, "/", true, false));

            int cols = 5;
            CultureInfo culture = new CultureInfo("en-US");
            var fdate = CastAide.AsDateTime(fromdate, culture);
            var tdate = CastAide.AsDateTime(todate, culture);
            if (fdate <= tdate)
            {
                DbResult result = DataObjects.TrackingSystem.SelClickStatisticsBySite(AppSession.CustID, fromdate, todate);


                if (result.ResultSet.Tables[0].Rows.Count > 0)
                {
                    int intNo = 0;
                    int totalSignups = 0, totalDeposits = 0;
                    StringBuilder strBuild = new StringBuilder();
                    foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
                    {
                        intNo++;
                        string strTmp = strContent;
                        totalSignups += CastAide.AsInt32(dr["Signups"]);
                        totalDeposits += CastAide.AsInt32(dr["FDM"]);
                        TemplateHandler.Assign(ref strTmp, "Site-val", dr["DomainReferrer"]);
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                        TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());

                        if (string.Compare(dr["DomainReferrer"].ToString(), "none", true) == 0)
                            TemplateHandler.ReplaceBlock(ref strTmp, "domain", dr["DomainReferrer"].ToString());

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
                    return strBuild.ToString();
                }
            }
            return AffCommon.NoInfo(cols);

        }

        public static string GetStatisticsDetail_New(DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageIndex)
        {
            DbResult result = DataObjects.TrackingSystem.SelStatisticsDetail_New(AppSession.CustID, fDate, tDate, domain, sportCode, languageCode, pageSize, pageIndex);
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_New.htm");
            string strContentTable = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetail");

            TemplateHandler.Assign(ref strContentTable, "{Site}", Resources.Translator.Get("SiteUrl"));
            TemplateHandler.Assign(ref strContentTable, "{TotalClick}", Resources.Translator.Get("TotalClick"));
            TemplateHandler.Assign(ref strContentTable, "{UniqueClick}", Resources.Translator.Get("UniqueClick"));
            TemplateHandler.Assign(ref strContentTable, "{Signups}", Resources.Translator.Get("SignUp"));
            TemplateHandler.Assign(ref strContentTable, "{FDM}", Resources.Translator.Get("FDM"));


            int totalPage = CastAide.AsInt32(result.Outputs["totalpages"]);
            TemplateHandler.Assign(ref strContentTable, "paging-top", AppCommon.GenPaging(totalPage, pageIndex, pageSize, 0, "aPageTop", "txtPageTop", 617, false, true));
            TemplateHandler.Assign(ref strContentTable, "paging-bottom", AppCommon.GenPaging(totalPage, pageIndex, pageSize, 0, "aPageBottom", "txtPageBottom", 617, false, false));
            TemplateHandler.Assign(ref strContentTable, "page", pageIndex);
            strContentTable = strContentTable.Replace("LoadData", "LoadDetail");
            strContentTable = strContentTable.Replace("ChangePage", "submitPage");


            string strContent = TemplateHandler.CopyBlock(strContentTable, "StatisticsDetailContent");
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
                    var url = CastAide.AsString(dr["UrlReferrer"], "");
                    if (url.Length > 40)
                        url = url.Substring(0, 40) + "...";
                    TemplateHandler.Assign(ref strTmp, "FullSiteUrl-val", dr["UrlReferrer"]);
                    TemplateHandler.Assign(ref strTmp, "Site-val", url);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
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
            {
                TemplateHandler.ReplaceBlock(ref strContentTable, "StatisticsDetailContent", strBuild.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strContentTable, "StatisticsDetailContent", AffCommon.NoInfo(cols));
            }
            return strContentTable;

        }

        public static string GetStatisticsByPage(object fromdate, object todate, string sportCode, string languageCode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_LandingPage.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");

            TemplateHandler.Assign(ref strContent, "fdate", AffCommon.ConvertDateTime(fromdate, "/", true, false));
            TemplateHandler.Assign(ref strContent, "tdate", AffCommon.ConvertDateTime(todate, "/", true, false));

            int cols = 5;
            CultureInfo culture = new CultureInfo("en-US");
            var fdate = CastAide.AsDateTime(fromdate, culture);
            var tdate = CastAide.AsDateTime(todate, culture);
            if (fdate <= tdate)
            {
                DbResult result = DataObjects.TrackingSystem.SelClickStatisticsByPage(AppSession.CustID, fromdate, todate, sportCode, languageCode);
                
                int totalSignups = 0, totalDeposits = 0;
                if (result.ResultSet.Tables[0].Rows.Count > 0)
                {
                    int intNo = 0;
                    StringBuilder strBuild = new StringBuilder();
                    foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
                    {
                        intNo++;
                        string strTmp = strContent;
                        totalSignups += CastAide.AsInt32(dr["Signups"]);
                        totalDeposits += CastAide.AsInt32(dr["FDM"]);

                        TemplateHandler.Assign(ref strTmp, "Page-val", dr["sportcode"]);
                        TemplateHandler.Assign(ref strTmp, "Language-val", AffCommon.GetLanguageOriginal(dr["languagecode"].ToString()));
                        TemplateHandler.Assign(ref strTmp, "langCode-val", dr["languagecode"]);
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                        TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
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
                    return strBuild.ToString();

                }
            }
            return AffCommon.NoInfo(cols);
        }
    }

}

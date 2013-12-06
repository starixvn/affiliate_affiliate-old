namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections;
    using DNAide.Data;    
    using System.Web;
    using System.Globalization;
    using DNAide.Core;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;

    public class TrackingSystem
    {
        public static bool InsertStatistic(int affId, string pageCode, string languageCode, string affDomain, HttpRequestBase request, string urlReferrer, string domainReferrer, out string redirectPage)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("clickid", 0);
            hsParam.Add("affdomain", affDomain);
            hsParam.Add("affiliateid", affId);
            hsParam.Add("pagecode", pageCode);
            hsParam.Add("languagecode", languageCode);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("browser", request.Browser.Browser);
            hsParam.Add("urlreferrer", urlReferrer);
            hsParam.Add("domainreferrer", domainReferrer);
            hsParam.Add("os", Utilities.GetOSName(request.UserAgent));
            hsParam.Add("device", Utilities.GetDeviceName(request.UserAgent));
            hsParam.Add("browserversion", request.Browser.Version);
            hsParam.Add("browserresolution", request.Browser.ScreenPixelsWidth.ToString() + "x" + request.Browser.ScreenPixelsHeight.ToString());
            hsParam.Add("browserlanguage", string.IsNullOrEmpty(request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]) ? string.Empty : request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]);
            hsParam.Add("useragent", request.UserAgent);
            hsParam.Add("country", Utilities.ResolveCountry() != null ? Utilities.ResolveCountry().EnglishName : System.Globalization.CultureInfo.CurrentCulture.ToString());
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            var data = DbHelper.Execute("Aff_AffiliateInsClickStatistics", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            redirectPage = data.Outputs["redirectpage"].ToString();
            return result == 0 ? true : false;
        }

        public static DbResult TrackingCodeSel()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid",HttpContext.Current.Session["CustID"]);
            return DbHelper.Execute("Aff_AffiliateSelTrackingCode", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelClickStatisticsAllMonths(string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid",HttpContext.Current.Session["CustID"]);
            hsParam.Add("trackingcode", trackingCode);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsAllMonths", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelClickStatisticsDetail(int month, int year, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid",HttpContext.Current.Session["CustID"]);
            if (!string.IsNullOrEmpty(trackingCode))
                hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetail", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        public static DbResult TrackingCodeSel(int affiliateID)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            return DbHelper.Execute("Aff_AffiliateSelTrackingCode", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelClickStatisticsAllMonths(int affiliateID, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("trackingcode", trackingCode);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsAllMonths", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelClickStatisticsDetail(int affiliateID, int month, int year, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            if (!string.IsNullOrEmpty(trackingCode))
                hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetail", hsParam, DBType.AFFILIATE);
        }
        //Statistics
        public static DbResult SelStatisticsAllForMonth(int affiliateID, DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("monthdate", dtmMonthDate);
            hsParam.Add("totaluniqueclicks", 0);
            hsParam.Add("totalclicks", 0);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetailAll", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        #region Statistics by Page
        public static DbResult SelClickStatisticsByPage(int affId, object fromDate, object toDate, string sportCode, string languageCode)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo culture = new CultureInfo("en-US");

            hsParam.Add("affid", affId);
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(toDate, culture));
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            return DbHelper.Execute("Aff_ClickStatisticsSelByPage", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelClickStatisticsDetailByPage(int affId, DateTime fDate, DateTime tDate, string sportCode, string languageCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affId);

            CultureInfo culture = new CultureInfo("en-US");
            hsParam.Add("fromdate", CastAide.AsDateTime(fDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(tDate, culture));
            hsParam.Add("sportcode", sportCode);
            hsParam.Add("languagecode", languageCode);
            return DbHelper.Execute("Aff_ClickStatisticsDetailByPage", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        #endregion

        public static DbResult SelClickStatisticsBySite(int affId, object fromDate, object toDate)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo culture = new CultureInfo("en-US");

            hsParam.Add("affid", affId);
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(toDate, culture));
            return DbHelper.Execute("Aff_ClickStatisticsSelBySite", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelStatisticsDetail_New(int affId, DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageIndex)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affId);

            CultureInfo culture = new CultureInfo("en-US");
            hsParam.Add("fromdate", CastAide.AsDateTime(fDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(tDate, culture));
            if (!string.IsNullOrEmpty(domain))
                hsParam.Add("domain", domain);
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            return DbHelper.Execute("Aff_ClickStatisticsDetailBySite", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
    }
 }


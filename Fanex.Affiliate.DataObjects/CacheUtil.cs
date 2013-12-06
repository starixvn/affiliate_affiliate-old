namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Web;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
    using DNAide.Data;
    using DNAide.Core;
    using System.Data;
    using System.Collections;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// Summary description for CacheUtil
    /// </summary>
    public class CacheUtil
    {
        private static string hotNewsKey = "hotNewsKey";
        private static string importantKeys = "importantKey";
        private static string bannerTypesKey = "bannerTypesKey";
        static DateTime LastRefreshHotNews;
        static DateTime LastRefreshBannerTypes;
        static int _TimeCache = 30;
        static int _TimeCacheBannerTypes = 120;

        //used for admin site
        private static string hsAffInfo = "hsAffInfo";
        //private static string dtbAffInfo = "dtbAffInfo";
        static DateTime LastRefresh;
        static int _AdminTimeCache = 120;

        static CacheUtil()
        {
            LastRefresh = DateTime.MinValue;
            LastRefreshHotNews = DateTime.MinValue;
            LastRefreshBannerTypes = DateTime.MinValue;
            InitCache();
        }

        private static void AddAffInfo()
        {
            DbResult result = DbHelper.Execute("Aff_AffiliateSelAffInfo", DBType.AFFILIATE);
            Hashtable hsResult = new Hashtable();
            foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
            {
                hsResult[dr["affid"].ToString()] = dr["username"];
            }

            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                CacheFactory.GetCacheManager().Add(hsAffInfo, hsResult, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_AdminTimeCache)));
                //CacheFactory.GetCacheManager().Add(dtbAffInfo, result.ResultSet.Tables[0], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            }
            else // no rows -- means there was system errors.Try to refresh after 2 seconds
            {
                CacheFactory.GetCacheManager().Add(hsAffInfo, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_AdminTimeCache)));
                //CacheFactory.GetCacheManager().Add(dtbAffInfo, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            }

        }
        public static Hashtable GetHSAffInfo()
        {
            lock (HttpContext.Current.Application)
            {
                if (LastRefresh.AddSeconds(_AdminTimeCache).CompareTo(DateTime.Now) <= 0)
                {
                    LastRefresh = DateTime.Now;
                    AddAffInfo();
                }
            }
            return (Hashtable)CacheFactory.GetCacheManager().GetData(hsAffInfo);

        }
       

       

        private static void AddHotNews()
        {
            DbResult result = HotNews.GetLatestHotNews();
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn item in result.ResultSet.Tables[0].Columns)
                {
                    if(item.ColumnName.Contains("-"))
                        CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + item.ColumnName, 
                            result.ResultSet.Tables[0].Rows[0][item], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
                }                               
                CacheFactory.GetCacheManager().Add(importantKeys, result.ResultSet.Tables[0].Rows[0]["important"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            }
            else // no rows -- means there isn't any hot news
            {
                if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Columns.Count > 0)
                {
                    foreach (DataColumn item in result.ResultSet.Tables[0].Columns)
                    {
                        if (item.ColumnName.Contains("-"))
                            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + item.ColumnName, string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
                    }
                }             
                CacheFactory.GetCacheManager().Add(importantKeys, false, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            }


        }
        private static void AddBannerTypes()
        {
            DbResult result = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE);
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                CacheFactory.GetCacheManager().Add(bannerTypesKey, result.ResultSet.Tables[0], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCacheBannerTypes)));
            }
            else // no rows -- means there was system errors.Try to refresh after 2 seconds
            {
                CacheFactory.GetCacheManager().Add(bannerTypesKey, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCacheBannerTypes)));
            }

        }
        public static string GetLatestHotNews(out bool isImportant, string lang)
        {
            lock (HttpContext.Current.Application)
            {
                if (LastRefreshHotNews.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
                {
                    LastRefreshHotNews = DateTime.Now;
                    AddHotNews();
                }
            }
            isImportant = Convert.ToBoolean(CacheFactory.GetCacheManager().GetData(importantKeys));
            if (string.IsNullOrEmpty(lang))
                lang = "id-ID";
            var item = CacheFactory.GetCacheManager().GetData(hotNewsKey + "_" + lang);
            return item == null ? string.Empty : item.ToString();
        }
        public static object GetBannerTypes()
        {
            lock (HttpContext.Current.Application)
            {
                object bt = CacheFactory.GetCacheManager().GetData(bannerTypesKey);
                if ((bt == null && LastRefreshBannerTypes.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
                    || LastRefreshBannerTypes.AddSeconds(_TimeCacheBannerTypes).CompareTo(DateTime.Now) <= 0)
                {
                    LastRefreshBannerTypes = DateTime.Now;
                    AddBannerTypes();
                }
            }
            return CacheFactory.GetCacheManager().GetData(bannerTypesKey);

        }
        public static void InitCache()
        {
            AddHotNews();
            AddBannerTypes();
            AddAffInfo();
        }
    }
}
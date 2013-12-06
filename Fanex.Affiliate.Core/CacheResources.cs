namespace Fanex.Affiliate.Core
{
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using DNAide.Data;
    using DNAide.Core;
    using System.Data;
    using Fanex.Affiliate.DataObjects;
    /// <summary>
    /// Summary description for CacheResources
    /// </summary>
    public class CacheResources
    {
        private static string CacheKey = "CacheResources";

        public static void RefreshCache()
        {
            DbResult result = DBResources.GetAllResource();

            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                CacheFactory.GetCacheManager().Add(CacheKey, result.ResultSet.Tables[0]);
            }
            else // no rows -- means there isn't any hot news
            {
                CacheFactory.GetCacheManager().Add(CacheKey, new DataTable());
            }
        }


        public static DataTable GetReourcesCache()
        {
            if (CacheFactory.GetCacheManager().GetData(CacheKey) == null)
                RefreshCache();
            return (DataTable)CacheFactory.GetCacheManager().GetData(CacheKey);
        }

        public static string GetResourceByKey(string strResourceKey, string strLanguageCode)
        {
            DataTable dtb = GetReourcesCache();
            DataRow[] rows = dtb.Select("ResourceKey = " + strResourceKey + " and LanguageCode = " + strLanguageCode);

            return CastAide.AsString(rows[0]["ResourceValue"], string.Empty);

        }

    }
}
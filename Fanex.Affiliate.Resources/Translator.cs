// -----------------------------------------------------------------------
// <copyright file="AffLang.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Caching;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Translator
    {
        private const string CacheKey = "e946bd02-81b4-4f91-a9ab-e433cf93f59d";
      

        public static string Get(string expression, string refType = "Resource")
        {
            return Get(expression, null, refType);
        }
        public static string Get(string expression, string culture , string refType )
        {
            var data = GetData();
            if (expression == "LandingPages")
            {
                expression = expression.Trim();
                var a = data.Where(x => x["RefId"].ToString() == refType && x["LanguageCode"].ToString() == "vi-VN").OrderBy(x => x["ResourceKey"].ToString());

            }
            if(String.IsNullOrEmpty(culture)) 
                culture= CultureInfo.CurrentCulture.Name;

            
            var items =  (from i in data
                          where
                           i["RefId"].ToString() == refType &&
                           i["ResourceKey"].ToString().Equals(expression,System.StringComparison.OrdinalIgnoreCase) &&
                           i["LanguageCode"].ToString() == culture
                          select i);
            var row = items.FirstOrDefault();
            string text = String.Empty;
            if (row != null)
                text = row["ResourceValue"].ToString().Trim();

            return String.IsNullOrEmpty(text) && culture!="en-US" ? Get(expression,"en-US",refType) : text;
        }
        private static IEnumerable<DataRow> GetData()
        {
            if (CacheFactory.GetCacheManager().Contains(CacheKey))
            {
                return (IEnumerable<DataRow>)CacheFactory.GetCacheManager().GetData(CacheKey);
            }
            var data = DbConnect.GetData().ResultSet.Tables[0].Rows.Cast<DataRow>();
            CacheFactory.GetCacheManager().Add(CacheKey, data);
            return data;
        }
    }
}

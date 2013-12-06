using System.CodeDom;
using System.Web.UI;
using System.Web.Compilation;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Fanex.Affiliate.Resources
{
    public class Aff : DynamicObject
    {               
        private const string CacheKey = "e946bd02-81b4-4f91-a9ab-e433cf93f59d";

        // Handle static properties
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetEvalData(binder.Name, CultureInfo.CurrentCulture.Name);            
            return true;
        }
             
        public static string GetEvalData(string expression, string language)
        {
            var data = GetData();
            var items =  (from i in data
                        where
                         i["RefId"].ToString() == "AffiliateContent" &&
                         i["ResourceKey"].ToString() == expression &&
                         i["LanguageCode"].ToString() == language
                        select i);
            var text = items.FirstOrDefault();
            return text == null ? string.Empty : text["ResourceValue"].ToString();
        }

        private static IEnumerable<DataRow> GetData()
        {
            if (CacheFactory.GetCacheManager().Contains(CacheKey))
            {
                return (IEnumerable<DataRow>) CacheFactory.GetCacheManager().GetData(CacheKey);
            }
            var data = DbConnect.GetData().ResultSet.Tables[0].Rows.Cast<DataRow>();
            CacheFactory.GetCacheManager().Add(CacheKey, data);
            return data;
        }
    }
}
using System.CodeDom;
using System.Web.UI;
using System.Web.Compilation;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

namespace Fanex.Affiliate.Resources
{
    public class TextExpressionBuilder : ExpressionBuilder
    {
        private const string CacheKey = "e946bd02-81b4-4f91-a9ab-e433cf93f59d";
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            var refMe = new CodeTypeReferenceExpression(base.GetType());
            var expression = new CodePrimitiveExpression(entry.Expression);
            return new CodeMethodInvokeExpression(refMe, "GetEvalData", new CodeExpression[] { expression });
        }

        public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            return GetEvalData(entry.Expression);
        }

        public override bool SupportsEvaluate
        {
            get
            {
                return true;
            }
        }

        public static string GetEvalData(string expression)
        {
            return GetEvalData(expression, CultureInfo.CurrentCulture.Name);
        }

        public static string GetEvalData(string expression, string language)
        {
            var data = GetData();
            var items =  (from i in data
                        where
                         i["RefId"].ToString() == "Resource" &&
                        i["ResourceKey"].ToString() == expression &&
                        i["LanguageCode"].ToString() == language
                        select i);
            var text = items.FirstOrDefault();
            return text == null ? string.Empty : text["ResourceValue"].ToString();
        }


        public static IEnumerable<DataRow> GetListCountry()
        {
            var data = GetData();
            IEnumerable<DataRow> listCountry = (from i in data
                        where i["RefId"].ToString() == "CountryCode" &&
                        i["LanguageCode"].ToString() == CultureInfo.CurrentCulture.Name
                        orderby i["ResourceValue"]
                        select i);
            return listCountry;
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
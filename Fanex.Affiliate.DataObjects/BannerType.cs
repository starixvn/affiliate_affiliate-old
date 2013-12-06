// -----------------------------------------------------------------------
// <copyright file="Currency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Collections.Generic;
    using DNAide.Data;
    using System.Linq;

    using Fanex.Affiliate.Model;
    using DNAide.Core;
    using System.Data;
    using System.Collections;
    using System;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BannerType
    {

        public static BannerTypeObj Select(IEnumerable<BaseObj> list, string id)
        {
            if (String.IsNullOrWhiteSpace(id) || id == "0")
                return null;
            return list.Where(x => x.Id == id).Cast<BannerTypeObj>().FirstOrDefault();
        }
        public static IList<BaseObj> Execute(string type, int parentid = 0, string languagecode = "en-US")
        {

            Hashtable ht = new Hashtable();
            ht.Add("typename", type);
            ht.Add("language", languagecode);
            if(parentid > 0)
            ht.Add("parenttype", parentid );

            DbResult result = DbHelper.Execute("Aff_BannerSelType", ht, DBType.AFFILIATE);

            var list = new List<BaseObj>();
            
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {                    
                    list.Add(new BannerTypeObj() {
                            Id = CastAide.AsString(row ["TypeID"]),
                            Value = CastAide.AsString(row["Value"]),                            
                            HasChildren = row["HasSubCategory"].ToString() != "0"
                        });
                }
            }
            return list;
        }
        public static IList<BaseObj> GetSubCategories(int category)
        {
            return Execute("MediaSubCategory",category);
        }
        public static IList<BaseObj> GetCategories()
        {
            return Execute("MediaCategory"); 
        }
        public static IList<BaseObj> GetSizes()
        {
            return Execute("Size"); 
        }

        public static IList<BaseObj> GetTypes()
        {
            return Execute("ImageType"); 
        }

        public static IList<BaseObj> GetCurrencies()
        {
            return Execute("Currency"); 
        }
    }
}

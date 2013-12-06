// -----------------------------------------------------------------------
// <copyright file="Currency.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Collections.Generic;
    using DNAide.Data;
    using Fanex.Affiliate.Model;
    using DNAide.Core;
    using System.Data;
    using System.Collections;
    using System;
    using System.Web;
    using Fanex.Core.Utility;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AffType
    {
        public static DbResult Get(string id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);

            return DbHelper.Execute("Aff_AffTypeSelByID", hsParam, DBType.AFFILIATE);
        }

        public static IList<BaseObj> GetAll()
        {
            
            DbResult result = DbHelper.Execute("Aff_AffTypeSel", DBType.AFFILIATE);

            var list = new List<BaseObj>();
            
            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    list.Add(new BaseObj()
                    {
                            Id = CastAide.AsString(row ["typeid"]),
                            Value = CastAide.AsString(row["name"])
                        });                    
                }
            }
            return list;
        }

        public static DbResult GetAll(string typename, int pageindex, int pagesize)
        {
            Hashtable ht = new Hashtable();
            ht.Add("typename", String.IsNullOrEmpty(typename) ? null : typename);
            ht.Add("pagesize", pagesize);
            ht.Add("pageindex", pageindex);
            return DbHelper.Execute("Aff_AffTypeSelAll", ht, DBType.AFFILIATE);            
        }

        public static string Add(Hashtable hsParam)
        {
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            var data = DbHelper.Execute("Aff_AffTypeIns", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string Update(Hashtable hsParam, string lang = "en-US")
        {

            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("language", lang);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            var data = DbHelper.Execute("Aff_AffTypeUpd", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string Delete(string ids)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("idlist", ids);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            var data = DbHelper.Execute("Aff_AffTypeDel", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

    }
}

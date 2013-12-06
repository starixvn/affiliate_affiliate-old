namespace Fanex.Affiliate.DataObjects
{
    using System.Data;
    using System.Web;
    using DNAide.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    using DNAide.Core;
    using System.Resources;
    using Fanex.Affiliate.Resources;
    using System.Globalization;
    /// <summary>
    /// Summary description for Settings
    /// </summary>
    public static class Country
    {
        
        public static DbResult GetCountries(string name, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("name", string.IsNullOrEmpty(name) == true ? null : name);            
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", pageindex);

            return DbHelper.Execute("Aff_CountrySelAll", hsParam, DBType.AFFILIATE);
        }


        public static IList<BaseObj> GetAll()
        {

            DbResult rs = GetCountries("", 1, 500);
            IList<BaseObj> list = new List<BaseObj>();


            if (rs.ResultSet.Tables.Count > 0)
            {
                foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
                {
                    CountryObj obj = new CountryObj();
                    obj.Id = CastAide.AsString(dr["CountryID"]);
                    obj.Value= CastAide.AsString(dr["CountryName"]);

                    //obj.Description = CastAide.AsString(dr["Description"]);
                    //obj.Icon = CastAide.AsString(dr["Icon"]);
                    //obj.DisplayOrder = CastAide.AsInt32(dr["DisplayOrder"]);
                    //obj.IsEnabled = CastAide.AsBoolean(dr["IsEnabled"]);
                    //obj.ExchangeID = CastAide.AsInt32(row ["ExchangeID"]);
                    //obj.Code = CastAide.AsString(row["Code"]);
                    //obj.Prefix = CastAide.AsString(row["Prefix"]);

                    list.Add(obj);
                }
            }

            return list;
        }
        
       public static DbResult GetCountry(string id)
       {
           Hashtable hsParam = new Hashtable();
           hsParam.Add("id", id);

           return DbHelper.Execute("Aff_CountrySelByID", hsParam, DBType.AFFILIATE);
       }


       public static string Add(Hashtable hsParam)
       {
           hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
           hsParam.Add("ip", Utilities.GetIPAddressOfClient());

           var data = DbHelper.Execute("Aff_CountryIns", hsParam, DBType.AFFILIATE);
           int result = int.Parse(data.Outputs["errcode"].ToString());
           return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
       }

       public static string Update(Hashtable hsParam, string lang = "en-US")
       {
           
           hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);           
           hsParam.Add("ip", Utilities.GetIPAddressOfClient());

           var data = DbHelper.Execute("Aff_CountryUpd", hsParam, DBType.AFFILIATE);
           int result = int.Parse(data.Outputs["errcode"].ToString());
           return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
       }

       public static string Delete(string ids)
       {
           Hashtable hsParam = new Hashtable();
           hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
           hsParam.Add("idlist", ids);
           hsParam.Add("ip", Utilities.GetIPAddressOfClient());

           var data = DbHelper.Execute("Aff_CountryDel", hsParam, DBType.AFFILIATE);
           int result = int.Parse(data.Outputs["errcode"].ToString());
           return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
       }

       
    }
}

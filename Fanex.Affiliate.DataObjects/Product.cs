namespace Fanex.Affiliate.DataObjects
{
    using System.Web;
    using DNAide.Data;
    using System.Collections;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// Summary description for Settings
    /// </summary>
    public static class Product
    {
    

        public static DbResult Get(string name, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("name", string.IsNullOrEmpty(name) == true ? null : name);            
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", pageindex);

            return DbHelper.Execute("Aff_ProductSelAll", hsParam, DBType.AFFILIATE);
        }
        
       public static DbResult Get(int id)
       {
           Hashtable hsParam = new Hashtable();
           hsParam.Add("id", id);

           return DbHelper.Execute("Aff_ProductSelByID", hsParam, DBType.AFFILIATE);
       }


       public static string Add(Hashtable hsParam)
       {           
           hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
           hsParam.Add("ip", Utilities.GetIPAddressOfClient());

           var data = DbHelper.Execute("Aff_ProductIns", hsParam, DBType.AFFILIATE);
           int result = int.Parse(data.Outputs["errcode"].ToString());
           return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
       }

       public static string Update(Hashtable hsParam, string lang = "en-US")
       {
           
           hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
           hsParam.Add("ip", Utilities.GetIPAddressOfClient());
           hsParam.Add("lang", lang);
           hsParam.Add("errcode", 0);
           hsParam.Add("errmsg", string.Empty);

           var data = DbHelper.Execute("Aff_ProductUpd", hsParam, DBType.AFFILIATE);
           int result = int.Parse(data.Outputs["errcode"].ToString());
           return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
       }

       public static string Delete(string ids)
       {
           Hashtable hsParam = new Hashtable();
           hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
           hsParam.Add("idlist", ids);
           hsParam.Add("ip", Utilities.GetIPAddressOfClient());

           var data = DbHelper.Execute("Aff_ProductDel", hsParam, DBType.AFFILIATE);
           int result = int.Parse(data.Outputs["errcode"].ToString());
           return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
       }

       
    }
}

// -----------------------------------------------------------------------
// <copyright file="Resource.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections;
    using DNAide.Data;
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Resource
    {
        public static DbResult GetResources(string key, string value, string type, string language, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("key", string.IsNullOrEmpty(key) == true ? null : key);
            hsParam.Add("value", string.IsNullOrEmpty(value) == true ? null : value);
            hsParam.Add("resourcetype", string.IsNullOrEmpty(type) == true ? null : type);
            hsParam.Add("languagecode", string.IsNullOrEmpty(language) == true ? null : language);
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", pageindex);

            return DbHelper.Execute("Aff_ResourceKeySel", hsParam, DBType.AFFILIATE);
        }

        public static DbResult GetResource(Guid id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);

            return DbHelper.Execute("Aff_ResourceKeySelByID", hsParam, DBType.AFFILIATE);
        }

        public static string AddResource(string key, string value, string type, string langCode, string description, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("key", key);
            hsParam.Add("value", value);
            hsParam.Add("type", type);
            if(!String.IsNullOrEmpty(langCode))
                hsParam.Add("languagecode", langCode);
            hsParam.Add("description", description);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            var data = DbHelper.Execute("Aff_ResourceKeyIns", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string UpdateResource(Guid id, string value, string description, string lang = null)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("id", id);
            hsParam.Add("value", value);
            hsParam.Add("description", description);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            if(!String.IsNullOrEmpty(lang))
                hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            var data = DbHelper.Execute("Aff_ResourceKeyUpd", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }
     
        public static string DeleteResources(string ids)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("idlist", ids);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            var data = DbHelper.Execute("Aff_ResourceKeyDel", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static DbResult GetResourceTypes()
        {
            return DbHelper.Execute("Aff_ResourcesTypeSel", DBType.AFFILIATE);
        }

        //public static DbResult GetResourceLanguages()
        //{
        //    return DbHelper.Execute("Aff_SupportedLanguagesSel", DBType.AFFILIATE);
        //}
        public static DbResult GetResources(string key, string value, string type, string language, string option)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("resourcetype", string.IsNullOrEmpty(type) == true ? null : type);
            hsParam.Add("languagecode", string.IsNullOrEmpty(language) == true ? null : language);
            hsParam.Add("key", string.IsNullOrEmpty(key) == true ? null : key);
            hsParam.Add("value", string.IsNullOrEmpty(value) == true ? null : value);
            hsParam.Add("option", string.IsNullOrEmpty(option) == true ? null : option);

            return DbHelper.Execute("Aff_ResourcesTranslationSel", hsParam, DBType.AFFILIATE);
        }
        public static DbResult InsertOrUpdateResources(string strXML)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("xmldata", string.IsNullOrEmpty(strXML) == true ? null : strXML);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            return DbHelper.Execute("Aff_ResourcesTranslationInsOrUpdateXML", hsParam, DBType.AFFILIATE);
        }
    }
}

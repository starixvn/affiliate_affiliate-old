namespace Fanex.Affiliate.DataObjects
{
    using System.Data;
    using System.Web;
    using DNAide.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Core.Utility;
    using System;
    using Fanex.Affiliate.Model;
    using DNAide.Core;
    /// <summary>
    /// Summary description for Settings
    /// </summary>
    public static class Settings
    {
        public static int GetMaxAdjustmentAmount(string currency)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("currency", currency);

            var rs =  DbHelper.Execute("Aff_GetMaxAdjustmentAmount", hsParam, DBType.AFFILIATE).ResultSet;
            if (rs.Tables.Count > 0 && rs.Tables[0].Rows.Count > 0)
                return CastAide.AsInt32(rs.Tables[0].Rows[0][0],0);
            return 0;

        }
        public static DataRowCollection GetSettingsByType(string type)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("type", type);

            return DbHelper.Execute("Aff_SystemSettingsSelByType", hsParam, DBType.AFFILIATE).ResultSet.Tables[0].Rows;
        }

        public static string GetLinkDomain()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", HttpContext.Current.Session["CustID"]);

            var data = DbHelper.Execute("Aff_AffiliateLinkDomainSel", hsParam, DBType.AFFILIATE);
            IList<string> domains = Common.GetStringIList(data.Outputs["domainlist"].ToString(), ",");
            return domains[0];
        }

        public static string GetBannerDomain()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", HttpContext.Current.Session["CustID"]);

            var data = DbHelper.Execute("Aff_BannerLinkDomainSel", hsParam, DBType.AFFILIATE);
            IList<string> domains = Common.GetStringIList(data.Outputs["domainlist"].ToString(), ",");
            return domains[0];
        }
        public static string GetBetDomain()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", HttpContext.Current.Session["CustID"]);

            var data = DbHelper.Execute("Aff_MemberLinkDomainSel", hsParam, DBType.AFFILIATE);
            IList<string> domains = Common.GetStringIList(data.Outputs["domainlist"].ToString(), ",");
            return domains[0];
        }

        public static DbResult GetSettingByType(string typeName)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("type", typeName);

            return DbHelper.Execute("Aff_SystemSettingsSelByType", hsParam, DBType.AFFILIATE);
        }

        public static DbResult GetSettingByName(string Name)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("name", Name);

            return DbHelper.Execute("Aff_SystemSettingsSelByName", hsParam, DBType.AFFILIATE);
        }

        public static string GetSettingValue(string name = null, string type = null, string environment = null, int id = 0, string value = null)
        {
            var rs = Settings.GetSettings(name, type, environment, id, value);
            var strSValue = rs.ResultSet.Tables.Count > 0 && rs.ResultSet.Tables[0].Rows.Count > 0 ? rs.ResultSet.Tables[0].Rows[0]["SValue"].ToString() : string.Empty;

            return strSValue;
        }

        public static DbResult GetSettings(string name = null, string type = null, string environment = null, int id = 0, string value = null)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);
            hsParam.Add("type", string.IsNullOrEmpty(type) == true ? null : type);
            hsParam.Add("name", string.IsNullOrEmpty(name) == true ? null : name);
            hsParam.Add("value", string.IsNullOrEmpty(value) == true ? null : value);
            hsParam.Add("environment", string.IsNullOrEmpty(environment) == true ? null : environment);

            return DbHelper.Execute("Aff_SystemSettingsSel", hsParam, DBType.AFFILIATE);
        }

        public static DbResult GetSettings(string key, string type, string value, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("key", string.IsNullOrEmpty(key) == true ? null : key);
            hsParam.Add("type", string.IsNullOrEmpty(type) == true ? null : type);
            hsParam.Add("value", string.IsNullOrEmpty(value) == true ? null : value);
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", pageindex);

            return DbHelper.Execute("Aff_SystemSettingsSelAll", hsParam, DBType.AFFILIATE);
        }

        public static DbResult GetSetting(int id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);

            return DbHelper.Execute("Aff_SystemSettingsSelByID", hsParam, DBType.AFFILIATE);
        }
        public static DbResult GetSetting(string name)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("name", name);

            return DbHelper.Execute("Aff_SystemSettingsSelByName", hsParam, DBType.AFFILIATE);
        }

        public static string AddSetting(string key, string description, string value, string type, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("key", key);
            hsParam.Add("value", value);
            hsParam.Add("type", type);
            hsParam.Add("description", description);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            var data = DbHelper.Execute("Aff_SystemSettingsIns", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string UpdateSetting(int id, string description, string value, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("id", id);
            hsParam.Add("description", description);
            hsParam.Add("value", value);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            var data = DbHelper.Execute("Aff_SystemSettingsUpd", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string DeleteSettings(string ids)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("idlist", ids);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            var data = DbHelper.Execute("Aff_SystemSettingsDel", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static DateTime GetSystemStartDate()
        {

            var rs = GetSettingByName("SystemStartDate");
            var strSValue = rs.ResultSet.Tables.Count > 0 ? rs.ResultSet.Tables[0].Rows[0]["SValue"].ToString() : "USD";

            var date = DateTime.Now;
            DateTime.TryParse(strSValue, out date);

            return date;

        }

    }
}

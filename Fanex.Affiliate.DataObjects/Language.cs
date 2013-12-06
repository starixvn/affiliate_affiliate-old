namespace Fanex.Affiliate.DataObjects
{
    using DNAide.Data;
    using System.Data;
    using System.Collections;
    using System.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for Language
    /// </summary>
    public static class Language
    {
        private static string _english = "en-US";
        private static string _chineseCS = "zh-CN";//Simplified Chinese 
        private static string _chineseCT = "zh-TW";//Traditional Chinese
        private static string _spanish = "es-ES";
        private static string _thai = "th-TH";
        private static string _vietnamese = "vi-VN";
        private static string _indonesia = "id-ID";

        private static string _chinese = "zh-CN";
        // ... more language

        public static string CHINESE
        {
            get
            {
                return _chinese;
            }
        }

        public static string ENGLISH
        {
            get
            {
                return _english;
            }
        }

        public static string CHINESE_CS
        {
            get
            {
                return _chineseCS;
            }
        }

        public static string CHINESE_CT
        {
            get
            {
                return _chineseCT;
            }
        }
        public static string SPANISH
        {
            get
            {
                return _spanish;
            }
        }
        public static string THAI
        {
            get
            {
                return _thai;
            }
        }
        public static string VIETNAMESE
        {
            get
            {
                return _vietnamese;
            }
        }
        public static string INDONESIA
        {
            get
            {
                return _indonesia;
            }
        }
        public static IList<BaseObj> GetAdminSupportedLanguages()
        {

            DbResult result = DbHelper.Execute("Aff_AdminSupportedLanguagesSel", DBType.AFFILIATE);

            var list = new List<BaseObj>();

            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    var obj = new LanguageObj()
                    {
                        Id = row["LanguageID"].ToString(),
                        Value = row["LanguageName"].ToString()                        
                    };
                    list.Add(obj);
                }
            }

            return list;
        }
        public static IList<BaseObj> GetAffiliateSupportedLanguages(string environment = "Production")
        {
            var param = new Hashtable();
            param.Add("environment", environment);

            DbResult result = DbHelper.Execute("Aff_AffiliateSupportedLanguagesSel",param, DBType.AFFILIATE);

            var list = new List<BaseObj>();

            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    var obj = new LanguageObj()
                    {
                        Id = row["LanguageID"].ToString(),
                        Value = row["LanguageName"].ToString()
                    };
                    list.Add(obj);
                }
            }

            return list;
        }
        public static IList<BaseObj> Get()
        {

            DbResult result = DbHelper.Execute("Aff_LanguageSel", DBType.AFFILIATE);

            var list = new List<BaseObj>();

            if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in result.ResultSet.Tables[0].Rows)
                {
                    var obj = new LanguageObj()
                    {
                        Id = row["LanguageID"].ToString(),
                        Value = row["LanguageName"].ToString(),
                        CountryID = row["CountryID"].ToString()
                    };
                    list.Add(obj);
                }
            }

            return list;
        }

        public static DbResult Get(string name, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("name", string.IsNullOrEmpty(name) == true ? null : name);
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", pageindex);

            return DbHelper.Execute("Aff_LanguageSelAll", hsParam, DBType.AFFILIATE);
        }

        public static DbResult Get(string id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);

            return DbHelper.Execute("Aff_LanguageSelByID", hsParam, DBType.AFFILIATE);
        }


        public static string Add(Hashtable hsParam, string lang = "en-US")
        {
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);

            var data = DbHelper.Execute("Aff_LanguageIns", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string Update(Hashtable hsParam, string lang = "en-US")
        {

            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);

            var data = DbHelper.Execute("Aff_LanguageUpd", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

        public static string Delete(string ids)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("updatedby", HttpContext.Current.Session["CustID"]);
            hsParam.Add("idlist", ids);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());

            var data = DbHelper.Execute("Aff_LanguageDel", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }

    }
}

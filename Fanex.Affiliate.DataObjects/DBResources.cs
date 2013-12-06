namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using DNAide.Data;
    using DNAide.Core;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// Summary description for DBResources
    /// </summary>
    public class DBResources
    {
        public static string GetDBResourceText(string strResourceKey, string strLangCode)
        {
            Hashtable htb = new Hashtable();
            htb.Add("ResourceKey", strResourceKey);
            htb.Add("LanguageCode", strLangCode);
            htb.Add("ResourceValue", "");
            DbResult dbr = DbHelper.Execute("Aff_GetDBResourceText", htb, DBType.AFFILIATE);
            return CastAide.AsString(dbr.Outputs["ResourceValue"], string.Empty);
        }

        public static DbResult GetAllResource()
        {
            DbResult dbr = DbHelper.Execute("Aff_GetDBResourceText", null, DBType.AFFILIATE);
            return dbr;
        }

    }
}
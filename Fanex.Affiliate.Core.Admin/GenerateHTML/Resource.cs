namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// Summary description for Settings
    /// </summary>
    public class Resource
    {
        public static string GenHtml(string query, string value, string type, string lang, int curpage, int pageSize, ref int minNo, ref int maxNo, ref int totalMess, ref int intTotalPage)
        {
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.Resource.GetResources(query, value, type, lang, curpage, pageSize);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            totalMess = nRows;
            int cols = 10;
            minNo = 0;
            maxNo = 0;

            //var languages = DataObjects.Resource.GetResourceLanguages().ResultSet.Tables[0].Rows.Cast<DataRow>();

            if (nRows > 0)
            {
                int pagesize = pageSize;
                string settings = TemplateHandler.LoadTemplate("Resources/Resources.htm");
                string strContent = TemplateHandler.CopyBlock(settings, "Content");

                DataTable dtbResult = result.ResultSet.Tables[0];
                int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                minNo = (curpage - 1) * pagesize + intNo + 1;
                TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
                foreach (DataRow dr in dtbResult.Rows)
                {
                    string strTmp = strContent;
                    intNo++;

                    TemplateHandler.Assign(ref strTmp, "No-val", (curpage - 1) * pagesize + intNo);
                    TemplateHandler.Assign(ref strTmp, "ID-val", dr["ResourceID"].ToString());

                    TemplateHandler.Assign(ref strTmp, "Name-val", dr["ResourceKey"]);
                    TemplateHandler.Assign(ref strTmp, "Type-val", dr["RefID"]);
                    TemplateHandler.Assign(ref strTmp, "Code-val", GetLanguageName(AppSettings.BaseLanguages, dr["LanguageCode"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "Value-val", Common.Snippet(dr["ResourceValue"].ToString(), 100));
                    TemplateHandler.Assign(ref strTmp, "Description-val", dr["Description"]);                    
                    TemplateHandler.Assign(ref strTmp, "LangCode", dr["LanguageCode"]);
                    if ((intNo % 2) == 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgEven);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgOdd);
                    }

                    strBuild.Append(strTmp);

                }
                maxNo = (curpage - 1) * pagesize + intNo;
                intTotalPage = totalpage;

                return strBuild.ToString();
            }
            return AdminCommon.NoInfo(cols);

        }
        private static string GetLanguageName(IEnumerable<BaseObj> languages, string code)
        {
            return (from i in languages
                    where i.Id == code
                    select i.Value.ToString()).SingleOrDefault();
        }
    }
}
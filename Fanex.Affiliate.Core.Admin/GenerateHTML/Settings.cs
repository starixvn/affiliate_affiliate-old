

namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using Fanex.Core.Utility;
    /// <summary>
    /// Summary description for Settings
    /// </summary>
    public class Settings
    {
        public static string GenSettingsHtml(string query, string type, string value, int curpage, int pageSize, ref int minNo, ref int maxNo, ref int totalMess, ref int intTotalPage)
        {
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.Settings.GetSettings(query, type, value, curpage, pageSize);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            totalMess = nRows;
            int cols = 10;
            minNo = 0;
            maxNo = 0;

            if (nRows > 0)
            {
                int pagesize = pageSize;
                string settings = TemplateHandler.LoadTemplate("Settings/Settings.htm");
                string strContent = TemplateHandler.CopyBlock(settings, "Content");
                //TemplateHandler.Assign(ref strContent, "ViewDetail", Resources.Admin.ViewDetail);
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
                    TemplateHandler.Assign(ref strTmp, "ID-val", dr["SID"].ToString());

                    TemplateHandler.Assign(ref strTmp, "SName-val", dr["SName"]);
                    TemplateHandler.Assign(ref strTmp, "SType-val", dr["Type"]);
                    TemplateHandler.Assign(ref strTmp, "SValue-val", Common.Snippet(dr["SValue"].ToString(), 100));
                    TemplateHandler.Assign(ref strTmp, "SDescription-val", dr["Description"]);
                    TemplateHandler.Assign(ref strTmp, "Update", Resources.Admin.Update);

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
    }
}
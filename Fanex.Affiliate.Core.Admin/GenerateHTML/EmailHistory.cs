// -----------------------------------------------------------------------
// <copyright file="EmailHistory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using Fanex.Core.Utility;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EmailHistory
    {
        public static string GetEmailHistoryList(object mid, string senderName, string email, string affiliateName, object fromdate, object todate, int curpage, ref int minNo, ref int maxNo, ref int totalEmail, int pagesize, ref int totalpage)
        {
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.EmailHistory.EmailHistorySel(mid, senderName, email, affiliateName, fromdate, todate, pagesize, curpage);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            totalEmail = nRows;
            int cols = 7;
            minNo = 0;
            maxNo = 0;

            if (nRows > 0)
            {
                string hotNewsContent = TemplateHandler.LoadTemplate("Others/EmailHistory.htm");
                string strContent = TemplateHandler.CopyBlock(hotNewsContent, "Content");
                TemplateHandler.Assign(ref strContent, "ViewDetail", Resources.Admin.ViewDetail);
                TemplateHandler.Assign(ref strContent, "View", Resources.Admin.View);
                DataTable dtbResult = result.ResultSet.Tables[0];
                int totalResultPage = CastAide.AsInt32(result.Outputs["totalpages"]);
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                minNo = (curpage - 1) * pagesize + 1;
                TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
                foreach (DataRow dr in dtbResult.Rows)
                {
                    string strTmp = strContent;
                    intNo++;
                    TemplateHandler.Assign(ref strTmp, "No-val", (curpage - 1) * pagesize + intNo);
                    TemplateHandler.Assign(ref strTmp, "ID-val", dr["mid"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Sender-val", dr["sender"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Affiliate-val", dr["affname"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Subject-val", Common.CutLongText(dr["subject"].ToString(), 90));
                    TemplateHandler.Assign(ref strTmp, "From-val", dr["email"].ToString());
                    TemplateHandler.Assign(ref strTmp, "SentDate-val", AdminCommon.IBCDateTimeFormat(dr["createdate"], DTFormatType.DT24h));
                    if ((intNo % 2) == 0)
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgEven);
                    else
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgOdd);

                    strBuild.Append(strTmp);

                }
                maxNo = (curpage - 1) * pagesize + intNo;
                totalpage = totalResultPage;
                return strBuild.ToString();
            }
            return AdminCommon.NoInfo(cols);

        }
    }
}

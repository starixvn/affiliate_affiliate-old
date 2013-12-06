namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using Fanex.Core.Utility;
    using System.Web;
    /// <summary>
    /// Generate content for Hot news
    /// Written by : Tong Hoang Dung on 19/03/2009
    /// </summary>
    public class HotNews
    {
        public static string GetHotNewsContentList(int important, object fromdate, object todate, int curpage, int pageSize,ref int minNo,ref int maxNo, ref int totalMess, ref int intTotalPage)
        {            
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.HotNews.HotNewsSel(important, fromdate, todate, curpage, pageSize);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            totalMess = nRows;
            int cols = 10;
            minNo = 0;
            maxNo = 0;

            if (nRows > 0)
            {
                int pagesize = pageSize;
                string hotNewsContent = TemplateHandler.LoadTemplate("Others/Message.htm");
                string strContent = TemplateHandler.CopyBlock(hotNewsContent, "Content");
                TemplateHandler.Assign(ref strContent, "ViewDetail", Resources.Admin.ViewDetail);
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
                    TemplateHandler.Assign(ref strTmp, "ID-val", dr["msgid"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Postdate-val", AdminCommon.IBCDateTimeFormat(dr["posteddate"], DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "Message-val", HttpUtility.HtmlEncode(Common.CutLongText(dr["message_en"].ToString(), 110)));
                    TemplateHandler.Assign(ref strTmp, "Creator-val", dr["creatorname"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UpdatedBy-val", dr["updatename"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UpdateDate-val", AdminCommon.IBCDateTimeFormat(dr["updateddate"], DTFormatType.DT24h));
                   
                    if (CastAide.AsBoolean(dr["important"]))
                    {
                        TemplateHandler.Assign(ref strTmp, "hotnewsstyle", "text_hotnews_important");
                        TemplateHandler.Assign(ref strTmp, "Important-val", Resources.Admin.Yes);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "hotnewsstyle", "text_hotnews");
                        TemplateHandler.Assign(ref strTmp, "Important-val", Resources.Admin.No);
                    }

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

namespace Fanex.Affiliate.Core.Customer.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Generate content for Hot news
    /// Written by : Tong Hoang Dung on 19/03/2009
    /// </summary>
    public class HotNews
    {
        public static string GetHotNewsContentList(int important, object fromdate, object todate, int curpage, int intPageSize, ref int totalpage)
        {
            string hotNewsContent = TemplateHandler.LoadTemplate("UserInfo/HotNews.htm");
            string strContent = TemplateHandler.CopyBlock(hotNewsContent, "Content");
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.HotNews.HotNewsSel(important, fromdate, todate, curpage, intPageSize,AppSession.Language);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int pagesize = AppSettings.PageSize;
            int cols = 4;

            if (nRows > 0)
            {
                DataTable dtbResult = result.ResultSet.Tables[0];
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                int intNo = intPageSize * (curpage - 1);
                StringBuilder strBuild = new StringBuilder();
                foreach (DataRow dr in dtbResult.Rows)
                {
                    string strTmp = strContent;
                    intNo++;

                    TemplateHandler.Assign(ref strTmp, "No-val", intNo);
                    TemplateHandler.Assign(ref strTmp, "ID-val", dr["msgid"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Postdate-val", AffCommon.ConvertDateTime(dr["posteddate"], "/", true, true));
                    TemplateHandler.Assign(ref strTmp, "Message-val", HttpUtility.HtmlEncode(dr["message"].ToString()));
                       //HttpUtility.HtmlEncode(Fanex.Core.Utility.Common.BreakLongString(dr["message"].ToString(), 60)));
                    
                    if (CastAide.AsBoolean(dr["important"]))
                    {
                        TemplateHandler.Assign(ref strTmp, "hotnewsstyle", "text_hotnews_important");
                    }
                    else
                    {
                      //  TemplateHandler.Assign(ref strTmp, "hotnewsstyle", "text_hotnews");
                        TemplateHandler.Assign(ref strTmp, "hotnewsstyle", string.Empty);
                    }

                    if ((intNo % 2) == 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                    }

                    strBuild.Append(strTmp);

                }
                //if (totalpage > 0)
                //{
                //    strBuild.Append(Common.GenPagingFooter("", totalpage, curpage, cols));
                //}

               return strBuild.ToString();
            }           
            return AffCommon.NoInfo(cols);
           
        }
    }

}

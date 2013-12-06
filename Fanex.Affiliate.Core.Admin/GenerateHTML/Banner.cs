// -----------------------------------------------------------------------
// <copyright file="Banner.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using System.Data;
    using System.Text;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Banner
    {

        public const int NORMAL_TYPE = 1;
        public const int MEDIA_TYPE = 2;

        public static string GetViewedText(int textID, int noRow)
        {
            DbResult result = DataObjects.BannerText.GetTextDetail(textID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");

                DataRow dr = result.ResultSet.Tables[0].Rows[0];
                
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                if (noRow % 2 == 0)
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordFirst");
                }
                else
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordSecond");
                }
                TemplateHandler.Assign(ref content, "contentvalue", AdminCommon.FormatStringHtml((dr["content"].ToString())));
                //TemplateHandler.Assign(ref content, "lastupdate", dr["Lastupdate"].ToString());
                TemplateHandler.Assign(ref content, "catevalue", dr["category"].ToString());
                TemplateHandler.Assign(ref content, "langvalue", dr["languagename"].ToString());
                TemplateHandler.Assign(ref content, "textid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue", dr["currencyname"].ToString());
                return content;

            }

            return string.Empty;
        }

      
        public static string GetEditedBanner(int bannerID, int noRow)
        {
            DbResult result = DataObjects.Banner.GetBannerDetail(bannerID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListBanner.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "ViewMode1");
                TemplateHandler.RemoveBlock(ref content, "ViewMode2");
                DataRow dr = result.ResultSet.Tables[0].Rows[0];

                DataView dv = DataObjects.Banner.GetBannerType(string.Empty).ResultSet.Tables[0].DefaultView;
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "rowstyle", "active_listbanner");
                TemplateHandler.Assign(ref content, "filenamevalue", dr["content"].ToString());
                TemplateHandler.Assign(ref content, "catevalue", 
                     HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "slCategory", dr["category"].ToString(), false));
                    //AdminCommon.GetDataOptionBanner(dv, "slCategory", "MediaCategory", dr["category"].ToString(), false, false));
                TemplateHandler.Assign(ref content, "langvalue",
                    HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", dr["language"].ToString(), false));
                    //AdminCommon.GetDataOptionBanner(dv, "slLanguage", "SupportedLanguages", dr["language"].ToString(), false, false));
                TemplateHandler.Assign(ref content, "imagetypevalue",
                    HtmlHelper.DropDownList(AppSettings.BaseBannerTypes, "slImageType", dr["imagetype"].ToString(), false));
                    //AdminCommon.GetDataOptionBanner(dv, "slImageType", "MediaFileType", dr["imagetype"].ToString(), false, true));
                TemplateHandler.Assign(ref content, "sizevalue", string.Empty /*Common.GetDataOptionBanner(dv, "slSize", "MediaSize", dr["size"].ToString(), false, false)*/);
                TemplateHandler.Assign(ref content, "bannerid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue",
                    HtmlHelper.DropDownList(AppSettings.AllCurrencies, "slCurrency", dr["currency"].ToString(), false));
                    //AdminCommon.GetDataOptionBanner(dv, "slCurrency", "MediaCurrency", dr["currency"].ToString(), false, false));
                return content;


            }

            return string.Empty;
        }
        public static string GetViewedBanner(int bannerID, int noRow)
        {
            DbResult result = DataObjects.Banner.GetBannerDetail(bannerID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListBanner.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");

                DataRow dr = result.ResultSet.Tables[0].Rows[0];
                string[] strSize = dr["size"].ToString().Split('x');
                int realWidth = CastAide.AsInt32(strSize[0]);
                int realHeight = CastAide.AsInt32(strSize[1]);
                TemplateHandler.Assign(ref content, "real-width", realWidth.ToString());
                TemplateHandler.Assign(ref content, "real-height", realHeight.ToString());
                string bannerType = string.Empty;
                if (System.IO.Path.GetExtension(dr["content"].ToString()) == ".swf")
                {
                    TemplateHandler.RemoveBlock(ref content, "limit-img");
                    bannerType = "flash";
                }
                else
                {
                    TemplateHandler.RemoveBlock(ref content, "limit-fls");
                    bannerType = "image";
                }
                if (realWidth > 500 || realHeight > 500)
                {
                    TemplateHandler.Assign(ref content, "css-class", "hand");
                    string viewFull = string.Format("DoCal('BannerDetail.aspx?bannertype={0}&imagevalue={1}&lastupdate={2}&width={3}&height={4}','{5}',{6} + 100,{7} + 50);",
                                            bannerType, dr["content"].ToString(), dr["Lastupdate"].ToString(), realWidth, realHeight, dr["bannerid"].ToString(),
                                            realWidth, realHeight);
                    TemplateHandler.Assign(ref content, "viewfull", viewFull);
                    if (realWidth > realHeight)
                    {
                        realHeight = 500 * realHeight / realWidth;
                        realWidth = 500;
                    }
                    else
                    {
                        realWidth = 500 * realWidth / realHeight;
                        realHeight = 500;
                    }
                }
                else
                {
                    TemplateHandler.Assign(ref content, "css-class", string.Empty);
                    TemplateHandler.Assign(ref content, "viewfull", string.Empty);
                }
                TemplateHandler.Assign(ref content, "width", realWidth.ToString());
                TemplateHandler.Assign(ref content, "height", realHeight.ToString());

                DataView dv = DataObjects.Banner.GetBannerType(string.Empty).ResultSet.Tables[0].DefaultView;
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                if (noRow % 2 == 0)
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordFirst");
                }
                else
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordSecond");
                }


                TemplateHandler.Assign(ref content, "lastupdate", dr["Lastupdate"].ToString());
                TemplateHandler.Assign(ref content, "imagevalue", dr["content"].ToString());
                TemplateHandler.Assign(ref content, "filenamevalue", dr["content"].ToString());
                TemplateHandler.Assign(ref content, "catevalue", dr["category"].ToString());
                TemplateHandler.Assign(ref content, "langvalue", dr["language"].ToString());
                TemplateHandler.Assign(ref content, "imagetypevalue", dr["imagetype"].ToString());
                TemplateHandler.Assign(ref content, "sizevalue", dr["size"].ToString());
                TemplateHandler.Assign(ref content, "bannerid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue", dr["currency"].ToString());
                return content;

            }

            return string.Empty;
        }

        public static string GetListText(int category, string language, int currency, int pageIndex, ref int minNo, ref int maxNo, ref int totalText, ref int totalPage, int pageSize)
        {
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.Banner.GetBannerListText(category, language, currency, pageIndex, pageSize);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int pagesize = pageSize;
            int cols = 10;
            minNo = 0;
            maxNo = 0;
            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");
                int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 0;
                minNo = (pageIndex - 1) * pagesize + 1;
                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    if (counter % 2 == 0)
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordFirst");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordSecond");
                    }
                    
                    TemplateHandler.Assign(ref rowText, "No-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "order-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "lastupdate", dr["Lastupdate"].ToString());
                    TemplateHandler.Assign(ref rowText, "contentvalue", AdminCommon.FormatStringHtml(dr["content"].ToString()));
                    TemplateHandler.Assign(ref rowText, "catevalue", dr["category"].ToString());
                    TemplateHandler.Assign(ref rowText, "langvalue", dr["language"].ToString());
                    TemplateHandler.Assign(ref rowText, "textid", dr["bannerid"].ToString());
                    TemplateHandler.Assign(ref rowText, "currencyvalue", dr["currency"].ToString());
                    contentList.Append(rowText);
                }
                maxNo = (pageIndex - 1) * pagesize + counter;
                totalText = nRows;
                if (totalpage > 0)
                {
                    totalPage = totalpage;
                }
                return contentList.ToString();
            }
            else
            {
               return AdminCommon.NoInfo(cols);
            }

        }

        public static string GetBannerList(int category, string language, int imageType, int size, int currency, int pageIndex, ref int minNo, ref int maxNo, ref int totalBanner, ref int totalPage, int pageSize, int type)
        {
            DbResult result = null;
            int nRows = 0;
            result = DataObjects.Banner.GetBannerList(category, language, imageType, size, currency, pageIndex, pageSize, type);

            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int pagesize = pageSize;
            int cols = 10;
            minNo = 0;
            maxNo = 0;
            if (nRows > 0)
            {
                //string temp = (type == Banner.NORMAL_TYPE) 
                //    ? TemplateHandler.LoadTemplate("Media/MainListBanner.htm")
                //    : TemplateHandler.LoadTemplate("Media/MainListMediaBanner.htm");

                string temp = TemplateHandler.LoadTemplate("Media/MainListMediaBanner.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");
                int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 0;
                minNo = (pageIndex - 1) * pagesize + 1;
                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    if (counter % 2 == 0)
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordFirst");
                    }
                    else
                    {

                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordSecond");
                    }

                    string[] strSize = "-1x-1".Split('x');

                    if (dr["size"].ToString().Contains("x"))
                    {
                        strSize = dr["size"].ToString().Split('x');
                    }
                    
                    int realWidth = CastAide.AsInt32(strSize[0]);
                    int realHeight = CastAide.AsInt32(strSize[1]);
                    
                    TemplateHandler.Assign(ref rowText, "real-width", realWidth);
                    TemplateHandler.Assign(ref rowText, "real-height", realHeight);
                    string bannerType = string.Empty;
                    if (CastAide.AsString(dr["imagetype"],"Image").Equals("Flash",System.StringComparison.OrdinalIgnoreCase))
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "limit-img");
                        bannerType = "flash";
                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "limit-fls");
                        bannerType = "image";
                    }
                    if (realWidth > 500 || realHeight > 500)
                    {
                        TemplateHandler.Assign(ref rowText, "css-class", "hand");
                        string viewFull = string.Format("DoCal('BannerDetail.aspx?bannertype={0}&imagevalue={1}&version={2}&width={3}&height={4}','{5}',{6} + 100,{7} + 50);",
                                                bannerType, dr["content"].ToString(), dr["version"].ToString(), realWidth, realHeight, dr["bannerid"].ToString(),
                                                realWidth, realHeight);
                        TemplateHandler.Assign(ref rowText, "viewfull", viewFull);

                        if (realWidth > realHeight)
                        {
                            realHeight = 500 * realHeight / realWidth;
                            realWidth = 500;
                        }
                        else
                        {
                            realWidth = 500 * realWidth / realHeight;
                            realHeight = 500;
                        }
                    }
                    else
                    {
                        TemplateHandler.Assign(ref rowText, "css-class", string.Empty);
                        TemplateHandler.Assign(ref rowText, "viewfull", string.Empty);
                    }

                    TemplateHandler.Assign(ref rowText, "No-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "order-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "version", dr["version"].ToString());
                    TemplateHandler.Assign(ref rowText, "imagevalue", AppSettings.BannerDomain + dr["content"].ToString());
                    TemplateHandler.Assign(ref rowText, "filenamevalue", dr["content"].ToString());
                    TemplateHandler.Assign(ref rowText, "catevalue", dr["category"].ToString());
                    TemplateHandler.Assign(ref rowText, "langvalue", dr["language"].ToString());
                    TemplateHandler.Assign(ref rowText, "imagetypevalue", dr["imagetype"].ToString());
                    TemplateHandler.Assign(ref rowText, "sizevalue", dr["size"].ToString());
                    TemplateHandler.Assign(ref rowText, "width", realWidth);
                    int intWidth = CastAide.AsInt32(strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height", realHeight);
                    TemplateHandler.Assign(ref rowText, "tdheight", intWidth > 515 ? CastAide.AsString(CastAide.AsInt32(strSize[1]) + 17, string.Empty) + "px" : "auto");
                    TemplateHandler.Assign(ref rowText, "bannerid", dr["bannerid"].ToString());
                    TemplateHandler.Assign(ref rowText, "currencyvalue", dr["currency"].ToString());

                    var totalrequest = CastAide.AsInt32(dr["totalrequest"],0);
                    if (totalrequest > 0)
                        TemplateHandler.RemoveBlock(ref rowText, "Checkbox");
                    TemplateHandler.Assign(ref rowText, "totalrequest_val",totalrequest);
                    TemplateHandler.Assign(ref rowText, "lastrequest_val", CastAide.AsString(dr["lastrequest"], ""));
                    contentList.Append(rowText);

                }
                maxNo = (pageIndex - 1) * pagesize + counter;
                totalBanner = nRows;
                if (totalpage > 0)
                {
                    //contentList.Append(Common.GenPagingFooter("", totalpage, pageIndex, cols));
                    totalPage = totalpage;
                }
                return contentList.ToString();

            }
            else
            {
                return AdminCommon.NoInfo(cols);
            }

        }
    }
}









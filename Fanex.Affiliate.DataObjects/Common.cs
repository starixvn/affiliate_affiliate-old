namespace Fanex.IndoPaul.DataObjects
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Web;
    using System.Collections;
    using System.Text;
    using DNAide.Utils;
    using DNAide.Web;
    using DNAide.Core;
    using LoadBalancerLibrary;
    using System.Collections.Generic;
    using DNAide.Data;
    using Fanex.IndoPaul.Resources;
    /// <summary>
    /// Summary description for Common
    /// </summary>
    public class Common
    {
        public const string Male = "Male";
        public const string Female = "Female";
        private const int TimeCacheUM = 10;//second   


        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /*smith*/
        public static IList<string> GetStringIList(string stringArray, string sep)
        {
            string[] separator = { sep };
            string[] result = stringArray.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            IList<string> data = new List<string>();
            foreach (var item in result)
            {
                data.Add(item);
            }
            return data;
        }
        public static string GetLangCodeByShortLang(string value)
        {
            switch (value.ToLower())
            {
                case "en": return "en-US";
                case "eneuro": return "en-US";
                case "ct": return "zh-TW";
                case "cs": return "zh-CN";
                case "th": return "th-TH";
                case "es": return "es-ES";
                case "vn": return "vi-VN";
                case "id": return "id-ID";
                default:
                    return "en-US";
            }
        }

        public static string GetLangCode(string value)
        {
            switch (value.ToLower())
            {
                case "english": return "en";
                case "english (europe)": return "eneuro";
                case "chinese traditional": return "ct";
                case "chinese simplified": return "cs";
                case "thai": return "th";
                case "spanish": return "es";
                case "vietnamese": return "vn";
                case "indonesian": return "id";
                default:
                    return "en";
            }
        }

        public static string GetLanguage()
        {
            switch (AppSettings.Session.Language.ToLower())
            {
                case "en-us":
                    return "English";
                case "zh-cn":
                    return "Simplified";
                case "zh-tw":
                    return "Tradition";
                case "th-th":
                    return "Thai";
                case "es-es":
                    return "Spanish";
                case "vi-vn":
                    return "Vietnamese";
                case "id-id":
                    return "Indonesian";
                default:
                    return "English";
            }
        }

        public static string GetPreferLanguage()
        {
            switch (AppSettings.Session.PreferLang.ToLower())
            {
                case "en-us":
                    return "English";
                case "en-gb":
                    return "English (Europe)";
                case "zh-cn":
                    return "Chinese Simplified";
                case "zh-tw":
                    return "Chinese Traditional";
                case "th-th":
                    return "Thai";
                case "es-es":
                    return "Spanish";
                case "vi-vn":
                    return "Vietnamese";
                case "id-id":
                    return "Indonesian";
                default:
                    return "English";
            }
        }

        public static string GetLanguageText(string strLangCode)
        {
            switch (strLangCode.ToLower())
            {
                case "en-us":
                    return Resources.Lang.English;
                case "zh-cn":
                    return Resources.Lang.ChineseSimplifi;
                case "zh-tw":
                    return Resources.Lang.ChineseTraditio;
                case "th-th":
                    return Resources.Lang.Thai;
                case "es-es":
                    return Resources.Lang.Spanish;
                case "vi-vn":
                    return Resources.Lang.Vietnamese;
                case "id-id":
                    return Resources.Lang.Indonesian;
                default:
                    return Resources.Lang.English;
            }
        }

        public static string GetIndex(string strType)
        {
            switch (strType)
            {
                case "Player": return "0";
                case "Neteller": return "1";
                case "Moneybookers": return "2";
                case "Wire": return "3";
                case "Cheque": return "4";
                default: return "0";
            }
        }
        public static string GetMethodName(string strValue)
        {
            switch (strValue)
            {
                case "0": return "Player";
                case "1": return "Neteller";
                case "2": return "Moneybookers";
                case "3": return "Wire";
                case "4": return "Cheque";
                default: return "Player";
            }
        }
        public static string GetErrMsg(int intErrCode, ref string strErrObj)
        {
            switch (intErrCode)
            {
                case 1:
                    return Resources.Lang.Err1;
                case 2:
                    return Resources.Lang.Err2;
                case 3:
                    return Resources.Lang.Err3;
                case 4:
                    return Resources.Lang.Err4;
                case 5:
                    return Resources.Lang.Err5;
                case 101:
                    return Resources.Lang.Err101;
                case 102:
                    return Resources.Lang.Err102;
                case 103:
                    return Resources.Lang.Err103;
                case 104:
                    return Resources.Lang.Err104;
                case 105:
                    return Resources.Lang.Err105;
                case 107:
                    return Resources.Lang.Err107;
                case 111:
                    return Resources.Lang.Err111;
                case 112:
                    return Resources.Lang.Err112;
                case 113:
                    return Resources.Lang.Err113;
                case 114:
                    return Resources.Lang.Err114;
                case 115:
                    return Resources.Lang.Err115;
                case 116:
                    return Resources.Lang.Err116;
                case 100:
                    return Resources.Lang.Err100;
                case 999:
                    return Resources.Lang.Err999;
                case 1001:
                    return Resources.Lang.Err1001;
                case 1002:
                    strErrObj = "txtEmail";
                    return Resources.Lang.Err1002;
                case 1003:
                    strErrObj = "txtBet12UserName";
                    return Resources.Lang.Err1003;
                case 1004:
                    return Resources.Lang.Err1004;
                case 1005:
                    return Resources.Lang.Err1005;
                case 1006:
                    return Resources.Lang.Err1006;
                case 3001:
                    return Resources.Lang.Err3001;
                case 3002:
                    return Resources.Lang.Err3002;
                case 3003:
                    return Resources.Lang.Err3003;
                case 3004:
                    return Resources.Lang.Err3004;
                case 3006:
                    return Resources.Lang.Err3006;
                case 4001:
                    return Resources.Lang.Err4001;
                case 4002:
                    return Resources.Lang.Err4002;
                case 4003:
                    return Resources.Lang.Err4003;
                case 4004:
                    return Resources.Lang.Err4004;
                case 4005:
                    return Resources.Lang.Err4005;
                default:
                    return string.Empty;
            }
        }
        public static string FormatNumber(object Number)
        {
            decimal dcNumber = CastAide.AsDecimal(Number);
            CultureInfo info = new CultureInfo("en-US");
            if (dcNumber == 0)
                return AppSettings.Session.Prefix + "0.00";
            if (dcNumber >= 0)
            {
                return AppSettings.Session.Prefix + string.Format(info, "{0:#,##0.00}", dcNumber); //FormatHandler.FormatNumber20(dcNumber, "en-US");
            }
            else
            {
                dcNumber = dcNumber * -1;
                return "-" + AppSettings.Session.Prefix + string.Format(info, "{0:#,##0.00}", dcNumber);
            }
        }
        public static string GenerateSelectObj(string strID, ArrayList arrValue, ArrayList arrText, string strValueSelected)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<select id=\"" + strID + "\" name=\"" + strID + "\">");
            for (int i = 0; i < arrValue.Count; i++)
            {
                if (string.Compare(strValueSelected, arrValue[i].ToString()) == 0)
                    strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\" selected>" + arrText[i].ToString() + "</option>");
                else
                    strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\">" + arrText[i].ToString() + "</option>");
            }
            strBuild.Append("</select>");
            return strBuild.ToString();
        }
        public static string NoInfo(int intNumCols)
        {
            string strNoInfo = TemplateHandler.LoadTemplate("NoInfo.html");
            TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Lang.NoInfo);
            TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
            return strNoInfo;
        }

        public static string GerStatus(object strStatus)
        {
            switch (strStatus.ToString().ToLower())
            {
                case "running":
                case "pending":
                    return Resources.Lang.Proccessing;
                case "completed":
                    return Resources.Lang.Approved;
                case "cancelled":
                    return Resources.Lang.Cancelled;
                case "approved":
                    return Resources.Lang.Approved;
                case "rejected":
                    return Resources.Lang.Rejected;
                case "current":
                    return Resources.Lang.Current;
                default:
                    return strStatus.ToString();
            }
        }
        /// <summary>
        /// Split long string into many smaller lines DungTong@20090319
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nChars"></param>
        /// <returns></returns>
        public static string BreakLongString(string strContent, int numChar)
        {
            StringBuilder strBuild = new StringBuilder();
            int txtLength = strContent.Length;
            for (int i = 0; i <= (txtLength / numChar); i++)
            {
                string strTmp = strContent.Substring(i * numChar, ((i + 1) * numChar > txtLength ? txtLength - (i * numChar) : numChar));
                int start = strTmp.IndexOf(" ");
                if (start < 0)
                {
                    strTmp = strTmp + " ";
                }
                else
                {
                    int last = strTmp.LastIndexOf(" ");
                    if (last < (numChar / 2))//the space is too far with last character
                    {
                        strTmp = strTmp + " ";
                    }
                }
                strBuild.Append(strTmp);
            }
            return strBuild.ToString();
        }
        public static string GenPagingFooter(string url, int pageNum, int cpage, int colsNo)
        {
            try
            {
                string pageIndexTpl = TemplateHandler.LoadTemplate("PageIndex.html");
                string footerHtml = TemplateHandler.LoadTemplate("Paging.html");
                string GotoPageHtml = TemplateHandler.LoadTemplate("GotoPage.html");
                TemplateHandler.Assign(ref footerHtml, "{CurrentPage}", cpage.ToString());
                TemplateHandler.Assign(ref footerHtml, "{Colspan}", colsNo.ToString());
                string pageList = string.Empty;

                int startpage, endpage;
                startpage = cpage - 3;
                if (startpage < 1)
                    startpage = 0;

                endpage = startpage + 5;
                if (endpage > pageNum)
                    endpage = pageNum;

                if ((endpage - startpage) < 5 && (cpage < pageNum) && (pageNum - 5 > 0))
                {
                    startpage = pageNum - 5;
                    endpage = pageNum;
                }

                if ((endpage + 1) == pageNum)
                    endpage = pageNum;

                if (cpage >= 4)
                {
                    string pageIndex = (string)pageIndexTpl.Clone();
                    TemplateHandler.Assign(ref pageIndex, "{Url}", url);
                    TemplateHandler.Assign(ref pageIndex, "{CurrentPage}", string.Empty);
                    TemplateHandler.Assign(ref pageIndex, "{PageText}", "1");
                    TemplateHandler.Assign(ref pageIndex, "{Page}", "1");
                    if (cpage == 4 || pageNum == 6)
                        pageList += pageIndex;
                    else
                        pageList += pageIndex + " ... ";
                }

                if ((endpage == cpage) && (endpage - 5) > 0)
                    startpage = endpage - 5;

                for (int i = startpage; i < endpage; ++i)
                {
                    string pageIndex = (string)pageIndexTpl.Clone();

                    TemplateHandler.Assign(ref pageIndex, "{Url}", url);
                    if (cpage == (i + 1))
                    {
                        pageIndex = "&nbsp;<strong style=\"padding:0 1px;\">" + cpage.ToString() + "</strong>";
                    }
                    else
                    {
                        TemplateHandler.Assign(ref pageIndex, "{CurrentPage}", string.Empty);
                        TemplateHandler.Assign(ref pageIndex, "{PageText}", "" + (i + 1));
                        TemplateHandler.Assign(ref pageIndex, "{Page}", "" + (i + 1));
                    }
                    pageList += pageIndex;
                }

                if ((endpage + 1) < pageNum)
                {
                    string pageIndex = (string)pageIndexTpl.Clone();
                    TemplateHandler.Assign(ref pageIndex, "{Url}", url);
                    TemplateHandler.Assign(ref pageIndex, "{CurrentPage}", string.Empty);
                    TemplateHandler.Assign(ref pageIndex, "{PageText}", " " + pageNum);
                    TemplateHandler.Assign(ref pageIndex, "{Page}", " " + pageNum);
                    pageList += " ... " + pageIndex;
                }

                if (pageList.Length > 0)
                {
                    TemplateHandler.Assign(ref footerHtml, "{PageList}", pageList.Remove(pageList.Length - 1));
                    TemplateHandler.Assign(ref footerHtml, "{GotoPage}", GotoPageHtml);
                    TemplateHandler.Assign(ref footerHtml, "{Url}", url);
                }
                else
                {
                    TemplateHandler.Assign(ref footerHtml, "{PageList}", "<strong>1</strong>");
                    TemplateHandler.Assign(ref footerHtml, "{Url}", "");
                }
                return footerHtml;
            }
            catch { throw; }
        }
        public static string RootURL()
        {
            string strURL = String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath);
            if (strURL.LastIndexOf("/") == strURL.Length - 1)
                strURL = strURL.Substring(0, strURL.Length - 1);
            return strURL;
        }
        public static string HTMLEncode(string text)
        {
            return text.Replace("\r\n", "").Replace("'", "&rsquo;");
        }

        /// <summary>
        /// Generate paging header and footer include button at top of table VinhTrinh@20090612
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nChars"></param>
        /// <returns></returns>
        public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, int width, bool hasCheckAll, bool hasPageSize)
        {
            string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
            TemplateHandler.Assign(ref strPaging, "select", Resources.Lang.Select);
            TemplateHandler.Assign(ref strPaging, "alltext", Resources.Lang.All);
            TemplateHandler.Assign(ref strPaging, "pagetext", Resources.Lang.Page);
            TemplateHandler.Assign(ref strPaging, "pagesizetext", Resources.Lang.PageSize);
            TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of.ToLower());
            TemplateHandler.Assign(ref strPaging, "fpage", 1);
            TemplateHandler.Assign(ref strPaging, "lpage", numPages);
            TemplateHandler.Assign(ref strPaging, "ppage", currPage - 1);
            TemplateHandler.Assign(ref strPaging, "npage", currPage + 1);
            TemplateHandler.Assign(ref strPaging, "maxpage", numPages);
            TemplateHandler.Assign(ref strPaging, "aId", aID);
            TemplateHandler.Assign(ref strPaging, "txtID", txtID);
            TemplateHandler.Assign(ref strPaging, "width", width);

            if (!hasCheckAll)
                TemplateHandler.RemoveBlock(ref strPaging, "selectall");
            if (!hasPageSize)
                TemplateHandler.RemoveBlock(ref strPaging, "pagesize");

            TemplateHandler.Assign(ref strPaging, "page", currPage);
            TemplateHandler.Assign(ref strPaging, "all", numPages);
            TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
            TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
            return strPaging;
        }
        public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, string[] txtButton, string[] txtButtonEvent, int width, bool hasCheckAll, bool hasPageSize, bool hasButton, bool hasExcel)
        {
            string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
            TemplateHandler.Assign(ref strPaging, "select", Resources.Lang.Select);
            TemplateHandler.Assign(ref strPaging, "alltext", Resources.Lang.All);
            TemplateHandler.Assign(ref strPaging, "pagetext", Resources.Lang.Page);
            TemplateHandler.Assign(ref strPaging, "pagesizetext", Resources.Lang.PageSize);
            TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of.ToLower());
            TemplateHandler.Assign(ref strPaging, "fpage", 1);
            TemplateHandler.Assign(ref strPaging, "lpage", numPages);
            TemplateHandler.Assign(ref strPaging, "ppage", currPage - 1);
            TemplateHandler.Assign(ref strPaging, "npage", currPage + 1);
            TemplateHandler.Assign(ref strPaging, "maxpage", numPages);
            TemplateHandler.Assign(ref strPaging, "aId", aID);
            TemplateHandler.Assign(ref strPaging, "txtID", txtID);
            TemplateHandler.Assign(ref strPaging, "width", width != 0 ? width.ToString() : "100%");

            if (!hasCheckAll)
                TemplateHandler.RemoveBlock(ref strPaging, "selectall");
            if (!hasPageSize)
                TemplateHandler.RemoveBlock(ref strPaging, "pagesize");
            TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
            TemplateHandler.Assign(ref strPaging, "page", currPage);
            TemplateHandler.Assign(ref strPaging, "all", numPages);
            if (!hasButton)
                TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
            else
            {
                string strButton = TemplateHandler.CopyBlock(strPaging, "headbutton");
                StringBuilder strBuild = new StringBuilder();
                for (int i = 0; i < txtButton.Length; i++)
                {
                    string strTmp = strButton;
                    TemplateHandler.Assign(ref strTmp, "txtheadbutton", txtButton[i]);
                    TemplateHandler.Assign(ref strTmp, "buttonevent", txtButtonEvent[i]);
                    strBuild.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strPaging, "headbutton", strBuild.ToString());
            }
            if (!hasExcel)
                TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
            return strPaging;
        }
        public static string CutLongText(string strText, int maxLength)
        {
            if (strText.Length <= maxLength)
                return strText;
            if (AppSettings.Session.Language != Language.ENGLISH)
                return strText.Substring(0, maxLength) + "...";
            int end = strText.LastIndexOf(" ", maxLength);
            return strText.Substring(0, end) + "...";
        }
        ///Johnny@20100317: Get suffix for sending mail template
        public static string EmailSuffix(bool MailOption)
        {
            return (MailOption ? "" : "_html");
        }
        ///Johnny@20100420: Get suffix for sending mail template
        public static bool GetDownTime(out string startTime, out string endTime)
        {
            startTime = endTime = null;
            DateTime Now = DateTime.Now;
            if (Now >= AppSettings.UMStartTime && Now <= AppSettings.UMEndTime)
            {
                startTime = AppSettings.UMStartTime.ToString("hh:mm tt");
                endTime = AppSettings.UMEndTime.ToString("hh:mm tt"); ;
                return true;
            }
            if (string.IsNullOrEmpty(AppSettings.LBUrl))
            {
                return false;
            }
            bool isUM = false;
            if (HttpContext.Current.Cache["IsUM"] == null)
            {
                LoadBalancer LB = null;

                try
                {
                    LB = (LoadBalancer)Activator.GetObject(typeof(LoadBalancer), AppSettings.LBUrl);
                    isUM = LB.isUnderMaintainence(IBCSites.Agency);
                    if (isUM)
                    {
                        string[] UMTime = LB.GetUMTimeSetting();
                        startTime = UMTime[0];
                        endTime = UMTime[1];
                    }
                    //Cache Information which is gotten from Service
                    HttpContext.Current.Cache.Insert("IsUM", isUM, null, DateTime.Now.AddSeconds(TimeCacheUM), TimeSpan.Zero);
                    HttpContext.Current.Cache.Insert("StartTime", startTime, null, DateTime.Now.AddSeconds(TimeCacheUM), TimeSpan.Zero);
                    HttpContext.Current.Cache.Insert("EndTime", endTime, null, DateTime.Now.AddSeconds(TimeCacheUM), TimeSpan.Zero);

                }
                catch (Exception ex)
                {
                    DNAide.Web.DNAideLog.Write("LoadBalancerLibrary Error : " + ex.Message);
                }
            }
            else
            {
                isUM = CastAide.AsBoolean(HttpContext.Current.Cache["IsUM"]);
                startTime = CastAide.AsString(HttpContext.Current.Cache["StartTime"]);
                endTime = CastAide.AsString(HttpContext.Current.Cache["EndTime"]);
            }

            return isUM;
        }
        public static string ConvertDateTime(object dtmTime, string slash, bool isFullDate, bool hasTime)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime Tmp1 = Convert.ToDateTime(dtmTime, culture);
            if (AppSettings.Session.Language != "th-TH")
            {
                if (isFullDate)
                    return Tmp1.ToString("MM" + slash + "dd" + slash + "yyyy") + (hasTime ? " " + Tmp1.ToString("hh:mm:ss") : string.Empty);
                else
                    return Tmp1.ToString("MMMM" + slash + "yyyy");
            }
            if (isFullDate)
                return Tmp1.ToString("MM" + slash + "dd" + slash) + Tmp1.ToString("yyyy", culture) + (hasTime ? " " + Tmp1.ToString("hh:mm:ss") : string.Empty);
            else
                return Tmp1.ToString("MMMM" + slash) + Tmp1.ToString("yyyy", culture);
        }

        public static bool IsAffInListEuLink()
        {
            //List Affiliate in web.config and Affiliate use English Euro, Spanish language
            string[] strTmp = AppSettings.Session.UserName.ToLower().Split(':');
            string strUsername = strTmp[0].Trim();
            return (AppSettings.ListAffUseEuLink.ToLower().IndexOf(strUsername) >= 0 || AppSettings.Session.PreferLang.ToLower() == "es-es" || AppSettings.Session.PreferLang.ToLower() == "en-gb");
        }

        public static string GetDBResourceTextByKey(string strResourceKey)
        {
            return string.Empty;//DBResources.GetDBResourceText(strResourceKey, AppSettings.Session.Language);
        }
        public static string GetCopyRight(string preferredLanguage)
        {
            string text = TextExpressionBuilder.GetEvalData("Copyright", preferredLanguage);
            return text.Replace("{year}", DateTime.Now.Year.ToString());
        }
        public static string GetCopyRight()
        {
            string text = TextExpressionBuilder.GetEvalData("Copyright");
            return text.Replace("{year}", DateTime.Now.Year.ToString());
        }
        public static string GetListSupportedLang()
        {
            DbResult rs = Settings.GetResourceLanguages();
            StringBuilder strResult = new StringBuilder();
            strResult.AppendLine("<option value=\"\">" + Resources.Lang.SelectOne + "</option>");
            if (rs.ResultSet.Tables.Count > 0)
            {
                foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
                {
                    strResult.AppendLine(string.Format("<option value='{0}'>{1}</option>", dr["Key"], dr["TypeName"]));
                }
            }
            return strResult.ToString();
        }

        public static string GetListSupportedCurrency()
        {
            DbResult rs = Settings.GetSettingByName("AffiliateCurrencies");
            string strSValue = rs.ResultSet.Tables.Count > 0 ? rs.ResultSet.Tables[0].Rows[0]["SValue"].ToString() : "USD";

            var listValue = GetStringIList(strSValue, ",");

            StringBuilder strResult = new StringBuilder();
            foreach (var item in listValue)
            {
                if (item != "UUS" || AppSettings.LocalTest == "1")//don't display UUS in live production
                    strResult.AppendLine(string.Format("<option value='{0}'>{1}</option>", item, item));
            }
            return strResult.ToString();
        }

        public static void WriteLog(string typeName, string spName, string details)
        {
            var hsParam = new Hashtable
                          {
                              {"creator", AppSettings.Session.CustID},
                              {"affid", AppSettings.Session.CustID},
                              {"typename", typeName},
                              {"spname", spName},
                              {"ip", HttpContext.Current.Request.UserHostAddress},
                              {"refno", 0},
                              {"details", details}
                          };
            DbHelper.Execute("Aff_WriteLog", hsParam, DBType.AFFILIATE);
        }
    }

    public class IBCException : Exception
    {
        public IBCException() : base() { }
        public IBCException(string message) : base(message) { }
        public IBCException(string message, Exception ex) : base(message, ex) { }
        protected IBCException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
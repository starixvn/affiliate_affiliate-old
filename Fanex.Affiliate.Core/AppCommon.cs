// -----------------------------------------------------------------------
// <copyright file="Common.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Xml;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Utility;
    using Sunplus.UMControllerLibrary;
    using System.Runtime.Caching;
    using System.Text.RegularExpressions;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class AppCommon
    {
        public const string Male = "Male";
        public const string Female = "Female";
        public const string RowBgOdd = "#ffffff";
        public const string RowBgEven = "#f0f0f0";
        public const string RowBgOver = "#FFF5A5";
        public const string RowBgClosed = "#d6d4d2";
        public const string RowBgPending = "#e8ded6";
        private const int TimeCacheUM = 10;//second   
        public const int NormalBanner = 0;
        public const int MediaBanner = 1;
        private static MemoryCache _memoryCache = MemoryCache.Default;




        public static void AssignToTemplate(ref string template, Dictionary<string, object> values)
        {
            foreach (var item in values)
            {
                TemplateHandler.Assign(ref template, item.Key, item.Value);
            }
        }
        public static void AssignToAdminTemplate(ref string template, params string[] tags)
        {
            Admin.ResourceManager.IgnoreCase = true;
            foreach (var item in tags)
            {
                var val = Admin.ResourceManager.GetString(item);
                if (string.IsNullOrEmpty(val))
                    val = Translator.Get(item);
                if (string.IsNullOrEmpty(val))
                    val = item;
                TemplateHandler.Assign(ref template, item, val);
            }
        }

        public static void AssignToTemplate(ref string template, params string[] tags)
        {
            foreach (var item in tags)
            {
                TemplateHandler.Assign(ref template, item, Fanex.Affiliate.Resources.Translator.Get(item));
            }
        }
        public static IList<T> LocalizeList<T>(IList<T> list) where T : BaseObj
        {
            var culture = AppSession.Language;

            if (culture != "en-US")
            {
                var cultureInfo = new CultureInfo(culture);

                IList<T> newlist = list.Clone();

                foreach (var item in newlist)
                {
                    if (!String.IsNullOrEmpty(item.Value))
                    {
                        //var value = Translator.Get( Lang.ResourceManager.GetString(item.Value, cultureInfo);
                        var value = Translator.Get(item.Value, culture, "Resource");
                        if (!String.IsNullOrEmpty(value))
                            item.Value = value;
                    }
                }
                return newlist;
            }
            return list;
        }

        public static string GenMonthOptions(int selectedMonth)
        {
            StringBuilder strBuild = new StringBuilder();

            for (int i = 1; i <= 12; i++)
            {
                var m = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                strBuild.AppendFormat("<option value='{0}' {1}>{2}</option>", i,
                    i == selectedMonth ? "selected" : String.Empty, m);
            }

            return strBuild.ToString();
        }

        public static string GenMonthDropDown(string name, int selectedMonth, string className = null)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.AppendFormat("<select id='{0}' name='{0}'  class='{1}'>", name, className);

            strBuild.Append(GenMonthOptions(selectedMonth));
            strBuild.Append("</select>");
            return strBuild.ToString();
        }

        public static string GenYearOptions(int selectedYear)
        {
            StringBuilder strBuild = new StringBuilder();
            var date = AppSettings.SystemStartDate;
            for (int i = date.Year; i <= DateTime.Now.Year; i++)
            {
                strBuild.AppendFormat("<option value='{0}' {1}>{0}</option>", i, i == selectedYear ? "selected" : String.Empty);
            }

            return strBuild.ToString();
        }

        public static string GenYearDropDown(string name, int selectedYear, string className = null)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.AppendFormat("<select id='{0}' name='{0}' class='{1}'>", name, className);
            strBuild.Append(GenYearOptions(selectedYear));
            strBuild.Append("</select>");
            return strBuild.ToString();
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

            UMBusiness UMCtrl = new UMBusiness();
            bool isUM = false;

            if (_memoryCache["IsUM"] == null)
            {
                // LoadBalancer LB = null;

                try
                {
                    isUM = UMCtrl.isUnderMaintainence(Sites.Affiliate, AppSettings.LBUrl, ref startTime, ref endTime);//Get Agnet Site UM Status.
                    //LB = (LoadBalancer)Activator.GetObject(typeof(LoadBalancer), AppSettings.LBUrl);
                    //isUM = LB.isUnderMaintainence(IBCSites.Agency);
                    //if (isUM)
                    //{
                    //    string[] UMTime = LB.GetUMTimeSetting();
                    //    startTime = UMTime[0];
                    //    endTime = UMTime[1];
                    //}
                    //Cache Information which is gotten from Service
                    _memoryCache.Add("IsUM", isUM, DateTime.Now.AddSeconds(TimeCacheUM));
                    _memoryCache.Add("StartTime", startTime, DateTime.Now.AddSeconds(TimeCacheUM));
                    _memoryCache.Add("EndTime", endTime, DateTime.Now.AddSeconds(TimeCacheUM));

                }
                catch (Exception ex)
                {
                    DNAide.Web.DNAideLog.Write("LoadBalancerLibrary Error : " + ex.Message);
                }
            }
            else
            {
                isUM = CastAide.AsBoolean(_memoryCache["IsUM"]);
                startTime = CastAide.AsString(_memoryCache["StartTime"]);
                endTime = CastAide.AsString(_memoryCache["EndTime"]);
            }

            return isUM;
        }

        public static void WriteLog(string typeName, string spName, string details)
        {
            DbLog.WriteLog(typeName, spName, details, AppSession.CustID, AppSession.CustID, Utilities.GetIPAddressOfClient());
        }


        ///Johnny@20100317: Get suffix for sending mail template
        public static string EmailSuffix(bool MailOption)
        {
            return (MailOption ? "" : "_html");
        }


        public static string GetCopyRight(string preferredLanguage)
        {
            string text = Translator.Get("Copyright");// Fanex.Affiliate.Resources.TextExpressionBuilder.GetEvalData("Copyright", preferredLanguage);
            return text.Replace("{year}", DateTime.Now.Year.ToString());
        }

        public static string GetLangCodeByShortLang(string value)
        {
            if (value.Contains("-"))
                return value;
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
        public static string GetListAlPhabet(string currSearch)
        {
            StringBuilder html = new StringBuilder();
            string[] arrAlpha ={ Fanex.Affiliate.Resources.Admin.All , "0-9","A","B","C","D","E","F","G","H","I","J","K","L","M", 
            "N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };
            foreach (string str in arrAlpha)
            {
                if (string.Compare(str, currSearch, true) == 0)
                {
                    html.Append("<option selected=\"true\" value=\"" + str + "\">" + str + "</option>");
                }
                else
                {
                    html.Append("<option value=\"" + str + "\">" + str + "</option>");
                }
            }
            return html.ToString();
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

        public static string GenerateSelectObj(string strID, ArrayList arrValue, ArrayList arrText, string strValueSelected, bool hasSelect)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<select id=\"" + strID + "\" name=\"" + strID + "\">");
            if (hasSelect)
                strBuild.Append("<option value=\"\">" + Resources.Translator.Get("All") + "</option>");
            for (int i = 0; i < arrValue.Count; i++)
            {
                if (string.Compare(strValueSelected, arrValue[i].ToString(), true) == 0)
                    strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\" selected>" + arrText[i].ToString() + "</option>");
                else
                    strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\">" + arrText[i].ToString() + "</option>");
            }
            strBuild.Append("</select>");
            return strBuild.ToString();
        }

        public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, int width, bool hasCheckAll, bool hasPageSize)
        {
            string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
            TemplateHandler.Assign(ref strPaging, "select", Resources.Admin.Select);
            TemplateHandler.Assign(ref strPaging, "alltext", Resources.Admin.All);
            TemplateHandler.Assign(ref strPaging, "pagetext", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "Page-text", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "pagesizetext", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "PageSize-text", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "of", Resources.Admin.Of.ToLower());
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
            TemplateHandler.Assign(ref strPaging, "select", Resources.Admin.Select);
            TemplateHandler.Assign(ref strPaging, "alltext", Resources.Admin.All);
            TemplateHandler.Assign(ref strPaging, "pagetext", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "Page-text", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "pagesizetext", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "PageSize-text", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "of", Resources.Admin.Of.ToLower());
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

        public static string NoInfo(int intNumCols)
        {
            string strNoInfo = TemplateHandler.LoadTemplate("NoInfo.html");
            TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Translator.Get("NoInfo"));
            TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
            return strNoInfo;
        }


        public static bool IsCurrencyExisted(string currencyCode)
        {
            foreach (CurrencyObj obj in AppSettings.AffSystemSupportedCurrencies)
            {
                if (string.Compare(obj.Value, currencyCode, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;
            }
            return false;
        }

        public static string CutLongText(string strContent, int numChar)
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
        public static string cutLongText(string val, int maxLength)
        {
            return (val.Length <= maxLength) ? val : "<span title='" + System.Web.HttpUtility.HtmlEncode(val) + "'>" + val.Substring(0, maxLength - 3) + "...</span>";
        }

        public static string DropdownCustomerPage(string id, string selectedTarget, bool hasAll = false)
        {
            DataRowCollection rows = Settings.GetSettingsByType("CustomerPage");
            ArrayList arrVal = new ArrayList();
            foreach (DataRow dr in rows)
            {
                arrVal.Add(CastAide.AsString(dr["SValue"]));
            }
            return AppCommon.GenerateSelectObj(id, arrVal, arrVal, selectedTarget, hasAll);
        }

        /// <summary>
        /// Send Email via exception
        /// </summary>
        /// <param name="ex"></param>
        public static void SendEmail(Exception ex)
        {
            string message = ex.Message +
                        "\r\nSOURCE: " + ex.Source +
                        "\r\nTARGETSITE: " + ex.TargetSite +
                        "\r\nSTACKTRACE: " + ex.StackTrace;
            SendEmail(message);
        }

        public static void SendEmail(string message)
        {
            SendEmail(message, AppSettings.ToMail, AppSettings.MailErrorTitle, AppSettings.MailErrorTitle, AppSettings.BCCMail, AppSettings.CCMail);
        }
        /// <summary>
        /// Send email with detail content
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mailTo"></param>
        /// <param name="subject"></param>
        /// <param name="title"></param>
        /// <param name="mailBcc"></param>
        /// <param name="mailCC"></param>
        public static void SendEmail(string message, string mailTo, string subject, string title, string mailBcc = "", string mailCC = "")
        {
            var mailItem = new Fanex.MailDeliver.Model.MailRecord
            {
                MailTo = mailTo,
                Subject = subject,
                Title = title,
                Body = message, //IsBodyHtml ready
                MailBcc = mailBcc,
                MailCc = mailCC,

            };

            using (Fanex.MailDeliver.MailClient.API.MailDeliver client = new Fanex.MailDeliver.MailClient.API.MailDeliver())
            {
                client.SendMail(mailItem);
            }
        }

        public static void WriteErrorLog(string message, string stacktrace, int siteid)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("message", message);
            hsParam.Add("stacktrace", stacktrace);
            hsParam.Add("siteid", siteid);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("browser", HttpContext.Current.Request.Browser.Browser);
            hsParam.Add("os", HttpContext.Current.Request.Browser.Platform);
            hsParam.Add("sessionid", HttpContext.Current.Session.SessionID);
            try
            {
                DbResult result = Fanex.Affiliate.DataObjects.DbHelper.Execute("Aff_ErrorLogInsert", hsParam, DBType.AFFILIATE);
            }
            catch (Exception)
            { }
        }

        public static XmlDocument ConvertToXML(DataTable table, int fromrow = 0, int torow = 0)
        {

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            if (table == null && table.Rows.Count == 0)
            {
                return doc;
            }
            if (torow == 0)
                torow = table.Rows.Count;



            for (int i = fromrow; i < torow; i++)
            {

                XmlElement transNode = doc.CreateElement("trans");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    transNode.SetAttribute(table.Columns[j].ColumnName.ToLower(), table.Rows[i][j].ToString());
                }
                root.AppendChild(transNode);
            }
            return doc;
        }

        public static string GenListProduct(int intCurrent, int custId, string language, object dtmMonthDate = null)
        {
            StringBuilder strBuild = new StringBuilder();
            DbResult rs = UserAff.ProductSelByAffId(custId, language, dtmMonthDate);
            //Get List Product type
            if (rs.ResultSet.Tables[0].Rows.Count > 1)
            {
                strBuild.Append(Translator.Get("Product") + "&nbsp;&nbsp;&nbsp;" + "<select id=\"slProduct\" name=\"slProduct\" onchange=\"LoadData(1);\">");
                if (intCurrent == 0)
                {
                    strBuild.Append("<option value=\"0\" selected>" + Translator.Get("All") + "</option>");
                }
                else
                    strBuild.Append("<option value=\"0\">" + Translator.Get("All") + "</option>");

                DataTable dtb = rs.ResultSet.Tables[0];
                foreach (DataRow dr in dtb.Rows)
                {
                    int intId = CastAide.AsInt32(dr["ProductID"], 0);
                    if (intId == intCurrent)
                        strBuild.Append("<option value=\"" + intId.ToString() + "\" selected>" + dr["ProductName"].ToString() + "</option>");
                    else
                        strBuild.Append("<option value=\"" + intId.ToString() + "\">" + dr["ProductName"].ToString() + "</option>");
                }
                strBuild.Append("</select>");
            }
            return strBuild.ToString();
        }

        public static string GetDomainEmail(HttpRequest request)
        {
            var siteUrl = request.Url;
            var regex = new Regex(@":\d+");
            var host = regex.Replace(siteUrl.Authority, string.Empty);
            var domainUrl = string.Format("www.{0}", host);

            if (siteUrl.HostNameType == UriHostNameType.Dns)
            {
                var words = host.Split('.');
                if (words.Length > 2)
                {
                    domainUrl = string.Format("www.{0}.{1}", words[1], words[2]);
                }
            }

            return domainUrl;
        }

    }
}

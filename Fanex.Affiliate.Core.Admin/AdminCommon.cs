// -----------------------------------------------------------------------
// <copyright file="Common.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    //  using EddieServices;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Utility;
   

    public class AdminCommon
    {
        public const string System = "Affiliate";
        public const string Approved = "Approved";
        public const string Pending = "Pending";
        public const string Disapproved = "Disapproved";
        public const string Rejected = "Rejected";
        public const string RowBgOdd = "#ffffff";
        public const string RowBgEven = "#f0f0f0";
        public const string RowBgOver = "#FFF5A5";
        public const string RowBgClosed = "#d6d4d2";
        public const string RowBgPending = "#e8ded6";
        private const int TimeCacheUM = 10;


        public static readonly string[] CategoryMediaCasino = new string[] { 
        "Casino Bonus", "Baccarat", "Blackjack" , "Slot", "Roulette", "Sic bo"
    };

        public static readonly string[] CategoryMediaSportsbook = new string[] { 
        "SB Bonus", "Soccer", "Basketball" , "Tennis"
    };

        public static string CreateCurrencyNotificationLine(string currencycode, int multipliedby)
        {
            var number = 2;
            var s = new StringBuilder("<p>").Append(Fanex.Affiliate.Resources.Admin.PleaseInput1For);
            s.Replace("{currencycode}", currencycode);
            s.Replace("{multipliedby}", multipliedby.ToString());
            s.Replace("{value1}", CurrencyUtils.Format(number,currencycode,String.Empty,0));
            s.Replace("{value2}", CurrencyUtils.Format(number * multipliedby, currencycode, String.Empty,0));
            return s.Append("</p>").ToString();
        }

        public static bool CheckHaveParentCategory(string subCategory, out string category)
        {
    
            foreach (string item in CategoryMediaCasino)
            {
                if (item == subCategory)
                {
                    category = "Casino";
                    return true;
                }
            }


            foreach (string item in CategoryMediaSportsbook)
            {
                if (item == subCategory)
                {
                    category = "Sportsbook";
                    return true;
                }
            }

            category = string.Empty;
            return false;
        }

        /*smith*/
        /// <summary>
        /// 0: username not existing
        /// 1: existing in Licensee DB
        /// 3: existing in both
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int GetStatus(string userName)
        {
            /*WriteLog("GetStatus", "APICall", string.Format("username={0}", userName));
            var serviceClient = new WebServiceProviderSoapClient(); 
          
            var result = serviceClient.GetMemberStatus(
                ConfigurationManager.AppSettings.Get("VendorId"),
                userName,
                ConfigurationManager.AppSettings.Get("OperatorId"));
            return int.Parse(result.ChildNodes[1].InnerText);*/
            return 0;
        }
        /*smith*/
        /// <summary>
        /// 0: success
        /// other: fail
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int CreateSystemAccount(string userName)
        {
           /* WriteLog("CreateSystemAccount", "APICall", string.Format("username={0}", userName));

            var serviceClient = new WebServiceProviderSoapClient();
            var result = serviceClient.CreateSystemAccount(
                ConfigurationManager.AppSettings.Get("VendorId"),
                userName,
                ConfigurationManager.AppSettings.Get("OperatorId"));
            return int.Parse(result.ChildNodes[1].InnerText);*/
            return 0;

        }

       
        public static string GetLangChar()
        {
            switch (CastAide.AsString(HttpContext.Current.Session["Language"]))
            {
                case "zh-CN":
                    return "CS";
                case "en-US":
                    return "E";
                default:
                    return "E";
            }
        }
       
       

        public static string GerFullTextLang(object strLang)
        {
            switch (strLang.ToString().ToLower())
            {
                case "en-us":
                    return Resources.Admin.English;
                case "en-gb":
                    return Resources.Admin.EnglishEU;
                case "zh-tw":
                    return Resources.Admin.ChineseTraditional;
                case "zh-cn":
                    return Resources.Admin.ChineseSimplified;
                case "th-th":
                    return Resources.Admin.Thai;
                case "es-es":
                    return Resources.Admin.Spanish;
                case "vi-vn":
                    return Resources.Admin.Vietnamese;
                case "id-id":
                    return Resources.Admin.Indonesian;
                default:
                    return Resources.Admin.English;
            }
        }

       
      
   
      
        public static bool IsValidExtension(string fileName, ref string ext)
        {
            string[] arr = fileName.Split('\\');
            string[] arr1 = arr[arr.Length - 1].Split('.');
            ext = arr1[arr1.Length - 1].ToLower();
            return ext.Equals("gif") == true || ext.Equals("jpg") == true || ext.Equals("png") == true || ext.Equals("jpeg") == true || ext.Equals("swf") == true;
        }
        public static void UploadFile(HttpContext context, string newfolder, string path, ref int errcode, string filename, out string size, ref string strDst)
        {
            size = string.Empty;
            try
            {
                errcode = 0;
                string file = "";
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                    {
                        file = context.Request.Files[i].FileName;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(file))
                {
                    if (Directory.Exists(path + newfolder) == false)
                    {
                        Directory.CreateDirectory(path + newfolder);

                    }
                    

                    //string strDst;
                    //strSrc = context.Request.Files[0].FileName;
                    string Extension = Path.GetExtension(file);
                    if (Extension.ToLower().Equals(".gif") == true || Extension.ToLower().Equals(".jpg") == true || Extension.ToLower().Equals(".png") == true || Extension.ToLower().Equals(".jpeg") == true || Extension.ToLower().Equals(".swf") == true)
                    {
                        for (int i = 0; i < context.Request.Files.Count; i++)
                        {
                            if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                            {
                                if (context.Request.Files[i].ContentLength <= AppSettings.ImageSize)
                                {
                                    int nPos1 = file.LastIndexOf('\\');
                                    int nPos2 = file.LastIndexOf('/');
                                    //strDst = path + file.Substring(Math.Max(nPos1, nPos2) + 1);
                                    strDst = path + newfolder + "\\" + filename;                                    
                                    
                                    //string fullPath = context.Server.MapPath(" ") + "\\" + trailingPath;


                                    context.Request.Files[i].SaveAs(strDst);
                                    if (!Extension.ToLower().Equals(".swf"))
                                    {
                                        Bitmap bitmap = new Bitmap(context.Request.Files[0].InputStream);
                                        size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
                                    }
                                    else
                                    {
                                        SwfInfo swfInfo = new SwfInfo(strDst);
                                        size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
                                    }
                                    break;
                                }
                                else
                                {
                                    errcode = 3;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Extension fail
                        errcode = 2;
                    }
                }
                else
                {
                    // file is null
                    errcode = 1;
                }
            }
            catch
            {
                throw;
            }
        }

      
        public static string GetAlertMsg(int intAlertCode)
        {
            switch (intAlertCode)
            {
                case 0:
                    return Resources.Admin.UpdAffSuccess;
                case 1:
                    return Resources.Admin.PaymentSuccess;
                case 2:
                    return Resources.Admin.AddUserSucc;
                case 3:
                    return Resources.Admin.UpdateUserSucc;
                case 4:
                    return Resources.Admin.DeleteUserSucc;
                case 5:
                    return Resources.Admin.UpdateStaSucc;
                default:
                    return Resources.Admin.UpdAffSuccess;
            }
        }
        public static string FormatNumber(object number)
        {
            var dcNumber = CastAide.AsDecimal(number);
            var format = "<span class='red'>{0}</span>";
            return CurrencyUtils.Format(NumberFormat.DecFormat(dcNumber, 2), AdminSettings.SystemCurrency.Value,format);
            /*
            if (dcNumber >= 0)
            {
                return CurrencyUtils.Format(NumberFormat.DecFormat(dcNumber, 2), AdminSettings.SystemCurrency.Code);
            }
            dcNumber = dcNumber * -1;
            return "-" + AdminSession.SystemCurrency + NumberFormat.DecFormat(dcNumber, 2);*/
        }

        //@Peter
        public static string FormatBalanceNumber(object balance)
        {
         //   return CastAide.AsDouble(balance) >= 0 ? AdminCommon.FormatNumber(balance) : "<span class='red'>" + AdminCommon.FormatNumber(balance) + "</span>";
            var format = "<span class='red'>{0}</span>";
            return CurrencyUtils.Format(NumberFormat.DecFormat(balance, 2), AdminSession.AffCurrency, format);

        }

    
        public static string NoInfo(int intNumCols)
        {
            var strNoInfo = TemplateHandler.LoadTemplate("NoInfo.htm");
            TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Admin.NoInfo);
            TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
            return strNoInfo;
        }
       
        /// <summary>
        /// Create date time selector
        /// </summary>
        /// <param name="txtDateId"></param>
        /// <param name="currentPos"></param>
        /// <param name="triggerId"></param>
        /// <param name="currentDate"></param>
        /// <param name="noJs"></param>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public static string GenCalendarControl(string txtDateId, string currentPos, string triggerId, string currentDate, bool noJs, bool showTime)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDateId) || string.IsNullOrEmpty(triggerId)) return null;
                var js = (noJs ? "" : TemplateHandler.LoadTemplate("CalendarJs.html"));
                var txtClass = showTime ? "txtdate-show-time" : "txtdate-no-time";
                var singleClick = showTime ? "false" : "true";
                TemplateHandler.Assign(ref js, "{CurrentPos}", currentPos);
                return TemplateHandler.LoadHtml("Calendar.html",
                    new string[] { "{txtDate}", "{themepath}", "{triggerId}", "{currentDate}", "{JS}", "{ShowTime}", "{TxtDateCls}", "{SecFormat}", "{IsSingleClick}", "{submit}" },
                    new string[] { txtDateId, currentPos + AppSettings.ThemeDir, triggerId, currentDate, js, showTime.ToString().ToLower(), txtClass, (showTime ? " %H:%M:%S" : ""), singleClick, Resources.Admin.Submit });
            }
            catch { throw; }
        }
        public static string GenCalendarControl(string txtDateId, string currentPos, string triggerId, string currentDate, bool noJs)
        {
            try
            {
                return GenCalendarControl(txtDateId, currentPos, triggerId, currentDate, noJs, false);
            }
            catch { throw; }
        }
        /// <summary>
        /// Create date/time range selector
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="noDayReset"></param>
        /// <param name="fdateId"></param>
        /// <param name="tdateId"></param>
        /// <param name="fdate"></param>
        /// <param name="tdate"></param>
        /// <param name="noJs"></param>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public static string CreateDateRangeSelector(string currentPos, int noDayReset, string fdateId, string tdateId, DateTime fdate, DateTime tdate, bool noJs, bool showTime, bool blShowToday)
        {
            try
            {
                var dateRangeTpl = TemplateHandler.LoadTemplate("DateRangeSelect.html");
                TemplateHandler.Assign(ref dateRangeTpl, "{CurrentPos}", currentPos);
                TemplateHandler.Assign(ref dateRangeTpl, "{DayNoReset}", noDayReset.ToString());
                TemplateHandler.Assign(ref dateRangeTpl, "{Fdate}", fdateId);
                TemplateHandler.Assign(ref dateRangeTpl, "{Tdate}", tdateId);
                TemplateHandler.Assign(ref dateRangeTpl, "{submit}", Resources.Admin.Submit);
                TemplateHandler.Assign(ref dateRangeTpl, "{today_now}", Resources.Admin.Today);
                TemplateHandler.Assign(ref dateRangeTpl, "{yesterday}", Resources.Admin.Yesterday);
                TemplateHandler.Assign(ref dateRangeTpl, "fdatetext", Resources.Admin.From);
                TemplateHandler.Assign(ref dateRangeTpl, "tdatetext", Resources.Admin.To);
                var strShowToday = blShowToday ? "" : "hidden";
                TemplateHandler.Assign(ref dateRangeTpl, "{hide}", strShowToday);
                var fdateStr = showTime ? DateFormat.ShortDateFormat(fdate) : DateFormat.ShortDateFormat(fdate);
                var tdateStr = showTime ? DateFormat.ShortDateFormat(tdate) : DateFormat.ShortDateFormat(tdate);

                TemplateHandler.Assign(ref dateRangeTpl, "{FromDateCal}", GenCalendarControl(fdateId, currentPos, fdateId + "_tg", fdateStr, noJs, showTime));
                TemplateHandler.Assign(ref dateRangeTpl, "{ToDateCal}", GenCalendarControl(tdateId, currentPos, tdateId + "_tg", tdateStr, true, showTime));
                return dateRangeTpl;
            }
            catch { throw; }
        }


        public static string CreateDateRangeSelector(string currentPos, int noDayReset, string fdateId, string tdateId, DateTime fdate, DateTime tdate, bool blShowToday)
        {
            try
            {
                return CreateDateRangeSelector(currentPos, noDayReset, fdateId, tdateId, fdate, tdate, false, false, blShowToday);
            }
            catch { throw; }
        }
        public static string CreateDateRangeSelector(string currentPos, int noDayReset, string fdateId, string tdateId)
        {
            try
            {
                return CreateDateRangeSelector(currentPos, noDayReset, fdateId, tdateId, DateTime.Now.AddDays(-noDayReset), DateTime.Now, false);
            }
            catch { throw; }
        }
        public static string CreateDateRangeSelector(string currentPos, string fdateId, string tdateId)
        {
            try
            {
                return CreateDateRangeSelector(currentPos, 1, fdateId, tdateId);
            }
            catch { throw; }
        }
        public static string GerStatus(object strStatus)
        {
            switch (strStatus.ToString().ToLower())
            {
                case "pending":
                    return Resources.Admin.Proccessing;
                case "completed":
                    return Resources.Admin.Approved;
                case "cancelled":
                    return Resources.Admin.Cancelled;
                case "approved":
                    return Resources.Admin.Approved;
                case "rejected":
                    return Resources.Admin.Rejected;
                default:
                    return strStatus.ToString();
            }
        }
        public static string GenNoInformation(string colspan)
        {
            string noDataTpl = TemplateHandler.LoadTemplate("NoInformation.htm");
            TemplateHandler.Assign(ref noDataTpl, "{bgcolor-nodata}", "bgcolor-nodata");
            TemplateHandler.Assign(ref noDataTpl, "{tdno}", colspan);
            TemplateHandler.Assign(ref noDataTpl, "{nodata}", Resources.Admin.NoInfo);
            return noDataTpl;
        }
        public static void InitAccessPermission()
        {
            Dictionary<string, string> pages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                DataTable dtb = UserControl.GetFunctionInfo().ResultSet.Tables[0];
                foreach (DataRow dr in dtb.Rows)
                {
                    if (CastAide.AsInt32(dr["SubFuncCount"]) == 0)
                    {
                        string strFilesName = dr["FileNames"].ToString();
                        string strFuncName = dr["FuncName"].ToString();
                        string[] strListFiles = strFilesName.Split(';');
                        for (int i = 0; i < strListFiles.Length; i++)
                        {
                            try
                            {
                                pages.Add(strListFiles[i].Trim(), strFuncName.Trim() + "-1");
                            }
                            catch { }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            //Application["AccessPermission"] = pages;
            HttpContext.Current.Cache.Remove("AccessPermission");
            HttpContext.Current.Cache.Add("AccessPermission", pages, null, Cache.NoAbsoluteExpiration, TimeSpan.Zero, CacheItemPriority.Normal, null);
        }

        //Get permission host per function
        public static bool isHost(int CustId, string funcName)
        {
            bool isTrue = false;
            DbResult result = Affiliate.SelPermission(CustId, funcName);
            if (CastAide.AsInt32(result.Outputs["errcode"]) == 0)
                isTrue = true;
            return isTrue;
        }

        // get Error message according to error code
        public static string GetErrorText(string key)
        {
            key = "Err" + key;
            object text = HttpContext.GetGlobalResourceObject("Lang", key);
            if (text == null) return key;
            return text.ToString();
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

            TemplateHandler.Assign(ref strPaging, "of", Resources.Admin.Of.ToLower());
            TemplateHandler.Assign(ref strPaging, "Page-text", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "PageSize-text", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "All-text", Resources.Admin.All);
            TemplateHandler.Assign(ref strPaging, "None-text", Resources.Admin.None);
            TemplateHandler.Assign(ref strPaging, "Select", Resources.Admin.Select);
            
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

            TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
            TemplateHandler.Assign(ref strPaging, "page", currPage);
            TemplateHandler.Assign(ref strPaging, "all", numPages);
            TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
            TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
            return strPaging;
        }

        /// <summary>
        /// Generate paging header and footer include button at top of table VinhTrinh@20090612
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nChars"></param>
        /// <returns></returns>
        public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, String width, bool hasCheckAll, bool hasPageSize)
        {
            string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
            TemplateHandler.Assign(ref strPaging, "of", Resources.Admin.Of.ToLower());
            TemplateHandler.Assign(ref strPaging, "Page-text", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "PageSize-text", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "All-text", Resources.Admin.All);
            TemplateHandler.Assign(ref strPaging, "None-text", Resources.Admin.None);
            TemplateHandler.Assign(ref strPaging, "Select", Resources.Admin.Select);
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

            TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
            TemplateHandler.Assign(ref strPaging, "page", currPage);
            TemplateHandler.Assign(ref strPaging, "all", numPages);
            TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
            TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
            return strPaging;
        }
        public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, string[] txtButton, string[] txtButtonEvent, int width, bool hasCheckAll, bool hasPageSize, bool hasButton, bool hasExcel)
        {
            string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
            TemplateHandler.Assign(ref strPaging, "of", Resources.Admin.Of.ToLower());
            TemplateHandler.Assign(ref strPaging, "Page-text", Resources.Admin.Page);
            TemplateHandler.Assign(ref strPaging, "PageSize-text", Resources.Admin.PageSize);
            TemplateHandler.Assign(ref strPaging, "All-text", Resources.Admin.All);
            TemplateHandler.Assign(ref strPaging, "None-text", Resources.Admin.None);
            TemplateHandler.Assign(ref strPaging, "Select", Resources.Admin.Select);
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
      
        /// <summary>
        /// Common function to generate dropdown list in media gallery page by Peter 20090328
        /// </summary>
        /// <param name="dv">DataView of returned DataTable</param>
        /// <param name="type">type in ("Category","Language","Currency","Type","Size"))</param>
        /// <param name="activeValue">current selected value in the dropdownlist. string.Empty mean All</param>
        /// <returns>string for dropdownlist</returns>
        public static string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll, bool isOnchange)
        {
            if (type.Contains("MediaSize") || type.Contains("SupportedLanguages"))
            {
                dv.RowFilter = "type='" + type + "'";
            }
            else
            {
                dv.RowFilter = "type='" + type + "' and lang='" + CastAide.AsString(HttpContext.Current.Session["Language"]) + "'";
            }
            StringBuilder result = new StringBuilder();
            string opt = TemplateHandler.LoadTemplate(isOnchange ? "Option2.htm" : "Option1.htm");
            TemplateHandler.Assign(ref opt, "id", id);
            if (isOnchange)
            {
                TemplateHandler.Assign(ref opt, "onchange", id + "Onchange();");
            }

            string temp = TemplateHandler.CopyBlock(opt, "optionList");
            string tempopt = temp;
            if (isGenAll)
            {
                TemplateHandler.Assign(ref tempopt, "Optvalue", "");
                TemplateHandler.Assign(ref tempopt, "Optname", "All");
                if (string.IsNullOrEmpty(activeValue))
                {
                    TemplateHandler.Assign(ref tempopt, "selected", "selected");
                }
                result.Append(tempopt);
            }
            DataTable dt = dv.ToTable();
            foreach (DataRow dr in dt.Rows)
            {
                tempopt = temp;
                TemplateHandler.Assign(ref tempopt, "Optvalue", dr["key"].ToString());
                TemplateHandler.Assign(ref tempopt, "Optname", dr["value"].ToString());

                if (string.Compare(activeValue, dr["key"].ToString(), true) == 0)
                {
                    TemplateHandler.Assign(ref tempopt, "selected", "selected");
                }
                else
                {
                    TemplateHandler.Assign(ref tempopt, "selected", "");
                }
                result.Append(tempopt);
            }
            TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
            return opt;
        }
        

        //@VinhTrinh
        public static string GenSubMenu(string[] arrUrl, string[] arrText, int width, int currMenu)
        {
            string strTpl = TemplateHandler.LoadTemplate("SubMenu.htm");
            TemplateHandler.Assign(ref strTpl, "width", width);
            string strMenuItem = TemplateHandler.CopyBlock(strTpl, "menu");
            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < arrText.Length; i++)
            {
                string strTmp = strMenuItem;
                TemplateHandler.Assign(ref strTmp, "url", arrUrl[i]);
                TemplateHandler.Assign(ref strTmp, "text", arrText[i]);
                TemplateHandler.Assign(ref strTmp, "curr", (i == currMenu ? "id=\"current\"" : string.Empty));
                strBuild.AppendLine(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "menu", strBuild.ToString());
            return strTpl;
        }
        /// <summary>
        /// Generate a sub page
        /// </summary>
        /// <param name="currPage">Menu index to active</param>
        /// <param name="paramIndexs">List of pages that have parameters</param>
        /// <param name="paramValues">List of values that have values</param>
        /// <param name="activePages">if we do not pass this param it means all of pages are active.Otherwise we will remove list of menu items not in active menu</param>
        /// <returns></returns>
        public static string GenSubPage(int currPage, string nMembers, string playerDetails, int[] paramIndexs, string[] paramValues, params int[] activeMenus)
        {
            string strTpl = TemplateHandler.LoadTemplate("SubPage.htm");
            TemplateHandler.Assign(ref strTpl, "StatementHistory", Resources.Admin.StatementHistory);
            TemplateHandler.Assign(ref strTpl, "Edit", Resources.Admin.Edit);
            TemplateHandler.Assign(ref strTpl, "Member", Resources.Admin.Member);
            TemplateHandler.Assign(ref strTpl, "Note", Resources.Admin.Note);
            TemplateHandler.Assign(ref strTpl, "PlayersDetail", Resources.Admin.PlayersDetail);
            TemplateHandler.Assign(ref strTpl, "Adjustment", Resources.Admin.Adjustment);
            TemplateHandler.Assign(ref strTpl, "TrackingStatistics", Resources.Admin.TrackingStatistics);
            int i;
            List<int> activeMenuList = new List<int>(activeMenus);
            /* Assign Parameters for page */
            if (paramIndexs != null && paramValues != null)
            {
                for (i = 0; i < paramIndexs.Length; i++)
                {
                    TemplateHandler.Assign(ref strTpl, "param" + paramIndexs[i].ToString(), paramValues[i]);
                }
            }
            /*Activate the current page */
            for (i = 1; i <= 7; i++)
            {
                if (activeMenus.Length == 0 || activeMenuList.Contains(i))
                {
                    TemplateHandler.Assign(ref strTpl, "curr" + i.ToString(), (i == currPage ? "id=\"current\"" : string.Empty));
                    TemplateHandler.Assign(ref strTpl, "param" + i.ToString(), string.Empty);
                }
                else
                {
                    TemplateHandler.RemoveBlock(ref strTpl, "MenuItem" + i.ToString());
                }
            }
            TemplateHandler.Assign(ref strTpl, "nMember", nMembers);
            TemplateHandler.Assign(ref strTpl, "nPlayersDetail", playerDetails);
            return strTpl;
        }
        public static string GenSubPage(int currPage, params int[] activeMenus)
        {
            return AdminCommon.GenSubPage(currPage, string.Empty, string.Empty, null, null, activeMenus);
        }
        public static string GenSubPage(int currPage, string nMembers, string playerDetails, params int[] activeMenus)
        {
            return AdminCommon.GenSubPage(currPage, nMembers, playerDetails, null, null, activeMenus);
        }
        public static string GenSubPage(int currPage, int[] paramIndexs, string[] paramValues, params int[] activeMenus)
        {
            return AdminCommon.GenSubPage(currPage, string.Empty, string.Empty, paramIndexs, paramValues, activeMenus);
        }

        public static string GenListProduct(int intCurrent,bool loadOnChange = true,string caption = "All")
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<select id=\"slProduct\" name=\"slProduct\" " + (loadOnChange? " onchange=\"LoadData(1);\" " : String.Empty) + " >");
            if(caption=="All")
            {
                caption = Translator.Get(caption);
            }
            if (intCurrent == 0)
            {
                strBuild.Append("<option value=\"0\" selected>" + caption + "</option>");
            }
            else
                strBuild.Append("<option value=\"0\">" + Resources.Admin.All + "</option>");
            //Get List Product type
            DbResult rs = Affiliate.ProductSel();
            if (rs.ResultSet.Tables.Count > 0)
            {
                DataTable dtb = rs.ResultSet.Tables[0];
                foreach (DataRow dr in dtb.Rows)
                {
                    int intId = CastAide.AsInt32(dr["ProductID"], 0);
                    if (intId == intCurrent)
                        strBuild.Append("<option value=\"" + intId.ToString() + "\" selected>" + dr["ProductName"].ToString() + "</option>");
                    else
                        strBuild.Append("<option value=\"" + intId.ToString() + "\">" + dr["ProductName"].ToString() + "</option>");
                }
            }
            strBuild.Append("</select>");
            return strBuild.ToString();
        }

        public static string IBCDateTimeFormat(object dateCell, DTFormatType type)
        {
            switch (type)
            {
                case DTFormatType.DT24h:
                    return string.Format("{0:MM/dd/yyyy HH:mm:ss}", dateCell);
                default:
                    return string.Empty;

            }
        }
        public static void SendEmailApproved(int affID, string strPreferLang, string strFname, string strUsername, string strEmail, string strPassword, string strEmailSuffix, int affType = 1)
        {
            string[] strTags = { "fname", "username", "pass", "custid", "mailcontact", "url", "rooturl", "lang", "copyright", "year", "currentmembersitedomain", "background", "topcolor", "bottomcolor" };
            string[] strValue = { strFname, strUsername, strPassword, affID.ToString(), AppSettings.MailContact, affType == 3 ? AdminSettings.UrlAffSite_Kiosk : AdminSettings.UrlAffSite, AdminSettings.UrlAffSiteThemes, strPreferLang, AppCommon.GetCopyRight(strPreferLang), DateTime.Now.Year.ToString(), AppCommon.GetDomainEmail(HttpContext.Current.Request), AdminSettings.EmailBackground, AdminSettings.EmailTopColor, AdminSettings.EmailBottomColor };
            string suffixFolder = (affType == 3 ? "_Kiosk" : string.Empty);

            string FileName = "Email/AffApprove" + suffixFolder + "/Approved_" + strPreferLang + strEmailSuffix + ".htm";
            string FileName2 = "Email/AffApprove" + suffixFolder + "/ApprovalEmail_" + strPreferLang + strEmailSuffix + ".htm";
            string FileName3 = "Email/AffApprove" + suffixFolder + "/WithdrawalEmail_" + strPreferLang + strEmailSuffix + ".htm";

            string strApproved = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
            TemplateHandler.Assign(ref strApproved, "systemname", AppSettings.SystemNameForMail);
            //string strApproved = Fanex.Affiliate.Resources.TextExpressionBuilder.GetEvalData("Email_AffApprove" + strEmailSuffix, strPreferLang).Replace("\r\n", "");
            AssignMultiTags(ref strApproved, strTags, strValue);
            string strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
            //DNAide.Utils.SendMailService.SendMail2(AdminSettings.MailServer, strEmail, AdminSettings.FromMail, AdminSettings.CCMail, AdminSettings.BCCMail, strSubject, strApproved, AdminSettings.MailPwd);
            AppCommon.SendEmail(strApproved, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);

            strApproved = TemplateHandler.LoadHtml(FileName2, strTags, strValue).Replace("\r\n", "");
            TemplateHandler.Assign(ref strApproved, "systemname", AppSettings.SystemNameForMail);
            //strApproved = Fanex.Affiliate.Resources.TextExpressionBuilder.GetEvalData("Email_AffApproveLoginInfo" + strEmailSuffix, strPreferLang.Replace("\r\n", ""));
            AssignMultiTags(ref strApproved, strTags, strValue);
            strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
            //DNAide.Utils.SendMailService.SendMail2(AdminSettings.MailServer, strEmail, AdminSettings.FromMail, AdminSettings.CCMail, AdminSettings.BCCMail, strSubject, strApproved, AdminSettings.MailPwd);
            AppCommon.SendEmail(strApproved, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
            if (affType != 3)//Donot send withdrawal email with kisok affiliate
            {
                strApproved = TemplateHandler.LoadHtml(FileName3, strTags, strValue).Replace("\r\n", "");
                TemplateHandler.Assign(ref strApproved, "systemname", AppSettings.SystemNameForMail);
                //strApproved = Fanex.Affiliate.Resources.TextExpressionBuilder.GetEvalData("Email_AffApproveWithdrawal" + strEmailSuffix, strPreferLang).Replace("\r\n", "");
                AssignMultiTags(ref strApproved, strTags, strValue);
                strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
                //DNAide.Utils.SendMailService.SendMail2(AdminSettings.MailServer, strEmail, AdminSettings.FromMail, AdminSettings.CCMail, AdminSettings.BCCMail, strSubject, strApproved, AdminSettings.MailPwd);
                AppCommon.SendEmail(strApproved, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
            }
        }
        public static void SendEmailDisapproved(string strPreferLang, string strFname, string strEmail, string strEmailSuffix)
        {

            string[] strTags = { "fname", "mailcontact", "rooturl", "lang", "copyright", "year", "currentmembersitedomain", "background", "topcolor", "bottomcolor" };
            string[] strValue = { strFname, AppSettings.MailContact, AdminSettings.UrlAffSiteThemes, strPreferLang, AppCommon.GetCopyRight(strPreferLang), DateTime.Now.Year.ToString(), AppCommon.GetDomainEmail(HttpContext.Current.Request), AdminSettings.EmailBackground, AdminSettings.EmailTopColor, AdminSettings.EmailBottomColor };

            string FileName = "Email/AffDisapprove/Disapproved_" + strPreferLang + strEmailSuffix + ".htm";

            string strDisapproved = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
            TemplateHandler.Assign(ref strDisapproved, "systemname", AppSettings.SystemNameForMail);
            //string strDisapproved = Fanex.Affiliate.Resources.TextExpressionBuilder.GetEvalData("Email_AffDisapprove" + strEmailSuffix, strPreferLang).Replace("\r\n", "");
            AssignMultiTags(ref strDisapproved, strTags, strValue);
            string strSubject = TemplateHandler.ReplaceBlock(ref strDisapproved, "subject", "").Replace("\r\n", "").Trim();
            //DNAide.Utils.SendMailService.SendMail2(AdminSettings.MailServer, strEmail, AdminSettings.FromMail, AdminSettings.CCMail, AdminSettings.BCCMail, strSubject, strDisapproved, AdminSettings.MailPwd);
            AppCommon.SendEmail(strDisapproved, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }

        public static string GetIPCheckerURL(string ip)
        {
            StringBuilder strIPCheckerURL = new StringBuilder(AdminSettings.IPCheckerURL);
            strIPCheckerURL.Replace("{ip-val}", ip);
            return strIPCheckerURL.ToString();
        }
        
        public static string GetWebsiteCheckerURL(string url)
        {
            StringBuilder strWebsiteCheckerURL = new StringBuilder(AdminSettings.WebsiteCheckerURL);
            strWebsiteCheckerURL.Replace("{siteurl-val}", url);
            return strWebsiteCheckerURL.ToString();
        }            
        
        public static void AssignMultiTags(ref string strContent, string[] Tags, string[] Values)
        {
            for (int i = 0; i < Tags.Length; i++)
            {
                TemplateHandler.Assign(ref strContent, Tags[i], Values[i]);
            }
        }

        //public static string GetListSupportedLang(bool hasSelectLang, bool hasAll)
        //{
        //    DbResult rs = Resource.GetResourceLanguages();
        //    StringBuilder strResult = new StringBuilder();
        //    if (hasSelectLang)
        //        strResult.AppendLine("<option value=\"\">Select Language</option>");
        //    else if (hasAll)
        //        strResult.AppendLine("<option value=\"\">All</option>");

        //    if (rs.ResultSet.Tables.Count > 0)
        //    {
        //        foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
        //        {
        //            strResult.AppendLine(string.Format("<option value='{0}'>{1}</option>", dr["Key"], GerFullTextLang(dr["Key"])));
        //        }
        //    }
        //    return strResult.ToString();
        //}
        
        //public static Hashtable GetListSupportedLang()
        //{
        //    DbResult rs = Resource.GetResourceLanguages();
        //    Hashtable htLang = new Hashtable();
        //    if (rs.ResultSet.Tables.Count > 0)
        //    {
        //        IList<string> lsKey = new List<string>();
        //        IList<string> lsValue = new List<string>();

        //        foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
        //        {
        //            lsKey.Add(dr["Key"].ToString());
        //            lsValue.Add(GerFullTextLang(dr["Key"]));
        //        }

        //        htLang["key"] = lsKey;
        //        htLang["value"] = lsValue;
        //    }
        //    return htLang;
        //}

        public static IList<string> GetListSupportedCurrency()
        {            
            var rs = Settings.GetSettingByName("AffiliateCurrencies"); 
            var strSValue = rs.ResultSet.Tables.Count > 0 ? rs.ResultSet.Tables[0].Rows[0]["SValue"].ToString() : "USD";

            if (AdminSettings.LocalTest != "1")//Remove UUS for live production
                strSValue = strSValue.Replace(",UUS", string.Empty);

            return Common.GetStringIList(strSValue, ",");
        }

        public static string FormatStringHtml(string value)
        {
            return HttpUtility.HtmlDecode(value.Replace("\r\n", "<br/>"));
        }

        public static string FormatStringJavascript(string value)
        {
            return HttpUtility.HtmlDecode(value).Replace("<br/>", "\r\n");
        }

        public static void UploadFileWithAutoDetectSize(HttpContext context, string newFolder, string path, string fileName, ref int errcode, out string size, ref string strDst)
        {
            size = string.Empty;
            try
            {
                errcode = 0;
                string file = "";
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                    {
                        file = context.Request.Files[i].FileName;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(file))
                {
                    if (Directory.Exists(path + newFolder) == false)
                    {
                        Directory.CreateDirectory(path + newFolder);

                    }
                    //string strDst;
                    //strSrc = context.Request.Files[0].FileName;
                    string Extension = Path.GetExtension(file);
                    if (Extension.ToLower().Equals(".gif") == true || Extension.ToLower().Equals(".jpg") == true || Extension.ToLower().Equals(".png") == true || Extension.ToLower().Equals(".jpeg") == true || Extension.ToLower().Equals(".swf") == true)
                    {
                        for (int i = 0; i < context.Request.Files.Count; i++)
                        {
                            if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                            {
                                if (context.Request.Files[i].ContentLength <= AppSettings.ImageSize)
                                {
                                    int nPos1 = file.LastIndexOf('\\');
                                    int nPos2 = file.LastIndexOf('/');
                                    //strDst = path + file.Substring(Math.Max(nPos1, nPos2) + 1);
                                    strDst = path + newFolder + fileName;
                                    context.Request.Files[i].SaveAs(strDst);
                                    if (!Extension.ToLower().Equals(".swf"))
                                    {
                                        Bitmap bitmap = new Bitmap(context.Request.Files[i].InputStream);
                                        size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
                                    }
                                    else
                                    {
                                        SwfInfo swfInfo = new SwfInfo(strDst);
                                        size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
                                    }
                                    break;
                                }
                                else
                                {
                                    errcode = 3;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Extension fail
                        errcode = 2;
                    }
                }
                else
                {
                    // file is null
                    errcode = 1;
                }
            }
            catch
            {

                throw;
            }
        }
        
        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
                file.Delete();
        }
    }
}

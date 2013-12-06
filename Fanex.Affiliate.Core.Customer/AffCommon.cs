namespace Fanex.Affiliate.Core.Customer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Text;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.DataObjects.Model;


    /// <summary>
    /// Summary description for Common
    /// </summary>
    public class AffCommon
    {
        public const string Male = "Male";
        public const string Female = "Female";
        private const int TimeCacheUM = 10;//second   

        
        public AffCommon()
        {
            //
            // TODO: Add constructor logic here
            //
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
            switch (Fanex.Affiliate.Core.AppSession.Language.ToLower())
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
        public static string GetLanguageOriginal(string strLangCode)
        {
            switch (strLangCode.ToLower())
            {
                case "en-us": return "English";
                case "eneuro": return "English_Euro";
                case "zh-tw": return "繁體中文";
                case "zh-cn": return "简体中文";
                case "th-th": return "ภาษาไทย";
                case "es-es": return "Español";
                case "vi-vn":
                case "vi": return "Tiếng Việt";
                case "id-id": return "Indonesian";
                default:
                    return "en";
            }
        }

        public static string GenCodeByPreferLang()
        {
            switch (AffSession.PreferLang.ToLower())
            {
                case "en-us":
                    return "en";
                case "en-gb":
                case "en":
                    return "eneuro";
                case "zh-cn":
                    return "cs";
                case "zh-tw":
                    return "ch";
                case "th-th":
                    return "th";
                case "es-es":
                    return "es";
                case "vi-vn":
                    return "vn";
                case "id-id":
                    return "id";
                default:
                    return "en";
            }
        }

        public static string GetBannerLang(string value)
        {

            switch (value.ToLower())
            {
                case "bonus": return Translator.Get("Bonus");
                case "tennis": return Translator.Get("Tennis");
                case "football": return Translator.Get("Football");
                case "poker": return Translator.Get("Poker");
                case "image": return Translator.Get("Image");
                case "flash": return Translator.Get("Flash");
                case "english": return Translator.Get("English");
                case "chinese traditional": return Translator.Get("ChineseTraditio");
                case "chinese simplified": return Translator.Get("ChineseSimplifi");
                case "thai": return Translator.Get("Thai");
                case "spanish": return Translator.Get("Spanish");
                case "indonesian": return Translator.Get("Indonesian");
                case "vietnamese": return Translator.Get("Vietnamese");
                case "english (europe)": return Translator.Get("EnglishEU");
                case "casino": return Translator.Get("Casino");
                case "basketball": return Translator.Get("Basketball");
                case "tennins": return Translator.Get("Tennis");
                case "rmb": return Translator.Get("RMB");
                case "myr": return Translator.Get("MYR");
                case "thb": return Translator.Get("THB");
                case "usd": return Translator.Get("USD");
                case "eur": return Translator.Get("EUR");
                case "gbp": return Translator.Get("GBP");
                case "aud": return Translator.Get("AUD");
                case "chf": return Translator.Get("CHF");
                case "number game": return Translator.Get("NumberGame");
                case "sportsbook": return Translator.Get("Sportbook");
                case "racebook": return Translator.Get("Racebook");
                case "sb bonus": return Translator.Get("SBBonus");
                case "soccer": return Translator.Get("Soccer");
                case "casino bonus": return Translator.Get("CasinoBonus");
                case "baccarat": return Translator.Get("Baccarat");
                case "blackjack": return Translator.Get("Blackjack");
                case "slot": return Translator.Get("Slot");
                case "roulette": return Translator.Get("Roulette");
                case "sic bo": return Translator.Get("Sicbo");
                    
                default:
                    return value;

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
                    return Resources.Translator.Get("Err1");
                case 2:
                    return Resources.Translator.Get("Err2");
                case 3:
                    return Resources.Translator.Get("Err3");
                case 4:
                    return Resources.Translator.Get("Err4");
                case 5:
                    return Resources.Translator.Get("Err5");
                case 101:
                    return Resources.Translator.Get("Err101");
                case 102:
                    return Resources.Translator.Get("Err102");
                case 103:
                    return Resources.Translator.Get("Err103");
                case 104:
                    return Resources.Translator.Get("Err104");
                case 105:
                    return Resources.Translator.Get("Err105");
                case 107:
                    return Resources.Translator.Get("Err107");
                case 111:
                    return Resources.Translator.Get("Err111");
                case 112:
                    return Resources.Translator.Get("Err112");
                case 113:
                    return Resources.Translator.Get("Err113");
                case 114:
                    return Resources.Translator.Get("Err114");
                case 115:
                    return Resources.Translator.Get("Err115");
                case 116:
                    return Resources.Translator.Get("Err116");
                case 100:
                    return Resources.Translator.Get("Err100");
                case 999:
                    return Resources.Translator.Get("Err999");
                case 1001:
                    return Resources.Translator.Get("Err1001");
                case 1002:
                    strErrObj = "txtEmail";
                    return Resources.Translator.Get("Err1002");
                case 1003:
                    strErrObj = "txtBet12UserName";
                    return Resources.Translator.Get("Err1003");
                case 1004:
                    return Resources.Translator.Get("Err1004");
                case 1005:
                    return Resources.Translator.Get("Err1005");
                case 1006:
                    return Resources.Translator.Get("Err1006");
                case 3001:
                    return Resources.Translator.Get("Err3001");
                case 3002:
                    return Resources.Translator.Get("Err3002");
                case 3003:
                    return Resources.Translator.Get("Err3003");
                case 3004:
                    return Resources.Translator.Get("Err3004");
                case 3006:
                    return Resources.Translator.Get("Err3006");
                case 4001:
                    return Resources.Translator.Get("Err4001");
                case 4002:
                    return Resources.Translator.Get("Err4002");
                case 4003:
                    return Resources.Translator.Get("Err4003");
                case 4004:
                    return Resources.Translator.Get("Err4004");
                case 4005:
                    return Resources.Translator.Get("Err4005");
                default:
                    return string.Empty;
            }
        }
        //public static string FormatNumber(object Number)
        //{
        //    decimal dcNumber = CastAide.AsDecimal(Number);
        //    CultureInfo info = new CultureInfo("en-US");
        //    if (dcNumber == 0)
        //        return Fanex.Affiliate.Core.AppSession.Prefix + "0.00";
        //    if (dcNumber >= 0)
        //    {
        //        return Fanex.Affiliate.Core.AppSession.Prefix + string.Format(info, "{0:#,##0.00}", dcNumber); //FormatHandler.FormatNumber20(dcNumber, "en-US");
        //    }
        //    else
        //    {
        //        dcNumber = dcNumber * -1;
        //        return "-" + Fanex.Affiliate.Core.AppSession.Prefix + string.Format(info, "{0:#,##0.00}", dcNumber);
        //    }
        //}
      
        public static string NoInfo(int intNumCols)
        {
            string strNoInfo = TemplateHandler.LoadTemplate("NoInfo.html");
            TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Translator.Get("NoInfo"));
            TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
            return strNoInfo;
        }

        public static string NoInfo2(int intNumCols)
        {
            string strNoInfo = TemplateHandler.LoadTemplate("NoInfo2.html");
            TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Translator.Get("NoInfo"));
            TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
            return strNoInfo;
        }

        public static string GerStatus(object strStatus)
        {
            switch (strStatus.ToString().ToLower())
            {
                case "running":
                case "pending":
                    return Resources.Translator.Get("Proccessing");
                case "completed":
                    return Resources.Translator.Get("Approved");
                case "cancelled":
                    return Resources.Translator.Get("Cancelled");
                case "approved":
                    return Resources.Translator.Get("Approved");
                case "rejected":
                    return Resources.Translator.Get("Rejected");
                case "current":
                    return Resources.Translator.Get("Current");
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
        public static string CutLongText(string strText, int maxLength)
        {
            if (strText.Length <= maxLength)
                return strText;
            if (Fanex.Affiliate.Core.AppSession.Language != Language.ENGLISH)
                return strText.Substring(0, maxLength) + "...";
            int end = strText.LastIndexOf(" ", maxLength);
            return strText.Substring(0, end) + "...";
        }
      
    
        public static string ConvertDateTime(object dtmTime, string slash, bool isFullDate, bool hasTime)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime Tmp1 = Convert.ToDateTime(dtmTime, culture);
            if (Fanex.Affiliate.Core.AppSession.Language != "th-TH")
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
     
        public static string GetCopyRight()
        {
            string text = TextExpressionBuilder.GetEvalData("Copyright");
            return text.Replace("{year}", DateTime.Now.Year.ToString());
        }
       
      

        public static string GenListLink2Page(string id, string targetRequest, bool hasSelect)
        {
            ArrayList arrTarget = new ArrayList();
            arrTarget.Add("sportsbook");
            arrTarget.Add("casino");
            arrTarget.Add("signup");
            arrTarget.Add("promotion");
            
            return AppCommon.GenerateSelectObj(id, arrTarget, arrTarget, targetRequest, true);
        }

        public static string DropdownDomainByAff(int custId, ref string selectedDomain)
        {
            IList<DomainObj> list = Fanex.Affiliate.DataObjects.Domain.SelDomainsByAff(custId);
            if (list.Count > 0 && string.IsNullOrEmpty(selectedDomain))
                selectedDomain = list[0].DomainName;
            ArrayList arrDomain = new ArrayList();
            foreach (var item in list)
            {
                arrDomain.Add(item.DomainName);
            }
            return AppCommon.GenerateSelectObj("slDomain", arrDomain, arrDomain, selectedDomain);
        }

        public static string GetTextLink(string domain, IList<BaseObj> list, string target)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            StringBuilder textLinkList = new StringBuilder();
            string textLink = TemplateHandler.CopyBlock(htmlContent, "TextLinkList");

            foreach (var item in list)
            {
                textLinkList.Append(GenLink(textLink, AffCommon.GetBannerLang(item.Value), AppSettings.LinkFormat,
                    domain, item.Id, target));
            }
            //Default link
            textLinkList.Append(GenLink(textLink, Translator.Get("Default"), AppSettings.LinkFormat, domain, AffSession.PreferLang, target));
            //}           
            return textLinkList.ToString();
        }

        static string GenLink(string textLink, string langText, string link, string domain,
            string linkLang, string target)
        {
            TemplateHandler.Assign(ref textLink, "language_val", langText);
            TemplateHandler.Assign(ref link, "{domain}", domain);
            TemplateHandler.Assign(ref link, "{target}", target);
            TemplateHandler.Assign(ref link, "{custid}", AppSession.CustID.ToString());
            //TemplateHandler.Assign(ref link, "lang", string.Compare(linkLang, "", true) == 0 ? );
            if (string.IsNullOrEmpty(linkLang))
                link = link.Replace("/{lang}", string.Empty);
            else
                TemplateHandler.Assign(ref link, "lang", linkLang);

            TemplateHandler.Assign(ref textLink, "link_val", link);
            return textLink;
        }

        public static string GenListProduct(int AffId, string language, int intCurrent, object dtmMonthDate = null)
        {
            DbResult rs = UserAff.ProductSelByAffId(AffId, language, dtmMonthDate);
            StringBuilder strBuild = new StringBuilder();

            if (rs.ResultSet.Tables[0].Rows.Count >= 1)
            {
                strBuild.Append(Translator.Get("Product") + "&nbsp;&nbsp;&nbsp;");
                strBuild.Append("<select id=\"slProduct\" name=\"slProduct\">");
                if (intCurrent == 0)
                {
                    strBuild.Append("<option value=\"0\" selected>" + Translator.Get("All") + "</option>");
                }
                else
                    strBuild.Append("<option value=\"0\">" + Translator.Get("All") + "</option>");
                //Get List Product type
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
            }
            return strBuild.ToString();
        }
      
    }

  

}

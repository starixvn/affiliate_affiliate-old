/// <summary>
/// Creater: Vinh
/// </summary>
namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Web;
    using System.Collections;
    using System.Data;
    using DNAide.Data;
    using DNAide.Core;
    using DNAide.Utils;
    using System.Configuration;
    using Fanex.Core.Utility;
    using System.Globalization;
    using Fanex.Affiliate.Model;

    public static class UserAff
    {
        public static void CreateUser(string strFName, string strLName, bool Gender, string strEmail, string strPhone, string strMobile, string strAddress, string strCity, string strState, string strPostal, string strCountry, string strLang, ref int intCustId, string strPayment, string strPlayer, string strPreferLang, string strPreferCurrency, string strEmailFormat, string strUrl, string strMoreUrl, string strNetellerId, string strMoneyBookerEmail, string strBankName, string strBankAddress, string strSwiftCode, string strAccName, string strAccNo, string strCheque_FullName, string strCheque_Address, string strCheque_City, string strCheque_State, string strCheque_Postal, string strCheque_Country, string strQuestion, string strAnswer, ref string strUsername, ref int intErrCode, ref string strErrMsg, int affType = 1)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", "0");
            hsParam.Add("afftype", affType);
            //hsParam.Add("username2", HttpContext.Current.Server.HtmlEncode(strNickname));
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(strFName));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(strLName));
            hsParam.Add("gender", Gender);
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(strEmail));
            hsParam.Add("phone", HttpContext.Current.Server.HtmlEncode(strPhone));
            hsParam.Add("mobilephone", HttpContext.Current.Server.HtmlEncode(strMobile));
            //hsParam.Add("masterusername", AppSettings.AffMaster);
            hsParam.Add("address", HttpContext.Current.Server.HtmlEncode(strAddress));
            hsParam.Add("city", HttpContext.Current.Server.HtmlEncode(strCity));
            hsParam.Add("state", HttpContext.Current.Server.HtmlEncode(strState));
            hsParam.Add("postal", HttpContext.Current.Server.HtmlEncode(strPostal));
            hsParam.Add("country", HttpContext.Current.Server.HtmlEncode(strCountry));
            hsParam.Add("affmaster", ConfigurationManager.AppSettings["AffMaster"]);
            hsParam.Add("siteurl", HttpContext.Current.Server.HtmlEncode(strUrl));
            hsParam.Add("moresiteurls", HttpContext.Current.Server.HtmlEncode(strMoreUrl.Trim()));
            hsParam.Add("preferredlanguage", HttpContext.Current.Server.HtmlEncode(strPreferLang));
            hsParam.Add("currency", HttpContext.Current.Server.HtmlEncode(strPreferCurrency));
            if (strEmailFormat == "0" || strEmailFormat == "1")
                hsParam.Add("emailformat", strEmailFormat == "0" ? false : true);
            hsParam.Add("preferredpayment", HttpContext.Current.Server.HtmlEncode(strPayment));
            hsParam.Add("player_username", HttpContext.Current.Server.HtmlEncode(strPlayer));
            hsParam.Add("neteller_accountid", HttpContext.Current.Server.HtmlEncode(strNetellerId));
            hsParam.Add("moneybookers_email", HttpContext.Current.Server.HtmlEncode(strMoneyBookerEmail));
            hsParam.Add("wire_bankname", HttpContext.Current.Server.HtmlEncode(strBankName));
            hsParam.Add("wire_bankaddress", HttpContext.Current.Server.HtmlEncode(strBankAddress));
            hsParam.Add("wire_swiftcode", HttpContext.Current.Server.HtmlEncode(strSwiftCode));
            hsParam.Add("wire_accountname", HttpContext.Current.Server.HtmlEncode(strAccName));
            hsParam.Add("wire_accountno", HttpContext.Current.Server.HtmlEncode(strAccNo));
            hsParam.Add("cheque_fullname", HttpContext.Current.Server.HtmlEncode(strCheque_FullName));
            hsParam.Add("cheque_address", HttpContext.Current.Server.HtmlEncode(strCheque_Address));
            hsParam.Add("cheque_city", HttpContext.Current.Server.HtmlEncode(strCheque_City));
            hsParam.Add("cheque_state", HttpContext.Current.Server.HtmlEncode(strCheque_State));
            hsParam.Add("cheque_postal", HttpContext.Current.Server.HtmlEncode(strCheque_Postal));
            hsParam.Add("cheque_country", HttpContext.Current.Server.HtmlEncode(strCheque_Country));
            hsParam.Add("question", HttpContext.Current.Server.HtmlEncode(strQuestion));
            hsParam.Add("answer", HttpContext.Current.Server.HtmlEncode(strAnswer));
            hsParam.Add("username", HttpContext.Current.Server.HtmlEncode(strUsername));
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("browser", HttpContext.Current.Request.Browser.ToString());
            hsParam.Add("language", HttpContext.Current.Server.HtmlEncode(strLang));           
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);

            DbResult dbr = DbHelper.Execute("Aff_AffiliateIns", hsParam, DBType.AFFILIATE, 600);
            intCustId = CastAide.AsInt32(dbr.Outputs["custid"]);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
            strUsername = dbr.Outputs["username"].ToString();
        }

        public static void UpdateUser(int intAffId, bool Gender, string strEmail, string strAddress, string strLang, string strPreferLang, string strEmailFormat, string strMoreUrl, string strPlayerUserName, string strPayment, string strNetellerId, string strMoneyBookerEmail, string strBankName, string strBankAddress, string strSwiftCode, string strAccName, string strAccNo, string strCheque_FullName, string strCheque_Address, string strCheque_City, string strCheque_State, string strCheque_Postal, string strCheque_Country, ref int intErrCode, ref string strErrMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("userid", intAffId);
            hsParam.Add("gender", Gender);
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(strEmail));
            //hsParam.Add("phone", HttpContext.Current.Server.HtmlEncode(strPhone));
            //hsParam.Add("mobilephone", HttpContext.Current.Server.HtmlEncode(strMobile));
            //hsParam.Add("masterusername", AppSettings.AffMaster);
            hsParam.Add("address", HttpContext.Current.Server.HtmlEncode(strAddress));
            //hsParam.Add("city", HttpContext.Current.Server.HtmlEncode(strCity));
            //hsParam.Add("state", HttpContext.Current.Server.HtmlEncode(strState));
            //hsParam.Add("postal", HttpContext.Current.Server.HtmlEncode(strPostal));
            //hsParam.Add("country", HttpContext.Current.Server.HtmlEncode(strCountry));
           // hsParam.Add("affmaster", AppSettings.AffMaster);
            //hsParam.Add("siteurl", strUrl);
            hsParam.Add("moresiteurls", HttpContext.Current.Server.HtmlEncode(strMoreUrl.Trim()));
            //combine
            hsParam.Add("preferredlanguage", HttpContext.Current.Server.HtmlEncode(strPreferLang));

            if (strEmailFormat == "0" || strEmailFormat == "1")
                hsParam.Add("emailformat", strEmailFormat == "0" ? false : true);

            hsParam.Add("preferredpayment", HttpContext.Current.Server.HtmlEncode(strPayment));
            hsParam.Add("player_username", HttpContext.Current.Server.HtmlEncode(strPlayerUserName));
            hsParam.Add("neteller_accountid", HttpContext.Current.Server.HtmlEncode(strNetellerId));
            hsParam.Add("moneybookers_email", HttpContext.Current.Server.HtmlEncode(strMoneyBookerEmail));
            hsParam.Add("wire_bankname", HttpContext.Current.Server.HtmlEncode(strBankName));
            hsParam.Add("wire_bankaddress", HttpContext.Current.Server.HtmlEncode(strBankAddress));
            hsParam.Add("wire_swiftcode", HttpContext.Current.Server.HtmlEncode(strSwiftCode));
            hsParam.Add("wire_accountname", HttpContext.Current.Server.HtmlEncode(strAccName));
            hsParam.Add("wire_accountno", HttpContext.Current.Server.HtmlEncode(strAccNo));
            hsParam.Add("cheque_fullname", HttpContext.Current.Server.HtmlEncode(strCheque_FullName));
            hsParam.Add("cheque_address", HttpContext.Current.Server.HtmlEncode(strCheque_Address));
            hsParam.Add("cheque_city", HttpContext.Current.Server.HtmlEncode(strCheque_City));
            hsParam.Add("cheque_state", HttpContext.Current.Server.HtmlEncode(strCheque_State));
            hsParam.Add("cheque_postal", HttpContext.Current.Server.HtmlEncode(strCheque_Postal));
            hsParam.Add("cheque_country", HttpContext.Current.Server.HtmlEncode(strCheque_Country));
            //endcombine
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", HttpContext.Current.Server.HtmlEncode(strLang));
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);
            //hsParam.Add("currency", currency);
            DbResult dbr = DbHelper.Execute("Aff_AffiliateUpdInfo", hsParam, DBType.AFFILIATE, 600);
            intAffId = CastAide.AsInt32(dbr.Outputs["affid"]);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
        }
       /* public static DataTable SelAffInfo(int intCustId, ref string strMinCheque, ref string strMinMoneyBooker, ref string strMinNetteller, ref string strMinWire)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", intCustId);
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");            

            DbResult result = DbHelper.Execute("Aff_CustomerSelEditAffiliate", hsParam, DBType.AFFILIATE, 600);
            strMinCheque = NumberFormat.DecFormat(result.Outputs["aff_mincheque"], 0);
            strMinMoneyBooker = NumberFormat.DecFormat(result.Outputs["aff_minmoneybookers"], 0);
            strMinNetteller = NumberFormat.DecFormat(result.Outputs["aff_minneteller"], 0);
            strMinWire = NumberFormat.DecFormat(result.Outputs["aff_minwire"], 0);
            return result.ResultSet.Tables[0];
        }*/
        public static DataTable SelAffInfo(int intCustId, ref string strMinCheque, ref string strMinMoneyBooker, ref string strMinNetteller, ref string strMinWire, ref string strMinPlayer)
        {
            
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");
            hsParam.Add("aff_minplayer", "0");
            DbResult result = DbHelper.Execute("Aff_AffiliateSelEditInfo", hsParam, DBType.AFFILIATE);
            strMinCheque = NumberFormat.DecFormat(result.Outputs["aff_mincheque"], 0);
            strMinMoneyBooker = NumberFormat.DecFormat(result.Outputs["aff_minmoneybookers"], 0);
            strMinNetteller = NumberFormat.DecFormat(result.Outputs["aff_minneteller"], 0);
            strMinWire = NumberFormat.DecFormat(result.Outputs["aff_minwire"], 0);
            strMinPlayer = NumberFormat.DecFormat(result.Outputs["aff_minplayer"], 0);
            return result.ResultSet.Tables[0];
        }

        public static DbResult SelAffOverview(int AffID, DateTime dtmMonthDate)
        {
            /*Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffID);
            hsParam.Add("monthdate", dtmMonthDate);            
            hsParam.Add("totalactiveplayer", 0);
            hsParam.Add("totaldeposit", 0);            
            hsParam.Add("totaldepositup", 0);//vinh add
            hsParam.Add("totalturnover", 0); //vinh add      
            hsParam.Add("totalplayerdepup", 0);//vinh add      
            hsParam.Add("newsignupdep", 0);//vinh add                 
            hsParam.Add("balbroughtforward", 0);            
            hsParam.Add("totalqualifiedmembers", 0); //Dung added
            hsParam.Add("totalturnoverformonth", 0); //Dung added
            return DbHelper.Execute("Aff_CustomerSelSummary", hsParam, DBType.AFFILIATE);*/
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffID);
            hsParam.Add("monthdate", dtmMonthDate);
            hsParam.Add("totalactiveplayer", 0);
            hsParam.Add("balbroughtforward", 0);
            hsParam.Add("totalplayerdepup", 0);//vinh add      
            hsParam.Add("newsignupdep", 0);//vinh add                 
            hsParam.Add("totaldeposit", 0);
            hsParam.Add("totaldepositup", 0);
            hsParam.Add("totalturnover", 0);
            hsParam.Add("totalturnoverup", 0);
            hsParam.Add("totalqualifiedmembers", 0);
            hsParam.Add("totalqualifiedmembershr", 0);
            hsParam.Add("totalwinlostup", 0);
            hsParam.Add("totalwinlost", 0);
            hsParam.Add("newsignup", 0);
            hsParam.Add("totalplayer", 0);
            return DbHelper.Execute("Aff_StatisticsSel", hsParam, DBType.AFFILIATE);
        }

        public static DbResult SelAffOverview_DEPOSIT(int intCustId, DateTime dtmMonthDate)
        {
            Hashtable p = new Hashtable();
            p.Add("affiliateid", intCustId);
            p.Add("monthdate", dtmMonthDate);
            p.Add("newsignup", 0);
            p.Add("totalplayer", 0);
            return DbHelper.Execute("DEP_AffiliateSelSummary", p, DBType.AFFILIATE);
        }

        public static DataTable SelAffBannerType()
        {
            return DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE).ResultSet.Tables[0];
        }
        public static DbResult SelAffBanner(string category, string language, string imageType, string size,int currency, int pageIndex, int pageSize)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (currency!=0)
            {
                hsParam.Add("currency", currency);
            }
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSel", hsParam, DBType.AFFILIATE);
        }
        public static void ChangePassword(int intCustId, string strNewPass, string strOldPass, int intUserId, string strLang, ref int intErrCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            hsParam.Add("affpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strNewPass.Trim()));
            hsParam.Add("oldpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strOldPass.Trim()));
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", strLang);
            hsParam.Add("errcode", "0");
            DbResult result = DbHelper.Execute("Aff_AffiliateUpdPwd", hsParam, DBType.AFFILIATE); 
            intErrCode = CastAide.AsInt32(result.Outputs["errcode"]);
        }
        public static DbResult SelAffEarningDaily(int intCustId, int productId, object dtmMonthDate = null, object fromDate = null, object toDate = null)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo info = new CultureInfo("en-US");
            hsParam.Add("affid", intCustId);
            hsParam.Add("productid", productId);
            hsParam.Add("monthdate", dtmMonthDate);
            if(fromDate!=null)
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, info));
            if(toDate!=null)
            hsParam.Add("todate", CastAide.AsDateTime(toDate, info));
            hsParam.Add("affcurrency", "UUS");
            return DbHelper.Execute("Aff_EarningSelDaily", hsParam, DBType.AFFILIATE) ;
        }
        //Vinh@20090908
        public static DbResult ProductSel()
        {
            return DbHelper.Execute("Aff_ProductSel", null, DBType.AFFILIATE);
        }
        //Vinh@20130418
        public static DbResult ProductSelByAffId(int affId, string language, object dtmMonthDate = null)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affId);
            hsParam.Add("language", affId);
            hsParam.Add("monthdate", dtmMonthDate);
            return DbHelper.Execute("Aff_ProductSelByAffID", hsParam, DBType.AFFILIATE);
        }

        //Vinh@20110224
        public static bool IsAvailableNickname(string Nickname)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("isavailable", false);
            hsParam.Add("nickname", Nickname);
            DbResult result = DbHelper.Execute("aff_checknickname", hsParam, DBType.AFFILIATE);
            return CastAide.AsBoolean(result.Outputs["isavailable"], false);
        }      

    }
}

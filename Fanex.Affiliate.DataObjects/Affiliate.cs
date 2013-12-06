// -----------------------------------------------------------------------
// <copyright file="Affiliate.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using DNAide.Data;
    using System.Data;
    using System.Collections;
    using System.Web;   
    using DNAide.Utils;
    using DNAide.Core;
    using System.Configuration;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;

    public class Affiliate
    {

        public static void UpdateAffType(int affid, int afftype,ref int errCode, ref string errMsg, string lang = "en-US")
        {
            
            var hsParam = new Hashtable { 
                { "affid",affid},
                {"afftype",afftype},
                {"updatedby" , HttpContext.Current.Session["CustID"]},
                {"ip", Utilities.GetIPAddressOfClient()}, 
                {"language", lang}
            };

            var result = DbHelper.Execute("Aff_AffInfo_UpdAffType", hsParam, DBType.AFFILIATE);
            errCode = CastAide.AsInt32(result.Outputs["errcode"]);
            errMsg = result.Outputs["errmsg"].ToString();
        }

        public static DataRow GetDetail(int affId)
        {
            var hsParam = new Hashtable { { "affid", affId } };
            var table = DbHelper.Execute("Aff_AffSelDetail", hsParam, DBType.AFFILIATE).ResultSet.Tables[0];
            if (table.Rows.Count > 0)
                return table.Rows[0];
            return null;
        }
        public static DataTable AffInfo(int affId)
        {
            var hsParam = new Hashtable { { "affid", affId } };
            return DbHelper.Execute("Aff_AffiliateInfoSel", hsParam, DBType.AFFILIATE).ResultSet.Tables[0];
        }
        public static string UnLockedOut(string affList, string ip, string lang = "en-US")
        {
            var hsParam = new Hashtable { { "userid",  HttpContext.Current.Session["CustID"] }, { "affidlist", affList }, { "ip", ip }, {"language", lang} };
            var data = DbHelper.Execute("Aff_AffiliateUnlockMultiple", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? true.ToString() : data.Outputs["errmsg"].ToString();
        }
        public static DbResult ListAff(int groupId, string groupName, string username, string email, int custid, string status,int afftype, bool isClosed, bool lockedOut, int curpage, int pagesize,int currency)
        {
            var hsParam = new Hashtable
                              {
                                  {"userid",  HttpContext.Current.Session["CustID"]},
                                  {"status", status},
                                  {"pageindex", curpage},
                                  {"pagesize", pagesize},
                                  {"currency", currency}
                              };
            if (groupId != -1)
            {
                hsParam.Add("groupid", groupId);
            }

            hsParam.Add("groupname", groupName);

            if (!string.IsNullOrEmpty(username))
            {
                hsParam.Add("username", username);
            }
            if (!string.IsNullOrEmpty(email))
            {
                hsParam.Add("email", email);
            }
            if (afftype != -1)
                hsParam.Add("afftype", afftype);
            if (custid != -1)
                hsParam.Add("affid", custid);
            if (isClosed)
                hsParam.Add("isclosed", isClosed);
            if (lockedOut)
                hsParam.Add("islocked", lockedOut);
            return DbHelper.Execute("Aff_AffiliateSel", hsParam, DBType.AFFILIATE);
        }
        public static DbResult ListAffFavorie(int groupID, string groupName,
            string username, string email,
            int custid, string status, int afftype, bool isClosed, bool lockedOut, int curpage, int pagesize, int currency)
        {
            var hsParam = new Hashtable
                              {
                                  {"userid",  HttpContext.Current.Session["CustID"]},
                                  {"status", status},
                                  {"pageindex", curpage},
                                  {"pagesize", pagesize},
                                  {"currency", currency}
                              };
            if (groupID != -1)
            {
                hsParam.Add("groupid", groupID);
            }

            hsParam.Add("groupname", groupName);

            if (!string.IsNullOrEmpty(username))
            {
                hsParam.Add("username", username);
            }
            if (!string.IsNullOrEmpty(email))
            {
                hsParam.Add("email", email);
            }
            if (afftype != -1)
                hsParam.Add("afftype", afftype);
            if (custid != -1)
                hsParam.Add("affid", custid);
            if (isClosed)
                hsParam.Add("isclosed", isClosed);
            if (lockedOut)
                hsParam.Add("islocked", lockedOut);

            return DbHelper.Execute("Aff_AffiliateSelFavorite", hsParam, DBType.AFFILIATE);
        }
        public static DataTable SelAffInfo(int intCustId, ref string strMinCheque, ref string strMinMoneyBooker, ref string strMinNetteller, ref string strMinWire, ref string strMinPlayer)
        {

            var hsParam = new Hashtable
                              {
                                  {"affid", intCustId},
                                  {"aff_minwire", "0"},
                                  {"aff_minneteller", "0"},
                                  {"aff_mincheque", "0"},
                                  {"aff_minmoneybookers", "0"},
                                  {"aff_minplayer", "0"}
                              };
            var result = DbHelper.Execute("Aff_AffiliateSelEditInfo", hsParam, DBType.AFFILIATE);
            strMinCheque = NumberFormat.DecFormat(result.Outputs["aff_mincheque"], 0);
            strMinMoneyBooker = NumberFormat.DecFormat(result.Outputs["aff_minmoneybookers"], 0);
            strMinNetteller = NumberFormat.DecFormat(result.Outputs["aff_minneteller"], 0);
            strMinWire = NumberFormat.DecFormat(result.Outputs["aff_minwire"], 0);
            strMinPlayer = NumberFormat.DecFormat(result.Outputs["aff_minplayer"], 0);
            return result.ResultSet.Tables[0];
        }
        public static DbResult SelAffInfo_LeftPanel(string strUsername)
        {
            var hsParam = new Hashtable { { "username", strUsername } };
            return DbHelper.Execute("Aff_AffiliateSelGeneralInfo", hsParam, DBType.AFFILIATE);
        }
        public static void UpdateUser(int intAffId, 
            int intUserId, 
            string strFname, 
            string strLname, 
            string strGender, 
            string strCountry, 
            string strEmail, 
            string strPhone, 
            string strMobile, 
            string strAddress, 
            string strCity, 
            string strState, 
            string strPostal, 
            string strLang, 
            string strUrl, 
            string strMoreUrl, 
            string strPlayerUserName, 
            string strPayment, 
            string strNetellerId, 
            string strMoneyBookerEmail, 
            string strBankName, 
            string strBankAddress, 
            string strSwiftCode, 
            string strAccName, 
            string strAccNo, 
            string strCheque_FullName, 
            string strCheque_Address, 
            string strCheque_City, 
            string strCheque_State, 
            string strCheque_Postal, 
            string strCheque_Country, 
            string strEmailFormat, 
            int currency,
            string playerCurrency, 
            ref int intErrCode, 
            ref string strErrMsg)
        {
            var hsParam = new Hashtable
                              {
                                  {"affid", intAffId},
                                  {"userid", intUserId},
                                  {"firstname", HttpContext.Current.Server.HtmlEncode(strFname)},
                                  {"lastname", HttpContext.Current.Server.HtmlEncode(strLname)}
                              };
            if (strGender == "0" || strGender == "1")
                hsParam.Add("gender", strGender == "0" ? false : true);

            hsParam.Add("country", HttpContext.Current.Server.HtmlEncode(strCountry));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(strEmail));
            hsParam.Add("phone", HttpContext.Current.Server.HtmlEncode(strPhone));
            hsParam.Add("mobilephone", HttpContext.Current.Server.HtmlEncode(strMobile));
            hsParam.Add("address", HttpContext.Current.Server.HtmlEncode(strAddress));
            hsParam.Add("city", HttpContext.Current.Server.HtmlEncode(strCity));
            hsParam.Add("state", HttpContext.Current.Server.HtmlEncode(strState));
            hsParam.Add("postal", HttpContext.Current.Server.HtmlEncode(strPostal));
            hsParam.Add("siteurl", HttpContext.Current.Server.HtmlEncode(strUrl));
            hsParam.Add("moresiteurls", HttpContext.Current.Server.HtmlEncode(strMoreUrl.Trim()));
            //combine
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
            if (strEmailFormat == "0" || strEmailFormat == "1")
                hsParam.Add("emailformat", strEmailFormat == "0" ? false : true);
            //endcombine
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", strLang);
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);
            hsParam.Add("currency", currency);
            hsParam.Add("playercurrency", playerCurrency);
            var dbr = DbHelper.Execute("Aff_AffiliateUpdInfo", hsParam, DBType.AFFILIATE);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
        }
        public static void ResetPass(int intCustId, int intUserId, string strNewPass, ref int intErrCode, ref string errmsg, int isAdmin)
        {
            var hsParam = new Hashtable();
            DbResult result;
            if (isAdmin == 0)
            {
                hsParam.Add("affid", intCustId);
                hsParam.Add("affpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strNewPass.Trim()));
                hsParam.Add("userid", intUserId);
                hsParam.Add("ip", Utilities.GetIPAddressOfClient());
                hsParam.Add("errcode", "0");
                hsParam.Add("errmsg", "");
                //result = DbHelper.ExecuteCmd("Aff_AffiliateUpdPwd", hsParam, DBType.AFFILIATE);
                result = DbHelper.Execute("Aff_AffiliateUpdPwd", hsParam, DBType.AFFILIATE);
            }
            else
            {
                hsParam.Add("userid", intUserId);
                hsParam.Add("custid", intCustId);
                hsParam.Add("userpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strNewPass.Trim()));
                hsParam.Add("ip", Utilities.GetIPAddressOfClient());
                hsParam.Add("errcode", "0");
                hsParam.Add("errmsg", "");
                result = DbHelper.Execute("Aff_AdminUpdPwd", hsParam, DBType.AFFILIATE);
            }
            intErrCode = CastAide.AsInt32(result.Outputs["errcode"]);
            errmsg = result.Outputs["errmsg"].ToString();
        }
        //@Peter
        //Statistics
        public static DbResult SelStatistics(int AffID, DateTime dtmMonthDate)
        {
            var hsParam = new Hashtable
                              {
                                  {"affid", AffID},
                                  {"monthdate", dtmMonthDate},
                                  {"totalactiveplayer", 0},
                                  {"totaldeposit", 0},
                                  {"totaldepositup", 0},
                                  {"totalturnover", 0},
                                  {"totalturnoverup", 0},
                                  {"totalplayerdepup", 0},
                                  {"newsignupdep", 0},
                                  //{"totalqualifiedmembers", 0},
                                  //{"totalqualifiedmembershr", 0},
                                  {"totalwinlostup", 0},
                                  {"totalwinlost", 0},
                                  {"totaluniqueclicks", 0},
                                  {"totalclicks", 0},
                                  {"newsignup", 0},
                                  {"totalplayer", 0}
                              };
            return DbHelper.Execute("Aff_StatisticsSel", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh
        //Statistics
        public static DbResult SelBalanceBF(int affiliateID)
        {
            var hsParam = new Hashtable { { "affid", affiliateID }, { "balbroughtforward", 0 } };
            return DbHelper.Execute("Aff_CustomerSelBalBF", hsParam, DBType.AFFILIATE);
        }
        public static DbResult SelAffOverview_DEPOSIT(int intCustId, DateTime dtmMonthDate)
        {
            var p = new Hashtable
                        {
                            {"affiliateid", intCustId},
                            {"monthdate", dtmMonthDate},
                            {"newsignup", 0},
                            {"totalplayer", 0}
                        };
            //p.Add("newsignupdep", 0);
            //p.Add("totalplayerdep", 0);
            return DbHelper.Execute("DEP_AffiliateSelSummary", p, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        public static DbResult SelAffEarningDaily(int intAffID, int productId, DateTime dtmMonthDate)
        {
            var hsParam = new Hashtable
                              {
                                  {"affid", intAffID},
                                  {"productid", productId},
                                  {"monthdate", dtmMonthDate},
                                  {"affcurrency", "UUS"}
                              };
            return DbHelper.Execute("Aff_EarningSelDaily", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        public static DbResult SelAffEarningDetail(int intAffId, int productId, DateTime dtmMonthDate)
        {
            var hsParam = new Hashtable
                              {
                                  {"affid", intAffId},
                                  {"productid", productId},
                                  {"monthdate", dtmMonthDate},
                                  {"affcurrency", "UUS"}
                              };
            return DbHelper.Execute("Aff_EarningSelDetail", hsParam, DBType.AFFILIATE);
        }
        //@Vinh
        public static DbResult SelMemberReportByAff(int intCustId, DateTime dtmFDate, DateTime dtmTDate, int affType)
        {
            var hsParam = new Hashtable { { "affid", intCustId }, { "fromdate", dtmFDate }, { "todate", dtmTDate } };
            if (affType >= 0)
                hsParam.Add("afftype", affType);
            return DbHelper.Execute("Aff_ReportDetailSelByAff", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh
        public static DbResult SelMemberReport(DateTime dtmFDate, DateTime dtmTDate, int affType)
        {
            var hsParam = new Hashtable { { "fromdate", dtmFDate }, { "todate", dtmTDate } };
            if (affType >= 0)
                hsParam.Add("afftype", affType);
            return DbHelper.Execute("Aff_ReportDetailSelAll", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@Vinh
        public static DbResult SelAffiliateReport(string strPreferLang, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll, int affType, int currency)
        {
            var hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(strPreferLang))
                hsParam.Add("preferredlanguage", strPreferLang);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("currency", currency);
            if (affType > 0)
                hsParam.Add("afftype",affType);
            return DbHelper.Execute("Aff_AffiliateReport", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@Vinh
        public static DbResult SelAffiliateReportByAff(string strAffUsername, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affusername", strAffUsername);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("affid", 0);
            hsParam.Add("affcurrency", "MYR");
            return DbHelper.Execute("Aff_AffiliateReportDetail", hsParam, DBType.AFFILIATE, 180);
        }

        //@Vinh 20091209
        public static DbResult SelAffiliateStatistics(string strUsername, string strPreferLang, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll, int affType, int currency, int pageIndex, int pageSize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("username", strUsername);
            if (!string.IsNullOrEmpty(strPreferLang))
                hsParam.Add("preferredlanguage", strPreferLang);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("totalpages", 0);
            if (affType > 0)
                hsParam.Add("afftype", affType);
            if(currency != 0)
                hsParam.Add("currency", currency);
            return DbHelper.Execute("Aff_AffiliateStatistics", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh 20091209
        public static DbResult SelAffiliateTotalRegister(string strListAffId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("listaffid", strListAffId);
            return DbHelper.Execute("DEP_AffiliateSelTotalRegistered", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@Vinh 20091212
        public static DbResult SelAffiliateStatisticsDetail(int intAffId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll, int PageSize, int PageIndex)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("pagesize", PageSize);
            hsParam.Add("pageindex", PageIndex);
            return DbHelper.Execute("Aff_AffiliateStatisticsDetail", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh 20091212
        public static DbResult SelAffiliateTotalRegisterDetail(int intAffId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            return DbHelper.Execute("DEP_AffiliateSelTotalRegisteredByDate", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }



        //@Vinh
        public static DbResult SelPlayersDetail(int custId, int productId, string strMonth, int pageIndex, int pageSize, ref int totalPages)
        {
            Hashtable p = new Hashtable();
            p.Add("affid", custId);
            p.Add("productid", productId);
            if (!string.IsNullOrEmpty(strMonth)) p.Add("monthdate", CastAide.AsDateTime(strMonth));
            p.Add("pageindex", pageIndex);
            p.Add("pagesize", pageSize); // hardcode pagesize for member/player list
            p.Add("totalpages", 0);
            DbResult rs = DbHelper.Execute("Aff_PlayersSelDetail", p, DBType.AFFILIATE);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);
            return rs;
        }

        //@Vinh
        public static DbResult SelNoteList(int affid, int pageIndex, ref int rows, ref int totalPages, int pageSize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            DbResult rs = DbHelper.Execute("Aff_AdminNoteSel", hsParam, DBType.AFFILIATE);
            rows = CastAide.AsInt32(rs.Outputs["nrows"]);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"]);
            return rs;
        }
        //@Vinh
        public static DbResult SelNoteDetail(int affid, object noteid)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("noteid", noteid);
            DbResult rs = DbHelper.Execute("Aff_AdminNoteSel", hsParam, DBType.AFFILIATE);
            return rs;
        }
        //@Vinh
        public static DbResult AddNote(int affid, int updateby, string strNote, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("noteid", 0);
            hsParam.Add("note", HttpContext.Current.Server.HtmlEncode(strNote));
            hsParam.Add("custid", affid);
            hsParam.Add("updatedby", updateby);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AdminNoteIns", hsParam, DBType.AFFILIATE);
        }

        public static DbResult UpdateNote(int affid, int updateby, int noteid, string strNote, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("noteid", noteid);
            hsParam.Add("note", HttpContext.Current.Server.HtmlEncode(strNote));
            hsParam.Add("userid", affid);
            hsParam.Add("updatedby", updateby);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AdminNoteUpd", hsParam, DBType.AFFILIATE);
        }

        //@Vinh
        public static DbResult DeleteNote(int AffId, int UserId, string strListNote, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffId);
            hsParam.Add("userid", UserId);
            hsParam.Add("listnode", strListNote);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AdminNoteDel", hsParam, DBType.AFFILIATE);
        }

        //@Vinh
        public static DbResult SelPermission(int CustId, string funcName)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", CustId);
            hsParam.Add("funcname", funcName);
            hsParam.Add("system", "Affiliate");
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_Con_CheckPermission", hsParam, DBType.AFFILIATE);
        }

        //@Peter 
        public static DbResult UpdateStatus(int affid, int groupID, string strStatus, string strPassword, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("userid",  HttpContext.Current.Session["CustID"]);
            hsParam.Add("status", HttpContext.Current.Server.HtmlEncode(strStatus));
            if (!string.IsNullOrEmpty(strPassword.Trim()))
                hsParam.Add("password", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strPassword.Trim()));
            if (groupID != -1)
            {
                hsParam.Add("groupid", groupID);
            }
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AffiliateUpdStatus", hsParam, DBType.AFFILIATE);
        }

        //@Peter 
        public static DbResult AffUpdateClosedStatus(int affid, bool isClosed, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("userid",  HttpContext.Current.Session["CustID"]);
            hsParam.Add("closed", isClosed);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AffiliateUpdClosedStatus", hsParam, DBType.AFFILIATE);
        }
        public static DbResult UpdateLanguage(int affID, string strLang, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affID);
            hsParam.Add("userid",  HttpContext.Current.Session["CustID"]);
            hsParam.Add("preferredlanguage", HttpContext.Current.Server.HtmlEncode(strLang));
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_PreferLangUpd", hsParam, DBType.AFFILIATE);
        }
        //Vinh@20090512
        public static int Adjustment(decimal Amt, int CustId, int UserId, string Desc, ref Int64 intTransId, ref string strErrorMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("amount", Amt);
            hsParam.Add("affid", CustId);
            hsParam.Add("adminid", UserId);
            hsParam.Add("transdesc", HttpContext.Current.Server.HtmlEncode(Desc));
            hsParam.Add("transid", 0);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");

            DbResult result = DbHelper.Execute("Aff_AdjustmentIns", hsParam, DBType.AFFILIATE, 600);
            intTransId = CastAide.AsInt32(result.Outputs["transid"]);
            //dtmWinlostDate = CastAide.AsDateTime(result.Outputs["winlostdate"]);
            strErrorMsg = result.Outputs["errmsg"].ToString();
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
        //Vinh@20090512
        public static DbResult SelAffUsename()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affmaster", ConfigurationManager.AppSettings["AffMaster"]);
            return DbHelper.Execute("Aff_CustomerSelAffUsename", hsParam, DBType.AFFILIATE);
        }
        //Vinh@20090627
        public static DbResult AffFavoriteIns(int intAffId, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("adminid",  HttpContext.Current.Session["CustID"]);
            hsParam.Add("affid", intAffId);
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            return DbHelper.Execute("Aff_FavoriteIns", hsParam, DBType.AFFILIATE);
        }
        //Vinh@20090627
        public static DbResult AffFavoriteDel(int intAffId, string lang = "en-US")
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("adminid",  HttpContext.Current.Session["CustID"]);
            hsParam.Add("affid", intAffId);
            hsParam.Add("language", lang);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            return DbHelper.Execute("Aff_FavoriteDel", hsParam, DBType.AFFILIATE);
        }
        //Vinh@20090908
        public static DbResult ProductSel()
        {
            return DbHelper.Execute("Aff_ProductSel", null, DBType.AFFILIATE);
        }

        //Bryan@20091212
        public static DbResult SelAffiliateQMReport(int productid, string strUsername,int afftype, string strPreferLang, int month, int year, int currency, int intPage, int intPageSize)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(strUsername) && String.Compare(strUsername, ("[" + Resources.Translator.Get("All") + "]"), true) != 0)
                hsParam.Add("affname", strUsername);
            if (!string.IsNullOrEmpty(strPreferLang))
                hsParam.Add("preferredlanguage", strPreferLang);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("pageindex", intPage);
            hsParam.Add("currency", currency);
            hsParam.Add("productid", productid);
            if (afftype > 0)
                hsParam.Add("afftype", afftype);
            return DbHelper.Execute("Aff_ReportQM", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //Bryan20091212
        public static DbResult SelAffiliateQMReportDetail(int productid,int affid, int month, int year, int intPage, int intPageSize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("productid", productid);
            hsParam.Add("affid", affid);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            hsParam.Add("affcurrency", "UUS");
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("pageindex", intPage);
            return DbHelper.Execute("Aff_ReportQMDetail", hsParam, DBType.AFFILIATE);
        }

        //Pete20110110
        public static DbResult SelAffiliateCommissionReport(string strUsername, string strPreferLang,int afftype, int month, int year, int currency, int intPage, int intPageSize)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(strUsername) && String.Compare(strUsername, ("[" + Resources.Translator.Get("All") + "]"), true) != 0)
                hsParam.Add("username", strUsername);
            if (!string.IsNullOrEmpty(strPreferLang))
                hsParam.Add("preferredlanguage", strPreferLang);
            DateTime d = new DateTime(year, month, 1);
            hsParam.Add("monthdate", d);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("pageindex", intPage);
            hsParam.Add("currency", currency);
            if (afftype > 0)
                hsParam.Add("afftype", afftype);
            return DbHelper.Execute("Aff_AffiliateCommReport", hsParam, DBType.AFFILIATE);
        }
        //Johnny20100109
        public static void Update12BetPlayerStatusToDeposit(int AffId, string depositUserName, int ibcCustid, string ibcUserName, out int errcode, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", HttpContext.Current.Server.HtmlEncode(depositUserName));
            hsParam.Add("ibccustid", ibcCustid);
            hsParam.Add("ibcusername", HttpContext.Current.Server.HtmlEncode(ibcUserName));
            hsParam.Add("errorcode", 0);
            hsParam.Add("errormsg", string.Empty);
            DbResult result = DbHelper.Execute("Deposit_MemberIBCDetail", hsParam, DBType.AFFILIATE);
            errcode = CastAide.AsInt32(result.Outputs["errorcode"]);
            errmsg = result.Outputs["errormsg"].ToString();

            //wirite log if the user updating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator",  HttpContext.Current.Session["CustID"]);
                logParam.Add("affid", AffId);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Deposit_MemberIBCDetail");
                logParam.Add("ip", Utilities.GetIPAddressOfClient());
                logParam.Add("refno", refNo);
                logParam.Add("details", "Fix the problem when updating 12Bet player status to deposit DepositCustID=" + ibcCustid);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE);
            }
        }

        //@Johnny, 2010 Jan 14: Check 12Bet Exist in Deposit_Customer table or not
        public static DepositCustomerInfo Get12BetPlayerDepositInfo(string deposit_username)
        {
            DepositCustomerInfo customer = new DepositCustomerInfo();
            customer.DepositUserName = deposit_username;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("deposit_username", customer.DepositUserName);
            DbResult result = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserName", hsParam, DBType.AFFILIATE);
            DataTable tbl = result.ResultSet.Tables[0];
            customer.Success = CastAide.AsInt32(tbl.Rows[0]["success"]);
            customer.Address = tbl.Rows[0]["address"].ToString();
            customer.AffiliateId = CastAide.AsInt32(tbl.Rows[0]["affiliateid"]);
            customer.AgentID = CastAide.AsInt32(tbl.Rows[0]["agentid"]);
            // customer.AgentType = CastAide.AsInt32(tbl.Rows[0]["agenttype"]);
            customer.Birthday = CastAide.AsDateTime(tbl.Rows[0]["birthday"]);
            customer.BonusCode = tbl.Rows[0]["bonuscode"].ToString();
            //  customer.CasinoFlag = CastAide.AsInt32(tbl.Rows[0]["casinoflag"]);
            customer.City = tbl.Rows[0]["city"].ToString();
            customer.Country = tbl.Rows[0]["country"].ToString();
            customer.Currency = CastAide.AsInt32(tbl.Rows[0]["currency"]);
            // customer.DepositCustID = CastAide.AsInt32(tbl.Rows[0]["depositcustid"]);
            customer.DepositUserId = CastAide.AsInt32(tbl.Rows[0]["deposituserid"]);
            customer.Email = tbl.Rows[0]["email"].ToString();
            customer.Gender = CastAide.AsInt32(tbl.Rows[0]["gender"]);
            customer.FirstName = tbl.Rows[0]["firstname"].ToString();
            customer.LastName = tbl.Rows[0]["lastname"].ToString();
            customer.OddsType = tbl.Rows[0]["oddstype"].ToString();
            customer.Password = tbl.Rows[0]["password"].ToString();
            customer.Phone = tbl.Rows[0]["phone"].ToString();
            customer.State = tbl.Rows[0]["state"].ToString();
            customer.ZipCode = tbl.Rows[0]["zipcode"].ToString();
            return customer;

        }
        //@Johnny, 2010 Jan 14: Add 1 customer to our DB with username2=depositusername
        public static void Add12BetPlayerAccountToIBC(DepositCustomerInfo customer, out int custid, out string username, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(customer.FirstName));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(customer.LastName));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(customer.Email));
            hsParam.Add("depositusername", HttpContext.Current.Server.HtmlEncode(customer.DepositUserName));
            hsParam.Add("deposituserid", customer.DepositUserId);
            hsParam.Add("password", customer.Password);
            hsParam.Add("oddstype", HttpContext.Current.Server.HtmlEncode(customer.OddsType));
            hsParam.Add("agentid", customer.AgentID);
            hsParam.Add("affiliateid", customer.AffiliateId);
            hsParam.Add("bonuscode", HttpContext.Current.Server.HtmlEncode(customer.BonusCode));
            hsParam.Add("custid", 0);
            hsParam.Add("username", string.Empty);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Dot_Dep_CustomerInsMember_Ag", hsParam, DBType.MAIN);
            username = result.Outputs["username"].ToString();
            custid = CastAide.AsInt32(result.Outputs["custid"]);
            errmsg = result.Outputs["errmsg"].ToString();

            //wirite log if the user creating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator",  HttpContext.Current.Session["CustID"]);
                logParam.Add("affid",  HttpContext.Current.Session["CustID"]);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Dot_Dep_CustomerInsMember_Ag");
                logParam.Add("ip", Utilities.GetIPAddressOfClient());
                logParam.Add("refno", refNo);
                logParam.Add("details", "Add 12Bet player account to IBC DepositCustID=" + customer.DepositUserId);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE);
            }

        }
        //@Johnny, 2010 Jan 14: Update AccStatus = 2 in Deposit_Customer table in Deposit_DB. 
        public static void Update12BetPlayerStatusToDeposit(string depositUserName, int ibcCustid, string ibcUserName, out int errcode, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", HttpContext.Current.Server.HtmlEncode(depositUserName));
            hsParam.Add("ibccustid", ibcCustid);
            hsParam.Add("ibcusername", HttpContext.Current.Server.HtmlEncode(ibcUserName));
            hsParam.Add("errorcode", 0);
            hsParam.Add("errormsg", string.Empty);
            DbResult result = DbHelper.Execute("Deposit_MemberIBCDetail", hsParam, DBType.AFFILIATE);
            errcode = CastAide.AsInt32(result.Outputs["errorcode"]);
            errmsg = result.Outputs["errormsg"].ToString();

            //wirite log if the user updating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator",  HttpContext.Current.Session["CustID"]);
                logParam.Add("affid",  HttpContext.Current.Session["CustID"]);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Deposit_MemberIBCDetail");
                logParam.Add("ip", Utilities.GetIPAddressOfClient());
                logParam.Add("refno", refNo);
                logParam.Add("details", "Update 12Bet player status to deposit DepositCustID=" + ibcCustid);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE);
            }
        }
        //@Johnny, 2010 Jan 14: Check 12betUsername of Affiliate valid or not 
        public static bool Check12BetPlayerAccountActivated(int affiliate, out string depositUserName, out string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("language", HttpContext.Current.Session["Language"]);
            hsParam.Add("affid", affiliate);
            hsParam.Add("12betplayerusername", string.Empty);
            hsParam.Add("result", false);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            DbResult result = DbHelper.Execute("Aff_CustomerCheck12BetPlayerAccountActivated", hsParam, DBType.AFFILIATE);
            errMsg = result.Outputs["errmsg"].ToString();
            depositUserName = result.Outputs["12betplayerusername"].ToString();
            return CastAide.AsBoolean(result.Outputs["result"]);
        }
        //@Smith 20101223
        public static DbResult SignUpMembersReport(int? affId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            if (affId != -1)
                hsParam.Add("affid", affId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            return DbHelper.Execute("DEP_SignUpMembersReport", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Smith 20101223
        public static DbResult ActiveMemberReport(DateTime dtmFDate, DateTime dtmTDate, bool isGetAll, string userName,
            string preferredLanguage, int affType, int pageSize, int pageIndex)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("username", userName);
            if (!string.IsNullOrEmpty(preferredLanguage))
                hsParam.Add("preferredlanguage", preferredLanguage);
            hsParam.Add("affyype", affType);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            DbResult result = DbHelper.Execute("Aff_ActiveMemberReport", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            //totalPages = int.Parse(result.Outputs["TotalPages"].ToString());
            return result;
        }
    }
}

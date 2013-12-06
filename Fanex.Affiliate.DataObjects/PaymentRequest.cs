namespace Fanex.Affiliate.DataObjects
{
    
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;

    public static class PaymentRequest
    {
        public static Hashtable MinWithdraw()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");

            return DbHelper.Execute("Aff_MinAmountSel", hsParam, DBType.AFFILIATE).Outputs;
        }

        public static DbResult GetInfo(int CustId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", CustId);
            hsParam.Add("preferredpayment", string.Empty);            
            hsParam.Add("balbroughtforward", 0);            
            hsParam.Add("minamount", 0);
            return DbHelper.Execute("Aff_PaymentRequestSelInfoByAff", hsParam, DBType.AFFILIATE, 600);
        }
        public static int PayoutRequest(decimal Amt, int AffID, string Player_UserName, string Player_IBCUserName, string Player_Currency, ref Int64 intTransId, ref DateTime dtmWinlostDate, ref string strPreferredLang, ref string strErrorMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("amount", Amt);
            hsParam.Add("affid", AffID);
            //hsParam.Add("player_ibccustid", PlayerCustId);
            hsParam.Add("player_username", Player_UserName);
            hsParam.Add("player_ibcusername", Player_IBCUserName);
            hsParam.Add("player_currency", Player_Currency);
            //hsParam.Add("player_exrate", Player_ExRate);
            hsParam.Add("language",HttpContext.Current.Session["Language"]);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            DbResult result = DbHelper.Execute("Aff_PaymentRequestIns", hsParam, DBType.AFFILIATE, 600);
            intTransId = CastAide.AsInt64(result.Outputs["transid"]);
            dtmWinlostDate = CastAide.AsDateTime(result.Outputs["transdate"]);
            strErrorMsg = result.Outputs["errmsg"].ToString();
            strPreferredLang = result.Outputs["preferredlanguage"].ToString();
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
        public static int CancelRequest(Int64 intTransId,  string Lang, ref string strPreferredLang, ref string strErrorMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("transid", intTransId);            
            hsParam.Add("userid",HttpContext.Current.Session["CustID"]);            
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("language",HttpContext.Current.Session["Language"]);
            hsParam.Add("status", "Cancelled");
            
            DbResult result = DbHelper.Execute("Aff_PaymentRequestUpd", hsParam, DBType.AFFILIATE, 600);
            strPreferredLang = result.Outputs["preferredlanguage"].ToString();
            strErrorMsg = result.Outputs["errmsg"].ToString();
            
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }        
        /// <summary>
        /// Check 12Bet player account for this affiliate is activated or not @Dung2009-02-17
        /// </summary>
        /// <param name="affiliate">affiliate id</param>
        /// <param name="depositUserName">12 bet player username for this affiliate</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>true if exists else return false</returns>
        public static bool Check12BetPlayerAccountActivated(int affiliate, out string depositUserName,out string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("language",HttpContext.Current.Session["Language"]);
            hsParam.Add("affid", affiliate);
            hsParam.Add("12betplayerusername", string.Empty);
            hsParam.Add("result",false);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            DbResult result = DbHelper.Execute("Aff_CustomerCheck12BetPlayerAccountActivated", hsParam, DBType.AFFILIATE);           
            errMsg = result.Outputs["errmsg"].ToString();
            depositUserName = result.Outputs["12betplayerusername"].ToString();
            return CastAide.AsBoolean(result.Outputs["result"]);
        }
        /// <summary>
        /// Get the 12Bet Player Account information for this affiliate has the deposit_username @Dung2009-02-17
        /// </summary>
        /// <param name="deposit_username">12 bet player username</param>
        /// <returns></returns>
        public static DepositCustomerInfo Get12BetPlayerDepositInfo(string deposit_username)
        {
            DepositCustomerInfo customer = new DepositCustomerInfo();
            customer.DepositUserName = deposit_username;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("deposit_username", customer.DepositUserName);
            DbResult result = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserName", hsParam, DBType.AFFILIATE);
            DataTable tbl = result.ResultSet.Tables[0];
            customer.Success= CastAide.AsInt32(tbl.Rows[0]["success"]);
            customer.Address = tbl.Rows[0]["address"].ToString();
            customer.AffiliateId = CastAide.AsInt32(tbl.Rows[0]["affiliateid"]);
            customer.AgentID = CastAide.AsInt32(tbl.Rows[0]["agentid"]);
           // customer.AgentType = CastAide.AsInt32(tbl.Rows[0]["agenttype"]);
            customer.Birthday =CastAide.AsDateTime(tbl.Rows[0]["birthday"]);
            customer.BonusCode = tbl.Rows[0]["bonuscode"].ToString();
          //  customer.CasinoFlag = CastAide.AsInt32(tbl.Rows[0]["casinoflag"]);
            customer.City = tbl.Rows[0]["city"].ToString();
            customer.Country = tbl.Rows[0]["country"].ToString();
            customer.Currency = CastAide.AsInt32(tbl.Rows[0]["currency"]);
           // customer.DepositCustID = CastAide.AsInt32(tbl.Rows[0]["depositcustid"]);
            customer.DepositUserId = CastAide.AsInt32(tbl.Rows[0]["deposituserid"]);
            customer.Email = tbl.Rows[0]["email"].ToString();
            customer.Gender=CastAide.AsInt32(tbl.Rows[0]["gender"]);
            customer.FirstName = tbl.Rows[0]["firstname"].ToString();
            customer.LastName = tbl.Rows[0]["lastname"].ToString();
            customer.OddsType = tbl.Rows[0]["oddstype"].ToString();
            customer.Password = tbl.Rows[0]["password"].ToString();
            customer.Phone = tbl.Rows[0]["phone"].ToString();
            customer.State = tbl.Rows[0]["state"].ToString();
            customer.ZipCode= tbl.Rows[0]["zipcode"].ToString();
            return customer;
            
        }
        /// <summary>
        /// Create a new player username for affiliate in IBC system @Dung2009-02-17
        /// </summary>
        /// <param name="customer">12Bet Player Customer Info</param>
        /// <param name="custid">return custid for new user</param>
        /// <param name="username">return username for new user</param>
        /// <param name="errmsg">return error message if has</param>
        public static void Add12BetPlayerAccountToIBC(DepositCustomerInfo customer,out int custid,out string username,out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(customer.FirstName));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(customer.LastName));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(customer.Email));
            hsParam.Add("depositusername",HttpContext.Current.Server.HtmlEncode(customer.DepositUserName));
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
            DbResult result=DbHelper.Execute("Dot_Dep_CustomerInsMember_Ag", hsParam, DBType.MAIN);
            username = result.Outputs["username"].ToString();
            custid=CastAide.AsInt32(result.Outputs["custid"]);
            errmsg=result.Outputs["errmsg"].ToString();

            //wirite log if the user creating processed successfully
            if(string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator",HttpContext.Current.Session["CustID"]);
                logParam.Add("affid",HttpContext.Current.Session["CustID"]);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Dot_Dep_CustomerInsMember_Ag");
                logParam.Add("ip", Utilities.GetIPAddressOfClient());
                logParam.Add("refno", refNo);
                logParam.Add("details", "Add player account to IBC DepositCustID=" + customer.DepositUserId);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE);
            }
            
        }
        /// <summary>
        ///  Update status of this 12betplayer in deposit db to mark it as active @Dung2009-02-17
        /// </summary>
        /// <param name="depositUserName">Deposit username</param>
        /// <param name="ibcCustid">IBC cust ID for this Deposit username </param>
        /// <param name="ibcUserName">IBC user name for this Deposit username</param>
        /// <param name="errcode">return error code if has -- 0 : No error occur</param>
        /// <param name="errmsg">return string equivalent of this error code --- string.Empty : no error occur </param>
        public static void Update12BetPlayerStatusToDeposit(string depositUserName,int ibcCustid,string ibcUserName,out int errcode,out string errmsg)
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
                logParam.Add("creator",HttpContext.Current.Session["CustID"]);
                logParam.Add("affid",HttpContext.Current.Session["CustID"]);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Deposit_MemberIBCDetail");
                logParam.Add("ip", Utilities.GetIPAddressOfClient());
                logParam.Add("refno", refNo);
                logParam.Add("details", "Update player status to deposit DepositCustID=" + ibcCustid);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE);
            }
        }

        public static DbResult IsExistInIBC(string strDepUsername, string strSystemName)
        {
            //string strDepUsername = string.Empty;
            //string strSystemName = string.Empty;

            //GetAffPlayerInfo(ref strDepUsername, ref strSystemName);
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", strDepUsername);
            hsParam.Add("systemname", strSystemName);
            return DbHelper.Execute("Aff_IBCMemberExistingCheck", hsParam, DBType.ARCHIVE, 600);            
        }
        //public static void GetAffPlayerInfo(ref string DepUsername, ref string SystemName,ref string affcurrency)
        //{
        //    Hashtable hsParam = new Hashtable();
        //    hsParam.Add("affid",HttpContext.Current.Session["CustID"]);
        //    hsParam.Add("depusername", string.Empty);
        //    hsParam.Add("systemname", string.Empty);

        //    DbResult rs = DbHelper.Execute("Aff_PlayerPayoutInfoSel", hsParam, DBType.AFFILIATE, 600);
        //    DepUsername = rs.Outputs["depusername"].ToString();
        //    SystemName = rs.Outputs["systemname"].ToString();
        //    affcurrency = rs.Outputs["affcurrency"].ToString();
        //}

        public static Hashtable InsertMemberTest(string firstName, string lastName, string email,
            string depositUserName, int depositUserId, string password, string oddsType,
            int agentId, int affiliateId, string bonusCode, string country)
        {
            try
            {
                var hsParam = new Hashtable
                                  {
                                      {"firstname", firstName},
                                      {"lastname", lastName},
                                      {"email", email},
                                      {"depositusername", depositUserName},
                                      {"deposituserid", depositUserId},
                                      {"password", password},
                                      {"oddstype", oddsType},
                                      {"agentid", agentId},
                                      {"affiliateid", null},
                                      {"bonuscode", bonusCode},
                                      {"country", country}
                                  };

                var data = DbHelper.Execute("Dot_Dep_CustomerInsMember_Ag", hsParam, DBType.MAIN);
                return data.Outputs;
            }
            catch
            {
                return null;
            }
        }
        public static DbResult PaymentRequestList(string strSearchBy, string strUsername, string strStatus, DateTime dtmFromDate, DateTime dtmToDate, bool isGetAll, int currency, int page, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            if (string.Compare(strSearchBy, "0", true) == 0)//Search by Username
            {
                hsParam.Add("getall", true);
                hsParam.Add("username", strUsername);
            }
            else
            {
                hsParam.Add("getall", isGetAll);
                if (!string.IsNullOrEmpty(strStatus))
                    hsParam.Add("status", strStatus);
                hsParam.Add("fromdate", dtmFromDate);
                hsParam.Add("todate", dtmToDate.AddDays(1).AddMilliseconds(-3));
            }
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", page);
            hsParam.Add("totalpages", 0);
            if(currency>0)
            hsParam.Add("currency", currency);
            return DbHelper.Execute("Aff_PaymentRequestSelList", hsParam, DBType.AFFILIATE);
        }
        public static DataTable PaymentRequestById(Int64 TransId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("transid", TransId);
            return DbHelper.Execute("Aff_PaymentRequestSelDetail", hsParam, DBType.AFFILIATE).ResultSet.Tables[0];
        }
        public static string PaymentUpdate(Int64 intTransId, string strStatus, string strLang, string strAdminRemark, string strAffRemark, long ibcRefNo, long ibcTransId, string ibcUsername, ref string strPreferredLang, ref bool blMailOption)
        {
            Hashtable hsParam = new Hashtable();

            hsParam.Add("transid", intTransId);
            hsParam.Add("status", HttpContext.Current.Server.HtmlEncode(strStatus));
            hsParam.Add("admindesc", HttpContext.Current.Server.HtmlEncode(strAdminRemark));
            hsParam.Add("affdesc", HttpContext.Current.Server.HtmlEncode(strAffRemark));
            hsParam.Add("preferredlanguage", "en-US");
            hsParam.Add("mailoption", blMailOption);
            hsParam.Add("userid", CastAide.AsInt32(HttpContext.Current.Session["CustID"]));
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("refno", 0);
            hsParam.Add("language", strLang);
            hsParam.Add("ibcrefno", ibcRefNo);
            hsParam.Add("ibctransid", ibcTransId);
            //hsParam.Add("playercurrency", String.Empty);
            hsParam.Add("ibcusername", ibcUsername);
            

            var data = DbHelper.Execute("Aff_PaymentRequestUpd", hsParam, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            blMailOption = CastAide.AsBoolean(data.Outputs["mailoption"].ToString());
            strPreferredLang = data.Outputs["preferredlanguage"].ToString();
            return data.Outputs["errmsg"].ToString();

            //return PaymentRequestUpdate(intTransId, strStatus, strLang, strAdminRemark, strAffRemark,
            //      ref strPreferredLang, ref blMailOption);
        }

        public static string TransferMoneyToPlayerAccount(string playerUserName, string balanceUserName,
            string playerCurrency, decimal amount, string systemName, Int64 refId, int userId, string language,
            out Int64 tranId1, out Int64 tranId2)
        {
            Hashtable hsParam = new Hashtable();

            hsParam.Add("playerusername", playerUserName);
            hsParam.Add("balanceusername", balanceUserName);
            hsParam.Add("playercurrency", playerCurrency);
            hsParam.Add("amount", amount);
            hsParam.Add("systemname", systemName);
            hsParam.Add("refid", refId);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("userid", CastAide.AsInt32(HttpContext.Current.Session["CustID"]));
            hsParam.Add("language", language);

            var data = DbHelper.Execute("Aff_TransferMoneyToPlayerAccount", hsParam, DBType.MAIN);

            int result = int.Parse(data.Outputs["errcode"].ToString());
            tranId1 = Int64.Parse(data.Outputs["transid1"].ToString());
            tranId2 = Int64.Parse(data.Outputs["transid2"].ToString());
            return data.Outputs["errmsg"].ToString();
        }

        public static string PaymentRequestUpdate(Int64 intTransId, string strStatus, string strLang,
            string strAdminRemark, string strAffRemark, ref string strPreferredLang, ref bool blMailOption)
        {
            return PaymentUpdate(intTransId, strStatus, strLang, strAdminRemark, strAffRemark, 0, 0, string.Empty, ref strPreferredLang, ref blMailOption);
        }

        public static void PlayerPayoutInfoSel(int affId, ref string playerUsername, 
            ref string systemName, ref string dummyBalAcc, ref string affcurrency)
        {
            Hashtable hsParam = new Hashtable();

            hsParam.Add("affid", affId);
            hsParam.Add("playerusername", playerUsername);
            hsParam.Add("systemname", systemName);
            hsParam.Add("dummybalacc", systemName);
            hsParam.Add("affcurrency", affcurrency);         


            var data = DbHelper.Execute("Aff_PlayerPayoutInfoSel", hsParam, DBType.AFFILIATE);
            playerUsername = data.Outputs["playerusername"].ToString();
            systemName = data.Outputs["systemname"].ToString();
            dummyBalAcc = data.Outputs["dummybalacc"].ToString();
            affcurrency = data.Outputs["affcurrency"].ToString();                        
        }

        public static int GetCustIDByUsername(string playerUsername)
        {
            Hashtable hsParam = new Hashtable();

            hsParam.Add("username", playerUsername);
            hsParam.Add("custid", 0);
            var data = DbHelper.Execute("Aff_DummyAccID", hsParam, DBType.ARCHIVE);
            return CastAide.AsInt32(data.Outputs["custid"]);
        }
    }
}

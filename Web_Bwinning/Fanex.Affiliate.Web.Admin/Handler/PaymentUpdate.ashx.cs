namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;

    public class PaymentUpdate : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            try
            {
                if (!string.IsNullOrEmpty(context.Request["hdApprove"]))
                {
                    string currency = context.Request["hdCurrency"];
                    string strActionType = context.Request["hdApprove"];
                    string strAdminRemark = context.Request["txtAdminRemark"];
                    string strAffRemark = context.Request["txtAffRemark"];
                    string strPreferredLang = "en-US";
                    int AffId = CastAide.AsInt32(context.Request["hdCustId"]);
                   

                    Int64 intTransId = CastAide.AsInt64(context.Request["hdTransId"]);

                    bool blMailOption = false;
                    if (!(strActionType != AdminCommon.Approved || strActionType != AdminCommon.Rejected))
                        throw new Exception(Translator.Get("ApprovedErr"));
                    
                    string strMsgMail = string.Empty;
                    string strFName = context.Request["fname"];
                    string strLName = context.Request["lname"];
                    string strEmail = context.Request["hdEmail"];
                    string strPrefix = context.Request["hdPrefix"];
                    string Amt = context.Request["hdAmt"];
                    string realAmt = context.Request["hdRealAmt"];
                    string errMsg = string.Empty;
                    long ibcRefNo = 0, ibcTransId = 0;
                    
                    if (strActionType == AdminCommon.Approved)
                    {
                        string affcurrency = "MYR";
                        //Get Player User name and dummy account username
                        string playerUsername = string.Empty, systemName = string.Empty, dummyBalAcc = string.Empty;
                        PaymentRequest.PlayerPayoutInfoSel(AffId, ref playerUsername, ref systemName, ref dummyBalAcc,ref affcurrency);

                        //Get dummy account ID by dummy username
                        int balAccCustId = PaymentRequest.GetCustIDByUsername(dummyBalAcc);
                        //Call API to transfer money
                        if (transferToPlayerAcc(systemName, playerUsername, Math.Abs(CastAide.AsDouble(Amt)), balAccCustId, ref ibcRefNo, ref ibcTransId, ref errMsg))
                        {
                            string ibcUsername = string.Empty, playerCurrency = string.Empty;
                            IsExistLicenseeCustInfo(systemName, playerUsername, ref ibcUsername, ref playerCurrency, ref errMsg);//Update IbcUsername for the payout with non-deposited Acc
                            errMsg = PaymentRequest.PaymentUpdate(intTransId, strActionType, AdminCommon.GetLangChar(), strAdminRemark, strAffRemark, ibcRefNo, ibcTransId, ibcUsername, ref strPreferredLang, ref blMailOption);
                        }
                    }
                    else
                    {
                        errMsg = PaymentRequest.PaymentUpdate(intTransId, strActionType, AdminCommon.GetLangChar(), strAdminRemark, strAffRemark, ibcRefNo, ibcTransId, string.Empty, ref strPreferredLang, ref blMailOption);
                    }
                    
                    //Update payment status in affiliate db

                    if (string.IsNullOrEmpty(errMsg))
                    {
                        
                        string strMsg = Translator.Get("TransactionsId") + " " + intTransId + " " + Translator.Get("PaymentSuccess");
                        string strEmailSuffix = AppCommon.EmailSuffix(blMailOption);
                        
                        //Send Email
                        DateTime dtmWLDate = CastAide.AsDateTime(context.Request["wldate"]);
                        if (strActionType == AdminCommon.Approved)
                            strMsgMail = ApprovedMail(intTransId, strFName, strLName, realAmt, dtmWLDate, strEmail, strPreferredLang, strEmailSuffix, currency);
                        else
                            strMsgMail = CancelMail(intTransId, strFName, strLName, realAmt, dtmWLDate, strEmail, strPreferredLang, strEmailSuffix, currency);

                        if (!string.IsNullOrEmpty(strMsgMail))
                            strMsg = strMsgMail;
                        context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess('" + strMsg + "');</script>");
                    }
                    else
                    {
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + errMsg + "\");</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + ex.Message + "\");</script>");
               // Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
               // DNAideLog.Write(ex.Message);
            }
        }

        #region Email Content of Request Payment
        string CancelMail(Int64 intTransId, string strFName, string strLName, string Amt, DateTime dtmWLDate, string strEmail, string strPreferredLang, string strEmailSuffix,string currency)
        {
            try
            {
                string FileName = "Email/PaymentCan/PaymentCanEmail_" + strPreferredLang + strEmailSuffix + ".htm";            
                string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
                //string strNoticeMail = TextExpressionBuilder.GetEvalData("Email_PaymentReject" + strEmailSuffix, strPreferredLang).Replace("\r\n", "");
              /*  string strURLAff = AppSettings.Link;
                if (strPreferredLang.ToLower() == "th-th")
                    strURLAff = AdminSettings.Link_TH;*/

                TemplateHandler.Assign(ref strNoticeMail, "systemname", AppSettings.SystemNameForMail);
                TemplateHandler.Assign(ref strNoticeMail, "rooturl", AdminSettings.UrlAffSite);
                TemplateHandler.Assign(ref strNoticeMail, "lang", strPreferredLang);
                TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
                //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
                TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
                TemplateHandler.Assign(ref strNoticeMail, "amount", Amt);
                TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
                TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
                TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
                TemplateHandler.Assign(ref strNoticeMail, "copyright", AppCommon.GetCopyRight(strPreferredLang));
                string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");
                //SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strNoticeMail, AppSettings.MailPwd);
                AppCommon.SendEmail(strNoticeMail, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }
        #endregion

        #region Email Content of Request Payment
        string ApprovedMail(Int64 intTransId, string strFName, string strLName, string Amt, DateTime dtmWLDate, string strEmail, string strPreferredLang, string strEmailSuffix,string currency)
        {
            try
            {
                string FileName = "Email/PaymentApp/PaymentAppEmail_" + strPreferredLang + strEmailSuffix + ".htm";            
          
                string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
                //string strNoticeMail = TextExpressionBuilder.GetEvalData("Email_PaymentApprove" + strEmailSuffix, strPreferredLang).Replace("\r\n", "");

               /* string strURLAff = AdminSettings.Link;
                if (strPreferredLang.ToLower() == "th-th")
                    strURLAff = AdminSettings.Link_TH;*/
                TemplateHandler.Assign(ref strNoticeMail, "systemname", AppSettings.SystemNameForMail);
                TemplateHandler.Assign(ref strNoticeMail, "rooturl", AdminSettings.UrlAffSite);
                TemplateHandler.Assign(ref strNoticeMail, "lang", strPreferredLang);
                TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
                //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
                TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
                TemplateHandler.Assign(ref strNoticeMail, "amount", Amt);
                TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
                TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
                TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
                TemplateHandler.Assign(ref strNoticeMail, "copyright", AppCommon.GetCopyRight(strPreferredLang));
                string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");
                //SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strNoticeMail, AppSettings.MailPwd);
                AppCommon.SendEmail(strNoticeMail, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }
        #endregion

        bool transferToPlayerAcc(string systemName, string playerUsername, double Amt, int balAccCustId, ref long ibcRefNo, ref long ibcTransId, ref string errMsg)
        {

            var xml = ServiceWrapper.Transfer(systemName, playerUsername, Amt, balAccCustId);
            if (xml == null || xml.IsEmpty)
                return false;

            int errCode = CastAide.AsInt32(xml.Element("ReturnCode").Value);
            if (errCode == 0)
            {
                ibcRefNo = CastAide.AsInt64(xml.Element("IBCRefNo").Value);
                ibcTransId = CastAide.AsInt64(xml.Element("IBCTransId").Value);
                return true;
            }
            errMsg = xml.Element("ReturnMessage").Value;
            return false;
        }

        /// <summary>
        /// Get IBCUsername and player Currency by player's Username
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="userName"></param>
        /// <param name="ibcUsername"></param>
        /// <param name="player_Currency"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private static bool IsExistLicenseeCustInfo(string siteName, string userName, ref string ibcUsername, ref string player_Currency, ref string errMsg)
        {
            var xml = ServiceWrapper.GetLicenseeCustInfo(siteName, userName);
            if (xml == null || xml.IsEmpty)
                return false;

            int errCode = CastAide.AsInt32(xml.Element("ReturnCode").Value);
            errMsg = xml.Element("ReturnMessage").Value;
            if (errCode == 0)
            {
                ibcUsername = CastAide.AsString(xml.Element("IBCusername").Value);
                player_Currency = CastAide.AsString(xml.Element("Currency").Value);
                return true;
            }
            return false;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}

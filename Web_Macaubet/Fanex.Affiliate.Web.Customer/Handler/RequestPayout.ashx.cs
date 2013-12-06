namespace Fanex.Web.Macaubet.Customer.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Core.Culture;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    ////using EddieServices;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using System.Globalization;

    public class RequestPayout : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            string strPreferredLang = "en-US";
            if (AffSession.IsInternalSite)
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Translator.Get("ErrAuthorized") + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }
            try
            {
                int intErrCode = -1;
                decimal Amt = 0;
                Int64 intTransId = 0;
                //string strErrObj = "error_item";
                string strSuccMsg = string.Empty;
                string strErrorMsg = string.Empty;
                DateTime dtmWinlostDate = new DateTime();
                string affCurrency = AppSession.Currency;

                if (!string.IsNullOrEmpty(context.Request.Form["action"]) && context.Request.Form["action"] == "cancel")//Cacel request
                {                    
                    intTransId = CastAide.AsInt64(context.Request.Form["transid"]);
                    intErrCode = Fanex.Affiliate.DataObjects.PaymentRequest.CancelRequest(intTransId, AppSession.Language, ref strPreferredLang, ref strErrorMsg);
                    strSuccMsg = Translator.Get("CancelPayoutSucc");
                    Amt = CastAide.AsDecimal(context.Request.Form["amt"].Replace(',', '.'), 0, AppSettings.NumberFormat);
                }
                else//Submit request
                {
                    
                    Amt = CastAide.AsDecimal(context.Request.Form["txtAmt"],0, AppSettings.NumberFormat);
                    if (Amt <= 0)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"Amount is invalid.\");</script>");
                        return;
                    }
                    else
                    {

                        //Get deposit username and system name
                        string playerUsername = string.Empty, systemName = string.Empty, ibcUsername = string.Empty, player_Currency = string.Empty, dummyAcc = string.Empty;
                        Fanex.Affiliate.DataObjects.PaymentRequest.PlayerPayoutInfoSel(AppSession.CustID, ref playerUsername,
                            ref systemName, ref dummyAcc, ref affCurrency);

                        #region Call API to check whether member is exists
                        if (!IsExistLicenseeCustInfo(systemName, playerUsername, ref ibcUsername, ref player_Currency, ref strErrorMsg))
                        {
                            context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + Common.HTMLEncode(strErrorMsg) + "\");</script>");
                            return;
                        }
                        #endregion

                        //If member info is valid => insert into afftrans and affpayout (wait admin approve)
                        intErrCode = Fanex.Affiliate.DataObjects.PaymentRequest.PayoutRequest(Amt, AppSession.CustID, playerUsername, ibcUsername, player_Currency, ref intTransId, ref dtmWinlostDate, ref strPreferredLang, ref strErrorMsg);
                        strSuccMsg = Translator.Get("PayoutSuccess");
                    }
                }

                if (intErrCode != 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + Common.HTMLEncode(strErrorMsg).Replace("\"", "'") + "\");</script>");
                }
                else
                {
                    #region Return list payment
                    string strBalance = string.Empty;
                    string strMinAmt = string.Empty;
                    string strPaymentMethod = string.Empty;
                    double dBalance = 0, dMinAmt = 0;

                    string strListPayment = Fanex.Affiliate.Core.Customer.GenerateHTML.PaymentRequest.PaymentList(ref strBalance, ref strMinAmt, ref strPaymentMethod, ref dBalance, ref dMinAmt);
                    #endregion

                    #region Send Email
                    if (string.IsNullOrEmpty(strPreferredLang))
                        strPreferredLang = "en-US";
                    string strRequestMail = "Email/PaymentRequest/PaymentRequest_" + strPreferredLang + Common.EmailSuffix(AffSession.MailOption) + ".htm";
                    string strCancelMail = "Email/PaymentCancel/PaymentCancel_" + strPreferredLang + Common.EmailSuffix(AffSession.MailOption) + ".htm";
                    string FileName = strRequestMail;

                    if (!string.IsNullOrEmpty(context.Request["action"]) && context.Request["action"] == "cancel")//Cacel request
                    {
                        dtmWinlostDate = CastAide.AsDateTime(context.Request["date"]);
                        //strContent = TextExpressionBuilder.GetEvalData("Email_PaymentCancel" + AppCommon.EmailSuffix(AffSession.MailOption), strPreferredLang).Replace("\r\n", "");
                        FileName = strCancelMail;
                    }

                    string[] strTags = { "fname", "lname", "amount", "transid", "csdemail", "date", "rooturl", "lang" };
                    string[] strValue = { AppSession.FirstName, AppSession.LastName, CurrencyUtils.Format(Amt, affCurrency, null), intTransId.ToString(), AppSettings.MailContact, DateFormat.ShortDateFormat(dtmWinlostDate), AppSettings.FullDomainName, AffSession.PreferLang };
                    string strContent = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
                    TemplateHandler.Assign(ref strContent, "systemname", AppSettings.SystemNameForMail);
                    string strSubject = TemplateHandler.ReplaceBlock(ref strContent, "subject", "").Replace("\r\n", "").Trim();

                    try
                    {
                        //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, AppSession.Email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strContent, AppSettings.MailPwd);
                        AppCommon.SendEmail(strContent, AppSession.Email, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
                    }
                    catch (Exception exMail)
                    {
                        strSuccMsg = strSuccMsg + "<br />" + exMail.Message;
                    }
                    #endregion


                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess(\"" + Common.HTMLEncode(strSuccMsg) + "\", " + dBalance.ToString().Replace(',', '.') + ", " + dMinAmt.ToString().Replace(',', '.') + ", \"" + strBalance + "\", \"" + strMinAmt + "\", \"" + strListPayment.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\"", "'") + "\");</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.ShowError1('" + Common.HTMLEncode(ex.Message).Replace("\"", "'") + "');</script>");
                DNAide.Web.DNAideLog.Write(ex.Message);
                AppCommon.SendEmail(ex);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /*smith*/
        /// <summary>
        /// 0: username not existing
        /// 1: existing in Licensee DB
        /// 3: existing in both
        /// </summary>
        /// <param name="userName"></param>
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
        /*smith*/
        /// <summary>
        /// 0: success
        /// other: fail
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private static int CreateSystemAccount(string userName)
        {
            //AppCommon.WriteLog("GetStatus", "APICall", string.Format("username={0}", userName));
            //var serviceClient = new WebServiceProviderSoapClient();
            //var result = serviceClient.CreateSystemAccount(
            //    System.Configuration.ConfigurationManager.AppSettings.Get("VendorId"),
            //    userName,
            //    System.Configuration.ConfigurationManager.AppSettings.Get("OperatorId"));
            ////ibcId = int.Parse(result.ChildNodes[0].InnerText);
            //// return int.Parse(result.ChildNodes[1].InnerText);
            return 0;

        }

    }
}

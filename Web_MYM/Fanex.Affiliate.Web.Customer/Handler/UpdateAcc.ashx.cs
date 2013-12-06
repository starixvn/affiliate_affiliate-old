namespace Fanex.Web.MYM.Customer.Handler
{
    using System;
    using System.Web;
    using DNAide.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;

    public class UpdateAcc : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = Fanex.Core.Culture.CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            if (AffSession.IsInternalSite)
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"succMsg\").hide();parent.$(\"divErr\").show();parent.ShowError([\"" + Translator.Get("ErrAuthorized") + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }
            try
            {
                int intErrCode = 0;
                int intCustId = AppSession.CustID;

                //Check 12BetUsername Correct or not
                string strPayment = AffCommon.GetMethodName(context.Request.Form["hdMethod"]);
                string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string errMsg = string.Empty;

                //Request information to update
                bool Gender = (context.Request.Form["rdGender"] == "0" ? false : true);
                //string strCountry = context.Request.Form["selCountry"];
                string strEmail = AppSession.Email;//context.Request.Form["txtEmail"].ToString();
                //string strPhone = context.Request.Form["txtNumber"].ToString();
                //string strMobile = context.Request.Form["txtMobile"].ToString();
                string strAddress = context.Request.Form["txtAddress"].ToString();
                //string strCity = context.Request.Form["txtCity"].ToString();
                //string strPostCode = context.Request.Form["txtPost"].ToString();
                //string strState = context.Request.Form["txtState"].ToString();
                //string strSiteUrl = context.Request.Form["txtSiteUrl"].ToString();
                string strErrMsg = "";
                //string strPayment = Common.GetMethodName(context.Request.Form["hdMethod"]);//slPreferLang
                //string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                //string strURL = context.Request.Form["txtSiteUrl"];
                //string strMoreUrl = context.Request.Form["txtMoreSiteUrl"];
                string strEmailFormat = context.Request.Form["rdFormat"];//Johnny Add to config aff receive email template html or plain text
                string strMoreUrl = context.Request.Form["hdMoreurl"];
                string strNetellerAccId = context.Request.Form["txtNetellerAccId"];
                string strMoneyBookerEmail = context.Request.Form["txtMoneyBookerEmail"];
                string strBankName = context.Request.Form["txtBankName"];
                string strBankAddress = context.Request.Form["txtBankAddress"];
                string strSwiftCode = context.Request.Form["txtBankCode"];
                string strAccName = context.Request.Form["txtBankAccName"];
                string strAccNo = context.Request.Form["txtBankAccNumber"];
                string strCheque_FullName = context.Request.Form["txtChequeFullName"];
                string strCheque_Address = context.Request.Form["txtChequeAddress"];
                string strCheque_City = context.Request.Form["txtChequeCity"];
                string strCheque_State = context.Request.Form["txtChequeState"];
                string strCheque_Postal = context.Request.Form["txtChequePostcode"];
                string strCheque_Country = context.Request.Form["txtChequeCountry"];
                //Remove prefer lang and user English as default
                string strPreferLang = context.Request.Form["slPreferLang"];
                //int currency =CastAide.AsInt32(context.Request.Form["slCurrency"],0);
                //UserAff.UpdateUser(intCustId, Gender, strEmail, strPhone, strMobile, strAddress, strCity, strState, strPostCode, strCountry, AppSettings.Session.Language, strPreferLang, strEmailFormat, strMoreUrl, strPlayerUserName, strPayment, strNetellerAccId, strMoneyBookerEmail, strBankName, strBankAddress, strSwiftCode, strAccName, strAccNo, strCheque_FullName, strCheque_Address, strCheque_City, strCheque_State, strCheque_Postal, strCheque_Country,currency, ref intErrCode, ref strErrMsg);
                UserAff.UpdateUser(intCustId, Gender, strEmail, strAddress, AppSession.Language, strPreferLang, strEmailFormat, strMoreUrl, strPlayerUserName, strPayment, strNetellerAccId, strMoneyBookerEmail, strBankName, strBankAddress, strSwiftCode, strAccName, strAccNo, strCheque_FullName, strCheque_Address, strCheque_City, strCheque_State, strCheque_Postal, strCheque_Country, ref intErrCode, ref strErrMsg);
                if (intErrCode != 0)
                {
                    string strErrObj = "error_item";
                    string strMsg = AffCommon.GetErrMsg(intErrCode, ref strErrObj);
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"succMsg\").hide();parent.$(\"divErr\").show();parent.ShowError([\"" + (string.IsNullOrEmpty(strMsg) ? strErrMsg : strMsg) + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                }
                else
                {
                    AppSession.Email = strEmail;
                    AffSession.MailOption = (strEmailFormat == "1");
                    AffSession.PreferLang = strPreferLang;
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess();</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.$('errMsg').show();parent.$('succMsg').hide();parent.$('divErr').show();parent.ShowError([\"" + ex.Message + "\"], [\"error_item\"], \"error_item\");</script>");
                DNAideLog.Write(ex.Message);
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

    }
}
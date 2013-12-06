namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Web;
    using DNAide.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Utility;
    using System.Xml.Linq;
    using Fanex.Core.Culture;

    public class UpdateAcc : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            CultureHandler.InitializeCulture(HttpContext.Current);
            try
            {
                //Check 12BetUsername Correct or not
                string strPayment = context.Request.Form["slPayment"];
                string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string errMsg = string.Empty;

                /* comment for check in deposite_customer
                if (string.Compare(strPayment, "Player", true) == 0)
                {
                    DepositCustomerInfo customer = DataHandler.Affiliate.Get12BetPlayerDepositInfo(strPlayerUserName);
                    if (customer.Success != 0)
                    { // There was an error when get customer info in Deposit DB 
                        //  strErrorMsg = "There was an error when get customer info in Deposit DB";
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + Fanex.Affiliate.Fanex.Affiliate.Resources.Translator.Get("errDepositDBConnectFailed + "\", \"txtBet12UserName\");</script>");
                        return;
                    }
                }
                */

                int intErrCode = 0;
                string strGender = context.Request.Form["rdGender"];
                string strEmail = context.Request.Form["txtEmail"].ToString();
                string strPhone = context.Request.Form["txtNumber"].ToString();
                string strMobile = context.Request.Form["txtMobile"].ToString();
                string strAddress = context.Request.Form["txtAddress"].ToString();
                string strCity = context.Request.Form["txtCity"].ToString();
                string strPostCode = context.Request.Form["txtPost"].ToString();
                string strState = context.Request.Form["txtState"].ToString();
                string strSiteUrl = context.Request.Form["txtSiteUrl"].ToString();
                string strEmailFormat = context.Request.Form["rdFormat"];
                string strErrMsg = "";
                //string strPayment = Common.GetMethodName(context.Request.Form["hdMethod"]);
                //string strPayment = context.Request.Form["slPayment"];
                //string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string strURL = context.Request.Form["txtSiteUrl"];
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
                //Remove Prefer language and set to default English
                string strUsername = context.Request.Form["hdUsername"];
                string strFname = context.Request.Form["txtFName"];
                string strLname = context.Request.Form["txtLName"];
                string strCountry = context.Request["selCountry"];
                string strStatus = string.Empty;
                int intCustId = CastAide.AsInt32(context.Request.Form["hdCustid"]);
                int intReload = 0;
                int currency = context.Request.Form["slCurrency"] == null ? CastAide.AsInt32(context.Request.Form["hdCurrency"], 0) : CastAide.AsInt32(context.Request.Form["slCurrency"], 0);
                string playerCurrency = GetPlayerCurrency(strPlayerUserName);

                //if (Common.GetStatus(strUsername)==0)
                //{
                //    context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"Sorry! This account doesn't exist in licensee!\", \"error_item\");</script>");
                //    return;
                //}

                Affiliate.UpdateUser(intCustId, Fanex.Affiliate.Core.AppSession.CustID, strFname, strLname, strGender, strCountry, strEmail, strPhone, strMobile, strAddress, strCity, strState, strPostCode, AdminCommon.GetLangChar(), strURL, strMoreUrl, strPlayerUserName, strPayment, strNetellerAccId, strMoneyBookerEmail, strBankName, strBankAddress, strSwiftCode, strAccName, strAccNo, strCheque_FullName, strCheque_Address, strCheque_City, strCheque_State, strCheque_Postal, strCheque_Country, strEmailFormat, currency, playerCurrency, ref intErrCode, ref strErrMsg);
                if (intErrCode != 0)
                {
                    string strErrObj = "error_item";
                    string strMsg = strErrMsg;// Common.GetErrMsg(intErrCode, ref strErrObj);
                    if (intErrCode == 500)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + strErrMsg + "\", \"strErrObj\");</script>");
                    }
                    else
                    {
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + strMsg + "\", \"" + strErrObj + "\");</script>");
                    }
                }
                else
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess(\"" + strUsername + " " + Fanex.Affiliate.Resources.Admin.UpdAffSuccess + "\", \"../Alert.aspx?code=0&user=" + strUsername + "&link=UserInfo/AffiliateList.aspx?status=" + strStatus + "\", " + intReload + ");</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + ex.Message + "\", \"error_item\");</script>");
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
                DNAideLog.Write(ex.Message);
            }
        }

        string GetPlayerCurrency(string playerUsername)
        {
            var item = ServiceWrapper.GetLicenseeCustInfo(Settings.GetSettingValue(name: "SystemName"), playerUsername);
            if (item.Element("ReturnCode").Value == "0")
                return item.Element("Currency").Value;
            else
                return string.Empty;
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
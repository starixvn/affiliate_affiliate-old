namespace Fanex.Web.MYM.Customer.Handler
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Web;
    using Fanex.Core.Culture;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;

    public class SignUp : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string strPreferLang = context.Request.Form["slPreferLang"];          
            context.Response.ContentType = "text/html";
            try
            {
                string strCaptcha = context.Request.Form["txtValidateRegister"];
                if (string.Compare(AppSession.CaptchaCode, strCaptcha, true) != 0)
                {
                    StringBuilder err = new StringBuilder("<script type='text/javascript'>parent.$('validCodeRegister').src='Authorization/LoginCaptcha.ashx?ver=");
                    err.Append(DateTime.Now.ToBinary().ToString());
                    err.Append("'; parent.$('validCode').src=parent.$('validCodeRegister').src;");
                    err.Append("parent.$('txtvalidate').value=parent.$('txtValidateRegister').value;parent.AddError('txtValidateRegister','");
                    err.Append(Translator.Get("ValidationCode"));
                    err.Append("');parent.$('errMsg').show();parent.ShowError(parent.arrError, parent.arrItem, 'error_item');</script>");
                    context.Response.Write(err.ToString());

                    return;
                }

                string strPayment = AffCommon.GetMethodName(context.Request.Form["hdMethod"]);
                string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string errMsg = string.Empty;


                int intErrCode = 0;
                int intCustId = 0;
                bool Gender = (context.Request.Form["rdGender"] == "0" ? false : true);
                string strPreferCurrency = context.Request.Form["slCurrency"];
                //string strNickname = CastAide.AsString(context.Request.Form["txtNickname"], string.Empty).Trim();
                string strFName = context.Request.Form["txtFName"];
                string strLName = context.Request.Form["txtLName"];
                string strEmail = context.Request.Form["txtEmail"];
                string strPhone = context.Request.Form["txtNumber"];
                string strMobile = context.Request.Form["txtMobile"];
                string strAddress = context.Request.Form["txtAddress"];
                string strCity = context.Request.Form["txtCity"];
                string strCountry = context.Request.Form["selCountry"];
                string strPostCode = context.Request.Form["txtPost"];
                string strState = context.Request.Form["txtState"];
                string strSiteUrl = context.Request.Form["txtSiteUrl"];
                string strErrMsg = "";
                //Remove prefer lang and user English as default
                //string strPreferLang = context.Request.Form["slPreferLang"];
                string strEmailFormat = context.Request.Form["rdFormat"];//Johnny Add to config aff receive email template html or plain text
                string strQuestion = context.Request.Form["slSecurityQuestion"];
                string strAnswer = context.Request.Form["txtAnswer"];
                string strURL = context.Request.Form["txtSiteUrl"];
                string strMoreUrl = context.Request.Form["hdMoreurl"];
                //string strMoreUrl = context.Request.Form["txtMoreSiteUrl"];
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
                string strUsername = string.Empty;
                UserAff.CreateUser(strFName, strLName, Gender, strEmail, strPhone, strMobile, strAddress, strCity, strState, strPostCode, strCountry, AppSession.Language, ref intCustId, strPayment, strPlayerUserName, strPreferLang, strPreferCurrency, strEmailFormat, strURL, strMoreUrl, strNetellerAccId, strMoneyBookerEmail, strBankName, strBankAddress, strSwiftCode, strAccName, strAccNo, strCheque_FullName, strCheque_Address, strCheque_City, strCheque_State, strCheque_Postal, strCheque_Country, strQuestion, strAnswer, ref strUsername, ref intErrCode, ref strErrMsg);
                if (intErrCode != 0)
                {
                    //throw new Exception(strErrMsg);
                    string strErrObj = "error_item";
                    string strMsg = AffCommon.GetErrMsg(intErrCode, ref strErrObj);
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + (string.IsNullOrEmpty(strErrMsg) ? strMsg : strErrMsg) + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                }
                else
                {
                    string strCurrLang = AppSession.Language;
                    
                    AppSession.Language = strPreferLang;
                    context = CultureHandler.InitializeCulture(context);

                    string strFileName = string.Empty;
                    
                    if (strPreferLang.ToUpper() == string.Empty)
                        strPreferLang = "en-US";
                    strFileName = "Email/SignupMail/SignupMail_" + strPreferLang + AppCommon.EmailSuffix(strEmailFormat == "0" ? false : true) + ".htm";

                    string strRegisMail = TemplateHandler.LoadTemplate(strFileName).Replace("\r\n", "");
                    //string strRegisMail = TextExpressionBuilder.GetEvalData("Email_Signup" + AppCommon.EmailSuffix(strEmailFormat == "0" ? false : true), strPreferLang).Replace("\r\n", "");
                    TemplateHandler.Assign(ref strRegisMail, "systemname", AppSettings.SystemNameForMail);
                    TemplateHandler.Assign(ref strRegisMail, "fname", strFName);
                    TemplateHandler.Assign(ref strRegisMail, "lname", strLName);
                    TemplateHandler.Assign(ref strRegisMail, "mailadd", AppSettings.MailContact);
                    TemplateHandler.Assign(ref strRegisMail, "rooturl", AppSettings.FullDomainName);
                    TemplateHandler.Assign(ref strRegisMail, "lang", strPreferLang);
                    TemplateHandler.Assign(ref strRegisMail, "copyright", AppCommon.GetCopyRight(strPreferLang));
                    string strRegisSubject = TemplateHandler.ReplaceBlock(ref strRegisMail, "subject", "");
                    //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strRegisSubject, strRegisMail, AppSettings.MailPwd);
                    Fanex.Affiliate.Core.AppCommon.SendEmail(strRegisMail, strEmail, strRegisSubject, strRegisSubject, AppSettings.BCCMail, AppSettings.CCMail);

                    //CSD Email only have english content
                    string strCsdMail = TemplateHandler.LoadTemplate("CsdMail.htm").Replace("\r\n", "");
                    TemplateHandler.Assign(ref strCsdMail, "systemname", AppSettings.SystemNameForMail);
                    TemplateHandler.Assign(ref strCsdMail, "fname", strFName);
                    TemplateHandler.Assign(ref strCsdMail, "lname", strLName);
                    TemplateHandler.Assign(ref strCsdMail, "username", strUsername);
                    TemplateHandler.Assign(ref strCsdMail, "siteurl", strSiteUrl);
                    TemplateHandler.Assign(ref strCsdMail, "mailadd", AppSettings.MailContact);
                    if (strMoreUrl == "")
                        TemplateHandler.ReplaceBlock(ref strCsdMail, "moreurl", "");
                    else
                        TemplateHandler.Assign(ref strCsdMail, "moreurl", strMoreUrl.Replace("\\n", ","));
                    string strCsdSubject = TemplateHandler.ReplaceBlock(ref strCsdMail, "subject", "");
                    
                    
                    //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, AppSettings.ToMail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strCsdSubject, strCsdMail, AppSettings.MailPwd);
                    Fanex.Affiliate.Core.AppCommon.SendEmail(strCsdMail, AppSettings.ToMail, strCsdSubject, strCsdSubject, AppSettings.BCCMail, AppSettings.CCMail);


                    context.Response.Write("<script type='text/javascript'>top.location.href='../SignUpSuccess.aspx';</script>");
                    AppSession.Language = strCurrLang;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.document.getElementById(\"errMsg\").show();parent.ShowError([\"" + ex.Message + "\"], [\"error_item\"], \"error_item\");</script>");
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
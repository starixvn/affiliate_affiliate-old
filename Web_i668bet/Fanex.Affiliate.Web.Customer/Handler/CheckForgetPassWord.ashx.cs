namespace Fanex.Web.i668bet.Customer.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Culture;
    using Fanex.Core.Utility;

    public class CheckForgetPassWord : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            string email = (context.Request["eml"] == null) ? "" : context.Request["eml"].ToString();
            string code = Encryption.RandomString(10);
            string _captcha = context.Request.Form["vcode"];
            int errcode = 0;
            string errmsg = "";

            string username = "";
            string firstname = "";
            string lastname = "";
            string preferredLanguage = string.Empty;
            bool mailOption = false;
            if (string.Compare(AppSession.CaptchaCode, _captcha, true) != 0)
            {
                context.Response.Write(Translator.Get("ValidationCode"));
                return;
            }

            int idcust = Auth.CheckEmailForget(email, code, ref errcode, ref errmsg, ref username, ref firstname, ref lastname, ref preferredLanguage, ref mailOption);
            try
            {
                if (errcode == 0)
                {
                    string strFileName = "Email/ForgetPass/ForgetPass_" + preferredLanguage + Common.EmailSuffix(mailOption) + ".htm";

                    string strMail = TemplateHandler.LoadTemplate(strFileName).Replace("\r\n", "");
                    //string strMail = TextExpressionBuilder.GetEvalData("Email_ForgetPass" + AppCommon.EmailSuffix(mailOption), preferredLanguage).Replace("\r\n", "");
                    TemplateHandler.Assign(ref strMail, "systemname", AppSettings.SystemNameForMail);
                    TemplateHandler.Assign(ref strMail, "fname", firstname);
                    TemplateHandler.Assign(ref strMail, "username", username);
                    TemplateHandler.Assign(ref strMail, "url", AppSettings.FullDomainName + "/Recoverpw.aspx");
                    TemplateHandler.Assign(ref strMail, "idcust", idcust.ToString());
                    TemplateHandler.Assign(ref strMail, "code", code);
                    TemplateHandler.Assign(ref strMail, "mailadd", AppSettings.MailContact);
                    TemplateHandler.Assign(ref strMail, "rooturl", AppSettings.FullDomainName);
                    TemplateHandler.Assign(ref strMail, "lang", preferredLanguage);
                    TemplateHandler.Assign(ref strMail, "copyright", AppCommon.GetCopyRight(preferredLanguage));
                    TemplateHandler.Assign(ref strMail, "rooturl", AppSettings.FullDomainName);
                    string strSubject = TemplateHandler.ReplaceBlock(ref strMail, "subject", "");

                    //bool isok = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strMail, AppSettings.MailPwd);
                    Fanex.Affiliate.Core.AppCommon.SendEmail(strMail, email, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
                    context.Response.Write("Ok");


                    //if (isok == true)
                    //{
                    //    context.Response.Write("Ok");
                    //}
                    //else
                    //{
                    //    context.Response.Write(Translator.Get("CannotSendmail"));
                    //}
                }
                else if (errcode == 1001)
                {
                    context.Response.Write(Translator.Get("ErrmsgNotExistsEmail"));
                }
                else if (errcode == 1009)
                {
                    context.Response.Write(Translator.Get("ErrmsgEmailNotApprove"));
                }
                else if (errcode == 1008)
                {
                    context.Response.Write(Translator.Get("ErrmsgEmailClose"));
                }
                else
                {
                    context.Response.Write(errmsg);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
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
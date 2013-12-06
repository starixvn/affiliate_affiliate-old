namespace Fanex.Web.LiveBet365.Affiliate
{
    using System;
    using DNAide.Utils;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Utility;
    using System.Web;

    public partial class Recoverpw : BasePage
    {
        public string forgetpassword = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                forgetpassword = Translator.Get("ForgetPassword");
                var affid = CastAide.AsInt32(Request["uid"],0);
                string vid = (Request["vid"] != null ? Request["vid"] : "");
                string lang = Request["lang"];

                string errmsg = "";
                string email = "";
                string username = "";
                string firstname = "";
                string lastname = "";
                string strPreferredLanguage = string.Empty;
                bool mailOption = false;
                if (affid != 0 && vid != "")
                {

                    if (lang == null)
                    {
                        var row = Affiliate.GetDetail(affid);
                        if (row != null)
                        {
                            strPreferredLanguage = CastAide.AsString(row["preferredlanguage"], "en-US");
                     
                            lang = strPreferredLanguage;

                            if (AppSession.Language != lang)
                            {
                                AppSession.Language = lang;

                                Context.Response.Redirect(Request.RawUrl);
                            }
                        }
                    }

                    string newpw = Encryption.RandomString(8);
                    int result = Auth.ExecChangePassword(affid, vid, newpw, ref errmsg, ref email, ref username, ref firstname, ref lastname, ref strPreferredLanguage, ref mailOption);

                    
                    if (result == 0)
                    {                       
                        string subject = Translator.Get("SubjectNewPass");
                        //string body=TemplateHandler.LoadTemplate("BodyNewPassForget.htm");
                        string url = "<a href='{url}'>{url}</a>".Replace("{url}", AppSettings.FullDomainName);
                        //TemplateHandler.Assign(ref body, "UsernameText", Translator.Get("Username"));
                        //TemplateHandler.Assign(ref body, "PasswordText", Translator.Get("Password"));
                        //TemplateHandler.Assign(ref body, "Dear", Translator.Get("Dear"));
                        //TemplateHandler.Assign(ref body, "Asyouhaverequested", Translator.Get("Asyouhaverequested"));
                        //TemplateHandler.Assign(ref body, "Howeveryoucanvisit", Translator.Get("Howeveryoucanvisit"));                    
                        //TemplateHandler.Assign(ref body, "Haveagreatday", Translator.Get("Haveagreatday"));
                        //TemplateHandler.Assign(ref body, "BestRegards", Translator.Get("BestRegards"));
                        //TemplateHandler.Assign(ref body, "Theteamat_12Bet", Translator.Get("Theteamat")_12Bet);


                        string strMail = TemplateHandler.LoadTemplate("Email/RecoverPass/RecoverPass_" + strPreferredLanguage + Common.EmailSuffix(mailOption) + ".htm").Replace("\r\n", "");
                        //string strMail = TextExpressionBuilder.GetEvalData("Email_RecoverPass" + AppCommon.EmailSuffix(mailOption), strPreferredLanguage).Replace("\r\n", "");
                        TemplateHandler.Assign(ref strMail, "firstname", firstname);
                        TemplateHandler.Assign(ref strMail, "username", username);
                        TemplateHandler.Assign(ref strMail, "password", newpw);
                        TemplateHandler.Assign(ref strMail, "url", url);
                        TemplateHandler.Assign(ref strMail, "mailadd", AppSettings.MailContact);
                        TemplateHandler.Assign(ref strMail, "rooturl", AppSettings.FullDomainName);
                        TemplateHandler.Assign(ref strMail, "lang", strPreferredLanguage);
                        TemplateHandler.Assign(ref strMail, "copyright", AppCommon.GetCopyRight(strPreferredLanguage));
                        string strSubject = TemplateHandler.ReplaceBlock(ref strMail, "subject", "");
                        //bool isok = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strMail, AppSettings.MailPwd);
                        Fanex.Affiliate.Core.AppCommon.SendEmail(strMail, email, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);

                        //TemplateHandler.Assign(ref body,"username",username);
                        //TemplateHandler.Assign(ref body, "password",newpw);
                        //TemplateHandler.Assign(ref body, "firstname", firstname);
                        //TemplateHandler.Assign(ref body, "lastname", lastname);
                        //TemplateHandler.Assign(ref body, "url", url);
                        //bool isok = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, subject, body, AppSettings.MailPwd);
                        //if (isok)
                        //{
                            string tplSucc = TemplateHandler.LoadTemplate("LoginDetailSucc.htm");
                            TemplateHandler.Assign(ref tplSucc, "Yourlogindetail", Translator.Get("Yourlogindetail"));
                            TemplateHandler.Assign(ref tplSucc, "Pleasecheckyour", Translator.Get("Pleasecheckyour"));
                            TemplateHandler.Assign(ref tplSucc, "Foranyreasonyou", Translator.Get("Foranyreasonyou"));
                            Literal1.Text = tplSucc;
                        //}
                        //else
                        //{
                        //    Literal1.Text = Translator.Get("ErrSendMail");
                        //}
                    }
                    else
                    {
                        Literal1.Text = errmsg;
                    }
                }
                else
                {
                    string err = Translator.Get("ErrMsgForgetPwFail");
                    TemplateHandler.Assign(ref err, "here", AppSettings.FullDomainName + "/ForgetPass.aspx");
                    Literal1.Text = err;
                }
            }
        }
    }
}
namespace Fanex.Web.Aoncash.Customer.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Web;
    using System.Text;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core;

    public class ContactUS : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            if (AffSession.IsInternalSite)
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Translator.Get("ErrAuthorized") + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }
            try
            {
                context.Response.ContentType = "text/html";
                string strCaptcha = context.Request.Form["txtValidateRegister"];
                if (string.Compare(AppSession.CaptchaCode, strCaptcha, true) != 0)
                {
                    StringBuilder err = new StringBuilder("<script type='text/javascript'>parent.$('validCodeRegister').src='Authorization/LoginCaptcha.ashx?ver=");
                    err.Append(DateTime.Now.ToBinary().ToString());
                    err.Append("';if(parent.$('validCode')){ parent.$('validCode').src=parent.$('validCodeRegister').src};parent.$('txtValidateRegister').value='");
                    err.Append(Translator.Get("Validation"));
                    err.Append("';if(parent.$('txtvalidate')) {parent.$('txtvalidate').value=parent.$('txtValidateRegister').value};parent.AddError('txtValidateRegister','");
                    err.Append(Translator.Get("ValidationCode"));
                    err.Append("');parent.$('errMsg').show();parent.ShowError(parent.arrError, parent.arrItem, 'error_item');</script>");
                    context.Response.Write(err.ToString());

                    return;
                }


                string body = TemplateHandler.LoadTemplate("MailContact.htm");
                TemplateHandler.Assign(ref body, "name", context.Request.Form["txtName"]);
                TemplateHandler.Assign(ref body, "email", context.Request.Form["txtEmail"]);
                TemplateHandler.Assign(ref body, "AffID", context.Request.Form["txtAffID"]);
                TemplateHandler.Assign(ref body, "comment", context.Request.Form["txtContent"]);

                string subject = "Aoncash Affiliates - Contact Us - " + context.Request.Form["txtSubject"];
                string result = EmailHistory.InsertEmail(context.Request.Form["txtContent"], context.Request.Form["txtName"], context.Request.Form["txtAffID"], context.Request.Form["txtEmail"], subject);
                if (result != string.Empty)
                {
                    context.Response.Write("<script type ='text/javascript'>parent.SendMailFail('" + result + "');</script>");
                    return;
                }

                Fanex.Affiliate.Core.AppCommon.SendEmail(body, AppSettings.MailContact, subject, subject, AppSettings.BCCMail, AppSettings.CCMail);
               // bool rs = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, AppSettings.ToMail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, subject, body, AppSettings.MailPwd);
                string script = "<script type ='text/javascript'>";

                //if (rs)//send email success
                //{
                    if (AffSession.IsFromHome)
                    {
                        script += "parent.window.location.href = '../ContactUs.aspx?f=h';";//if the page was gone from Home page that show "message sent"
                    }
                    else
                        script += "parent.SendMailSuccess();";
                //}
                //else
                //    script += "parent.SendMailFail('');";

                script += "</script>";
                context.Response.Write(script);
            }
            catch (Exception ex)
            {
                string script = "<script type ='text/javascript'>";

                if (ex.Message.IndexOf("timed out") > 0)
                    script += "parent.SendMailSuccess();";
                else
                    script += "parent.SendMailFail('" + ex.Message.Replace("'", "").Replace(Environment.NewLine, "") + "');";

                script += "</script>";
                context.Response.Write(script);
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
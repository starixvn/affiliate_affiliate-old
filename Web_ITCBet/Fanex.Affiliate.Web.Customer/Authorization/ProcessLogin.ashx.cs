namespace Fanex.Web.ITCBet.Customer.Authorization
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;

    public class ProcessLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            string _s = string.Empty;
            try
            {
                string _captcha = HttpUtility.HtmlDecode(context.Request.Form["vcode"]);
                string _username = context.Request.Form["un"];
                string _password = HttpUtility.HtmlDecode(context.Request.Form["pw"]);
                if (string.Compare(AppSession.CaptchaCode, _captcha, true) != 0)
                {
                    _s = "101:" + Fanex.Affiliate.Resources.Translator.Get("ValidationCode");
                    //Fanex.Affiliate.Resources.Translator.Get("ValidationCode");
                }
                else
                {
                  AffSession.IsInternalSite = false;
                    DbResult dr = Auth.ExecLogin(_username, _password,AppSession.Language, context);
                    if (string.IsNullOrEmpty(dr.Outputs["errmsg"].ToString()))
                    {
                        FormsAuthentication.SetAuthCookie(_username, true);

                        Fanex.Affiliate.Core.AppSession.CustID = DNAide.Core.CastAide.AsInt32(dr.Outputs["custid"], -1);
                        AppSession.OnUser = Auth.LoginOnuser(context);
                        AppSession.UserName = _username;
                        AppSession.FirstName = CastAide.AsString(dr.Outputs["firstname"]);
                        AppSession.LastName = CastAide.AsString(dr.Outputs["lastname"]);
                        AppSession.Email = CastAide.AsString(dr.Outputs["email"]);
                        //20090202@DungTong Add to check this user has security question input
                        AffSession.HasSecurityQuestion = CastAide.AsBoolean(dr.Outputs["hassecurityquestion"]);
                        AffSession.MailOption = CastAide.AsBoolean(dr.Outputs["mailoption"], true);//true is defined plain text
                        AffSession.PreferLang = CastAide.AsString(dr.Outputs["preferlang"], "en-US");
                        AffSession.AffType = CastAide.AsInt16(dr.Outputs["afftype"], 1);
                        AppSession.Prefix = DNAide.Core.CastAide.AsString(dr.Outputs["prefix"]);

                        AppSession.Currency = DNAide.Core.CastAide.AsString(dr.Outputs["currency"]);
                        CurrencyUtils.SetDefaultCurrency(AppSession.Currency);
                        if (AppSession.Language == null)
                            AppSession.Language = "en-US";

                        _s = "100:OK";
                        AffSession.FirsTimeLoginSucc = true;
                    }
                    else
                    {
                        _s = "102:" + dr.Outputs["errmsg"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message);
                _s = "102:" + ex.Message;
#if DEBUG
                throw;
#endif
            }
            context.Response.Write(_s);
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

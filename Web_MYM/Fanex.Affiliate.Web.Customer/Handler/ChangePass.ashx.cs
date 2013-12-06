namespace Fanex.Web.MYM.Customer.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Resources;

    public class ChangePass : IHttpHandler, IRequiresSessionState
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

            if (string.IsNullOrEmpty(context.Request.Form["txtCurrPass"]) || string.IsNullOrEmpty(context.Request.Form["txtNewPass"]))
            {
                context.Response.Write("<script type='text/javascript'>parent.alert('" + Fanex.Affiliate.Resources.Translator.Get("OldPassandNewPass") + "');</script>");
                return;
            }
            try
            {
                string strOldPass = context.Request.Form["txtCurrPass"].Trim();
                string strNewPass = context.Request.Form["txtNewPass"].Trim();
                int intErrCode = 0;
                UserAff.ChangePassword(Fanex.Affiliate.Core.AppSession.CustID, strNewPass, strOldPass, Fanex.Affiliate.Core.AppSession.CustID, AppSession.Language, ref intErrCode);
                if (intErrCode == 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.alert('" + Translator.Get("PwdChanged") + "');parent.location.href='../UserInfo/MyAccount.aspx';</script>");
                    return;
                }
                else
                {
                    if (intErrCode == 1001)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Translator.Get("OldPassErr") + "\"], [\"txtCurrPass\"], \"error_item\");</script>");
                        return;
                    }
                    else
                    {
                        context.Response.Write("<script type='text/javascript'>parent.alert(\"" + Translator.Get("ChangePassErr") + "\");</scrip>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.alert(\"" + ex.Message + "\");</scrip>");
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
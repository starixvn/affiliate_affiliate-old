namespace Fanex.Web.Aoncash.Customer.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Core.Culture;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Core;

    public class UpdateSecurityQuestion : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            if (string.IsNullOrEmpty(context.Request.Form["slSecurityQuestion"]) || string.IsNullOrEmpty(context.Request.Form["txtAnswer"]))
            {
                string strErrObj = "error_item";
                string strMsg = "Invalid data are sent to the server!";// Common.GetErrMsg(intErrCode, ref strErrObj);
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + strMsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                return;
            }
            try
            {
                string strQuestion = context.Request.Form["slSecurityQuestion"];
                string strAnswer = context.Request.Form["txtAnswer"];
                int intErrCode = 0;
                string errmsg = string.Empty;
                intErrCode = Auth.ExecUpdateSecurityQuestion(strQuestion, strAnswer, ref errmsg, context);
                if (intErrCode == 0)
                {
                    AffSession.HasSecurityQuestion = true;
                    //HttpContext.Current.Response.Redirect("Index.aspx", false);
                    context.Response.Write("<script type='text/javascript'>parent.alert('" + Translator.Get("SecurityQuestionUpdated") + "');parent.location.href='" + AppSettings.FullDomainName + "';</script>");
                    return;
                }
                else
                {
                    string strErrObj = "error_item";
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + errmsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                    return;
                }
            }
            catch (Exception ex)
            {
                string strErrObj = "error_item";
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + ex.Message + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
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

    }
}
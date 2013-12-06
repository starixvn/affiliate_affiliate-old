namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Web;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;

    public class ChangePass : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            try
            {
                string strNewPass = context.Request["NewPassword"];
                int intCustId = CastAide.AsInt32(context.Request["custid"]);
                int isAdmin = CastAide.AsInt32(context.Request["isAdmin"], 0);
                int intErrCode = 0;
                string errmsg = "";
                Affiliate.ResetPass(intCustId, Fanex.Affiliate.Core.AppSession.CustID, strNewPass, ref intErrCode, ref errmsg, isAdmin);
                if (intErrCode == 0)//send email and close this form
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess(\"" + Fanex.Affiliate.Resources.Admin.ResetPassSuccess + "\");</script>");
                }
                else//alert error
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + errmsg + "\");</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + ex.Message + "\");</script>");
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
                DNAideLog.Write(ex.Message);
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

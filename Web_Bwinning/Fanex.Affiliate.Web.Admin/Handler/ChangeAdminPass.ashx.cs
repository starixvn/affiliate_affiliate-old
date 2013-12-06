namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Admin;

    public class ChangeAdminPass : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            try
            {
                string strOldPass = context.Request["OldPass"];
                string strNewPass = context.Request["NewPassword"];
                int intCustId = CastAide.AsInt32(context.Request["custid"]);
                int intErrCode = Account.ChangePass(Fanex.Affiliate.Core.AppSession.CustID, strOldPass, strNewPass, AdminCommon.GetLangChar());
                if (intErrCode == 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSucess('" + Fanex.Affiliate.Resources.Translator.Get("UpdateSucc") + "');</script>");///Alert.aspx?user=" + Fanex.Affiliate.Resources.Translator.Get("Password + "&code=0&link=UserInfo/AffiliateList.aspx?status=approved';</script>");
                }
                else//alert error
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + Fanex.Affiliate.Resources.Translator.Get("OldPassIncorrect") + "\");</script>");
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
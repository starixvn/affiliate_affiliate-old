namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Data;
    using DNAide.Utils;
    using DNAide.Core;
    using DNAide.Web;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;

    public class UpdAffInfo : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            try
            {
                int func = CastAide.AsInt32(context.Request["func"], 5);
                string strSuccessMsg = string.Empty;
                DbResult result = null;
                switch (func)
                {
                    case 0://Update Status
                        string strStatus = context.Request["status"];
                        string strPayment = context.Request["hdPreferredPayment"];
                        int groupID = CastAide.AsInt32(context.Request["groupid"], -1);
                        string strPassword = string.Empty;


                        if (string.Compare(context.Request["status"], AdminCommon.Approved, true) == 0 && strPayment == "Player")
                        {
                            //exit in licensee or not!
                            string strPlayerUserName = context.Request["hdPlayer_Username"];
                            //var systemName = DataHandler.Settings.GetSetting("SystemName").ResultSet.Tables[0].Rows[0]["SValue"].ToString();
                            //var checkData = DataHandler.Member.IBCMemberExistingCheck(strPlayerUserName, systemName);

                            //if (Common.GetStatus(strPlayerUserName)==0)
                            //{
                            //    context.Response.Write("1@" + Fanex.Affiliate.Resources.Translator.Get("errDepositDBConnectFailed);
                            //    return;
                            //}


                        }

                        if (string.Compare(strStatus, AdminCommon.Approved, true) == 0 || string.Compare(strStatus, AdminCommon.Disapproved, true) == 0)
                            strPassword = Encryption.RandomString(6);
                        result = Affiliate.UpdateStatus(AdminSession.AffId, groupID, context.Request["status"], strPassword);

                        string mailOption = string.IsNullOrEmpty(context.Request["mailoption"]) ? "1" : context.Request["mailoption"];
                        bool blMailOption = CastAide.AsBoolean(mailOption == "1" ? true : false);

                        if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                        {
                            if (string.Compare(context.Request["status"], AdminCommon.Approved, true) == 0)
                            {
                                AdminCommon.SendEmailApproved(AdminSession.AffId, context.Request["lang"], context.Request["fname"], context.Request["username"], context.Request["email"], strPassword, AppCommon.EmailSuffix(blMailOption), AdminSession.AffType);
                            }
                            if (string.Compare(context.Request["status"], AdminCommon.Disapproved, true) == 0)
                                AdminCommon.SendEmailDisapproved(context.Request["lang"], context.Request["fname"], context.Request["email"], AppCommon.EmailSuffix(blMailOption));
                        }

                        strSuccessMsg = Fanex.Affiliate.Resources.Translator.Get("UpdateStaSucc");
                        break;
                    case 1://Close, open affiliate
                        bool isClosed = CastAide.AsBoolean(context.Request["closed"]);
                        result = Affiliate.AffUpdateClosedStatus(AdminSession.AffId, isClosed);
                        strSuccessMsg = Fanex.Affiliate.Resources.Translator.Get("UpdateStaSucc");
                        break;
                    case 2://Update Preferred language
                        if (string.IsNullOrEmpty(context.Request["lang"]))
                            throw new Exception(Fanex.Affiliate.Resources.Translator.Get("ErrLang"));
                        string strPreferLang = context.Request["lang"];
                        result = Affiliate.UpdateLanguage(AdminSession.AffId, strPreferLang);
                        strSuccessMsg = Fanex.Affiliate.Resources.Translator.Get("ErrLanguageSucc");
                        break;
                }
                if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                    context.Response.Write("0@" + strSuccessMsg);
                else
                    context.Response.Write("1@" + result.Outputs["errmsg"]);


            }
            catch (Exception ex)
            {
                context.Response.Write("1@" + ex.Message);
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
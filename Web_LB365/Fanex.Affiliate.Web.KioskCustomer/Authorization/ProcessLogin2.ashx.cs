namespace Fanex.Web.LiveBet365.Affiliate.Authorization
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

    public class ProcessLogin2 : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context = CultureHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            string _s = string.Empty;
            string _nl = Environment.NewLine;
            string _url = string.Empty;
            try
            {
                string _accId = context.Request["accId"];
                string _custName = context.Request["custName"];
                string _accKey = context.Request["accKey"];
                string _chalengeStr = context.Request["chalengeStr"];
                string _accOnId = context.Request["accOnId"];
                if (string.IsNullOrEmpty(_accId) || string.IsNullOrEmpty(_custName)
                    || string.IsNullOrEmpty(_accKey) || string.IsNullOrEmpty(_chalengeStr) || string.IsNullOrEmpty(_accOnId))
                {
                    context.Response.Write("<script type='text/javascript'>top.location.href='../Root.aspx';</script>");
                    return;
                }
                AffSession.IsInternalSite = true;
                DbResult dr = Auth.GetAdminNetSessionID(_accOnId, context);
                string _accNetSessionID = dr.Outputs["netsessionid"].ToString();
                if (string.Compare(_accKey, FormsAuthentication.HashPasswordForStoringInConfigFile(_accNetSessionID + _chalengeStr, "MD5"), false) != 0)
                    throw new IBCException("Can not authenticate log-in, invalid ticket!");


                DbResult dr2 = Auth.LoginAuthentication(CastAide.AsInt32(_accId), _custName, context, AppSettings.AffType);
                if (string.IsNullOrEmpty(dr2.Outputs["errmsg"].ToString()))
                {
                    Fanex.Affiliate.Core.AppSession.CustID = CastAide.AsInt32(dr2.Outputs["custid"], -1);
                    AppSession.OnUser = Auth.LoginOnuserInternal(context, CastAide.AsInt32(_accId) * -1);
                    AppSession.UserName = _custName.ToUpper() + " : [" + dr2.Outputs["adminusername"].ToString() + "]";
                    AppSession.FirstName = CastAide.AsString(dr2.Outputs["firstname"]);
                    AppSession.LastName = CastAide.AsString(dr2.Outputs["lastname"]);
                    AppSession.Email = CastAide.AsString(dr2.Outputs["email"]);
                    AffSession.HasSecurityQuestion = true;
                    AffSession.PreferLang = CastAide.AsString(dr2.Outputs["preferlang"], "en-US");
                    AffSession.AffType = CastAide.AsInt16(dr2.Outputs["afftype"], 1);
                    AppSession.Prefix = CastAide.AsString(dr.Outputs["prefix"]);

                    AppSession.Currency = CastAide.AsString(dr2.Outputs["currency"]);
                    CurrencyUtils.SetDefaultCurrency(AppSession.Currency);
                    if (AppSession.Language == null)
                        AppSession.Language = "en-US";

                    _s = "100:OK";
                    //   AppSettings.Session.FirsTimeLoginSucc = true;
                }
                else
                {
                    _s = "102:" + dr2.Outputs["errmsg"].ToString();
                }

                _url = FormsAuthentication.DefaultUrl;
            }
            catch (IBCException ex)
            {
                context.Response.Write("<body><script type='text/javascript'>alert('" + ex.Message.Replace("'", string.Empty) + "');location.href='../SignIn.aspx';</script></body>");
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message);
                _s = "102:" + ex.Message;
#if DEBUG
                throw;
#endif
            }
            context.Response.Redirect(_url);
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
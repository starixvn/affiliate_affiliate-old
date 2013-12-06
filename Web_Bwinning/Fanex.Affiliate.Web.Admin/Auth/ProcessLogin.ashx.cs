namespace Fanex.Web.Bwinning.Admin.Auth
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;

    public class ProcessLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            string _username = context.Request["un"];
            string _password = HttpUtility.HtmlDecode(context.Request["pw"]);
            string errmsg = "";
            int custid = 0;
            int roleid = 0;
            string isok = "";
            string lang = "";
            bool isIT = false;
            //1 : invalid code
            //2 : wrong password
            //0 : successful 
            try
            {
                int errcode = Auth.ExecLogin(ref _username, _password, ref errmsg, context, ref custid, ref roleid, ref isIT, lang);
                if (errcode == 0)
                {
                    FormsAuthentication.SetAuthCookie(custid.ToString(), false);
                    Fanex.Affiliate.Core.AppSession.CustID = custid;
                    AppSession.OnUser = Auth.LoginOnuser(context,6);
                    AdminSession.RoleID = roleid;
                    AppSession.UserName = _username;
                    AdminSession.IsIT = isIT;
                    AdminSession.RoleAccess = Auth.AssignRoles(custid);
                    isok = "OK";
                }
                else
                {
                    isok = errmsg;
                }
                context.Response.Write(isok);
            }
            catch (Exception ex)
            {
                string startTime, endTime;
                bool isUM = AppCommon.GetDownTime(out startTime, out endTime);
                if (isUM)
                {
                    context.Response.Write("UM");
                    return;
                }
                context.Response.Write(ex.Message.ToString());
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

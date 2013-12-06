<%@ Application Language="C#" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Security.Principal"%>
<%@ Import Namespace="Fanex.Affiliate.Core.Admin"%>
<%@ Import Namespace="Fanex.Affiliate.Core"%>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        if (AppSettings.Environment == "Local Development")
            return;
        // Code that runs when an unhandled error occurs
        Exception ex = Server.GetLastError().GetBaseException();
        if (ex is HttpException)
        {
            return;
        }

        string message = ex.Message +
                        "<br />SOURCE: " + ex.Source +
                        "<br />FORM: " + Request.Form.ToString() +
                        "<br />QUERYSTRING: " + Request.QueryString.ToString() +
                        "<br />TARGETSITE: " + ex.TargetSite +
                        "<br />STACKTRACE: " + ex.StackTrace;

        try{
        DNAide.Web.DNAideLog.Write(message.Replace("<br />", "\r\n"));
        
        AppCommon.SendEmail(message, AppSettings.ToMail, AppSettings.MailErrorTitle, AppSettings.MailErrorTitle);
        }
        catch (Exception ex1)
        {
            AppCommon.WriteErrorLog(ex.Message, ex.StackTrace, Fanex.Affiliate.Core.AppSettings.SiteID);
            AppCommon.WriteErrorLog(ex1.Message, ex1.StackTrace, Fanex.Affiliate.Core.AppSettings.SiteID);
        }
    }   
    
    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {        
         try
        {           

            if (!User.Identity.IsAuthenticated || Request.RawUrl.IndexOf(".aspx") == -1 || Session["RoleAccess"] == null)
                return;
            
            
            string _accessPermission = "";
            _accessPermission = Session["RoleAccess"].ToString();
            if (_accessPermission.IndexOf("[All]", StringComparison.CurrentCultureIgnoreCase ) != -1) return;

            string url;
            //Update VirtualDir = 0 in web.config when deploying to live
            if (AdminSettings.VirtualDir)
            {
                url = Request.RawUrl.Substring(Request.RawUrl.IndexOf("/", 1) + 1);
            }
            else
            {
                url = Request.RawUrl.Substring(1);
            }
            
            url = url.Substring(0, url.IndexOf(".aspx") + 5);
            
            if (Context.Cache["AccessPermission"] == null)
                AdminCommon.InitAccessPermission();

            string pageRole = ((Dictionary<string, string>)Context.Cache["AccessPermission"]).ContainsKey(url) ? ((Dictionary<string, string>)Context.Cache["AccessPermission"])[url] : "";
             
            Dictionary<string, string> Pages = ((Dictionary<string, string>)Context.Cache["AccessPermission"]);

            //foreach (KeyValuePair<string, string> pair in Pages)
            //{
            //    Context.Response.Write("Key: " + pair.Key + "; Value: " + pair.Value + "<br />");
            //}
            //Context.Response.Write("permission: " + _accessPermission.ToString() + "<br />");
             
            if (string.Compare(pageRole, "[All]", true) == 0) return;
            if (_accessPermission.IndexOf(pageRole) == -1)
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
        catch 
        {

        }        
    }
    private void OnRemoveRolePermissionCache(String k, Object v, CacheItemRemovedReason r)
    {
        //InitRolePermission();
    }   
</script>

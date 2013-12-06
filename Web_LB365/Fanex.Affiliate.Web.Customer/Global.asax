<%@ Application Language="C#" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
      /*  CacheUtil.GetLatestHotNews(null);*/
        

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
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

        try
        {
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
       
</script>

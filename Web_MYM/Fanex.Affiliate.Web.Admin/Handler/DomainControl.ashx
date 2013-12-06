<%@ WebHandler Language="C#" Class="DomainControl" %>

using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;

public class DomainControl : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        int errCode = 0;
        string errMsg = string.Empty;
        bool status = CastAide.AsBoolean(context.Request["status"]);
        string action = context.Request["action"].ToString();

        bool isSusccess = false;
        try
        {
            switch (action)
            {
                case "ins":
                    isSusccess = Domain.DomainControlIns(AppSession.CustID, context.Request["country"], context.Request["domainAff"], context.Request["domainMember"], status, ref errCode, ref errMsg, Fanex.Affiliate.Core.AppSession.Language);
                    break;
                case "upd":
                    isSusccess = Domain.DomainControlUpd(AppSession.CustID, CastAide.AsInt32(context.Request["id"]), context.Request["country"], context.Request["domainAff"], context.Request["domainMember"], status, ref errCode, ref errMsg, Fanex.Affiliate.Core.AppSession.Language);
                    break;
                case "del":
                    isSusccess = Domain.DomainControlDel(AppSession.CustID, CastAide.AsInt32(context.Request["id"]), ref errCode, ref errMsg, Fanex.Affiliate.Core.AppSession.Language);
                    break;
            }
        }
        catch (Exception ex)
        {
            Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
        }

        if (isSusccess)
        {
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write(errMsg);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
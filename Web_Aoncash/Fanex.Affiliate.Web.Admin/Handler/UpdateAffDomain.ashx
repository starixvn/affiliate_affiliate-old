<%@ WebHandler Language="C#" Class="UpdateAffDomain" %>

using System;
using System.Web;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Web.SessionState;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;
    
public class UpdateAffDomain : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        string action = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

        int affDomainId = CastAide.AsInt32(context.Request.Form["affDomainId"]);
        string affDomainName = CastAide.AsString(context.Request.Form["affDomainName"]);
        DbResult dbr = null;
        try
        {
            switch (action)
            {
                case "Edit":
                    {
                        dbr = Domain.UpdateAffDomain(affDomainId, affDomainName, AppSession.CustID, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                    }
                case "Add":
                    {
                        dbr = Domain.AddAffDomain(affDomainName, AppSession.CustID, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                    }
                case "Del":
                    {
                        dbr = Domain.DelAffDomain(affDomainId, affDomainName, AppSession.CustID, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
        }

        if (CastAide.AsInt32(dbr.Outputs["errcode"]) == 0)
        {
            context.Response.Write("Ok");
        }
        else
        {
            context.Response.Write(dbr.Outputs["errmsg"].ToString());
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

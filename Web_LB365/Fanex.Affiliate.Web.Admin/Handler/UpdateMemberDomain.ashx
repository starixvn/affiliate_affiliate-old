<%@ WebHandler Language="C#" Class="UpdateMemberDomain" %>

    using System;
    using System.Web;
    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    public class UpdateMemberDomain : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string action = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

            int _12BetDomainId = CastAide.AsInt32(context.Request.Form["_12BetDomainId"]);
            string _12BetDomainName = CastAide.AsString(context.Request.Form["_12BetDomainName"]);
            DbResult dbr = null;

            switch (action)
            {
                case "Edit":
                    {
                        dbr = Domain.UpdateMemberDomain(_12BetDomainId, _12BetDomainName, AppSession.CustID);
                        break;
                    }
                case "Add":
                    {
                        dbr = Domain.AddMemberDomain(_12BetDomainName, AppSession.CustID);
                        break;
                    }
                case "Del":
                    {
                        dbr = Domain.DelMemberDomain(_12BetDomainId, _12BetDomainName, AppSession.CustID);
                        break;
                    }
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

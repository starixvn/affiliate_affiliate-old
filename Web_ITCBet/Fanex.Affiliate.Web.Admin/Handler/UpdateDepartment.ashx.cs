namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Web.SessionState;
    using Fanex.Affiliate.DataObjects;
    using System;

    public class UpdateDepartment : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string status = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

            string groupN = CastAide.AsString(context.Request.Form["depname"]);
            string groupD = CastAide.AsString(context.Request.Form["depdesc"]);
            DbResult dbr = null;
            try
            {
                switch (status)
                {
                    case "Edit":
                        {
                            int groupId = CastAide.AsInt32(context.Request.Form["depid"]);
                            dbr = UserControl.UpdateDepartment(groupId, groupN, groupD, Fanex.Affiliate.Core.AppSession.CustID);
                            break;
                        }
                    case "Add":
                        {
                            dbr = UserControl.AddDepartment(groupN, groupD, Fanex.Affiliate.Core.AppSession.CustID);
                            break;
                        }
                    case "Del":
                        {
                            int groupId = CastAide.AsInt32(context.Request.Form["depid"]);
                            dbr = UserControl.DelDepartment(groupId, Fanex.Affiliate.Core.AppSession.CustID);
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
}

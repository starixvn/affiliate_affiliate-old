namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using DNAide.Data;
    using Fanex.Affiliate.DataObjects;

    public class Note : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            int intStatus = CastAide.AsInt32(context.Request["status"]);
            int intCustId = CastAide.AsInt32(context.Request["custid"]);
            int noteid = CastAide.AsInt32(context.Request["id"]);
            string strNoteContent = context.Request["note"];
            
            //strNoteContent = context.Server.HtmlEncode(strNoteContent);
            try
            {
                DbResult result = null;
                switch (intStatus)
                {
                    case 1://Add
                        result = Affiliate.AddNote(intCustId, Fanex.Affiliate.Core.AppSession.CustID, strNoteContent, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                    case 2:
                        result = Affiliate.UpdateNote(intCustId, Fanex.Affiliate.Core.AppSession.CustID, noteid, strNoteContent, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                    case 3://Delete
                        string strListNote = context.Request["id"];
                        if (string.IsNullOrEmpty(strListNote.Trim()))
                            break;
                        result = Affiliate.DeleteNote(Fanex.Affiliate.Core.AppSession.CustID, Fanex.Affiliate.Core.AppSession.CustID, strListNote, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                    default:
                        result = Affiliate.AddNote(intCustId, Fanex.Affiliate.Core.AppSession.CustID, strNoteContent, Fanex.Affiliate.Core.AppSession.Language);
                        break;
                }
                if (CastAide.AsInt32(result.Outputs["errcode"], -1) == 0)
                    context.Response.Write("ok");
                else
                    context.Response.Write("error@" + result.Outputs["errmsg"].ToString());
            }
            catch (Exception ex)
            {
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
                context.Response.Write("error@" + ex.Message);
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
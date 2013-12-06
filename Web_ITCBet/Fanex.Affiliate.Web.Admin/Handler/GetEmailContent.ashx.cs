namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using DNAide.Data;
    using System.Data;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    public class GetEmailContent : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                int mid = -1;
                try
                {
                    mid = Convert.ToInt32(context.Request.Params["params"]);
                }
                catch (Exception e)
                {
                    context.Response.Write(e);
                }


                StringBuilder result = new StringBuilder();
                DbResult r = EmailHistory.EmailHistorySel(mid, string.Empty, string.Empty, string.Empty, null, null, AppSettings.PageSize, 1);

                if (Convert.ToInt32(r.Outputs["nrows"]) > 0)
                {
                    DataRow row = r.ResultSet.Tables[0].Rows[0];
                    result.Append(row["subject"].ToString());
                    result.Append("$$$");
                    result.Append(row["sender"].ToString());
                    result.Append("$$$");
                    result.Append(row["email"].ToString());
                    result.Append("$$$");
                    result.Append(row["createdate"].ToString());
                    result.Append("$$$");
                    result.Append(row["content"].ToString());
                    result.Append("$$$");
                    result.Append(row["mid"].ToString());

                }

                context.Response.Write(result.ToString());


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

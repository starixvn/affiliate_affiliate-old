namespace Fanex.Web.LiveBet365.AffiliateAdmin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using DNAide.Data;
    using System.Data;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;

    public class GetMessage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                int msgid = -1;
                try
                {
                    msgid = Convert.ToInt32(context.Request.Params["params"]);
                }
                catch (Exception e)
                {
                    context.Response.Write(e);
                }


                StringBuilder result = new StringBuilder();
                DbResult r = HotNews.GetHotNews(msgid);

                if (r.ResultSet.Tables[0].Rows.Count > 0)
                {
                    DataRow row = r.ResultSet.Tables[0].Rows[0];
                    result.Append(row["important"].ToString().ToLower());
                    result.Append("$$$");
                    result.Append(row["message_en"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_cn"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_tw"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_th"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_es"].ToString());
                    result.Append("$$$");
                    result.Append(row["msgid"].ToString());

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

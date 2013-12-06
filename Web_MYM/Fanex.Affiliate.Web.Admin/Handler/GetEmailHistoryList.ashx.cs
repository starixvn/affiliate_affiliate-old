namespace Fanex.Web.MYM.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;

    public class GetEmailHistoryList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string[] param = context.Request.Params["params"].Split(";".ToCharArray());

                int option = -1, curpage = 1, minNo = 0, maxNo = 0;
                object fromdate = null, todate = null;
                string text = string.Empty, sender = string.Empty, email = string.Empty, affiliate = string.Empty;
                StringBuilder result = ValidateData(param, ref option, ref text, ref fromdate, ref todate, ref curpage);
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    int totalpage = 0, totalEmail = 0;
                    //string content = GenerateHTML.EmailHistory.GetEmailHistoryList(mid,string.Empty,string.Empty,string.Empty, null,null, curpage, ref minNo, ref maxNo, ref totalEmail,AppSettings.PageSize, ref totalpage);
                    string content = string.Empty;
                    result.Append(minNo.ToString());
                    result.Append("$$$");
                    result.Append(maxNo.ToString());
                    result.Append("$$$");
                    result.Append(content);

                }
                context.Response.Write(result.ToString());
            }

        }

        private StringBuilder ValidateData(string[] param, ref int option, ref string text, ref object fromdate, ref object todate, ref int curPage)
        {

            StringBuilder err = new StringBuilder("");

            try
            {
                if (param.Length == 5)
                {
                    option = Convert.ToInt32(param[0].Trim());
                    if (param[1].Trim() != "")
                    {
                        text = param[1].Trim();
                    }
                    if (param[2].Trim() != "")
                    {
                        fromdate = param[2].Trim();
                    }
                    if (param[3].Trim() != "")
                    {
                        todate = param[3].Trim();
                    }
                    curPage = Convert.ToInt32(param[4].Trim());

                }

            }
            catch (Exception e)
            {
                return err.Append(e.Message);

            }
            return err;

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

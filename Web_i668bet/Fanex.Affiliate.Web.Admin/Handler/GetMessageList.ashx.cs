namespace Fanex.Web.i668bet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;

    public class GetMessageList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string[] param = context.Request.Params["params"].Split(";".ToCharArray());

                int important = -1, curpage = 1, minNo = 0, maxNo = 0, totalPage = 0;
                object fromdate = null, todate = null;
                StringBuilder result = ValidateData(param, ref important, ref fromdate, ref todate, ref curpage);
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    int pageSize = 20;
                    int totalRow = 0;
                    string content = Fanex.Affiliate.Core.Admin.GenerateHTML.HotNews.GetHotNewsContentList(important, fromdate, todate, curpage, pageSize, ref minNo, ref maxNo, ref totalRow, ref totalPage);
                    result.Append(minNo.ToString());
                    result.Append("$$$");
                    result.Append(maxNo.ToString());
                    result.Append("$$$");
                    result.Append(content);

                }
                context.Response.Write(result.ToString());


            }

        }

        private StringBuilder ValidateData(string[] param, ref int important, ref object fromdate, ref object todate, ref int curPage)
        {

            StringBuilder err = new StringBuilder("");

            try
            {
                if (param.Length == 4)
                {
                    important = Convert.ToInt32(param[0].Trim());
                    if (param[1].Trim() != "")
                    {
                        fromdate = param[1].Trim();
                    }
                    if (param[2].Trim() != "")
                    {
                        todate = param[2].Trim();
                    }
                    curPage = Convert.ToInt32(param[3].Trim());

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

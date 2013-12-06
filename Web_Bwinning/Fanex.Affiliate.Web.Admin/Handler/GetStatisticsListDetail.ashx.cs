namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core.Admin;

    public class GetStatisticsListPaging : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                string[] param = context.Request.Params["params"].Split(";".ToCharArray());

                string trackingcode = string.Empty;
                int month = 5, year = 2009;
                StringBuilder result = ValidateData(param, ref trackingcode, ref month, ref year);
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    result.Append(Fanex.Affiliate.Core.Admin.GenerateHTML.TrackingSystem.GetStatisticsMonthDetailContent(AdminSession.AffId, month, year, trackingcode));
                }

                context.Response.Write(result.ToString());

            }
        }

        private StringBuilder ValidateData(string[] param, ref string trackingcode, ref int month, ref int year)
        {

            StringBuilder err = new StringBuilder("");

            try
            {
                if (param.Length == 2)
                {
                    trackingcode = param[0].Trim();
                    DateTime dt = Convert.ToDateTime(param[1].Trim());
                    month = dt.Month;
                    year = dt.Year;
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

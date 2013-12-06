namespace Fanex.Web.Macaubet.Admin.Auth
{
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;

    public class ReCheckIn : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            var str = "1";
            if (AppSession.OnUser > 0)
            {
                //bool isok = DataHandler.Login.ReCheckin(context);
                if (Auth.ReCheckin())
                    str = "Expired";

            }
            else
            {
                context.Response.Write("Expired");
                return;
            }

            string startTime, endTime;
            bool isUM = AppCommon.GetDownTime(out startTime, out endTime);
            if (isUM)
            {
                str = "UM";
            }

            context.Response.Write(str);  

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

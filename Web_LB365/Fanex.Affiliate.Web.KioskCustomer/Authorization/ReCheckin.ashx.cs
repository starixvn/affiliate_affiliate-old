namespace Fanex.Web.LiveBet365.Affiliate.Authorization
{
    using System;
    using System.Web;
    using DNAide.Data;
    using System.Web.SessionState;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;


    public class ReCheckIn : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.MinValue);
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            var message = "1";
            if (AppSession.OnUser != -1)
            {
                if (Auth.ReCheckin()) message = "Expired";
            }

            string startTime, endTime;
            bool isUM = AppCommon.GetDownTime(out startTime, out endTime);
            if (isUM)
            {
                message = "UM";
            }
            
            context.Response.Write(message);
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
// -----------------------------------------------------------------------
// <copyright file="CultureHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Core.Culture
{
    using System.Web;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class CultureHandler
    {
        public static HttpContext InitializeCulture(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string lang = "en-US";
            if (context.Session!=null && context.Session["Culture"] != null)
                lang = context.Session["Culture"].ToString();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
            return context;
        }
    }
}

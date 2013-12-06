namespace Fanex.IndoPaul.Core.Affiliate
{
    using System.Web;

    /// <summary>
    /// Summary description for IBCHadler
    /// </summary>
    public static class IBCHadler
    {
        public static HttpContext InitializeCulture(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string lang = "en-US";
            if (context.Session["Culture"] != null) lang = context.Session["Culture"].ToString();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
            return context;
        }
    }
}
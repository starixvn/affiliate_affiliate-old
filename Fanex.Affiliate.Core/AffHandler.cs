namespace Fanex.IndoPaul.Core.Affiliate
{
using System.Web;
    using Fanex.IndoPaul.DataObjects;
/// <summary>
/// Summary description for AffHandler
/// </summary>
public class AffHandler
{
    public static HttpContext InitializeCulture(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.Private);
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(AppSettings.Session.Language);
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(AppSettings.Session.Language);
        return context;
    }
}
}
namespace Fanex.Affiliate.Model
{
    using System.Web;
    using DNAide.Core;
    /// <summary>
    /// Summary description for DBType
    /// </summary>
    public class DBType
    {
        private static string _main = "main";
        private static string _mainView = "main_view";
        private static string _archive = "archive";
        private static string _affiliate = "affiliate";
        private static string _affiliateView = "affiliate_view";

        public static string GetDbName(string dbName)
        {
            dbName = dbName.ToLower();
            switch (dbName)
            {
                case "main":
                    return DBType.MAIN;
                case "archive":
                    return DBType.ARCHIVE;
                default:
                    return DBType.AFFILIATE;
            }

        }

        public static string MAIN
        {
            get
            {
                return (CastAide.AsBoolean(HttpContext.Current.Session["IsInternalSite"], false)) ? _mainView : _main;
            }
        }


        public static string AFFILIATE
        {
            get
            {
                // var isInternalSite = HttpContext.Current.Session["IsInternalSite"] ?? false;
                var isInternalSite =  (HttpContext.Current != null &&
                                       HttpContext.Current.Session != null &&
                                       HttpContext.Current.Session["IsInternalSite"] != null) ? HttpContext.Current.Session["IsInternalSite"] : false;
                return (CastAide.AsBoolean(isInternalSite)) ? _affiliateView : _affiliate;
            }
        }

        public static string ARCHIVE
        {
            get
            {
                return _archive;
            }
        }


    }
}

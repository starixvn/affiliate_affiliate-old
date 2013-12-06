// -----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin
{
    using System;
    using System.Configuration;
    using System.Xml;
    using System.IO;
    using DNAide.Core;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// Summary description for AppSettings
    /// </summary>
    public static class AdminSettings
    {     
        
        static string _localtest;
        
        static string _affMedia;
        static string _mediaUrl;
       
        
       
        static string _virtualDir;
        static string _internalURL;
        static string _internalURL_Kiosk;
        static string _ipCheckerURL;
        static string _websiteCheckerURL;

        static string _urlAffSite;
        static string _urlAffSite_Kiosk;
        static string _urlAffSiteThemes;

        static string _emailBackground;
        static string _emailTopColor;
        static string _emailBottomColor;
        
        static CurrencyObj _systemCurrency;

        
        static AdminSettings()
        {            
            _virtualDir = ConfigurationManager.AppSettings["VirtualDir"];
            _localtest = ConfigurationManager.AppSettings["LocalTest"];
            _ipCheckerURL = ConfigurationManager.AppSettings["IPCheckerURL"];
            _affMedia = ConfigurationManager.AppSettings["AffMedia"];
            _mediaUrl = ConfigurationManager.AppSettings["MediaUrl"];
            
            _internalURL = ConfigurationManager.AppSettings["internalURL"];
            _internalURL_Kiosk = ConfigurationManager.AppSettings["internalURL_Kiosk"];
            _websiteCheckerURL = ConfigurationManager.AppSettings["WebsiteCheckerURL"];
            _urlAffSite = ConfigurationManager.AppSettings["UrlAffSite"];
            _urlAffSite_Kiosk = ConfigurationManager.AppSettings["UrlAffSite_Kiosk"];
            _urlAffSiteThemes = (ConfigurationManager.AppSettings["UrlAffSiteThemes"] ?? ConfigurationManager.AppSettings["UrlAffSite"]);
            _systemCurrency = Currency.GetSystemCurrency();

            _emailBackground = ConfigurationManager.AppSettings["EmailBackground"]??string.Empty;
            _emailTopColor = ConfigurationManager.AppSettings["EmailTopColor"] ?? string.Empty;
            _emailBottomColor = ConfigurationManager.AppSettings["EmailBottomColor"] ?? string.Empty;

        }
      
       
       
        public static CurrencyObj SystemCurrency
        {
            get
            {
                return _systemCurrency;
            }
        }
       
        public static string LocalTest
        {
            get
            {
                return _localtest;
            }
        }
        public static void InitThemeInfo()
        {

        }
       
        public static string AffMedia
        {
            get
            {

                return _affMedia;
            }
        }
        public static string MediaUrl
        {
            get
            {

                return _mediaUrl;
            }
        }
       
       
        public static bool VirtualDir
        {
            get
            {
                return _virtualDir == "1";
            }
        }
       
        public static string InternalURL
        {
            get
            {
                return _internalURL;
            }
        }
        public static string InternalURL_Kiosk
        {
            get
            {
                return _internalURL_Kiosk;
            }
        }
        public static string IPCheckerURL
        {
            get
            {
                return _ipCheckerURL;
            }
        }
        public static string WebsiteCheckerURL
        {
            get
            {
                return _websiteCheckerURL;
            }
        }
       
        public static string UrlAffSite
        {
            get
            {
                return _urlAffSite;
            }
        }

        public static string UrlAffSite_Kiosk
        {
            get
            {
                return _urlAffSite_Kiosk;
            }
        }

        public static string UrlAffSiteThemes
        {
            get
            {
                return _urlAffSiteThemes;
            }
        }

        public static string EmailBackground
        {
            get
            {
                return _emailBackground;
            }
        }

        public static string EmailTopColor
        {
            get
            {
                return _emailTopColor;
            }
        }

        public static string EmailBottomColor
        {
            get
            {
                return _emailBottomColor;
            }
        }

     

    
    }

}

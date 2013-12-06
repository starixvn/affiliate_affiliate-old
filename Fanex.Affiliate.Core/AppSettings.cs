// -----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using DNAide.Core;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;
    using System.Collections.Specialized;
    using System.Web;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.Core.Data;
    using DNAide.Web;
using System.Globalization;

    /// <summary>
    /// Summary description for AppSettings
    /// </summary>
    public static class AppSettings
    {
        static int _licenseeID;
        static string _bannerScriptUrl;
        static string _bannerDomain;
        static string _bannerDirectory;
        static string _site;
        static int _siteid;
        static string _linkFormat;

        static string _jsVersion;
        static string _serverId;
        static string _tplFolder;
        static string _themeDir;
        static string _pageHeader = string.Empty;
        static string[] _cssFiles;

        static string _pageTitle = string.Empty, _pageKeywords = string.Empty, _pageDescription = string.Empty;

        static string _lbUrl;
        static DateTime _UMStartTime;
        static DateTime _UMEndTime;
        static DateTime _systemStartDate;
        static string _mailErrorTitle;
        static int _pageSize;
        static string _systemName = null;
        static string _systemNameForMail = null;
        static IList<BaseObj> _supportedBaseCurrencies;
        static IList<BaseObj> _affCurrencies;
        static IList<BaseObj> _affSystemSupportedCurrencies;
        static IList<BaseObj> _affPlayerCurrencies;
        static IList<BaseObj> _baseBannerCategories;
        static IList<BaseObj> _baseBannerSizes;
        static IList<BaseObj> _baseBannerTypes;
        static IList<BaseObj> _allCurrencies;
        static IList<BaseObj> _baseLanguages;
        static IList<BaseObj> _adminLanguages;
        static IList<BaseObj> _affLanguages;
        static IList<BaseObj> _baseCountries;
        static IList<BaseObj> _baseCustomerPage;
        static IList<BaseObj> _baseAffTypes;

        static string _defaultLanguage;

        static int _imagesize;
        static string _environment;

        static NumberFormatInfo _numberFormatter;

        static AppSettings()
        {
            try
            {
                _defaultLanguage = CastAide.AsString(ConfigurationManager.AppSettings["DefaultLanguage"], "en-US");
                _licenseeID = CastAide.AsInt32(Settings.GetSettingValue("LicenseeID", "Meta", Environment));
                _environment = ConfigurationManager.AppSettings["Environment"];
                _bannerScriptUrl = Settings.GetSettingValue("BannerScriptUrl", "Meta", Environment);
                _bannerDirectory = Settings.GetSettingValue("BannerDirectory", "Meta", Environment);
                _bannerDomain = Settings.GetSettingValue("BannerDomain", "Meta", Environment);
                _site = ConfigurationManager.AppSettings["Site"];
                _siteid = CastAide.AsInt32(ConfigurationManager.AppSettings["SiteID"], 0);

                _jsVersion = ConfigurationManager.AppSettings["JsVersion"];
                _serverId = ConfigurationManager.AppSettings["ServerId"];
                _tplFolder = ConfigurationManager.AppSettings["template_folder"];

                _linkFormat = ConfigurationManager.AppSettings["LinkFormat"];
                _mailErrorTitle = ConfigurationManager.AppSettings["MailErrorTitle"];

                _lbUrl = ConfigurationManager.AppSettings["LBUrl"];
                _UMStartTime = CastAide.AsDateTime(ConfigurationManager.AppSettings["UM_StartTime"]);
                _UMEndTime = CastAide.AsDateTime(ConfigurationManager.AppSettings["UM_EndTime"]);
                _pageSize = DNAide.Core.CastAide.AsInt32(ConfigurationManager.AppSettings["PageSize"]);

                _themeDir = @"App_Themes/" + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Theme"]) ? "Default" : ConfigurationManager.AppSettings["Theme"]);
                //GET CSS FILES
                _imagesize = CastAide.AsInt32(ConfigurationManager.AppSettings["imagesize"]);
                var themeFullPath = System.AppDomain.CurrentDomain.BaseDirectory + _themeDir;
                if (Directory.Exists(themeFullPath))
                {
                    _cssFiles = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + _themeDir);
                    for (int j = 0; j < _cssFiles.Length; ++j)
                    {
                        _cssFiles[j] = _cssFiles[j].Substring(_cssFiles[j].IndexOf("App_Themes")).Replace("\\", "/");
                    }
                }
                _affCurrencies = Currency.GetAffiliateCurrencies();
                _affSystemSupportedCurrencies = Currency.GetAffSystemSupportedCurrencies();
                _supportedBaseCurrencies = Currency.GetSupportedBaseCurrencies();
                _affPlayerCurrencies = Currency.GetAffPlayerCurrencies();
                _baseBannerSizes = BannerType.GetSizes();
                _baseBannerCategories = BannerType.GetCategories();
                _baseBannerTypes = BannerType.GetTypes();
                _allCurrencies = Currency.GetAll();
                _baseLanguages = Fanex.Affiliate.DataObjects.Language.Get();
                _adminLanguages = Fanex.Affiliate.DataObjects.Language.GetAdminSupportedLanguages();
                _affLanguages = Fanex.Affiliate.DataObjects.Language.GetAffiliateSupportedLanguages(AppSettings.Environment);

                _baseCountries = Country.GetAll();
                _baseAffTypes = Fanex.Affiliate.DataObjects.AffType.GetAll();

                _systemStartDate = Settings.GetSystemStartDate();
                _numberFormatter = CultureInfo.GetCultureInfo("en-US").NumberFormat;
                initPageHeader();
            }
            catch (Exception ex)
            {
                DNAideLog.Write("Exception:" + ex.Message);
                if(ex.InnerException!=null)
                DNAideLog.Write("InnerException:" + ex.InnerException.Message);
                AppCommon.WriteErrorLog(ex.Message, ex.StackTrace, 0);
                AppCommon.SendEmail(ex);
            }

        }

        public static NumberFormatInfo NumberFormat
        {
            get { return _numberFormatter; }
        }
        public static string FullDomainName
        {
            get
            {
                NameValueCollection variables = HttpContext.Current.Request.ServerVariables;

                string domain = variables["SERVER_NAME"];
                string port = variables["SERVER_PORT"];
                string protocol = HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://";

                var fulldomain = protocol + domain;
                if (!Environment.Equals("Production", StringComparison.OrdinalIgnoreCase))
                    fulldomain = fulldomain + ":" + port;
                fulldomain = fulldomain + (HttpRuntime.AppDomainAppVirtualPath == "/" ? string.Empty : HttpRuntime.AppDomainAppVirtualPath);
                return fulldomain;
                //return protocol + domain + ":" + port + (HttpRuntime.AppDomainAppVirtualPath == "/" ? string.Empty : HttpRuntime.AppDomainAppVirtualPath);
            }
        }


        public static void RefreshBaseBannerSizes()
        {
            _baseBannerSizes = BannerType.GetSizes();
        }
        public static void RefreshAffiliateCurrencies()
        {
            _affCurrencies = Currency.GetAffiliateCurrencies();
        }
        static void initPageHeader()
        {
            //try
            //{
            _pageHeader = string.Empty;
            XmlDocument doc = new XmlDocument();
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Web.info";
            if (!File.Exists(filePath))
            {
                return;
            }

            doc.Load(filePath);
            foreach (XmlNode node in doc.ChildNodes[1].ChildNodes)
            {
                if (node.Attributes["name"].Value == "title")
                {
                    _pageHeader += "<title>" + node.Attributes["value"].Value + "</title>\n";
                    _pageTitle = node.Attributes["value"].Value;
                }
                else if (node.Attributes["name"].Value == "keywords")
                {
                    _pageHeader += "<meta name=\"keywords\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                    _pageKeywords = node.Attributes["value"].Value;
                }
                else if (node.Attributes["name"].Value == "description")
                {
                    _pageHeader += "<meta name=\"description\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                    _pageDescription = node.Attributes["value"].Value;
                }
                else if (node.Attributes["name"].Value == "contenttype")
                {
                    _pageHeader += "<meta http-equiv=\"Content-Type\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                }
            }
            /*}
            catch
            {
            }*/
        }
        public static string DefaultLanguage
        {
            get { return _defaultLanguage; }
        }
        public static int LicenseeID
        {
            get { return _licenseeID; }
        }

        public static int SiteID
        {
            get { return _siteid; }
        }

        public static DateTime SystemStartDate
        {
            get { return _systemStartDate; }
        }

        public static string Environment
        {
            get { return _environment; }
        }

        public static string BannerScriptUrl
        {
            get { return _bannerScriptUrl; }
        }
        public static string BannerDomain
        {
            get { return _bannerDomain; }
        }
        public static string BannerDirectory
        {
            get { return _bannerDirectory; }
        }
        public static IList<BaseObj> BaseAffTypes
        {
            get
            {
                return _baseAffTypes;
            }
        }
        public static int ImageSize
        {
            get
            {
                return _imagesize;
            }
        }
        public static IList<BaseObj> BaseLanguages
        {
            get
            {
                return _baseLanguages;
            }
        }
        public static IList<BaseObj> AdminLanguages
        {
            get
            {
                return _adminLanguages;
            }
        }
        public static IList<BaseObj> AffiliateLanguages
        {
            get
            {
                return _affLanguages;
            }
        }
        public static IList<BaseObj> AllCurrencies
        {
            get
            {
                return AppCommon.LocalizeList(_allCurrencies);
            }
        }
        public static IList<BaseObj> AffSystemSupportedCurrencies
        {
            get
            {
                return _affSystemSupportedCurrencies;
            }
        }
        public static IList<BaseObj> AffCurrencies
        {
            get
            {
                return _affCurrencies;
            }
        }
        public static IList<BaseObj> AffPlayerCurrencies
        {
            get
            {
                return _affPlayerCurrencies;
            }
        }
        public static IList<BaseObj> BaseCustomerPage
        {
            get
            {
                return AppCommon.LocalizeList(_baseCustomerPage);
            }
        }
        public static IList<BaseObj> BaseBannerTypes
        {
            get
            {
                return AppCommon.LocalizeList(_baseBannerTypes);
            }
        }

        public static IList<BaseObj> BaseBannerSizes
        {
            get
            {
                return _baseBannerSizes;
            }
        }

        public static IList<BaseObj> BaseBannerCategories
        {
            get
            {
                return _baseBannerCategories;
            }
        }

        public static IList<BaseObj> SupportedBaseCurrencies
        {
            get
            {
                return AppCommon.LocalizeList(_supportedBaseCurrencies);
            }
        }

        public static IList<BaseObj> BaseCountries
        {
            get
            {
                return _baseCountries;// AppCommon.LocalizeList(_baseCountries);
            }
        }

        public static string SystemName
        {
            get
            {
                if (_systemName == null)
                    _systemName = Settings.GetSetting("SystemName").ResultSet.Tables[0].Rows[0]["SValue"].ToString();
                return _systemName;
            }
        }
        public static string SystemNameForMail
        {
            get
            {
                if (_systemNameForMail == null)
                    _systemNameForMail = Settings.GetSetting("SystemNameForMail").ResultSet.Tables[0].Rows[0]["SValue"].ToString();
                return _systemNameForMail;
            }
        }
        public static int PageSize
        {
            get
            {
                return _pageSize;
            }
        }
        public static string[] CssFiles
        {
            get
            {
                return _cssFiles;
            }
        }
        public static string Site
        {
            get
            {
                return _site;
            }
        }

        public static string LinkFormat
        {
            get
            {
                return _linkFormat;
            }
        }

        public static string JsVersion
        {
            get
            {
                return _jsVersion;
            }
        }
        public static string ServerId
        {
            get
            {
                return _serverId;
            }
        }
        public static string TplFolder
        {
            get
            {
                return _tplFolder;
            }
        }

        public static void InitThemeInfo()
        {

        }
        public static string HtmlHeader
        {
            get
            {
                return _pageHeader;
            }
        }
        public static string ThemeDir
        {
            get
            {
                return _themeDir;
            }
        }
        public static string PageTitle
        {
            get
            {
                return _pageTitle;
            }
        }
        public static string PageKeywords
        {
            get
            {
                return _pageKeywords;
            }
        }
        public static string PageDescription
        {
            get
            {
                return _pageDescription;
            }
        }

        public static bool HasLogin
        {
            get
            {
                return (AppSession.CustID > 0);
            }
        }

        public static string PhoneContact
        {
            get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["PhoneContact"]); }
        }
        public static string TextBoxContentUrl
        {
            get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["TextBoxContentUrl"]); }
        }
        public static int AffType
        {
            get { return DNAide.Core.CastAide.AsInt32(System.Configuration.ConfigurationManager.AppSettings["AffType"], 1); }
        }

        public static string MailErrorTitle
        {
            get
            {
                return _mailErrorTitle;
            }
        }
        public static string LBUrl
        {
            get
            {
                return _lbUrl;
            }
        }
        public static DateTime UMStartTime
        {
            get
            {
                return _UMStartTime;
            }
        }
        public static DateTime UMEndTime
        {
            get
            {
                return _UMEndTime;
            }
        }

        #region -------------For Mail Service------------------

        public static string MailServer
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]; }
        }
        public static string MailPwd
        {
            get { return DNAide.Utils.Encryption.CfsDeCrypt(System.Configuration.ConfigurationManager.AppSettings["PassMail"]); }
        }
        public static string FromMail
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FromMail"]; }
        }
        public static int Port
        {
            get { return DNAide.Core.CastAide.AsInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]); }
        }

        public static string ToMail
        {
            get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["ToMail"]); }
        }

        public static string CCMail
        {
            get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["CCMail"]); }
        }
        public static string BCCMail
        {
            get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["BCCMail"]); }
        }
        public static string MailContact
        {
            get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["MailContact"]); }
        }
        #endregion
    }
}

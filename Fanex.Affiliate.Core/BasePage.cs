// -----------------------------------------------------------------------
// <copyright file="BasePage.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{
    using System;
    using System.Web;

    /// <summary>
    /// TODO: Update summary. BasePage
    /// </summary>
    public class BasePage: System.Web.UI.Page
    {
        private string _locale;
        protected string HtmlContent = string.Empty;
        private string _vers = string.Empty;
        protected string ThemePath;
        protected override void InitializeCulture()
        {
            if (Request.Cookies["afffavoritelang"] != null && string.IsNullOrEmpty(AppSession.Language))
            {
                string lang = Request.Cookies["afffavoritelang"].Value;
                AppSession.Flag = lang;
                AppSession.Language = lang;
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("afffavoritelang");
                aCookie.Value =AppSession.Language;
                aCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(aCookie);
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!string.IsNullOrEmpty(Request["lang"]))
               AppSession.Language = AppCommon.GetLangCodeByShortLang(Request["lang"]);

            _locale = (string.IsNullOrEmpty(AppSession.Language)) ? "en-US" :AppSession.Language;
            this.Culture = _locale;
            this.UICulture = _locale;
           AppSession.Language = _locale;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_locale);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(_locale);

            string startTime, endTime;
            bool isUM = AppCommon.GetDownTime(out startTime, out endTime);
            if (isUM)
            {
                Response.Redirect("~/UnderMaintenance.aspx");
            }

            base.InitializeCulture();
        }

        public string GetText(string key)
        {
            object text = GetGlobalResourceObject("Lang", key);
            if (text == null) return key;
            return text.ToString();
        }

        public string GetText(string culture, string key)
        {
            if (culture == _locale) return GetText(key);
            this.Culture = culture;
            this.UICulture = culture;
            string result = GetText(key);
            this.Culture = _locale;
            this.UICulture = _locale;
            return result;
        }
        public string Locale
        {
            get
            {
                return _locale;
            }
        }

        public string GetLangChar
        {
            get
            {
                switch (_locale)
                {
                    case "en-US": return "E";
                    case "ja-JP": return "J";
                    case "zh-CN": return "CS";
                    case "zh-CT": return "CT";
                    case "th-TH": return "TH";
                    case "es-ES": return "ES";
                    default: return "E";
                }
            }
        }
        protected string RelativePath
        {
            get
            {
                string prePath = string.Empty;
                string fPath = Request.AppRelativeCurrentExecutionFilePath.Replace("~/", string.Empty);
                while (true)
                {
                    int pos = fPath.IndexOf("/");
                    if (pos == -1) break;
                    fPath = fPath.Remove(0, pos + 1);
                    prePath += "../";
                }
                return prePath;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (string.IsNullOrEmpty(this.Header.Title) || this.Header.Title == "Untitled Page")
                this.Header.Title = AppSettings.PageTitle;         
        }
    }
}

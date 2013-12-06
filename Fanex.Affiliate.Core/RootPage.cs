namespace Fanex.IndoPaul.Core.Affiliate
{
    using System;
    using System.Web;
    using Fanex.IndoPaul.DataObjects;

    /// <summary>
    /// Summary description for IBCPage2
    /// </summary>
    public class RootPage : System.Web.UI.Page
    {
        private string _locale;
        protected string HtmlContent = string.Empty;
        private string _vers = string.Empty;
        protected string ThemePath;
        protected override void InitializeCulture()
        {
            if (Request.Cookies["afffavoritelang"] != null && string.IsNullOrEmpty(AppSettings.Session.Language))
            {
                string lang = Request.Cookies["afffavoritelang"].Value;
                AppSettings.Session.Flag = lang;
                AppSettings.Session.Language = lang;
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("afffavoritelang");
                aCookie.Value = AppSettings.Session.Language;
                aCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(aCookie);
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!string.IsNullOrEmpty(Request["lang"]))
                AppSettings.Session.Language = Common.GetLangCodeByShortLang(Request["lang"]);

            _locale = (string.IsNullOrEmpty(AppSettings.Session.Language)) ? "en-US" : AppSettings.Session.Language;
            this.Culture = _locale;
            this.UICulture = _locale;
            AppSettings.Session.Language = _locale;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_locale);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(_locale);

            string startTime, endTime;
            bool isUM = Common.GetDownTime(out startTime, out endTime);
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
            /*if (string.IsNullOrEmpty(this.Header.) || this.Header.Title == "Untitled Page")
                this.Header.Title = AppSettings.PageTitle;
            if (string.IsNullOrEmpty(this.Header.Title) || this.Header.Title == "Untitled Page")
                this.Header.Title = AppSettings.PageTitle;*/
        }
    }
}
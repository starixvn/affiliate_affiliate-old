namespace Fanex.IndoPaul.Core.Affiliate
{
    using System.Web.UI;
    using Fanex.IndoPaul.DataObjects;
    /// <summary>
    /// Summary description for IBCPage
    /// </summary>
    public class IBCPage : System.Web.UI.Page
    {
        private string _locale;
        protected string HtmlContent = string.Empty;
        private string _vers = string.Empty;
        protected string ThemePath;
        protected override void InitializeCulture()
        {
            //_locale = (HttpContext.Current.Session["Culture"] == null) ? "en-US" : HttpContext.Current.Session["Culture"].ToString();
            _locale = (string.IsNullOrEmpty(AppSettings.Session.Language)) ? "en-US" : AppSettings.Session.Language;
            if (!string.IsNullOrEmpty(AppSettings.Session.Language))
            {
                this.Culture = _locale;
                this.UICulture = _locale;
            }
            else
            {
                //HttpContext.Current.Session["Culture"] = _locale;
                AppSettings.Session.Language = _locale;
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_locale);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(_locale);
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

        protected string CssLinks
        {
            get
            {
                string link = "<link href='{RelPath}' type='text/css' rel='stylesheet' />";
                string links = "\n";
                for (int i = 0; i < AppSettings.CssFiles.Length; ++i)
                {
                    links += link.Replace("{RelPath}", RelativePath + AppSettings.CssFiles[i]);
                }
                links += "\n";
                return links;
            }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            writer.Write("<html>\n");
            //header
            writer.Write("<head>\n");
            writer.Write(AppSettings.HtmlHeader);
            writer.Write(CssLinks);
            string aspxContent = DNAide.Web.TemplateHandler.LoadTemplate2(Request.AppRelativeCurrentExecutionFilePath.Replace("~/", string.Empty));
            try
            {
                writer.Write(DNAide.Web.TemplateHandler.CopyBlock(aspxContent, "js").Replace(".js", ".js?ver=" + AppSettings.JsVersion));
            }
            catch { }
            writer.Write("</head>\n");
            writer.Write("<body>\n");
            writer.Write(HtmlContent);
            writer.Write("\n</body>\n");
            writer.Write("</html>\n");

        }

    }
}
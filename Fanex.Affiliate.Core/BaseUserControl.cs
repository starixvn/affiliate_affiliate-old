// -----------------------------------------------------------------------
// <copyright file="BaseControl.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseUserControl : System.Web.UI.UserControl
    {
        private string _locale="en-US";
        private string _vers = string.Empty;
        protected string ThemePath;
        
        public string GetText(string key)
        {
            object text = GetGlobalResourceObject("Lang", key);
            if (text == null) return key;
            return text.ToString();
        }

        /*public string GetText(string culture, string key)
        {
            if (culture == _locale) return GetText(key);
            this.Culture = culture;
            this.UICulture = culture;
            string result = GetText(key);
            this.Culture = _locale;
            this.UICulture = _locale;
            return result;
        }*/
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
    }
}

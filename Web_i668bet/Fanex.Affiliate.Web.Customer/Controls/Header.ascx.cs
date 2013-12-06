
namespace Fanex.Web.i668bet.Customer
{
    using System;
    using System.Text;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;
    using Fanex.Core.Utility;
    using System.Linq;

    public partial class Controls_Header : BaseUserControl
    {
        public string Logout = string.Empty, displayLogout = "none";
        protected string strLogin = string.Empty;
        protected string LogoutDisplay = string.Empty;
        public string hotnews = string.Empty;        
        public string selectedLang = string.Empty;        
        public string gmt;
        public string year;
        public string month;
        public string day;
        public string hour;
        public string minute;
        public string second;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AppSession.IsAuthenticated)
            {
                A1.Visible = false;
                LogoutDisplay = "none";
            }
            else
                strLogin = "setInterval('KickedOut();',15000);";
            string navTpl = TemplateHandler.LoadTemplate("Controls/MainNavigator.htm");
            string strUrl = Request.Url.AbsoluteUri.ToString().ToLower();
            string strCurrent = "id=\"current\"";
            if (strUrl.IndexOf("programdetails.aspx") >= 0)
            {
                TemplateHandler.Assign(ref navTpl, "prog", strCurrent);
            }
            else if (strUrl.IndexOf("faqs.aspx") >= 0)
            {
                TemplateHandler.Assign(ref navTpl, "faq", strCurrent);
            }
            else if (strUrl.IndexOf("contactus.aspx") >= 0)
            {
                TemplateHandler.Assign(ref navTpl, "cont", strCurrent);
            }
            else if (strUrl.IndexOf("condition.aspx") >= 0)
            {
                TemplateHandler.Assign(ref navTpl, "term", strCurrent);
            }
            else if (strUrl.IndexOf("userinfo") < 0)
            {
                TemplateHandler.Assign(ref navTpl, "prog", strCurrent);
            }

            TemplateHandler.Assign(ref navTpl, "my-home", Translator.Get("Home"));
            TemplateHandler.Assign(ref navTpl, "faqs", Translator.Get("Faqs"));
            TemplateHandler.Assign(ref navTpl, "prog-details", Translator.Get("ProgDetails"));
            TemplateHandler.Assign(ref navTpl, "contact-us", Translator.Get("ContactUs"));
            TemplateHandler.Assign(ref navTpl, "term-condition", Translator.Get("TermCondition"));
            TemplateHandler.Assign(ref navTpl, "home-url", Page.ResolveUrl("~/"));
            MainNavHtml.Text = navTpl;
            


            InitializeHotNews();
            Logout = Translator.Get("Logout");

            selectedLang = AppSettings.AffiliateLanguages.Where(l => l.Id == AppSession.Language).FirstOrDefault().Value;

            DateTime dt = ServerDateTime.GetCurrentDBTime(out gmt);
            year = dt.Year.ToString();
            month = dt.Month.ToString();
            day = dt.Day.ToString();
            hour = dt.Hour.ToString();
            minute = dt.Minute.ToString();
            second = dt.Second.ToString();

        }
        private void InitializeHotNews()
        {
            bool isImportant;
            string latestews = CacheUtil.GetLatestHotNews(out isImportant, AppSession.Language);
            string textStyle = "text_hotnews";
            StringBuilder news = new StringBuilder();
            string url = String.Format(AppSettings.FullDomainName + "/UserInfo/Message.aspx");
            if (!string.IsNullOrEmpty(latestews))
            {
                if (isImportant)
                {
                    textStyle += "_important";
                }

                if (Fanex.Affiliate.Core.AppSession.CustID == -1)
                {
                    news.Append("<span class=\"" + textStyle + "\" id=\"topnews\">" + latestews + "</span>");
                }
                else
                {
                    news.Append("<a class=\"" + textStyle + "\" id=\"topnews\" href=\"" + url + "\">" + latestews + "</a>");
                }
            }
            hotnews = news.ToString();
        }
        protected string GetLangCss()
        {
            string langchar = AffCommon.GetLanguage();
            return (langchar == "Spanish" || langchar == "Vietnamese") ? "short" : string.Empty;
        }
        protected string FocusLang(string strLang)
        {
            if (string.Compare(AppSession.Language, strLang, true) == 0)
                return "selected";
            else
                return string.Empty;
        }

    }
}

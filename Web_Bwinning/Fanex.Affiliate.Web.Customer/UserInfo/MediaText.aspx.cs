using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.Core.Helper;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.DataObjects;

namespace Fanex.Web.Bwinning.Customer.UserInfo
{
    public partial class MediaText : BasePageLogIn
    {
        public string htmlContent = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaText.htm");
            TemplateHandler.Assign(ref htmlContent, "Category", Translator.Get("Category"));
            TemplateHandler.Assign(ref htmlContent, "Language", Translator.Get("Language"));
            TemplateHandler.Assign(ref htmlContent, "Currency", Translator.Get("Currency"));
            TemplateHandler.Assign(ref htmlContent, "MediaGallery", Translator.Get("Text"));
            TemplateHandler.Assign(ref htmlContent, "Search", Translator.Get("Search"));
            TemplateHandler.Assign(ref htmlContent, "TextLink", Translator.Get("TextLink"));
            TemplateHandler.Assign(ref htmlContent, "Banner", Translator.Get("Banner"));
            TemplateHandler.Assign(ref htmlContent, "TrackingCode", Translator.Get("TrackingCode"));
            TemplateHandler.Assign(ref htmlContent, "activatedTrackingCode", Translator.Get("ActivatedTrackingCode"));
            TemplateHandler.Assign(ref htmlContent, "newTrackingCode", Translator.Get("NewTrackingCode"));
            TemplateHandler.Assign(ref htmlContent, "GenerateLink", Translator.Get("GenerateLink"));

            TemplateHandler.Assign(ref htmlContent, "AvailableDomains", Translator.Get("AvailableDomains"));
            TemplateHandler.Assign(ref htmlContent, "LinkToLabel", Translator.Get("LinkTo"));

            /*Johnny:target*/
            string targetRequest = Request["target"];
            if (string.IsNullOrEmpty(targetRequest))
            {
                targetRequest = "sportsbook";
            }

            string domain = string.IsNullOrEmpty(Request["slDomain"]) ? string.Empty : Request["slDomain"];
            //Dropdown list for Domain
            TemplateHandler.Assign(ref htmlContent, "slDomain", AffCommon.DropdownDomainByAff(AppSession.CustID, ref domain));

            //Dropdown list for Landing page
            TemplateHandler.Assign(ref htmlContent, "target", AppCommon.DropdownCustomerPage("target", targetRequest));

            #region Get request information
            int pageIndex = CastAide.AsInt32(Request["page"], 1);
            int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int totalpage = 0;

            var category = CastAide.AsInt32(Request["slCategory"], 0);
            var lang = string.IsNullOrEmpty(Request["slLanguage"]) ? null : Request["slLanguage"];
            //string lang = string.IsNullOrEmpty(Request["slLanguage"]) ? Common.GetPreferLanguage() : Request["slLanguage"];
            var currency = CastAide.AsInt32(Request["slCurrency"],0);

            #endregion

            TemplateHandler.Assign(ref htmlContent, "slCategory",
                HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "slCategory", category, true));
            TemplateHandler.Assign(ref htmlContent, "slLanguage",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", lang, true));
            TemplateHandler.Assign(ref htmlContent, "slCurrency",
                HtmlHelper.DropDownList(AppSettings.AffPlayerCurrencies, "slCurrency", currency, true));            

            TemplateHandler.ReplaceBlock(ref htmlContent, "TextLinkList", AffCommon.GetTextLink(domain, AppSettings.BaseLanguages, targetRequest));

            TemplateHandler.ReplaceBlock(ref htmlContent, "Content", GetTextList(category, lang, currency, pageIndex, pageSize, ref totalpage, domain, targetRequest));
            TemplateHandler.Assign(ref htmlContent, "PageSize-val", pageSize);
            TemplateHandler.Assign(ref htmlContent, "page", pageIndex);
            /* Generate Banner paging*/
            if (totalpage > 0)
            {
                TemplateHandler.Assign(ref htmlContent, "paging-top", AppCommon.GenPaging(totalpage, pageIndex, pageSize, 0, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
                TemplateHandler.Assign(ref htmlContent, "paging-bottom", AppCommon.GenPaging(totalpage, pageIndex, pageSize, 0, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
            }
            else
            {
                TemplateHandler.Assign(ref htmlContent, "paging-top", string.Empty);
                TemplateHandler.Assign(ref htmlContent, "paging-bottom", string.Empty);
            }
        }

        public static string GetTextList(int category, string language, int currency, int pageIndex, int pagesize, ref int totalpage,
            string domain, string target)
        {
            string tmpLanguage = (language == "English (Europe)" ? "English" : language);//English Euro use English banner

            DbResult result = null;
            int nRows = 0;
            result = Banner.GetBannerListText(category, language, currency, pageIndex, pagesize);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int cols = 9;

            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("UserInfo/MediaText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 3;
                string signUpLink = AppSettings.LinkFormat; 
                
                string src = ConfigurationManager.AppSettings["MediaUrl"].ToString();

                TemplateHandler.Assign(ref signUpLink, "{domain}", domain);
                TemplateHandler.Assign(ref signUpLink, "{target}", target);
                TemplateHandler.Assign(ref signUpLink, "{custid}", AppSession.CustID.ToString());

                string flContent = HttpContext.Current.Server.HtmlEncode(TemplateHandler.LoadTemplate("UserInfo/FlashCode.htm"));
                TemplateHandler.Assign(ref flContent, "src", AppSettings.BannerDomain + "/SwfVersDetection.js");

                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    string aSignUpLink = signUpLink;
                    TemplateHandler.Assign(ref aSignUpLink, "lang", dr["languagecode"].ToString());
                    TemplateHandler.Assign(ref aSignUpLink, "target", target);
                    TemplateHandler.Assign(ref rowText, "messagevalue", HttpUtility.HtmlDecode(dr["content"].ToString().Replace("\r\n", "<br/>")));
                    TemplateHandler.Assign(ref rowText, "signuplink", aSignUpLink);
                    TemplateHandler.Assign(ref rowText, "src_val", AppSettings.BannerDomain + dr["content"]);//tmpSrc);
                    TemplateHandler.Assign(ref rowText, "bannerid", dr["bannerid"]);
                    TemplateHandler.Assign(ref rowText, "bannerscripturl", AppSettings.BannerScriptUrl);
                    TemplateHandler.Assign(ref rowText, "textid", dr["BannerID"]);
                    TemplateHandler.Assign(ref rowText, "SystemName", AppSettings.SystemName);
                    //TemplateHandler.Assign(ref rowText, "bannerurl", AppSettings.BannerScriptUrl);

                    

                    contentList.Append(rowText);
                }

                return contentList.ToString();

            }
            else
            {
                return AppCommon.NoInfo(cols);
            }

        }
    }
}
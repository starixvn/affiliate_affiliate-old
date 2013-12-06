namespace Fanex.Web.Macaubet.Customer.UserInfo
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Model;

    public partial class MediaGalleryNew : BasePageLogIn
    {
        public string htmlContent = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            /*Request to re-enable media gallerry again 20100917 by Peter */
            /* if (string.Compare(AppSettings.Session.PreferLang, "zh-CN", true) == 0 || string.Compare(AppSettings.Session.PreferLang, "zh-TW", true) == 0)
                 TemplateHandler.RemoveBlock(ref htmlContent, "listbanner");*/
            TemplateHandler.Assign(ref htmlContent, "SubCategory", Translator.Get("SubCategory"));
            TemplateHandler.Assign(ref htmlContent, "Category", Translator.Get("Category"));
            TemplateHandler.Assign(ref htmlContent, "Language", Translator.Get("Language"));
            TemplateHandler.Assign(ref htmlContent, "ImageType", Translator.Get("ImageType"));
            TemplateHandler.Assign(ref htmlContent, "Size", Translator.Get("Size"));
            TemplateHandler.Assign(ref htmlContent, "Currency", Translator.Get("Currency"));
            TemplateHandler.Assign(ref htmlContent, "MediaGallery", Translator.Get("Media"));
            TemplateHandler.Assign(ref htmlContent, "Search", Translator.Get("Search"));
            TemplateHandler.Assign(ref htmlContent, "TextLink", Translator.Get("TextLink"));
            TemplateHandler.Assign(ref htmlContent, "Banner", Translator.Get("Banner"));
            TemplateHandler.Assign(ref htmlContent, "TrackingCode", Translator.Get("TrackingCode"));
            TemplateHandler.Assign(ref htmlContent, "activatedTrackingCode", Translator.Get("ActivatedTrackingCode"));
            TemplateHandler.Assign(ref htmlContent, "newTrackingCode", Translator.Get("NewTrackingCode"));
            TemplateHandler.Assign(ref htmlContent, "GenerateLink", Translator.Get("GenerateLink"));
            TemplateHandler.Assign(ref htmlContent, "AvailableDomains", Translator.Get("AvailableDomains"));
            TemplateHandler.Assign(ref htmlContent, "LinkToLabel", Translator.Get("LinkTo"));

            /*smith:target*/
            string targetRequest = Request["target"];
            if (string.IsNullOrEmpty(targetRequest))
            {
                targetRequest = "sportsbook";
            }

            string domain = string.IsNullOrEmpty(Request["slDomain"]) ? string.Empty : Request["slDomain"];

            //Dropdown list for Domain
            TemplateHandler.Assign(ref htmlContent, "slDomain", AffCommon.DropdownDomainByAff(AppSession.CustID, ref domain));            

            //Dropdown list for Landing page
            //TemplateHandler.Assign(ref htmlContent, "target",
            //    HtmlHelper.DropDownList(AppSettings.BaseCustomerPage, "target", targetRequest, true));
            TemplateHandler.Assign(ref htmlContent, "target", AppCommon.DropdownCustomerPage("target", targetRequest));


            #region Get request information
            int pageIndex = CastAide.AsInt32(Request["page"], 1);
            int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int totalpage = 0;

            var category = CastAide.AsInt32(Request["slCategory"], 0);
            var subCategory = 0;            
            var paramCategory = category;
            var banner = BannerType.Select(AppSettings.BaseBannerCategories, category.ToString());
            
            
            if (banner!=null && banner.HasChildren)
            {
                subCategory = CastAide.AsInt32(Request["slSubCategoryMedia"], 0);
                paramCategory = subCategory>0 ? subCategory: category;                
            }

            //string lang = string.IsNullOrEmpty(Request["slLanguage"]) ? Common.GetPreferLanguage() : Request["slLanguage"];
            string lang = string.IsNullOrEmpty(Request["slLanguage"]) ? null : Request["slLanguage"];
            var currency = CastAide.AsInt32(Request["slCurrency"], 0);
            var imagetype = CastAide.AsInt32(Request["slImageType"], 0);
            var size = CastAide.AsInt32(Request["slSize"], 0);
           /* string trackOption = string.IsNullOrEmpty(Request["hdTrack"]) ? null : Request["hdTrack"];
            string trackingcode = string.IsNullOrEmpty(Request["slTrackingCode"]) ? "" : Request["slTrackingCode"];


            if (string.Compare(trackOption, "1", true) != 0)
            {
                TemplateHandler.Assign(ref htmlContent, "achecked", "checked");
                TemplateHandler.Assign(ref htmlContent, "nchecked", string.Empty);
                TemplateHandler.Assign(ref htmlContent, "track-val", string.Empty);
            }
            else
            {
                trackingcode = string.IsNullOrEmpty(Request["txtNewTrackingCode"]) ? "" : Request["txtNewTrackingCode"];
                TemplateHandler.Assign(ref htmlContent, "nchecked", "checked");
                TemplateHandler.Assign(ref htmlContent, "achecked", string.Empty);
                TemplateHandler.Assign(ref htmlContent, "track-val", trackingcode);
            }*/
            #endregion

            TemplateHandler.Assign(ref htmlContent, "slSubCategory", "");
            TemplateHandler.Assign(ref htmlContent, "currentSubCategory", subCategory);
            //TemplateHandler.Assign(ref htmlContent, "slTrackingCode", GenerateHTML.TrackingSystem.GetTrackingCodeList(false, trackingcode));


            TemplateHandler.Assign(ref htmlContent, "slCategory",
                HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "slCategory", category, true));
            TemplateHandler.Assign(ref htmlContent, "slLanguage",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", lang, true));
            TemplateHandler.Assign(ref htmlContent, "slCurrency",
                HtmlHelper.DropDownList(AppSettings.AllCurrencies, "slCurrency", currency, true));
            TemplateHandler.Assign(ref htmlContent, "slImageType",
                HtmlHelper.DropDownList(AppSettings.BaseBannerTypes, "slImageType", imagetype, true));
            TemplateHandler.Assign(ref htmlContent, "slSize",
                HtmlHelper.DropDownList(AppSettings.BaseBannerSizes, "slSize", size, true));

            //DataView dv = (CacheUtil.GetBannerTypes() as DataTable).DefaultView;
            //TemplateHandler.Assign(ref htmlContent, "slCategory", this.GetDataOptionBanner(dv, "slCategory", "CategoryMedia", category, true));
            //if (hasSubCategory)
            //    TemplateHandler.Assign(ref htmlContent, "currentSubCategory", subCategory);
            //else
            //    TemplateHandler.Assign(ref htmlContent, "currentSubCategory", "none");
            //TemplateHandler.Assign(ref htmlContent, "slLanguage", this.GetDataOptionBanner(dv, "slLanguage", "Language", lang, true));
            //TemplateHandler.Assign(ref htmlContent, "slCurrency", this.GetDataOptionBanner(dv, "slCurrency", "Currency", currency, true));
            //TemplateHandler.Assign(ref htmlContent, "slImageType", this.GetDataOptionBanner(dv, "slImageType", "ImageType", type, true));
            //TemplateHandler.Assign(ref htmlContent, "slSize", this.GetDataOptionBanner(dv, "slSize", "Size", size, true));

            /* Generate TestLinkList */
            //dv.RowFilter = "type='Language'";
            //DataTable dt = dv.ToTable();


            TemplateHandler.ReplaceBlock(ref htmlContent, "TextLinkList", AffCommon.GetTextLink(domain, AppSettings.BaseLanguages, targetRequest));

            TemplateHandler.ReplaceBlock(ref htmlContent, "Content",
                GetBannerList(paramCategory, lang, imagetype, size,
                currency, pageIndex, pageSize, ref totalpage, domain, targetRequest, AffSession.AffType));
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


        

        string GetBannerList(int category, string language,
            int imageType, int size, int currency, int pageIndex, int pagesize, ref int totalpage,
            string domain, string target, int type)
        {
            //string tmpLanguage = (language == "English (Europe)" ? "English" : language);//English Euro use English banner

            DbResult result = null;
            int nRows = 0;
            result = result = Banner.GetBannerList(category, language, imageType, size, currency, pageIndex, pagesize, type);

            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            //int pagesize = AppSettings.PageSize;
            int cols = 9;

            if (nRows > 0)
            {
                //string temp = (type == 1)//For normal template
                //    ? TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm")
                //    : TemplateHandler.LoadTemplate("UserInfo/MediaBanner.htm");

                string temp =TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 3;
                string signUpLink = AppSettings.LinkFormat.ToString();
                
              //  string src = ConfigurationManager.AppSettings["MediaUrl"].ToString();

                TemplateHandler.Assign(ref signUpLink, "{domain}", domain);
                TemplateHandler.Assign(ref signUpLink, "{target}", target);
                TemplateHandler.Assign(ref signUpLink, "{custid}", AppSession.CustID.ToString());

                string flContent = HttpContext.Current.Server.HtmlEncode(TemplateHandler.LoadTemplate("UserInfo/FlashCode.htm"));
                TemplateHandler.Assign(ref flContent, "src", AppSettings.BannerDomain + "SwfVersDetection.js" );

                foreach (DataRow dr in dtbResult.Rows)
                {
                    string rowText = content;
                    string aSignUpLink = signUpLink;
                    string tmpSrc = AppSettings.BannerDomain + dr["content"].ToString();
                    string[] strSize = dr["size"].ToString().Split(new char[] { 'x' });

                    counter++;
                    TemplateHandler.Assign(ref aSignUpLink, "lang", dr["languagecode"].ToString());
                    if (CastAide.AsString(dr["imagetype"],"Image").Equals("Flash",StringComparison.OrdinalIgnoreCase))
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "ImageContent");
                        string flashContent = flContent;
                        TemplateHandler.Assign(ref rowText, "FlashContent_val", flashContent);
                        TemplateHandler.Assign(ref rowText, "custid_val", AppSession.CustID.ToString());
                        TemplateHandler.Assign(ref rowText, "srcnoswf_val", tmpSrc.Replace(".swf", string.Empty));
                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "FlashContent");
                        //TemplateHandler.Assign(ref rowText, "imagesource", AppSettings.BannerDomain + dr["content"]);
                        
                    }
                    TemplateHandler.Assign(ref rowText, "SystemName", AppSettings.SystemName);
                    
                    TemplateHandler.Assign(ref rowText, "src_val", AppSettings.BannerDomain + dr["content"]);//tmpSrc);
                    TemplateHandler.Assign(ref rowText, "signuplink", aSignUpLink);
                    TemplateHandler.Assign(ref rowText, "bannerscripturl", AppSettings.BannerScriptUrl);
                    TemplateHandler.Assign(ref rowText, "bannerid", dr["bannerid"]);
                    TemplateHandler.Assign(ref rowText, "width_val", strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height_val", strSize[1]);
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

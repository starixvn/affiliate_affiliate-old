namespace Fanex.Web.Bwinning.Admin.Media
{
    using System;
    using System.Linq;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.Model;
    using Fanex.Affiliate.Resources;

    public partial class Listbanner : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var temp = TemplateHandler.LoadTemplate("Media/MainListBanner.htm");

            AppCommon.AssignToAdminTemplate(ref temp,
            "Files", "Category", "SubCategory", "Delete", "Add",
            "Language", "Currency", "Type", "Size", "AffType",
            "AddBanner", "ListBanner", "ConfirmDelBanner", 
            "ErrFileExtension", "Images", "Filename", "ImageType", "Edit","TotalRequest","LastRequest");

            var cate = CastAide.AsInt32(Request["cateoption"], 0);
            var lang = (Request["langoption"] == null || Request["langoption"].ToString() == "All") ? "" : Request["langoption"].ToString();
            var imgtype = CastAide.AsInt32(Request["typeoption"]);
            var size = CastAide.AsInt32(Request["sizeoption"]);
            //var currency = (Request["currencyoption"] == null || Request["currencyoption"].ToString() == "All") ? 0 : CastAide.AsInt32(Request["currencyoption"],0);
            var currency = CastAide.AsInt32(Request["currencyoption"]);
            var pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            var currPage = CastAide.AsInt32(Request["page"], 1);
            var afftype = CastAide.AsInt32(Request["afftypeoption"], 1);
            
            
            int subCategory = 0;

            int paramCategory = cate;
            if (cate > 0)
            {
                var banner = BannerType.Select(AppSettings.BaseBannerCategories, cate.ToString());
                
                if (banner!=null && banner.HasChildren)
                {
                    subCategory = CastAide.AsInt32(Request["slSubCategoryMedia"], 0);
                    if(subCategory>0)
                    paramCategory = subCategory;

                    TemplateHandler.Assign(ref temp, "optionsubcate",
          HtmlHelper.DropDownList(BannerType.GetSubCategories(cate), "slSubCategoryMedia", subCategory, true));
                    TemplateHandler.Assign(ref temp, "subcateStyle", String.Empty);
                }else
                    TemplateHandler.Assign(ref temp, "subcateStyle", "display: none");
            }else
                TemplateHandler.Assign(ref temp, "subcateStyle", "display: none");

            TemplateHandler.Assign(ref temp, "optioncate",
                HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "cateoption", cate, true));            
            TemplateHandler.Assign(ref temp, "optionlang",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "langoption", lang, true));            
            TemplateHandler.Assign(ref temp, "optioncurrency",
                HtmlHelper.DropDownList(AppSettings.AffPlayerCurrencies, "currencyoption", currency, true));
            TemplateHandler.Assign(ref temp, "optiontype",
                HtmlHelper.DropDownList(AppSettings.BaseBannerTypes, "typeoption", imgtype, true));  //AdminCommon.GetDataOptionBanner(dv, "typeoption", "MediaFileType", imgtype, true, false));
            TemplateHandler.Assign(ref temp, "optionsize",

                
                HtmlHelper.DropDownList(AppSettings.BaseBannerSizes, "sizeoption", size, true));

            if (AppSettings.BaseAffTypes.Count > 1)
                TemplateHandler.Assign(ref temp, "optionafftype",
                    HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypeoption", afftype, false));
            else
                TemplateHandler.RemoveBlock(ref temp, "afftype");
                        
            int minNo = 0, maxNo = 0, totalBanner = 0, totalPage = 0;
            TemplateHandler.ReplaceBlock(ref temp, "Content", 
                Fanex.Affiliate.Core.Admin.GenerateHTML.Banner.GetBannerList(
                paramCategory, lang, imgtype, size, currency, currPage, 
                ref minNo, ref maxNo, ref totalBanner, ref totalPage, 
                pageSize,afftype));


            if (totalBanner > 0)
            {
                TemplateHandler.Assign(ref temp, "paging-top", AdminCommon.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageTop", "txtPageTop", 
                    new string[] { Admin.Delete, Admin.Add }, new string[] { "DeleteSelectedMedia();",
                                                                            "javascript:AddBanner();" }, 1200, true, true, true, false));
                TemplateHandler.Assign(ref temp, "paging-bottom", AdminCommon.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageBottom", "txtPageBottom", 1200, true, false));
            }
            else
            {
                TemplateHandler.Assign(ref temp, "paging-top", "");
                TemplateHandler.Assign(ref temp, "paging-bottom", "");
            }
            TemplateHandler.Assign(ref temp, "submenu", SubMenu.GenSubMenu(
                new[] { "ListBanner.aspx", "Listtext.aspx" },
                new string[] { Admin.ListBanner, Admin.TextList }, 0, totalBanner,
                Fanex.Affiliate.Resources.Translator.Get("MediaGallery"), "i_media1.png"));
            TemplateHandler.Assign(ref temp, "totalMedia-val", totalBanner);
            TemplateHandler.Assign(ref temp, "minNo", minNo.ToString());
            TemplateHandler.Assign(ref temp, "maxNo", maxNo.ToString());
            TemplateHandler.Assign(ref temp, "page-val", currPage.ToString());            
            HtmlContent = temp;
        }
    }
}
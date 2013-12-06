namespace Fanex.Web.LiveBet365.AffiliateAdmin.Media
{
    using System;
    using DNAide.Core;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core.Helper;


    public partial class Listtext : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");            
            
            AppCommon.AssignToTemplate(ref temp,
            "Category", "Content", "Language", "Currency", "Action", 
            "Delete", "File", "Delete", "Add","ConfirmDelete", "ErrFileExtension");

            var cate = CastAide.AsInt32(Request["cateoption"], 0);
            var lang = (Request["langoption"] == null || Request["langoption"].ToString() == "All") ? "" : Request["langoption"].ToString();
            var currency = CastAide.AsInt32(Request["currencyoption"], 0);
            int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int currPage = CastAide.AsInt32(Request["page"], 1);

            TemplateHandler.Assign(ref temp, "optioncate",
              HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "cateoption", cate, true));            
            TemplateHandler.Assign(ref temp, "optionlang",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "langoption", lang, true));            
            TemplateHandler.Assign(ref temp, "optioncurrency",
                HtmlHelper.DropDownList(AppSettings.AllCurrencies, "currencyoption", currency, true));



            int minNo = 0, maxNo = 0, totalBanner = 0, totalPage = 0;
            TemplateHandler.ReplaceBlock(ref temp, "Content", 
                Fanex.Affiliate.Core.Admin.GenerateHTML.Banner.GetListText(
                cate, lang, currency, currPage, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize));
            if (totalBanner > 0)
            {

                TemplateHandler.Assign(ref temp, "paging-top", AdminCommon.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageTop", "txtPageTop", new string[] { "Delete", "Add" }, new string[] { "DeleteSelectedText();",
                                                                            "javascript:DoCal('AddText.aspx', 'Add Text', 460, 400);" }, 1200, true, true, true, false));
                TemplateHandler.Assign(ref temp, "paging-bottom", AdminCommon.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageBottom", "txtPageBottom", 1200, true, false));
            }
            else
            {
                TemplateHandler.Assign(ref temp, "paging-top", "");
                TemplateHandler.Assign(ref temp, "paging-bottom", "");
            }
            TemplateHandler.Assign(ref temp, "submenu", SubMenu.GenSubMenu(
               new[] { "ListBanner.aspx", "Listtext.aspx" },
               new string[] { "Banner List", "Text List" }, 1, totalBanner,
               Fanex.Affiliate.Resources.Translator.Get("MediaGallery"), "i_media1.png"));

            TemplateHandler.Assign(ref temp, "minNo", minNo.ToString());
            TemplateHandler.Assign(ref temp, "maxNo", maxNo.ToString());
            TemplateHandler.Assign(ref temp, "page-val", currPage.ToString());

            this.HtmlContent = temp;
        }
    }
}
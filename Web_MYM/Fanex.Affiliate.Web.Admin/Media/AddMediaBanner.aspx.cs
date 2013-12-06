namespace Fanex.Web.MYM.Admin.Media
{
    using System;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Helper;
    using DNAide.Data;
    using System.Data;
    using DNAide.Core;

    public partial class AddMediaBanner : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var bannertype = CastAide.AsInt16(Request["type"]);
            string temp = TemplateHandler.LoadTemplate("Media/MainAddMediaBanner.htm");
            //TemplateHandler.Assign(ref temp, "submenu", Common.GenSubMenu(new string[] { "ListBanner.aspx", "" }, new string[] { Resources.Lang.ListBanner, Resources.Lang.AddBanner }, 1000, 1));
            TemplateHandler.Assign(ref temp, "bannertype", bannertype);
            TemplateHandler.Assign(ref temp, "formAction", "../Handler/AddNewBannerV2.ashx");
            AppCommon.AssignToAdminTemplate(ref temp,
            "FileName",
            "AddBanner",
            "ListBanner",
            "File",
            "Category",
            "SubCategory",
            "Language",
            "Currency",
            "Type",
            "AffType",
            "Size",
             "ErrFileExtension","ValidTypes","Upload","Cancel","MaxFileNameLength","FileSizeLimit");


            int bannerID = CastAide.AsInt32(Context.Request["editBannerID"], 0);
            if (bannerID > 0) // edit
            {
                DbResult result = Fanex.Affiliate.DataObjects.Banner.GetBannerDetail(bannerID);

                DataRow dr = result.ResultSet.Tables[0].Rows[0];

                var category = CastAide.AsInt32(dr["category"], 0);

                var parentid = CastAide.AsInt32(dr["parentid"], 0);
                if (parentid > 0)
                {
                    TemplateHandler.Assign(ref temp, "optioncate",
                        HtmlHelper.DropDownList(AppSettings.BaseBannerCategories,
                            "slCategory", parentid, false));
                    TemplateHandler.Assign(ref temp, "subCategory", category);
                }
                else
                {
                    TemplateHandler.Assign(ref temp, "optioncate",
                        HtmlHelper.DropDownList(AppSettings.BaseBannerCategories,
                        "slCategory", category, false));

                    TemplateHandler.Assign(ref temp, "subCategory", "None");
                }
            }
            TemplateHandler.Assign(ref temp, "optionafftype",
                HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "slAffType", bannertype, false,null,AppSettings.BaseAffTypes.Count <2));
            TemplateHandler.Assign(ref temp, "optioncate",
                HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "slCategory", null, false));
            TemplateHandler.Assign(ref temp, "optionlang",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", null, false));
            TemplateHandler.Assign(ref temp, "optioncurrency",
                HtmlHelper.DropDownList(AppSettings.AffPlayerCurrencies, "slCurrency", null, false));
            TemplateHandler.Assign(ref temp, "optiontype",
                HtmlHelper.DropDownList(AppSettings.BaseBannerTypes, "slImageType", null, false, null, true));

            TemplateHandler.Assign(ref temp, "filenamevalue", string.Empty);

            this.HtmlContent = temp;

        }
    }
}

namespace Fanex.Web.Bwinning.Admin.Media
{
    using System;
    using System.Data;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Helper;

    public partial class EditMediaBanner : BasePageTpl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int bannerID = CastAide.AsInt32(Context.Request["editBannerID"],0);
            DbResult result = Fanex.Affiliate.DataObjects.Banner.GetBannerDetail(bannerID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainAddMediaBanner.htm");
                TemplateHandler.Assign(ref temp, "formAction", "../Handler/UpdateMediaBanner.ashx");
                AppCommon.AssignToAdminTemplate(ref temp,
                    "FileName",
                    "AddBanner",
                    "File",
                    "Category",
                    "SubCategory",
                    "Language",
                    "Currency",
                    "Type",
                    "Size",
                    "AffType","ErrFileExtension","ValidTypes","Upload","Cancel","FileSizeLimit");

                DataRow dr = result.ResultSet.Tables[0].Rows[0];

                var category = CastAide.AsInt32(dr["category"], 0);                
                
                var parentid = CastAide.AsInt32(dr["parentid"],0);
                if(parentid > 0)
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

                TemplateHandler.Assign(ref temp, "optionafftype",
                HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "slAffType", dr["type"], false, null, AppSettings.BaseAffTypes.Count < 2));
                TemplateHandler.Assign(ref temp, "optionlang", 
                        HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", dr["language"].ToString(), false));                    
                TemplateHandler.Assign(ref temp, "optioncurrency",
                    HtmlHelper.DropDownList(AppSettings.AllCurrencies, "slCurrency", dr["currency"].ToString(), false));                    
                TemplateHandler.Assign(ref temp, "optiontype",
                    HtmlHelper.DropDownList(AppSettings.BaseBannerTypes, "slImageType", dr["imagetype"].ToString(),false,null,true));               
                TemplateHandler.Assign(ref temp, "bannerid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref temp, "oldfilenamevalue", dr["content"].ToString());
                string fileNameValue = dr["content"].ToString();
                int idxTemp=fileNameValue.LastIndexOf("/") + 1;
                fileNameValue = fileNameValue.Substring(idxTemp, fileNameValue.Length - idxTemp);
                TemplateHandler.Assign(ref temp, "filenamevalue", fileNameValue);
                TemplateHandler.Assign(ref temp, "size", dr["size"].ToString());
                TemplateHandler.Assign(ref temp, "bannertype", dr["type"].ToString());
                TemplateHandler.Assign(ref temp, "oldCategory", category);
                TemplateHandler.Assign(ref temp, "imagetype", dr["imagetype"]);
                this.HtmlContent = temp;
            }
        }
    }
}
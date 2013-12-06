namespace Fanex.Web.Aoncash.Admin.Media
{
    using System;
    using System.Data;
    using DNAide.Web;    
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;
    using DNAide.Data;
    using DNAide.Core;

    public partial class Media_AddText : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //-------------------------

            string temp = TemplateHandler.LoadTemplate("Media/MainAddText.htm");

            AppCommon.AssignToAdminTemplate(ref temp,
            "Text", "Category", "Language", "Currency", "Type", "ErrFileExtension", "Submit", "Add", "Cancel");

            var content = String.Empty;
            var language = String.Empty;
            var currency = String.Empty;
            var category = String.Empty;

            var bannertype = CastAide.AsInt16(Request["type"]);
            int bannerID = CastAide.AsInt32(Context.Request["editBannerID"], 0);

            if (bannerID > 0)
            {

                DbResult result = Fanex.Affiliate.DataObjects.Banner.GetBannerDetail(bannerID);
                if (result.ResultSet.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = result.ResultSet.Tables[0].Rows[0];

                    category = dr["category"].ToString();
                    content = dr["content"].ToString();
                    currency = dr["currency"].ToString();
                    language = dr["language"].ToString();

                    TemplateHandler.Assign(ref temp, "submit", "Update");
                }
            }

            TemplateHandler.Assign(ref temp, "bannerid", bannerID > 0 ? bannerID.ToString() : String.Empty);
            TemplateHandler.Assign(ref temp, "submit", "Add");
            TemplateHandler.Assign(ref temp, "content", content);            
            TemplateHandler.Assign(ref temp, "optioncate",
                    HtmlHelper.DropDownList(AppSettings.BaseBannerCategories, "slCategory", category, false));
            TemplateHandler.Assign(ref temp, "optionlang",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", language, false));
            TemplateHandler.Assign(ref temp, "optioncurrency",
                HtmlHelper.DropDownList(AppSettings.AffPlayerCurrencies, "slCurrency", currency, false));


            this.HtmlContent = temp;
        }
    }
}
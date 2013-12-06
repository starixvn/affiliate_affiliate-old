namespace Fanex.Web.Macaubet.Admin.Media{

    using System;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;

    public partial class BannerDetail : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string content = TemplateHandler.LoadTemplate("Media/BannerDetail.htm");


            //get parameters from request
            string imagevalue = CastAide.AsString(Request["imagevalue"], string.Empty);
            string version = CastAide.AsString(Request["version"], string.Empty);
            int width = CastAide.AsInt32(Request["width"], 100);
            int height = CastAide.AsInt32(Request["height"], 50);
            string bannertype = CastAide.AsString(Request["bannertype"], "image");

            if (bannertype == "image")
                TemplateHandler.RemoveBlock(ref content, "limit-fls");
            else
                TemplateHandler.RemoveBlock(ref content, "limit-img");

            TemplateHandler.Assign(ref content, "imagevalue", AppSettings.BannerDomain + imagevalue);
            TemplateHandler.Assign(ref content, "version", version);
            TemplateHandler.Assign(ref content, "width", width);
            TemplateHandler.Assign(ref content, "height", height);

            this.HtmlContent = content;

            
        }
    }
}

namespace Fanex.Web.LiveBet365.Customer.Handler
{
    using System.Web;
    using DNAide.Core;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;
    using System.Data;
    using DNAide.Web;
    public class GetSubCategory : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                var category = CastAide.AsInt32(context.Request["category"], 0);
                bool genAll = CastAide.AsBoolean(context.Request["genAll"],false);
                string selectedSubCategory = context.Request["activeValue"];
                if (selectedSubCategory == "None")
                    selectedSubCategory = string.Empty;


                context.Response.Write(
                    HtmlHelper.DropDownList(BannerType.GetSubCategories(category), "slSubCategoryMedia", selectedSubCategory, genAll));


            }         
        }
     
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}

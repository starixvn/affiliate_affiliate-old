namespace Fanex.Web.i668bet.Admin.Handler
{
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Model;
    public class GetDataBanner : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                var category = CastAide.AsInt32(context.Request["cate"], 0);
                string lang = (context.Request["lang"].Equals("")) ? null : context.Request["lang"];
                string currency = (context.Request["currency"].Equals("")) ? null : context.Request["currency"];
                string type = (context.Request["type"].Equals("")) ? null : context.Request["type"];
                string size = (context.Request["size"].Equals("")) ? null : context.Request["size"];
                int page = string.IsNullOrEmpty(context.Request["page"]) ? 1 : CastAide.AsInt32(context.Request["page"]);
                int pageSize = (context.Request["PageSize"].Equals("")) ? 20 : CastAide.AsInt32(context.Request["PageSize"]);
                int minNo = 0, maxNo = 0, totalPage=0, totalBanner=0;
                StringBuilder result = new StringBuilder();
                // tam thoi type=0 <=> normal banner
                int bannerType = CastAide.AsInt32(context.Request.Params["bannerType"], 0);
                bool isNewCateStruct = CastAide.AsBoolean(context.Request.Params["isNewCateStruct"], false);
                string content = string.Empty;
                var subcategory = 0;

                if (isNewCateStruct)
                {
                    bool isSearchByParentCategory = false;
                    int paramCategory = category;

                    var cat = AppSettings.BaseBannerCategories.Where(x => x.Id == category.ToString()).Cast<BannerTypeObj>().FirstOrDefault();

                    if (cat!=null && cat.HasChildren)
                    {
                        if (string.IsNullOrEmpty(context.Request["subCategory"]) || context.Request["subCategory"].ToString() == "All")
                            isSearchByParentCategory = true;
                        else
                        {
                            subcategory = CastAide.AsInt32(context.Request["subCategory"], 0);
                            paramCategory = subcategory;
                        }
                    }
                    //content = Fanex.Affiliate.Core.Admin.GenerateHTML.Banner.GetBannerList(category, lang, type, size, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, bannerType);
                }
                else
                    //content = Fanex.Affiliate.Core.Admin.GenerateHTML.Banner.GetBannerList(category, lang, type, size, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, bannerType);

                result.Append(minNo.ToString());
                result.Append("$$$");
                result.Append(maxNo.ToString());
                result.Append("$$$");
                result.Append(content);

                context.Response.Write(result.ToString());
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

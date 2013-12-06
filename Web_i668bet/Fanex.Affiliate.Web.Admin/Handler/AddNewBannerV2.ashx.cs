namespace Fanex.Web.i668bet.Admin.Handler
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Core;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Core.Culture;
    using Fanex.Core.Utility;

    public class AddNewBannerV2 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/html";
                string newfilename = context.Request.Files[0].FileName;
                var category = CastAide.AsInt32(context.Request.Params["slCategory"], 0);
                var subcategoryName = CastAide.AsString(context.Request.Form["hidSubCategory"], String.Empty);

                if (subcategoryName == "null")
                    subcategoryName = null;

                var subcategory = !String.IsNullOrEmpty(subcategoryName) ? CastAide.AsInt32(context.Request.Params["slSubCategoryMedia"], 0) : 0;
                var language = context.Request.Params["slLanguage"];
                var currency = CastAide.AsInt32(context.Request.Params["slCurrency"], 0);
                var imageType = CastAide.AsInt32(context.Request.Params["hdImageType"], 0);
                var fileName = context.Request.Params["inpFileName"];
                int afftype = CastAide.AsInt32(context.Request.Params["slAffType"], CastAide.AsInt16(AppSettings.BaseAffTypes.First().Id,1));
                var afftypename = AppSettings.BaseAffTypes.Where(x => x.Id == afftype.ToString()).First().Value;
                string ext = string.Empty;
                if (!AdminCommon.IsValidExtension(newfilename, ref ext))
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("errFileExtension") + "\");</script>");
                    return;
                }

                if (!String.IsNullOrEmpty(ext))
                {
                    StringBuilder tempSB = new StringBuilder();
                    int temp = fileName.LastIndexOf(".");
                    if (temp > 0)
                        tempSB.Append(fileName.Substring(0, temp));
                    else
                        tempSB.Append(fileName);

                    tempSB.Append(".");
                    tempSB.Append(ext);

                    fileName = tempSB.ToString();
                }
                else
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("errFileExtension") + "\");</script>");
                    return;
                }


                string err = string.Empty;
                int errCode = 0;


                if (!string.IsNullOrEmpty(err))
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                    return;
                }

                var banner = BannerType.Select(AppSettings.BaseBannerCategories, category.ToString());
                var cateName = banner == null ? String.Empty : banner.Value;

                string size = string.Empty, strDst = string.Empty;

                var folder = afftypename + "/" + cateName + (String.IsNullOrEmpty(subcategoryName) ? String.Empty : "/" + subcategoryName);

                try
                {

                    AdminCommon.UploadFile(context, folder, AppSettings.BannerDirectory, ref errCode, fileName, out size, ref strDst);
                }
                catch (UnauthorizedAccessException)
                {
                    errCode = 99;
                    err = "Access to the Banner folder is denied.";
                }
                catch (Exception ex)
                {
                    errCode = 99;
                    err = ex.Message.Replace('\\', '/');

                }
                if (errCode == 0)
                {
                    int bannerID = Banner.InsertBanner(folder + "/" + fileName, subcategory == 0 ? category : subcategory, language, currency, imageType, size, afftype, ref errCode, ref err);

                    if (errCode != 0)
                    {

                        //Delete image uploaded
                        if (!string.IsNullOrEmpty(strDst))
                            Common.DeleteFile(strDst);
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                        return;
                    }
                    AppSettings.RefreshBaseBannerSizes();
                }
                if (errCode == 1)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgFileEmpty") + "');</script>");
                    return;
                }
                else if (errCode == 2)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgFile") + "');</script>");
                    return;
                }
                else if (errCode == 3)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgSizeFile") + "');</script>");
                    return;
                }
                else if (errCode == 99)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + err + "\");</script>");
                    return;
                }

                context.Response.Write("<script type=\"text/javascript\">alert('" + Fanex.Affiliate.Resources.Translator.Get("AddBannerSuccessful") + "');parent.ClosePopup();</script>");
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

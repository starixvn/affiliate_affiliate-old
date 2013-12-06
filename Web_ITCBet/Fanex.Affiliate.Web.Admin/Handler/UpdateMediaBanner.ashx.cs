using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Fanex.Core.Culture;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Model;
using DNAide.Core;

namespace Fanex.Web.ITCBet.Admin.Handler
{
    /// <summary>
    /// Summary description for GetEditedBannerText
    /// </summary>
    public class UpdateMediaBanner : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                try
                {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/html";
                string newfilename = context.Request.Files[0].FileName;
                
                string inputFileName = context.Request.Params["inpFileName"];
                var category = CastAide.AsInt32(context.Request.Params["slCategory"],0);
                var subcategoryName = CastAide.AsString(context.Request.Form["hidSubCategory"], String.Empty);

                if (subcategoryName == "null")
                    subcategoryName = null;

                var subCategory = !String.IsNullOrEmpty(subcategoryName) ? CastAide.AsInt32(context.Request.Params["slSubCategoryMedia"],0) : 0;
                var language = context.Request.Params["slLanguage"];
                var currency = CastAide.AsInt32(context.Request.Params["slCurrency"],0);
                var imageType = CastAide.AsInt32(context.Request.Params["hdImageType"],0);
                var size = CastAide.AsInt32(context.Request.Params["hdSize"],0);
                var idbanner = CastAide.AsInt32(context.Request.Params["hdBannerID"],0);
                var oldFileName = context.Request.Params["hdOldFileName"];
                var oldCategory = CastAide.AsInt32(context.Request.Params["hdOldCategory"], 0);
                bool needToUploadFile = false;
                int afftype = CastAide.AsInt32(context.Request.Params["slAffType"], CastAide.AsInt16(AppSettings.BaseAffTypes.First().Id, 1));
                var afftypename = AppSettings.BaseAffTypes.Where(x => x.Id == afftype.ToString()).First().Value;
                
                
                //Save DB Info
                string ext = string.Empty;
                if (!string.IsNullOrEmpty(newfilename))
                {
                    needToUploadFile = true;
                    if (!AdminCommon.IsValidExtension(newfilename, ref ext))
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("errFileExtension") + "\");</script>");
                        return;
                    }
                }
                else if(oldFileName.Contains("."))
                    ext = oldFileName.Substring(oldFileName.LastIndexOf(".") + 1, oldFileName.Length - oldFileName.LastIndexOf(".") - 1);

                if (string.IsNullOrEmpty(inputFileName))
                {
                    if (needToUploadFile)
                        inputFileName = newfilename;
                    else
                    {                        
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgFileNameEmpty") + "\");</script>");
                        return;
                    }
                }
                else if (!String.IsNullOrEmpty(ext))
                {
                    StringBuilder tempSB = new StringBuilder();
                    int temp = inputFileName.LastIndexOf(".");
                    if (temp > 0)
                        tempSB.Append(inputFileName.Substring(0, temp));
                    else
                        tempSB.Append(inputFileName);                    
                    
                    tempSB.Append(".");
                    tempSB.Append(ext);
                    
                    inputFileName = tempSB.ToString();
                }

                string err = string.Empty;
                int errCode = 0;
                
                string strDst = string.Empty;
                string newFolder = string.Empty;
                string path = AppSettings.BannerDirectory;

               

                var cat =  AppSettings.BaseBannerCategories.Where(x => x.Id == category.ToString()).Cast<BannerTypeObj>().FirstOrDefault();


                var banner = BannerType.Select(AppSettings.BaseBannerCategories, category.ToString());
                var cateName = banner == null ? String.Empty : banner.Value;


                var folder =afftypename + "/" + cateName + (String.IsNullOrEmpty(subcategoryName) ? String.Empty : "/" + subcategoryName);
                
                strDst = AppSettings.BannerDirectory + folder;

                if(!Directory.Exists(strDst))
                    Directory.CreateDirectory(strDst);

                 strDst += "/" + inputFileName;

                var newsize = String.Empty;
               
                if (needToUploadFile)
                {
                    //AdminCommon.UploadFileWithAutoDetectSize(context, newFolder, path, inputFileName, ref errCode, out newsize, ref strDst);
                    
                    AdminCommon.UploadFile(context, folder, AppSettings.BannerDirectory, ref errCode, inputFileName, out newsize, ref strDst);

                    if (errCode == 1)
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgFileNameEmpty") + "\");</script>");
                        return;
                    }
                    // File Extension fail
                    else if (errCode == 2)
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("errFileExtension") + "\");</script>");                        
                        return;
                    }
                    else if (errCode == 3)
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgSizeFile") + "\");</script>");                                                
                        return;
                    }
                }

                category = (subCategory == 0 ? category : subCategory);

                err = Banner.UpdateBanner(Convert.ToInt32(idbanner), folder + "/" + inputFileName, category, language, currency, imageType, newsize, afftype);
                if (!string.IsNullOrEmpty(err))
                {
                    //Delete image uploaded
                    if (!string.IsNullOrEmpty(strDst))
                        AdminCommon.DeleteFile(strDst);
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                    return;
                }
                AppSettings.RefreshBaseBannerSizes();
                string oldFullPath = AppSettings.BannerDirectory + oldFileName;

                if (!needToUploadFile)
                {
                    if (File.Exists(oldFullPath))
                    {
                        if (!category.Equals(oldCategory))
                        {
                            if (Directory.Exists(path + folder) == false)
                                Directory.CreateDirectory(path + folder);
                            if(File.Exists(strDst))
                                AdminCommon.DeleteFile(strDst);
                            File.Move(oldFullPath, strDst);
                        }else if(!oldFullPath.Equals(strDst,StringComparison.OrdinalIgnoreCase))
                            File.Move(oldFullPath, strDst);
                                                
                    }
                }
                else if (!oldFileName.Equals(folder +"/"+ newfilename))
                    AdminCommon.DeleteFile(oldFullPath);

                context.Response.Write("<script type=\"text/javascript\">alert('Update Banner Successfull');parent.Cancelfunction();</script>");
                 }
                catch (Exception ex)
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + ex.Message + "\");</script>");
                    return;
                }
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

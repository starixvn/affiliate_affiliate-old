//namespace Fanex.Web.i668bet.Admin.Handler
//{
//    using System;
//    using System.Web;
//    using System.Web.SessionState;
//    using Fanex.Core.Culture;
//    using Fanex.Affiliate.Core.Admin;
//    using Fanex.Core.Utility;
//    using Fanex.Affiliate.DataObjects;    
//    using DNAide.Core;
//    public class UpBanner : IHttpHandler, IRequiresSessionState
//    {

//        public void ProcessRequest(HttpContext context)
//        {
//            try
//            {
//                if (context.Request.UrlReferrer != null)
//                {
//                    CultureHandler.InitializeCulture(HttpContext.Current);
//                    context.Response.ContentType = "text/html";
//                    var idbanner = context.Request.Params["hdBannerID"];
//                    string newfilename = context.Request.Files[0].FileName;
//                    string oldfilename = context.Request.Params["hdOldFileName"];
//                    string category = context.Request.Params["slCategory"];
//                    string language = context.Request.Params["slLanguage"];
//                    int currency = CastAide.AsInt32(context.Request.Params["slCurrency"], 1);
//                    string imageType = context.Request.Params["slImageType"];
//                    //string size = context.Request.Params["slSize"];
//                    bool needToUploadFile = false;


//                    string size = string.Empty;

//                    //Save DB Info
//                    if (!string.IsNullOrEmpty(newfilename))
//                    {
//                        needToUploadFile = true;
//                        string ext = string.Empty;
//                        if (!AdminCommon.IsValidExtension(newfilename, ref ext))
//                        {
//                            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("errFileExtension") + "\");</script>");
//                            return;
//                        }

//                        newfilename = category + "/" + idbanner + "." + ext;
//                    }
//                    newfilename = string.IsNullOrEmpty(newfilename) ? oldfilename : newfilename;

//                    //Upload new file
//                    string path = context.Server.MapPath("../") + "Banner//";
//                    string strDst = string.Empty;
//                    if (needToUploadFile)
//                    {

//                        //Common.DeleteFile(path + oldfilename);
//                        int errcode = 0;
//                        AdminCommon.UploadFile(context, category, path, ref errcode, idbanner, out size, ref strDst);

//                        if (errcode == 1)
//                        {
//                            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgFileEmpty") + "');</script>");
//                            return;
//                        }
//                        else if (errcode == 2)
//                        {
//                            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgFile") + "');</script>");
//                            return;
//                        }
//                        else if (errcode == 3)
//                        {
//                            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Fanex.Affiliate.Resources.Translator.Get("ErrmsgSizeFile") + "');</script>");
//                            return;
//                        }

//                    }

//                    string err = DataObjects.Banner.UpdateBanner(Convert.ToInt32(idbanner), newfilename, category, language, currency, imageType, size);

//                    if (needToUploadFile)
//                    {
//                        if (!string.IsNullOrEmpty(err))
//                            Common.DeleteFile(strDst);
//                        else
//                            Common.DeleteFile(path + oldfilename);
//                    }
//                    if (!string.IsNullOrEmpty(err))
//                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
//                    else
//                        context.Response.Write("<script type=\"text/javascript\">parent.UpdateSucc('" + Fanex.Affiliate.Resources.Translator.Get("UpdateSucc") + "');</script>");
//                }
//            }
//            catch (Exception ex)
//            {
//                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
//            }

//        }

//        public bool IsReusable
//        {
//            get
//            {
//                return false;
//            }
//        }

//    }
//}

namespace Fanex.Web.i668bet.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Core.Utility;
    //using System.Transactions;
    public class DelBanner : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {
                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string[] param = context.Request.Params["params"].Split(new string[] { "$$$" }, StringSplitOptions.None);
                string listIdBanner = string.Empty, listFilename = string.Empty;

                string result = ValidateData(param, ref listIdBanner, ref listFilename);
                if (!string.IsNullOrEmpty(result))
                {
                    context.Response.Write(result);
                    return;
                }
                //Drop a comma at the end of message
                string parentFolder = context.Server.MapPath("../") + "Banner\\";
                string[] files = listFilename.Split(new char[] { ',' });
                /* Transaction oldAmbient = Transaction.Current;
                 CommittableTransaction committableTransaction = new CommittableTransaction();
                 Transaction.Current = committableTransaction;*/
                //Delete in DB
                result = Banner.DelDataBanner(listIdBanner, Fanex.Affiliate.Core.AppSession.Language);
                if (result.ToString() == string.Empty)
                {
                    try
                    {
                        //Delete files first
                        for (int i = 0; i < files.Length; i++)
                        {
                            Common.DeleteFile(parentFolder + files[i].Replace("/", "\\"));

                        }

                        /* if (!string.IsNullOrEmpty(result))
                         {
                             committableTransaction.Rollback();
                         }
                         else
                         {
                             committableTransaction.Commit();
                         }*/
                    }
                    catch (Exception e)
                    {
                        /*  committableTransaction.Rollback(e);*/
                        Fanex.Affiliate.Core.AppCommon.SendEmail(e);
#if DEBUG
                        throw;

#else
                    DNAide.Web.DNAideLog.Write(e.ToString());
#endif

                    }
                }
                /*finally
                {
                    //Restore the ambient transaction 
                    Transaction.Current = oldAmbient;
                }*/
                context.Response.Write(result);
            }

        }
        private string ValidateData(string[] param, ref string bannerIDList, ref string filenameList)
        {

            try
            {
                if (param.Length == 2)
                {
                    bannerIDList = param[0].Trim();
                    bannerIDList = bannerIDList.Substring(0, bannerIDList.Length - 1);
                    filenameList = param[1].Trim();
                    filenameList = filenameList.Substring(0, filenameList.Length - 1);
                }
                else
                {
                    return "Invalid data sent to server!";
                }

            }
            catch (Exception e)
            {
                return e.Message;

            }
            return string.Empty;

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

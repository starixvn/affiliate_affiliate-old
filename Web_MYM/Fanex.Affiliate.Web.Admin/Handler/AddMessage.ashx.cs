namespace Fanex.Web.MYM.Admin.Handler
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using System.Text;
    using Fanex.Core.Culture;
    using Fanex.Affiliate.DataObjects;
    using DNAide.Core;

    public class AddMessage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.UrlReferrer != null)
            {

                CultureHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string t = HttpContext.Current.Server.UrlDecode(context.Request.Params["params"]);
                string[] param = t.Split(new string[] { "$$$" },StringSplitOptions.RemoveEmptyEntries);

                int important = 0;
                
                int.TryParse(context.Request.Params["important"].ToString(),out important);

                
                StringBuilder str = new StringBuilder("<root>"); //ValidateData(param, out important, ref msgEN, ref msgCN, ref msgTW, ref msgTH, ref msgES, ref msgVI, ref msgID);
                foreach (var item in param)
                {
                    var arr = item.Split('=');
                    var value = HttpUtility.HtmlEncode(arr[1].Trim());
                   
                    str.AppendFormat("<item><code>{0}</code><value>{1}</value></item>", arr[0], value);
                }
                str.Append("</root>");
                
                var message = String.Empty;
                var xml = str.ToString();
                if (!string.IsNullOrEmpty(xml))
                {
                    bool isupdate = context.Request.Params["id"]!=null;
                    if (isupdate)
                    {
                        int id = CastAide.AsInt32(context.Request.Params["id"].ToString(),0);
                        message = HotNews.UpdateHotNews(id, important, xml, Fanex.Affiliate.Core.AppSession.Language);
                    }
                    else
                    {
                        message = HotNews.AddHotNews(important, xml, Fanex.Affiliate.Core.AppSession.Language);
                    }
                }
                context.Response.Write(message);

            }

        }

        //private StringBuilder ValidateData(string[] param, out bool important, ref string msgEN, ref string msgCN, ref string msgTW, ref string msgTH, ref string msgES, ref string msgVI, ref string msgID)
        //{

        //    StringBuilder err = new StringBuilder("");
        //    important = false;
        //    try
        //    {
        //        if (param.Length == 8)
        //        {
        //            important = Convert.ToBoolean(param[0].Trim());
        //            msgEN = param[1].Trim();
        //            msgCN = param[2].Trim();
        //            msgTW = param[3].Trim();
        //            msgTH = param[4].Trim();
        //            msgES = param[5].Trim();
        //            msgVI = param[6].Trim();
        //            msgID = param[7].Trim();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return err.Append(e.Message);

        //    }
        //    return err;

        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}

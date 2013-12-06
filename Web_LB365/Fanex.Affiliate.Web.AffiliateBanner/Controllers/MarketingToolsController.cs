using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using DNAide.Core;
using DNAide.Data;
using Fanex.Affiliate.Core;
using Microsoft.Practices.EnterpriseLibrary.Caching;


namespace Fanex.Web.LiveBet365.AffiliateBanner.Controllers
{
    public class MarketingToolsController : Controller
    {
        private const int TEXT = 0;
        private const int IMAGE = 1;
        private const int FLASH = 2;

        private const string SWF_VERSION_DETECT_JS = "SwfVersDetection.js";
        private const string AFF_MEDIA_JS = "affiliate.media.js";
        private const string FLASH_JS = "flash.js";

        private static string[] FILE_NAMES = { "text", "image", "flash1" };
        private static Regex _regex = new Regex(@"\.swf",RegexOptions.IgnoreCase);
        //
        // GET: /Banner/

        public ActionResult Index()
        {
            return View();
        }
        public ContentResult GetScript(int id, string signuplink)
        {
            try
            {                                
                System.Text.StringBuilder sbContent = ReadFile(Server.MapPath("~/Scripts/" + AFF_MEDIA_JS));

                sbContent = sbContent.Replace("{requesttime}", ConfigurationManager.AppSettings["RequestTime"]);
                sbContent = sbContent.Replace("{url}", Common.FullDomainName + "/MarketingTools/Refresh");                
                sbContent = sbContent.Replace("{id}", id.ToString());
                sbContent = sbContent.Replace("{systemname}", AppSettings.SystemName);
                sbContent = sbContent.Replace("{afflink}",Server.UrlEncode(signuplink + "?ref="));

                return Content(sbContent.ToString(), "text/javascript");
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "text/plain");
            }
            finally
            {
                UpdateSatistic(id);
            }
        }

        public ContentResult Refresh(int id, string signuplink)
        {                        
            
            var row = GetFirstRow(id);
            if(row!=null)
            {
                var systemname = AppSettings.SystemName;

                    var content = row["content"].ToString();
                    var version = row["version"].ToString();
                    var type = CastAide.AsInt16(row["imagetype"], 0);
                    
                    if (!String.IsNullOrEmpty(content))
                    {
                        var script = new StringBuilder();
                        var filename = FILE_NAMES[type] + ".htm";
                        var sb = ReadFile(Server.MapPath("~/Content/templates/" + filename));
                        sb.Replace("{systemname}", systemname).Replace("{id}", id.ToString());
                        //sb.Replace(Environment.NewLine, String.Empty);
                        sb = new StringBuilder(MinifyHtml(sb.ToString()));
                        

                        sb.Replace("{afflink}", signuplink);

                        if (type == TEXT)
                        {
                            sb.Replace("{content}", content.Replace("\r\n", "<br/>"));
                        }
                        else if (type == IMAGE)
                        {
                            sb.Replace("{src}", String.Format("{0}/Content/Banners/{1}?v={2}", Common.FullDomainName, Server.HtmlDecode(content), version));
                        }
                        else // FLASH
                        {
                            var sizes = row["size"].ToString().Split('x');
                            var width = sizes[0];
                            var height = sizes[1];
                            
                            var bannerurl =  String.Format("{0}/Content/Banners/{1}?v={2}", Common.FullDomainName, Server.HtmlDecode(content), version);
                            sb.Replace("{src}", bannerurl);
                            sb.Replace("{width}", width);
                            sb.Replace("{height}", height);

                            var file = ReadFile(Server.MapPath("~/Scripts/" + SWF_VERSION_DETECT_JS));
                            script = ReadFile(Server.MapPath("~/Content/templates/" + FLASH_JS));

                            script.Replace("{width}", width);
                            script.Replace("{height}", height);
                            script.Replace("{afflink}", signuplink);
                            script.Replace("{srcnoswf}", _regex.Replace(bannerurl,String.Empty));
                            script.Replace("{systemname}", systemname);
                            script.Replace("{id}", id.ToString());
                            script.Insert(0, file);
                        }
                        
                        var str = sb.ToString();
                        
                        return Content(
                            @"function hasClass(element, cls) {
                                return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
                            }
                            function replaceHtml(tag,classname,html) {		
                              var els = document.getElementsByTagName(tag), i, flag = false;
                              for (i = 0; i < els.length; i++){ if (hasClass(els[i],classname)){ if(!flag) flag = true; els[i].innerHTML = html;}}
                              return flag;
                            }
                            "
                            + "var content = \""+ str+"\";" +
                             "var r = replaceHtml('a','banner_" + systemname+id + "',content); \n" +                         
                            " if(!r) {var el = document.getElementById('bannerContainer'); if(el) el.innerHTML =content;} \n" + script,
                               "text/javascript");
                    }
            }
            return Content("");
        }
        string GetCacheData(string key)
        {
            var obj = CacheFactory.GetCacheManager().GetData(key);
            if (obj != null)
                return obj.ToString();

            return null;
        }
        void SetCacheData(string key, string data)
        {
            CacheFactory.GetCacheManager().Add(key, data);
        }
        string MinifyHtml(string html)
        {
            html = Regex.Replace(html, @"\n|\t", " ");
            html = Regex.Replace(html, @">\s+<", "><").Trim();
            html = Regex.Replace(html, @"\s{2,}", " ");
            return html;
        }
        //public static string GetContent(int id)
        //{
        //    var row = GetFirstRow(id);
            
        //    if (row!=null)
        //    {
        //        return HttpUtility.HtmlDecode(row["content"].ToString().Replace("\r\n", "<br/>"));
        //    }            
        //    return String.Empty;
        //}
        public void UpdateSatistic(int id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("bannerid", id);
            DbHelper.Execute("Aff_BannerUpdateStatistic", hsParam, "affiliate");
        }
        public DataRow GetFirstRow(int id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("bannerid", id);
            DbResult result = DbHelper.Execute("Aff_BannerSelInfo", hsParam, "affiliate");

            if (result.ResultSet.Tables.Count > 0)
            {
                DataTable dtbResult = result.ResultSet.Tables[0];
                if (dtbResult.Rows.Count > 0)
                {
                    return dtbResult.Rows[0];
                }
            }
            
            return null;
        }
        public System.Text.StringBuilder ReadFile(string fileScript)
        {
            var str = GetCacheData(fileScript);
            if (str != null)
                return new StringBuilder(str);
            StringBuilder sbContent = new StringBuilder();
            
            using (var fileReader = System.IO.File.OpenText(fileScript))
            {
                sbContent.Append(fileReader.ReadToEnd());
                fileReader.Close();
            }
            SetCacheData(fileScript, sbContent.ToString());
            return sbContent;
        }

    }
}

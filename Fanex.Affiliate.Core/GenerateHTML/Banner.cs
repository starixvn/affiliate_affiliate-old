using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Configuration;
using System.Web;
using Fanex.IndoPaul.DataObjects;
namespace Fanex.IndoPaul.Core.Affiliate.GenerateHTML
{
    /// <summary>
    /// Generate content for Banner
    /// Written by : Tong Hoang Dung on 24/03/2009
    /// </summary>
    public class Banner
    {
        static string Eu = "eneuro";
        static string Es = "es";
        public static string GetBannerLang(string value)
        {

            switch (value.ToLower())
            {
                case "bonus": return Resources.Lang.Bonus;
                case "tennis": return Resources.Lang.Tennis;
                case "football": return Resources.Lang.Football;
                case "poker": return Resources.Lang.Poker;
                case "image": return Resources.Lang.Image;
                case "flash": return Resources.Lang.Flash;
                case "english": return Resources.Lang.English;
                case "chinese traditional": return Resources.Lang.ChineseTraditio;
                case "chinese simplified": return Resources.Lang.ChineseSimplifi;
                case "thai": return Resources.Lang.Thai;
                case "spanish": return Resources.Lang.Spanish;
                case "indonesian": return Resources.Lang.Indonesian;
                case "vietnamese": return Resources.Lang.Vietnamese;
                case "english (europe)": return Resources.Lang.EnglishEU;
                case "casino": return Resources.Lang.Casino;
                case "basketball": return Resources.Lang.Basketball;
                case "tennins": return Resources.Lang.Tennis;
                case "rmb": return Resources.Lang.RMB;
                case "myr": return Resources.Lang.MYR;
                case "thb": return Resources.Lang.THB;
                case "usd": return Resources.Lang.USD;
                case "eur": return Resources.Lang.EUR;
                case "gbp": return Resources.Lang.GBP;
                case "aud": return Resources.Lang.AUD;
                case "chf": return Resources.Lang.CHF;

                default:
                    return value;

            }

        }

        public static string GetTextLink(DataView dv,string trackingcode, string activeLang,string page, string game) 
        {
            /* Generate TestLinkList */
          
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            StringBuilder textLinkList = new StringBuilder();
            string textLink = TemplateHandler.CopyBlock(htmlContent, "TextLinkList");
            string _link =  Settings.GetLinkDomain() + "/{AffId}{/TrackId}/{PageId}/{Game}/{Lang}/index.htm";

            

            if (!string.IsNullOrEmpty(trackingcode))
            { 
                trackingcode = "/" + trackingcode;
            }
            if (activeLang.Contains(Resources.Lang.All))
            {
                dv.RowFilter = "type = 'SupportedLanguages'";
                DataTable dt = dv.ToTable();

                foreach (DataRow dr in dt.Rows)
                {
                    textLinkList.Append(GenLink(textLink, dr["Value"].ToString(), _link, trackingcode, GetLangShort(dv, dr["Key"].ToString()), page, game));
                }

            }
            else
            {
                textLinkList.Append(GenLink(textLink, GetLanguageName(dv, activeLang), _link, trackingcode, GetLangShort(dv, activeLang), page, game));
            }
             
            return textLinkList.ToString();
        }
        public static string GetLangShort(DataView dv,string languageCode)
        {
            dv.RowFilter = "type = 'LangCodeShort' and key='" + languageCode +"'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["value"].ToString();
            return languageCode.Substring(0, 2);        
        }
        public static string GetLanguageName(DataView dv, string languageCode)
        {
            dv.RowFilter = "type = 'SupportedLanguages' and key='" + languageCode + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["value"].ToString();
            return languageCode;        
        }
        public static string GetBannerList(DataView dv,string category, string language, string page, string game, string imageType, string size, string currency, int pageIndex, int pagesize, ref int totalpage,string trackingcode)
        {
           
            DbResult result = null;
            int nRows = 0;
            result = UserAff.SelAffBanner(category, language.Contains(Resources.Lang.All)?string.Empty:language, imageType, size, currency, pageIndex, pagesize);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            
            int cols = 9;
            
            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");                
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                              
                string bannerDomain = Settings.GetBannerDomain();;

                string _memberDomain = Settings.GetBetDomain();
                                 

                string flContent = HttpContext.Current.Server.HtmlEncode(TemplateHandler.LoadTemplate("UserInfo/FlashCode.htm"));
                TemplateHandler.Assign(ref flContent, "bannerDomain_val", bannerDomain);

                string _link = Settings.GetLinkDomain() + "/{AffID}{/TrackingCode}/{page}/{game}/{Lang}/index.htm";

                TemplateHandler.Assign(ref _link, "AffID", AppSettings.Session.CustID);
                TemplateHandler.Assign(ref _link, "page", page);
                TemplateHandler.Assign(ref _link, "game", game);
                if(string.IsNullOrEmpty(trackingcode))
                    TemplateHandler.Assign(ref _link,"/TrackingCode",string.Empty);
                else
                    TemplateHandler.Assign(ref _link,"/TrackingCode","/" + trackingcode);



                foreach (DataRow dr in dtbResult.Rows)
                {
               
                    string rowText = content;
                    string link = _link;
                    TemplateHandler.Assign(ref link, "Lang", GetLangShort(dv, dr["language"].ToString()));

                    string tmpSrc = bannerDomain + "/" + dr["filename"].ToString();
                    
                    string []strSize = dr["size"].ToString().Split(new char[]{'x'});
                    if (System.IO.Path.GetExtension(dr["filename"].ToString()) == ".swf")
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "ImageContent");
                        string flashContent = flContent;
                        TemplateHandler.Assign(ref rowText, "FlashContent_val", flashContent);
                        TemplateHandler.Assign(ref rowText, "custid_val", AppSettings.Session.CustID.ToString());
                        TemplateHandler.Assign(ref rowText, "srcnoswf_val", tmpSrc.Replace(".swf", string.Empty));


                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "FlashContent");
                        TemplateHandler.Assign(ref rowText, "memberdomain_val", _memberDomain);                       
                    }
                    TemplateHandler.Assign(ref rowText, "src_val", tmpSrc);
                    TemplateHandler.Assign(ref rowText, "link_val", link);
                    TemplateHandler.Assign(ref rowText, "width_val", strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height_val", strSize[1]);
                    contentList.Append(rowText);

                }

                return contentList.ToString();

            }
            else
            {
                return Common.NoInfo(cols);
            }

        }



        static string GenLink(string textLink, string langText, string link, string trackingCode, string linkLang, string page, string game)
        {
            TemplateHandler.Assign(ref textLink, "language_val", langText);

            TemplateHandler.Assign(ref link, "{AffId}", AppSettings.Session.CustID.ToString());
            TemplateHandler.Assign(ref link, "{/TrackId}", trackingCode.Trim());
            TemplateHandler.Assign(ref link, "{PageId}", page);
            TemplateHandler.Assign(ref link, "{Game}", game);
            TemplateHandler.Assign(ref link, "{Lang}", linkLang);
            TemplateHandler.Assign(ref textLink, "link_val", link);
            return textLink;
        }

        static string GenCodeByPreferLang()
        {
            switch (AppSettings.Session.PreferLang.ToLower())
            {
                case "en-us":
                    return "en";
                case "en":
                    return "eneuro";
                case "zh-cn":
                    return "cs";
                case "zh-tw":
                    return "ct";
                case "th-th":
                    return "th";
                case "es-es":
                    return "es";
                case "vi-vn":
                    return "vi";
                case "id-id":
                    return "id";
                default:
                    return "en";
            }
        }
    }
}

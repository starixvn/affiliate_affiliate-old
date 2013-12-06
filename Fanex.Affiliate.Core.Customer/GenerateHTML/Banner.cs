namespace Fanex.Affiliate.Core.Customer.GenerateHTML
{
    using DNAide.Web;
    using DNAide.Core;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using System.Web;
    using Fanex.Affiliate.DataObjects;
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
                case "bonus": return Resources.Translator.Get("Bonus");
                case "tennis": return Resources.Translator.Get("Tennis");
                case "football": return Resources.Translator.Get("Football");
                case "poker": return Resources.Translator.Get("Poker");
                case "image": return Resources.Translator.Get("Image");
                case "flash": return Resources.Translator.Get("Flash");
                case "english": return Resources.Translator.Get("English");
                case "chinese traditional": return Resources.Translator.Get("ChineseTraditio");
                case "chinese simplified": return Resources.Translator.Get("ChineseSimplifi");
                case "thai": return Resources.Translator.Get("Thai");
                case "spanish": return Resources.Translator.Get("Spanish");
                case "indonesian": return Resources.Translator.Get("Indonesian");
                case "vietnamese": return Resources.Translator.Get("Vietnamese");
                case "english (europe)": return Resources.Translator.Get("EnglishEU");
                case "casino": return Resources.Translator.Get("Casino");
                case "basketball": return Resources.Translator.Get("Basketball");
                case "tennins": return Resources.Translator.Get("Tennis");
                case "rmb": return Resources.Translator.Get("RMB");
                case "myr": return Resources.Translator.Get("MYR");
                case "thb": return Resources.Translator.Get("THB");
                case "usd": return Resources.Translator.Get("USD");
                case "eur": return Resources.Translator.Get("EUR");
                case "gbp": return Resources.Translator.Get("GBP");
                case "aud": return Resources.Translator.Get("AUD");
                case "chf": return Resources.Translator.Get("CHF");

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
            if (activeLang.Contains(Resources.Translator.Get("All")))
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
        public static string GetBannerList(DataView dv,string category, string language, string page, string game, string imageType, string size,int currency, int pageIndex, int pagesize, ref int totalpage,string trackingcode)
        {
           
            DbResult result = null;
            int nRows = 0;
            result = UserAff.SelAffBanner(category, language.Contains(Resources.Translator.Get("All"))?string.Empty:language, imageType, size, currency, pageIndex, pagesize);
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

                TemplateHandler.Assign(ref _link, "AffID", Fanex.Affiliate.Core.AppSession.CustID);
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
                        TemplateHandler.Assign(ref rowText, "custid_val", Fanex.Affiliate.Core.AppSession.CustID.ToString());
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
                return AffCommon.NoInfo(cols);
            }

        }



        static string GenLink(string textLink, string langText, string link, string trackingCode, string linkLang, string page, string game)
        {
            TemplateHandler.Assign(ref textLink, "language_val", langText);

            TemplateHandler.Assign(ref link, "{AffId}", Fanex.Affiliate.Core.AppSession.CustID.ToString());
            TemplateHandler.Assign(ref link, "{/TrackId}", trackingCode.Trim());
            TemplateHandler.Assign(ref link, "{PageId}", page);
            TemplateHandler.Assign(ref link, "{Game}", game);
            TemplateHandler.Assign(ref link, "{Lang}", linkLang);
            TemplateHandler.Assign(ref textLink, "link_val", link);
            return textLink;
        }

        static string GenCodeByPreferLang()
        {
            switch (Fanex.Affiliate.Core.Customer.AffSession.PreferLang.ToLower())
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

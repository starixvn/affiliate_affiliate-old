// -----------------------------------------------------------------------
// <copyright file="Banner.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.LiveBet365.DataObjects
{
    using DNAide.Data;
    using System.Collections;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BannerMedia
    {


        // Get banners in parent category
        public static DbResult GetBannerListByParentCategory(int category, int subcategory, string language, string imageType, string size, string currency, int pageIndex, int pageSize, int type)
        {
            Hashtable hsParam = new Hashtable();
            if (category>0)
            {
                hsParam.Add("category", category);
            }
            if (subcategory>0)
            {
                hsParam.Add("subcategory", subcategory);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            hsParam.Add("type", type);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSelByParentCate", hsParam, DBType.AFFILIATE);

        }
        // add "type" parameter to indicate normal banner or media banner
        public static DbResult GetBannerList(int category, string language, string imageType, string size, string currency, int pageIndex, int pageSize, int type)
        {
            Hashtable hsParam = new Hashtable();
            if (category>0)
            {
                hsParam.Add("category", category);
            }
            
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            // ++ Added by Frank.Ngo on IRP 671 - 10 Jan 2012
            hsParam.Add("type", type);
            // -- Added by Frank.Ngo on IRP 671 - 10 Jan 2012

            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerMediaSel", hsParam, DBType.AFFILIATE);

        }
    }
}

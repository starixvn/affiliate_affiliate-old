using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;
using DNAide.Data;
using System.Text;
using System.Data;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Helper;
using Fanex.Affiliate.Core;

public partial class UserInfo_ClicksLandingPages : BasePageLogIn
{
    protected string statisticsContent = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        statisticsContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_LandingPage.htm");

        TemplateHandler.RemoveBlock(ref statisticsContent, "StatisticsDetailContent");
        TemplateHandler.Assign(ref statisticsContent, "From", Translator.Get("From"));
        TemplateHandler.Assign(ref statisticsContent, "To", Translator.Get("To"));

        TemplateHandler.Assign(ref statisticsContent, "{TrackingStatistics}", Translator.Get("LandingPages"));
        TemplateHandler.Assign(ref statisticsContent, "{Page}", Translator.Get("Page"));
        TemplateHandler.Assign(ref statisticsContent, "{Language}", Translator.Get("Language"));
        TemplateHandler.Assign(ref statisticsContent, "{Submit}", Translator.Get("Submit"));
        TemplateHandler.Assign(ref statisticsContent, "{Site}", Translator.Get("SiteUrl"));
        TemplateHandler.Assign(ref statisticsContent, "{TotalClick}", Translator.Get("TotalClick"));
        TemplateHandler.Assign(ref statisticsContent, "{UniqueClick}", Translator.Get("UniqueClick"));
        TemplateHandler.Assign(ref statisticsContent, "{Signups}", Translator.Get("SignUp"));
        TemplateHandler.Assign(ref statisticsContent, "{FDM}", Translator.Get("FDM"));
        TemplateHandler.Assign(ref statisticsContent, "linkto", Translator.Get("LinkTo"));

        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        string productCode = Request["productCode"];
        string languageCode = Request["languageCode"];
        if (dtmFrom == null || dtmTo == null)
        {
            dtmFrom = DateTime.Now.AddMonths(-1);
            dtmTo = DateTime.Now;
        }
        TemplateHandler.Assign(ref statisticsContent, "slPage", AppCommon.DropdownCustomerPage("productCode", productCode, true));
        //TemplateHandler.Assign(ref statisticsContent, "slPage",
        //        HtmlHelper.DropDownList(AppSettings.BaseCustomerPage, "productCode", languageCode, true));

        TemplateHandler.Assign(ref statisticsContent, "slLanguage",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slLanguage", languageCode, true));

        //DataView dv = (CacheUtil.GetBannerTypes() as DataTable).DefaultView;
        //TemplateHandler.Assign(ref statisticsContent, "slLanguage", this.GetDataOptionBanner(dv, "languageCode", "Language", languageCode, true));


        //TemplateHandler.Assign(ref statisticsContent, "slLanguage", AffCommon.GenListLink2Page("languageCode", languageCode, true));
        TemplateHandler.Assign(ref statisticsContent, "fdate", AffCommon.ConvertDateTime(dtmFrom, "/", true, false));
        TemplateHandler.Assign(ref statisticsContent, "tdate", AffCommon.ConvertDateTime(dtmTo, "/", true, false));

        TemplateHandler.ReplaceBlock(ref statisticsContent, "Content", Fanex.Affiliate.Core.Customer.GenerateHTML.TrackingSystem.GetStatisticsByPage(dtmFrom, dtmTo, productCode, string.IsNullOrEmpty(languageCode) ? string.Empty : AffCommon.GetLangCode(languageCode)));
    }

    string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll)
    {
        dv.RowFilter = "type='" + type + "'";
        StringBuilder result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;
        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
            TemplateHandler.Assign(ref tempopt, "Optname", Translator.Get("All"));
            if (string.IsNullOrEmpty(activeValue))
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            result.Append(tempopt);
        }
        DataTable dt = dv.ToTable();
        foreach (DataRow dr in dt.Rows)
        {
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", dr["value"].ToString());
            TemplateHandler.Assign(ref tempopt, "Optname", Fanex.Affiliate.Core.Customer.GenerateHTML.Banner.GetBannerLang(dr["value"].ToString()));

            if (string.Compare(activeValue, dr["value"].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
            }
            result.Append(tempopt);
        }
        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
    }
}

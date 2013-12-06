using System;
using System.Collections;
using DNAide.Web;
using DNAide.Data;
using System.Text;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.Resources;
public partial class UserInfo_ClickReferrers : BasePageLogIn
{
    protected string statisticsContent = string.Empty; 
    protected void Page_Load(object sender, EventArgs e)
    {
        statisticsContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_New.htm");
        TemplateHandler.RemoveBlock(ref statisticsContent, "StatisticsDetailContent");
        TemplateHandler.Assign(ref statisticsContent, "From", Translator.Get("From"));
        TemplateHandler.Assign(ref statisticsContent, "To", Translator.Get("To"));
        
        TemplateHandler.Assign(ref statisticsContent, "{TrackingStatistics}", Translator.Get("Referrals"));
        TemplateHandler.Assign(ref statisticsContent, "{TrackingCode}", Translator.Get("TrackingCode"));
        TemplateHandler.Assign(ref statisticsContent, "{Submit}", Translator.Get("Submit"));
        TemplateHandler.Assign(ref statisticsContent, "{Site}", Translator.Get("SiteUrl"));
        TemplateHandler.Assign(ref statisticsContent, "{TotalClick}", Translator.Get("TotalClick"));
        TemplateHandler.Assign(ref statisticsContent, "{UniqueClick}", Translator.Get("UniqueClick"));
        TemplateHandler.Assign(ref statisticsContent, "{Signups}", Translator.Get("SignUp"));
        TemplateHandler.Assign(ref statisticsContent, "{FDM}", Translator.Get("FDM"));
        TemplateHandler.Assign(ref statisticsContent, "linkto", Translator.Get("LinkTo"));

        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        if (dtmFrom == null || dtmTo == null)
        {
            dtmFrom = DateTime.Now.AddMonths(-1);
            dtmTo = DateTime.Now;
        }
        TemplateHandler.Assign(ref statisticsContent, "fdate", AffCommon.ConvertDateTime(dtmFrom, "/", true, false));
        TemplateHandler.Assign(ref statisticsContent, "tdate", AffCommon.ConvertDateTime(dtmTo, "/", true, false));

        TemplateHandler.ReplaceBlock(ref statisticsContent, "Content", Fanex.Affiliate.Core.Customer.GenerateHTML.TrackingSystem.GetStatisticsBySite(dtmFrom, dtmTo));


        //TemplateHandler.ReplaceBlock(ref statisticsContent, "Content", GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(null));
    }
}

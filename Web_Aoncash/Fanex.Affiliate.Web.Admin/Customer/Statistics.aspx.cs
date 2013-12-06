namespace Fanex.Web.Aoncash.Admin.Customer
{
    using System;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Helper;


public partial class Statistics : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        CurrencyUtils.SetDefaultCurrency(AdminSession.AffCurrency);
        string strTpl = TemplateHandler.LoadTemplate("Customer/Statistics.htm");
        int intCustId = AdminSession.AffId;
        DateTime dtmFirstOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        if (!string.IsNullOrEmpty(Request["slMonth"]) && !string.IsNullOrEmpty(Request["slYear"]))
            dtmFirstOfMonth = new DateTime(Convert.ToInt32(Request["slYear"]), Convert.ToInt32(Request["slMonth"]), 1);
        
        TemplateHandler.Assign(ref strTpl, "custid", intCustId.ToString());
        AppCommon.AssignToAdminTemplate(ref strTpl,
        "NewSignup", "FDM", "TotalActive", "TotalDeposit", "Turnover",
        "Winlost", "TrackingRate",
        "Statistic", "UpToNow",
        "TotalQualifiedMembers");

        TemplateHandler.Assign(ref strTpl, "username", CastAide.AsString(Request["user"]));    
        TemplateHandler.Assign(ref strTpl, "month-sel", dtmFirstOfMonth.Month.ToString());
        TemplateHandler.Assign(ref strTpl, "year-sel", dtmFirstOfMonth.Year.ToString());        
        TemplateHandler.Assign(ref strTpl, "month", dtmFirstOfMonth.ToString("MMMM"));
        TemplateHandler.Assign(ref strTpl, "year", dtmFirstOfMonth.ToString("yyyy"));        
        TemplateHandler.Assign(ref strTpl, "custid", AdminSession.AffId);
        TemplateHandler.Assign(ref strTpl, "slMonth", AppCommon.GenMonthOptions(dtmFirstOfMonth.Month));
        TemplateHandler.Assign(ref strTpl, "slYear", AppCommon.GenYearOptions(dtmFirstOfMonth.Year));

        /*DbResult dep_result = DataHandler.Affiliate.SelAffOverview_DEPOSIT(intCustId, dtmFirstOfMonth);
        int newSignUp = CastAide.AsInt32(dep_result.Outputs["newsignup"]);
        int totalPlayer = CastAide.AsInt32(dep_result.Outputs["totalplayer"]);

        TemplateHandler.Assign(ref strTpl, "newsignup-val", newSignUp);
        TemplateHandler.Assign(ref strTpl, "totalplay-val", totalPlayer);*/

        //Assign info       
        DbResult result = Affiliate.SelStatistics(intCustId, dtmFirstOfMonth);

        var product = "N/A";
        var slProduct = String.Empty;
        var totalqualifiedmembers = "N/A";

        if (result.ResultSet.Tables.Count > 0)
        {

            var table = result.ResultSet.Tables[0];
            product = table.Rows[0]["product"].ToString();
            totalqualifiedmembers = table.Rows[0]["totalqualifiedmembers"].ToString();
            slProduct = HtmlHelper.OptionList(table, "product", "product", null, null, true);
        }
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembers-val", totalqualifiedmembers);
        TemplateHandler.Assign(ref strTpl, "product", product);
        TemplateHandler.Assign(ref strTpl, "slProduct", slProduct);

        int newSignUp = CastAide.AsInt32(result.Outputs["newsignup"]);
        int totalPlayer = CastAide.AsInt32(result.Outputs["totalplayer"]);

        TemplateHandler.Assign(ref strTpl, "newsignup-val", newSignUp);
        TemplateHandler.Assign(ref strTpl, "totalplay-val", totalPlayer);
        
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembers-val", CastAide.AsInt32(result.Outputs["totalqualifiedmembers"]));
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembersHr-val", CastAide.AsInt32(result.Outputs["totalqualifiedmembershr"]));        
        TemplateHandler.Assign(ref strTpl, "turnovermonth-val", CurrencyUtils.Format(result.Outputs["totalturnover"]));

        int totalactiveplayer = CastAide.AsInt32(result.Outputs["totalactiveplayer"]);
        //TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString());

        if (totalactiveplayer == 0 || dtmFirstOfMonth.CompareTo(AppSettings.SystemStartDate) < 0)
            TemplateHandler.ReplaceBlock(ref strTpl, "totalactive", totalactiveplayer.ToString());
        else
        {
            TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString());
        }
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFirstOfMonth.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmFirstOfMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString()); 
       
        TemplateHandler.Assign(ref strTpl, "newsignupdep-val", CastAide.AsInt32(result.Outputs["newsignupdep"], 0));
        //int newSignUpNonDep = newSignUp - CastAide.AsInt32(result.Outputs["newsignupdep"]);
        //TemplateHandler.Assign(ref strTpl, "newsignupnondep", (newSignUpNonDep > 0) ? newSignUpNonDep : 0);
        //totalplaydep
        //int totalpayerdepup = CastAide.AsInt32(result.Outputs["totalplayerdepup"]);
        TemplateHandler.Assign(ref strTpl, "totalplaydep-val", CastAide.AsInt32(result.Outputs["totalplayerdepup"]));
        //TemplateHandler.Assign(ref strTpl, "totalplaynondep", totalPlayer - totalpayerdepup);

        TemplateHandler.Assign(ref strTpl, "totaldeposit-val", CurrencyUtils.Format(result.Outputs["totaldeposit"]));
        TemplateHandler.Assign(ref strTpl, "turnover-val", CurrencyUtils.Format(result.Outputs["totalturnoverup"]));
        TemplateHandler.Assign(ref strTpl, "totaldepositup-val", CurrencyUtils.Format(result.Outputs["totaldepositup"]));

        TemplateHandler.Assign(ref strTpl, "winlost-val", CurrencyUtils.Format(result.Outputs["totalwinlost"]));
        TemplateHandler.Assign(ref strTpl, "winlostup-val", CurrencyUtils.Format(result.Outputs["totalwinlostup"]));


        //Tracking Statistics        
        TemplateHandler.Assign(ref strTpl, "trackingrate-val", CastAide.AsInt32(result.Outputs["totaluniqueclicks"], 0) + "/" + CastAide.AsInt32(result.Outputs["totalclicks"], 0));
        
        this.HtmlContent = strTpl;
    }
}
}

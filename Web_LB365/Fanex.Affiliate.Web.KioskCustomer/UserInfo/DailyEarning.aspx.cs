using System;
using System.Data;
using System.Text;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;

public partial class DailyEarning : BasePageLogIn
{
    private System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;
    protected string strListProducts = "";
    protected string fromDate = string.Empty;
    protected string toDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        //DateTime _date = DateTime.Now;//string.IsNullOrEmpty(Request["monthdate"]) ? DateTime.Parse(Request["monthdate"].ToString(), cult) : DateTime.Now.AddDays(-1);        
        int productId = CastAide.AsInt32(Request["slProduct"], 1);
        this.ltrDailyEarning.Text = Translator.Get("DailyEarnings");
        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDaily.htm");
        //Assign header        
        TemplateHandler.Assign(ref strTemplate, "turnover", Translator.Get("Turnover"));
        TemplateHandler.Assign(ref strTemplate, "winlost", Translator.Get("Winlost"));
        TemplateHandler.Assign(ref strTemplate, "bonus", Translator.Get("Bonus"));
        TemplateHandler.Assign(ref strTemplate, "adjustment", Translator.Get("Adjustment"));
        TemplateHandler.Assign(ref strTemplate, "psp", Translator.Get("PSP"));
        TemplateHandler.Assign(ref strTemplate, "total", Translator.Get("Total"));
        TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);
        TemplateHandler.Assign(ref strTemplate, "sporttype", productId);
        AppCommon.AssignToTemplate(ref strTemplate, "original", "date","deposit");

        //int product = CastAide.AsInt32(Request["slProduct"], 0);
        //Peter@20091231 Only enable to view ProductsBook
        //if (productId == 2)//if product is horse racing, remove columns bonus, deposit, adjustment
        //{
        //    TemplateHandler.RemoveBlock(ref strTemplate, "sportsbookonly");
        //    TemplateHandler.RemoveBlock(ref strTemplate, "totalsportsbookonly");
        //    TemplateHandler.RemoveBlock(ref strTemplate, "sportsbookonlyhead");
        //    TemplateHandler.Assign(ref strTemplate, "width-val", 250);

        //}
        strListProducts = AffCommon.GenListProduct(AppSession.CustID, AppSession.Language, productId);

        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        if (dtmFrom == null || dtmTo == null)
        {
            dtmFrom = DateTime.Now.AddMonths(-1).Date;
            dtmTo = DateTime.Now;
        }

        DbResult result = UserAff.SelAffEarningDaily(Fanex.Affiliate.Core.AppSession.CustID, productId, null, dtmFrom, dtmTo);

        fromDate = AffCommon.ConvertDateTime(dtmFrom, "/", true, false);
        toDate = AffCommon.ConvertDateTime(dtmTo, "/", true, false);

        DataTable dtbResult = result.ResultSet.Tables[0];
        var currencycode = "MYR";
        if (dtbResult.Rows.Count > 0)
        {
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            decimal dcDeposit = 0, dcTurnover = 0, dcBonus = 0, dcAdjustment = 0, dcPSP = 0,dcRealWilost=0;                  
            string strRowStyle = "bgcgy";//bgwhite
            string strMouseStyle = "b";//bgwhite
            currencycode = result.Outputs["affcurrency"].ToString();
            CurrencyUtils.SetDefaultCurrency(currencycode);
             strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
             strMouseStyle = (strMouseStyle == "b") ? "a" : "b";

             

            foreach (DataRow dr in dtbResult.Rows)
            {
                
                string strTmp = strContent;
                /*decimal dcPercent = CastAide.AsDecimal(dr["PercentReceived"]);*/
                intNo++;
                
                dcDeposit += CastAide.AsDecimal(dr["deposit"]);                    
                dcBonus += CastAide.AsDecimal(dr["bonus"]);
                dcAdjustment += CastAide.AsDecimal(dr["adjustment"]);
                dcPSP += CastAide.AsDecimal(dr["psp"]);
                dcRealWilost += CastAide.AsDecimal(dr["memberwinlost"]);
                dcTurnover += CastAide.AsDecimal(dr["turnover"]);
                
                
                
                TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                TemplateHandler.Assign(ref strTmp, "date-val", AffCommon.ConvertDateTime(dr["winlostdate"], "/", true, false));
                TemplateHandler.Assign(ref strTmp, "winlost-val", CurrencyUtils.Format(dr["memberwinlost"]));
                //Comment winlost fake
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["turnover"]));
                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dr["bonus"]));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dr["adjustment"]));
                    
                TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);

                TemplateHandler.Assign(ref strTmp, "psp-val", CurrencyUtils.Format(dr["psp"]));
                strBuild.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTemplate, "totalwinlost", CurrencyUtils.Format(dcRealWilost));
            TemplateHandler.Assign(ref strTemplate, "totalturnover", CurrencyUtils.Format(dcTurnover));
            TemplateHandler.Assign(ref strTemplate, "totaldeposit", CurrencyUtils.Format(dcDeposit));
            TemplateHandler.Assign(ref strTemplate, "totalbonus", CurrencyUtils.Format(dcBonus));
            TemplateHandler.Assign(ref strTemplate, "totaladjustment", CurrencyUtils.Format(dcAdjustment));
            TemplateHandler.Assign(ref strTemplate, "totalpsp", CurrencyUtils.Format(dcPSP));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", AffCommon.NoInfo(11));
            TemplateHandler.RemoveBlock(ref strTemplate, "total");
        }

        TemplateHandler.Assign(ref strTemplate, "usdeqv", currencycode + " Eqv");

        this.ltrViewDaily.Text = strTemplate;
    }
    
}

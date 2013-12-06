using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Customer;
using System.Text;
using System.Data;
using Fanex.Affiliate.Core;

namespace Fanex.Web.LiveBet365.Affiliate.UserInfo
{
    public partial class DailyEarningDetail : BasePageLogIn
    {
        private System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;
        public string strTemplate = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            strTemplate = TemplateHandler.LoadTemplate("UserInfo/DailyEarningDetail.htm");
            DateTime _date = (Request["monthdate"] != null) ? DateTime.Parse(Request["monthdate"].ToString(), cult) : DateTime.Now.AddDays(-1);
            DateTime lastDateofMonth = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));
            int productId = CastAide.AsInt32(Request["slProduct"], 0);
            int step1 = CastAide.AsInt32(Request["step1"], 0) + 1;
            int step2 = CastAide.AsInt32(Request["step2"], 0) + 1;
            bool isComm = (string.IsNullOrEmpty(Request["iscomm"]) ? false : Request["iscomm"] == "1");


            int width = 780;
            if (productId == 2)
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "sportbook");
                width = 250;
            }
            else if (productId == 3)
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "casino");
                width = 420;
            }

            TemplateHandler.Assign(ref strTemplate, "width", width);
            TemplateHandler.Assign(ref strTemplate, "slSports", AffCommon.GenListProduct(AppSession.CustID, AppSession.Language, productId));
            TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);
            TemplateHandler.Assign(ref strTemplate, "date", _date.ToString("dd MMMM yyyy"));
            TemplateHandler.Assign(ref strTemplate, "username", Translator.Get("Username"));
            TemplateHandler.Assign(ref strTemplate, "deposit", Translator.Get("Deposit"));
            TemplateHandler.Assign(ref strTemplate, "winlost", Translator.Get("Winlost"));
            TemplateHandler.Assign(ref strTemplate, "turnover", Translator.Get("Turnover"));
            TemplateHandler.Assign(ref strTemplate, "total", Translator.Get("Total"));
            TemplateHandler.Assign(ref strTemplate, "back", Translator.Get("Back"));
            TemplateHandler.Assign(ref strTemplate, "Submit", Translator.Get("Submit"));
            TemplateHandler.Assign(ref strTemplate, "statement", Translator.Get("StatementHistory"));
            TemplateHandler.Assign(ref strTemplate, "DailyEarningDetail", Translator.Get("DailyEarningDetail"));
            TemplateHandler.Assign(ref strTemplate, "selectdate", Translator.Get("Date"));
            TemplateHandler.Assign(ref strTemplate, "bonus", Translator.Get("Bonus"));
            TemplateHandler.Assign(ref strTemplate, "adjustment", Translator.Get("Adjustment"));
            TemplateHandler.Assign(ref strTemplate, "earningMonth", AffCommon.ConvertDateTime(_date, "-", false, false));
            TemplateHandler.Assign(ref strTemplate, "user", CastAide.AsString(Request["user"]));
            TemplateHandler.Assign(ref strTemplate, "lastdayofmonth", AffCommon.ConvertDateTime(lastDateofMonth, "/", true, false));
            TemplateHandler.Assign(ref strTemplate, "detaildate", AffCommon.ConvertDateTime(_date, "/", true, false));


            try
            {
                DataTable dtbResult = Fanex.Affiliate.DataObjects.Affiliate.SelAffEarningDetail(AppSession.CustID, productId, _date).ResultSet.Tables[0];
                if (dtbResult.Rows.Count > 0)
                {
                    string strRowStyle = "bgcgy";//bgwhite
                    string strMouseStyle = "b";//bgwhite

                    string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
                    int intNo = 0;
                    StringBuilder strBuild = new StringBuilder();
                    decimal dcDeposit = 0, dcWinlost = 0, dcTurnover = 0, dcBonus = 0, dcAdjustment = 0;//, dcAdjustment = 0, dcPSP = 0, dcWithdrawal = 0;
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                        strMouseStyle = (strMouseStyle == "b") ? "a" : "b";

                        string strTmp = strContent;
                        intNo++;
                        dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                        dcWinlost += CastAide.AsDecimal(dr["winlost"]);
                        dcTurnover += CastAide.AsDecimal(dr["turnover"]);
                        dcBonus += CastAide.AsDecimal(dr["bonus"]);
                        dcAdjustment += CastAide.AsDecimal(dr["adjustment"]);

                        TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                        TemplateHandler.Assign(ref strTmp, "ibcusername-val", CastAide.AsString(dr["username"]));

                        string strUsername2 = dr["username"].ToString();//make sure remove 12bet$ in the first position of strUsername2                    
                        strUsername2 = (strUsername2.ToLower().IndexOf("12bet$") == 0 ? strUsername2.Substring(6, strUsername2.Length - 6) : strUsername2);

                        TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                        TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                        TemplateHandler.Assign(ref strTmp, "username-val", strUsername2);
                        TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dr["deposit"]));
                        TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["turnover"]));
                        TemplateHandler.Assign(ref strTmp, "winlost-val", CurrencyUtils.Format(dr["winlost"]));
                        TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dr["bonus"]));
                        TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dr["adjustment"]));
                        strBuild.Append(strTmp);
                    }
                    TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
                    TemplateHandler.Assign(ref strTemplate, "totaldeposit", CurrencyUtils.Format(dcDeposit));
                    TemplateHandler.Assign(ref strTemplate, "totalturnover", CurrencyUtils.Format(dcTurnover));
                    TemplateHandler.Assign(ref strTemplate, "totalwinlost", CurrencyUtils.Format(dcWinlost));
                    TemplateHandler.Assign(ref strTemplate, "totalbonus", CurrencyUtils.Format(dcBonus));
                    TemplateHandler.Assign(ref strTemplate, "totaladjustment", CurrencyUtils.Format(dcAdjustment));
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTemplate, "content", AppCommon.NoInfo(8));
                    TemplateHandler.RemoveBlock(ref strTemplate, "total");
                }
                TemplateHandler.Assign(ref strTemplate, "iscomm", Request["iscomm"]);
                TemplateHandler.RemoveBlock(ref strTemplate, "directory");

                this.HtmlContent = strTemplate;
            }
            catch (Exception ex)
            {
                throw ex;
                DNAideLog.Write(ex.Message);
            }
        }
    }
}
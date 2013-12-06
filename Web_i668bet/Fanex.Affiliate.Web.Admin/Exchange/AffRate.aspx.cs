namespace Fanex.Web.i668bet.Admin.Exchange
{
    using System;
    using System.Data;
    using System.Text;
    using System.Linq;
    using DNAide.Core;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;
    using System.Collections.Generic;

    public partial class AffRate : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var strTpl = TemplateHandler.LoadTemplate("Exchange/ExchangeRate.htm");
            var dtmNow = DateTime.Now;

            var dtmTime = !string.IsNullOrEmpty(Request["txtDate"])
                          ? CastAide.AsDateTime(Request["txtDate"])
                          : dtmNow;

            TemplateHandler.Assign(ref strTpl, "date", Fanex.Affiliate.Resources.Translator.Get("Date"));
            TemplateHandler.Assign(ref strTpl, "date-val", dtmTime.ToString("MM/dd/yyyy"));

            AppCommon.AssignToAdminTemplate(ref strTpl,
            "Currency", "MarketRate", "autoUpdate", "rate", "submit", "ConfirmDelRate", "SelRate", "rate_", "CopyRateMarket", "AddCurrencies");
            TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
            TemplateHandler.Assign(ref strTpl, "isupdated", (dtmTime.DayOfYear == dtmNow.DayOfYear && dtmTime.Year == dtmNow.Year) ? "1" : "0");

            //TemplateHandler.Assign(ref strTpl, "submenu", Common.GenSubMenu(new string[] { "#" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("ExchangeRate }, 399, 0));

            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(1, 1, 100, 100, string.Empty, string.Empty, new string[] { }, new string[] { }, 900, true, false, true, false));
            var intCount = 0;
            //IList<string> supportedCurrencies;

            //   var dtbResult = ExchangeRate.Exchange_Sel(dtmTime, out supportedCurrencies) ;
            var dtbResult = ExchangeRate.Exchange_Sel(dtmTime);

            var noInfoCol = AppSettings.SupportedBaseCurrencies.Count;

            // check to hide add aff currencies
            //var aff = AppSettings.AffCurrencies.Select(c => c.Value);
            //var currencies = AppSettings.SupportedBaseCurrencies.Select(c => c.Value).Where(c => !aff.Contains(c));


            ///if (currencies.Count() == 0)
            TemplateHandler.RemoveBlock(ref strTpl, "AddAffCurrencies");
            // check to hide add exchanges
            //if(Currency.CountCurrenciesNotInExchangeTable() == 0)
            TemplateHandler.RemoveBlock(ref strTpl, "AddExchanges");

            if (dtmTime <= dtmNow)
            {
                var isHost = AdminCommon.isHost(Fanex.Affiliate.Core.AppSession.CustID, "[AffEx.Rate]");

                var isToday = dtmTime.Date == DateTime.Today;

                if (!AppCommon.IsCurrencyExisted("UUS")) TemplateHandler.RemoveBlock(ref strTpl, "UUS");
                if (!AppCommon.IsCurrencyExisted("USD")) TemplateHandler.RemoveBlock(ref strTpl, "USD");
                if (!AppCommon.IsCurrencyExisted("EUR")) TemplateHandler.RemoveBlock(ref strTpl, "EUR");
                if (!AppCommon.IsCurrencyExisted("CNY")) TemplateHandler.RemoveBlock(ref strTpl, "CNY");
                if (!AppCommon.IsCurrencyExisted("MYR")) TemplateHandler.RemoveBlock(ref strTpl, "MYR");

                if (!AppCommon.IsCurrencyExisted("SGD")) TemplateHandler.RemoveBlock(ref strTpl, "SGD");
                if (!AppCommon.IsCurrencyExisted("IDR")) TemplateHandler.RemoveBlock(ref strTpl, "IDR");
                if (!AppCommon.IsCurrencyExisted("AUD")) TemplateHandler.RemoveBlock(ref strTpl, "AUD");
                if (!AppCommon.IsCurrencyExisted("CHF")) TemplateHandler.RemoveBlock(ref strTpl, "CHF");
                if (!AppCommon.IsCurrencyExisted("GBP")) TemplateHandler.RemoveBlock(ref strTpl, "GBP");
                if (!AppCommon.IsCurrencyExisted("THB")) TemplateHandler.RemoveBlock(ref strTpl, "THB");
                if (!AppCommon.IsCurrencyExisted("VND")) TemplateHandler.RemoveBlock(ref strTpl, "VND");
                //if (ConfigurationManager.AppSettings.Get("ShowUUSCurrency") != "true")
                //    TemplateHandler.RemoveBlock(ref strTpl, "UUS");

                var strNotifications = new StringBuilder();
                var multipliedCurrencies = new List<string>();
                foreach (CurrencyObj obj in AppSettings.AffSystemSupportedCurrencies)
                {
                    if (obj.MultipliedBy > 1)
                    {
                        multipliedCurrencies.Add(obj.Value);
                        strNotifications.Append(AdminCommon.CreateCurrencyNotificationLine(obj.Value, obj.MultipliedBy));
                    }
                }

                if (dtbResult.Rows.Count > 0)
                {
                    var strListRow = new StringBuilder();
                    string strRow = TemplateHandler.CopyBlock(strTpl, "content");
                    TemplateHandler.Assign(ref strRow, "over", AdminCommon.RowBgOver);

                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        intCount++;
                        var strTmp = strRow;
                        var currencycode = dr["code"].ToString();
                        TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                        TemplateHandler.Assign(ref strTmp, "no-val", intCount);
                        TemplateHandler.Assign(ref strTmp, "ID-val", dr["exchangeid"]);
                        TemplateHandler.Assign(ref strTmp, "exid-val", dr["exchangeid"]);
                        TemplateHandler.Assign(ref strTmp, "currency-val", currencycode);

                        var multipliedBy = CastAide.AsInt32(dr["multipliedBy"], 1);
                        if (multipliedBy > 1)
                        {

                            if (!multipliedCurrencies.Contains(currencycode))
                                strNotifications.Append(AdminCommon.CreateCurrencyNotificationLine(currencycode, multipliedBy));
                        }
                        #region Hide to do not allow update exchange rate
                        var c = dr["code"].ToString().ToLower();


                        if (isToday && !c.Equals("usd")) TemplateHandler.Assign(ref strTmp, "usd_class-val", "link popup");
                        if (isToday && !c.Equals("eur")) TemplateHandler.Assign(ref strTmp, "eur_class-val", "link popup");
                        if (isToday && !c.Equals("cny")) TemplateHandler.Assign(ref strTmp, "cny_class-val", "link popup");
                        if (isToday && !c.Equals("uus")) TemplateHandler.Assign(ref strTmp, "uus_class-val", "link popup");
                        if (isToday && !c.Equals("myr")) TemplateHandler.Assign(ref strTmp, "myr_class-val", "link popup");

                        if (isToday && !c.Equals("sgd")) TemplateHandler.Assign(ref strTmp, "sgd_class-val", "link popup");
                        if (isToday && !c.Equals("idr")) TemplateHandler.Assign(ref strTmp, "idr_class-val", "link popup");
                        if (isToday && !c.Equals("aud")) TemplateHandler.Assign(ref strTmp, "aud_class-val", "link popup");
                        if (isToday && !c.Equals("chf")) TemplateHandler.Assign(ref strTmp, "chf_class-val", "link popup");
                        if (isToday && !c.Equals("gbp")) TemplateHandler.Assign(ref strTmp, "gbp_class-val", "link popup");
                        if (isToday && !c.Equals("thb")) TemplateHandler.Assign(ref strTmp, "thb_class-val", "link popup");
                        if (isToday && !c.Equals("vnd")) TemplateHandler.Assign(ref strTmp, "vnd_class-val", "link popup");

                        TemplateHandler.Assign(ref strTmp, c + "_class-val", string.Empty);
                        #endregion


                        /*auto update*/
                        if (dr["IsAutoRefresh"].ToString() == "True")
                        {
                            TemplateHandler.Assign(ref strTmp, "autoUpdate-val", "<input id='aur_" + dr["exchangeid"].ToString() + "' type='checkbox' checked='checked' onchange='autoUpdate(" + dr["exchangeid"].ToString() + ");' />");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref strTmp, "autoUpdate-val", "<input id='aur_" + dr["exchangeid"].ToString() + "' type='checkbox' onchange='autoUpdate(" + dr["exchangeid"].ToString() + ");' />");
                        }

                        TemplateHandler.Assign(ref strTmp, "usd_rate", NumberFormat.DecFormat(dr["usd"], 5));
                        TemplateHandler.Assign(ref strTmp, "usd_market", NumberFormat.DecFormat(dr["usd_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "eur_rate", NumberFormat.DecFormat(dr["eur"], 5));
                        TemplateHandler.Assign(ref strTmp, "eur_market", NumberFormat.DecFormat(dr["eur_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "cny_rate", NumberFormat.DecFormat(dr["cny"], 5));
                        TemplateHandler.Assign(ref strTmp, "cny_market", NumberFormat.DecFormat(dr["cny_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "uus_rate", NumberFormat.DecFormat(dr["uus"], 5));
                        TemplateHandler.Assign(ref strTmp, "uus_market", NumberFormat.DecFormat(dr["uus_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "myr_rate", NumberFormat.DecFormat(dr["myr"], 5));
                        TemplateHandler.Assign(ref strTmp, "myr_market", NumberFormat.DecFormat(dr["myr_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "sgd_rate", NumberFormat.DecFormat(dr["sgd"], 5));
                        TemplateHandler.Assign(ref strTmp, "sgd_market", NumberFormat.DecFormat(dr["sgd_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "idr_rate", NumberFormat.DecFormat(dr["idr"], 5));
                        TemplateHandler.Assign(ref strTmp, "idr_market", NumberFormat.DecFormat(dr["idr_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "aud_rate", NumberFormat.DecFormat(dr["aud"], 5));
                        TemplateHandler.Assign(ref strTmp, "aud_market", NumberFormat.DecFormat(dr["aud_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "chf_rate", NumberFormat.DecFormat(dr["chf"], 5));
                        TemplateHandler.Assign(ref strTmp, "chf_market", NumberFormat.DecFormat(dr["chf_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "gbp_rate", NumberFormat.DecFormat(dr["gbp"], 5));
                        TemplateHandler.Assign(ref strTmp, "gbp_market", NumberFormat.DecFormat(dr["gbp_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "thb_rate", NumberFormat.DecFormat(dr["thb"], 5));
                        TemplateHandler.Assign(ref strTmp, "thb_market", NumberFormat.DecFormat(dr["thb_market"], 5));

                        TemplateHandler.Assign(ref strTmp, "vnd_rate", NumberFormat.DecFormat(dr["vnd"], 5));
                        TemplateHandler.Assign(ref strTmp, "vnd_market", NumberFormat.DecFormat(dr["vnd_market"], 5));

                        strListRow.Append(strTmp);
                    }
                    if (multipliedCurrencies.Count > 0)
                    {
                        TemplateHandler.Assign(ref strTpl, "DropLastZeroDigits", Fanex.Affiliate.Resources.Admin.DropLastZeroDigits);
                        TemplateHandler.Assign(ref strTpl, "Notifications", strNotifications);
                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref strTpl, "currencynotification");
                    }
                    TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTpl, "content", AdminCommon.NoInfo(3));
                }
            }
            else
                TemplateHandler.ReplaceBlock(ref strTpl, "content", AdminCommon.NoInfo(4 + noInfoCol * 2));
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(new string[] { "AffRate.aspx" }, new string[] { Fanex.Affiliate.Resources.Admin.ExchangeRate }, 0, intCount, Fanex.Affiliate.Resources.Translator.Get("ExchangeRate"), "i_ex_rate.png"));
            HtmlContent = strTpl;
        }

        
    }
}

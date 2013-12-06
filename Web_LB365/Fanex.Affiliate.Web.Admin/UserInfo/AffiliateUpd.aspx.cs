using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;
using Fanex.Affiliate.Core.Helper;

public partial class AffiliateUpd : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/AffUpd.htm");
        
        int intcustid = AdminSession.AffId;
        AppCommon.AssignToTemplate(ref strTpl,                
        "maininfo", "username", "status", "comm", "select", "personalinfo",
        "additionalinfo", "accinfo", "fname", "lname", "gender", "male",
        "female", "address", "city", "state", "postal", "country", "email",
        "ContactNumber", "mobile", "siteinfo", "siteurl", "moreurl", "preference",
        "language", "paymentinfo", "preferpayment", "BetAcc12", "netteller",
        "moneybooker", "banktrans", "cheque", "Bet12Username", "NonDepositAcc",
        "netelleraccid", "moneybookeremail", "fullname", "bankname", "bankaddress",
        "bankswift", "bankaccname", "bankaccnumber", "minwithdraw", "submit",
        "cancel", "accoverview", "securequestion", "answer", "EarningsHistory",
        "addurl", "emailpref", "PlainText", "HtmlFormat", "emailformat", "currency");

        TemplateHandler.Assign(ref strTpl, "ischild", Request["ischild"]);
        //Load content user
        #region Get list country

            //string strCountryTpl = TemplateHandler.CopyBlock(strTpl, "country-list");
            //StringBuilder strCountry = new StringBuilder();
            //foreach (DataRow dr in Fanex.Affiliate.Resources.TextExpressionBuilder.GetListCountry())
            //{
            //    string strTmp = strCountryTpl;
            //    TemplateHandler.Assign(ref strTmp, "countrycode", dr["ResourceKey"]);
            //    TemplateHandler.Assign(ref strTmp, "countryvalue", dr["ResourceValue"]);
            //    strCountry.Append(strTmp);
            //}

            //TemplateHandler.ReplaceBlock(ref strTpl, "country", strCountry.ToString());
        TemplateHandler.Assign(ref strTpl, "country-list",
            HtmlHelper.DropDownList(AppSettings.BaseCountries, "selCountry",null,true,null,false,"width='90px'"));
            #endregion
            
        string strMinWire = "0";
        string strMinNetteller = "0";
        string strMinCheque = "0";
        string strMinMoneyBooker = "0";
        string strMinPlayer = "0";

        DataTable dtbCustInfo = Affiliate.SelAffInfo(intcustid, ref strMinCheque, ref strMinMoneyBooker, ref strMinNetteller, ref strMinWire, ref strMinPlayer);
        if (dtbCustInfo.Rows.Count > 0)
        {
            DataRow dr = dtbCustInfo.Rows[0];
            ArrayList arrLangValue = new ArrayList();
            ArrayList arrLangText = new ArrayList();
            arrLangValue.Add("");
            arrLangValue.Add("E");                
            arrLangValue.Add("CT");
            arrLangValue.Add("CS");
            arrLangValue.Add("TH");
            arrLangText.Add("Select One");
            arrLangText.Add("English");
            arrLangText.Add("Chinese Traditional");
            arrLangText.Add("Chinese Simplified");
            arrLangText.Add("Thai");

            ArrayList arrComValue = new ArrayList();                
            for (decimal i = 20; i < 81; i++)
            {
                decimal dc = i / 100;                    
                arrComValue.Add(NumberFormat.DoubleFormat(dc, 2));
            }
            if (string.Compare(Status.Approved, dr["status"].ToString()) == 0)
            {
                TemplateHandler.Assign(ref strTpl, "closed", Fanex.Affiliate.Resources.Translator.Get("Closed"));
                TemplateHandler.ReplaceBlock(ref strTpl, "notapproved", string.Empty);
                //TemplateHandler.Assign(ref strTpl, "closechecked", CastAide.AsBoolean(dr["closed"]) ? "checked" : string.Empty);
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "approved", string.Empty);
            }
            if (DBNull.Value.Equals(dr["gender"]))
            {
                TemplateHandler.Assign(ref strTpl, "male-checked", string.Empty);
                TemplateHandler.Assign(ref strTpl, "female-checked", string.Empty);
            }
            else
            {
                if(CastAide.AsBoolean(dr["gender"]))
                    TemplateHandler.Assign(ref strTpl, "male-checked", "checked");
                else
                    TemplateHandler.Assign(ref strTpl, "female-checked", "checked");
                TemplateHandler.Assign(ref strTpl, "male-checked", string.Empty);
                TemplateHandler.Assign(ref strTpl, "female-checked", string.Empty);
            }
                
            //Email format preference
            if (DBNull.Value.Equals(dr["mailoption"]))
            {
                TemplateHandler.Assign(ref strTpl, "plaintext-checked", string.Empty);
                TemplateHandler.Assign(ref strTpl, "html-checked", string.Empty);
            }
            else
            {
                if (CastAide.AsBoolean(dr["mailoption"]))
                    TemplateHandler.Assign(ref strTpl, "plaintext-checked", "checked");
                else
                    TemplateHandler.Assign(ref strTpl, "html-checked", "checked");
                TemplateHandler.Assign(ref strTpl, "plaintext-checked", string.Empty);
                TemplateHandler.Assign(ref strTpl, "html-checked", string.Empty);
            }

            TemplateHandler.Assign(ref strTpl, "securequestion_val", dr["question"].ToString());
            TemplateHandler.Assign(ref strTpl, "answer_val", dr["answer"].ToString());

            TemplateHandler.Assign(ref strTpl, "custid-val", intcustid);
            TemplateHandler.Assign(ref strTpl, "username-val", dr["username"].ToString());
            TemplateHandler.Assign(ref strTpl, "fname-val", dr["firstname"].ToString());
            TemplateHandler.Assign(ref strTpl, "lname-val", dr["lastname"].ToString());
            TemplateHandler.Assign(ref strTpl, "address-val", dr["address"].ToString());
            TemplateHandler.Assign(ref strTpl, "city-val", dr["city"].ToString());
            TemplateHandler.Assign(ref strTpl, "state-val", dr["state"].ToString());
            TemplateHandler.Assign(ref strTpl, "post-val", dr["postal"].ToString());
            TemplateHandler.Assign(ref strTpl, "country-val", dr["countryID"].ToString());
            TemplateHandler.Assign(ref strTpl, "email-val", dr["email"].ToString());
            TemplateHandler.Assign(ref strTpl, "number-val", dr["phone"].ToString());
            TemplateHandler.Assign(ref strTpl, "mobile-val", dr["mobilephone"].ToString());
            TemplateHandler.Assign(ref strTpl, "url-val", dr["siteurl"].ToString());
            string strUrls = dr["moresiteurls"].ToString();
            if (strUrls.Trim().Length > 0)
            {
                string[] strMoreUrls = strUrls.Split(';');
                if (strMoreUrls.Length > 0)
                {
                    StringBuilder strBuild = new StringBuilder();
                    int index = 2;
                    string strSiteContent = TemplateHandler.CopyBlock(strTpl, "sitecontent");
                    for (int i = 0; i < strMoreUrls.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strMoreUrls[i].Trim()))
                        {
                            string strTmp = strSiteContent;
                            TemplateHandler.Assign(ref strTmp, "moreurl-val", strMoreUrls[i]);
                            TemplateHandler.Assign(ref strTmp, "i", index.ToString());
                            strBuild.Append(strTmp);
                            index++;
                        }   
                    }
                    TemplateHandler.ReplaceBlock(ref strTpl, "sitecontent", strBuild.ToString());
                }
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "sitecontent", string.Empty);
            }

            TemplateHandler.Assign(ref strTpl, "12betusername-val", dr["player_username"].ToString());
            TemplateHandler.Assign(ref strTpl, "netelleraccid-val", dr["neteller_accountid"].ToString());
            TemplateHandler.Assign(ref strTpl, "moneybookeremail-val", dr["moneybookers_email"].ToString());
            TemplateHandler.Assign(ref strTpl, "bankname-val", dr["wire_bankname"].ToString());
            TemplateHandler.Assign(ref strTpl, "bankaddress-val", dr["wire_bankaddress"].ToString());
            TemplateHandler.Assign(ref strTpl, "bankswift-val", dr["wire_swiftcode"].ToString());
            TemplateHandler.Assign(ref strTpl, "bankaccname-val", dr["wire_accountname"].ToString());
            TemplateHandler.Assign(ref strTpl, "bankaccnumber-val", dr["wire_accountno"].ToString());
            TemplateHandler.Assign(ref strTpl, "cheque_fullname", dr["cheque_fullname"].ToString());
            TemplateHandler.Assign(ref strTpl, "cheque_address", dr["cheque_address"].ToString());
            TemplateHandler.Assign(ref strTpl, "cheque_city", dr["cheque_city"].ToString());
            TemplateHandler.Assign(ref strTpl, "cheque_state", dr["cheque_state"].ToString());
            TemplateHandler.Assign(ref strTpl, "cheque_postal", dr["cheque_postal"].ToString());
            TemplateHandler.Assign(ref strTpl, "cheque_country", dr["cheque_country"].ToString());
            //method-val
            TemplateHandler.Assign(ref strTpl, "method-val", dr["preferredpayment"]);
            TemplateHandler.Assign(ref strTpl, "status-val", dr["status"].ToString());
            if (string.Compare(dr["status"].ToString(), Fanex.Affiliate.Resources.Translator.Get("Approved"), true) != 0)
            {
                TemplateHandler.RemoveBlock(ref strTpl, "overview");
                TemplateHandler.RemoveBlock(ref strTpl, "earningshistory");
            }
            //Min withdraw
            TemplateHandler.Assign(ref strTpl, "minnetteller", "$" + strMinNetteller);
            TemplateHandler.Assign(ref strTpl, "minbooker", "$" + strMinMoneyBooker);
            TemplateHandler.Assign(ref strTpl, "minbank", "$" + strMinWire);
            TemplateHandler.Assign(ref strTpl, "mincheque", "$" + strMinCheque);
            TemplateHandler.Assign(ref strTpl, "minplayer", "$" + strMinPlayer);
            switch(dr["preferredpayment"].ToString().ToLower())
            {
                case "player":
                    TemplateHandler.Assign(ref strTpl, "cssplayer", string.Empty);
                    break;
                case "netteller":
                    TemplateHandler.Assign(ref strTpl, "cssnetteller", string.Empty);
                    break;
                case "cheque":
                    TemplateHandler.Assign(ref strTpl, "csscheque", string.Empty);
                    break;
                case "moneybookers":
                    TemplateHandler.Assign(ref strTpl, "cssbooker", string.Empty);
                    break;
                case "wire":
                    TemplateHandler.Assign(ref strTpl, "cssbank", string.Empty);
                    break;
                default:
                    TemplateHandler.Assign(ref strTpl, "cssplayer", string.Empty);
                    break;
            }
            TemplateHandler.Assign(ref strTpl, "cssplayer", "hide");
            TemplateHandler.Assign(ref strTpl, "cssnetteller", "hide");
            TemplateHandler.Assign(ref strTpl, "csscheque", "hide");
            TemplateHandler.Assign(ref strTpl, "cssbooker", "hide");
            TemplateHandler.Assign(ref strTpl, "cssbank", "hide");

            /*Begin List currency*/

            TemplateHandler.Assign(ref strTpl, "currencydisable", dr["IsCurrencyEditable"].ToString() == "1" ? "" : "disabled");
            TemplateHandler.Assign(ref strTpl, "currency-val", dr["Currency"].ToString());

            var currencies = AppSettings.AffCurrencies;
            string strCurrencyContent = TemplateHandler.CopyBlock(strTpl, "currency");

            TemplateHandler.Assign(ref strTpl, "currencylist",
                HtmlHelper.DropDownList(currencies, "slCurrency", dr["Currency"], false, null, !(dr["IsCurrencyEditable"].ToString() == "1")));
            
            /*End List currency*/

        }
       
        #region Load SubPage
        string subPage = string.Empty;
        if (string.Compare(Request["approved"], "0", true) == 0)
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffPendingLink, SubMenu.AffPendingText, 0, 0, Fanex.Affiliate.Resources.Translator.Get("Edit"), "i_customer.png"));
        else
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 1, 0, Fanex.Affiliate.Resources.Translator.Get("Edit"), "i_customer.png"));
        
        TemplateHandler.Assign(ref subPage, "ContentPage", strTpl);
        #endregion

        this.HtmlContent = strTpl;

      
    }
}


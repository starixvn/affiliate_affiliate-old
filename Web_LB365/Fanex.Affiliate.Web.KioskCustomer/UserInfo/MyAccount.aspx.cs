using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;

public partial class MyAccount : BasePageLogIn
{
    /*protected string strFName = "";
    protected string strLName = "";*/
    protected bool isGender = true;
    protected bool hasGender = false;
    protected string strAddress = "";
    protected string strCity = "";
    protected string strState = "";
    protected string strPost = "";
    protected string strCountry = "";
    protected string strEmail = "";
    protected string strNumber = "";
    protected string strMobile = "";
    protected string strSiteUrl = "";
    protected string strMoreSiteUrl = "";
    protected string strBet12UserName = "";
    protected string strNetellerAccId = "";
    protected string strMoneyBookerEmail = "";
    protected string strChequeFullName = "";
    protected string strChequeAddress = "";
    protected string strChequeCity = "";
    protected string strChequeState = "";
    protected string strChequePostcode = "";
    protected string strChequeCountry = "";
    protected string strBankName = "";
    protected string strBankAddress = "";
    protected string strBankCode = "";
    protected string strBankAccName = "";
    protected string strBankAccNumber = "";
    protected string strPreferLang = "";
    protected string currencyCode = "";    
    protected string strMethod = "0";
    protected string strMinWire = "0";
    protected string strMinNetteller = "0";
    protected string strMinCheque = "0";
    protected string strMinMoneyBooker = "0";
    protected string strPayment = "0";
    protected string strPlayerAccount = "0";
    protected string strMinPlayer = string.Empty;
    protected string strWebsites = string.Empty;
    protected string strIsCurrencyEditable = string.Empty;
    protected string strCurrency  = String.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Load language
        ltrContactInfo.Text = Translator.Get("ContactInfo");
        ltrMyAcc.Text = Translator.Get("MyAcc");
        ltrUser.Text = Translator.Get("Username");
        ltrPass.Text = Translator.Get("Password");
        ltrAccInfo.Text = Translator.Get("AccInfo");

        //ltrNickname.Text = Translator.Get("Nickname");
        ltrEmail.Text = Translator.Get("Email");
        ltrContactNumber.Text = Translator.Get("ContactNumber");
        ltrMobile.Text = Translator.Get("Mobile");
        ltrPrefer.Text = Translator.Get("PreferLang");
        ltrLang.Text = Translator.Get("Language");
        ltrAddress.Text = Translator.Get("Address");
        ltrCity.Text = Translator.Get("City");
        ltrCountry.Text = Translator.Get("Country");
        ltrPostcode.Text = Translator.Get("PostCode");
        ltrState.Text = Translator.Get("State");
        ltrSiteinfor.Text = Translator.Get("SiteInfor");
        ltrSiteurl.Text = Translator.Get("SiteUrl");
        ltrMailOption.Text = Translator.Get("EmailFormat");
        ltrPlainText.Text = Translator.Get("PlainText");
        ltrHtmlFormat.Text = Translator.Get("HtmlFormat");
        //ltrMoreURL.Text = Translator.Get("More") + " " + Translator.Get("SiteUrl") + "s";
        //ltrMoreURLText.Text = Translator.Get("More") + " " + Translator.Get("SiteUrl") + "s";
        ltrPaymentInfor.Text = Translator.Get("PaymentInfor");
        ltrPaymentMethod.Text = Translator.Get("PaymentMethod");
        ltrAccountPlayerAccount.Text = Translator.Get("PlayerAcc");
        ltr12UserName.Text = Translator.Get("Bet12Username");// +" " + Translator.Get("Bet12");
        ltrCurrency.Text = Translator.Get("Currency");
        #endregion

        strPayment = Request["ispay"] == null ? "0" : "1";
        DataTable dtbMyAcc = new DataTable();
        dtbMyAcc = UserAff.SelAffInfo(Fanex.Affiliate.Core.AppSession.CustID, ref strMinCheque, ref strMinMoneyBooker, ref strMinNetteller, ref strMinWire,ref strMinPlayer);
        DataRow drAcc = dtbMyAcc.Rows[0];
        #region bind data
        hasGender = (DBNull.Value.Equals(drAcc["gender"]) ? false : true);
        isGender = CastAide.AsBoolean(drAcc["gender"], true);
        strAddress = drAcc["address"].ToString();
        strCity = drAcc["city"].ToString();
        strState = drAcc["state"].ToString();
        strPost = drAcc["postal"].ToString();
        strCountry = drAcc["countryID"].ToString();
        strEmail = drAcc["email"].ToString();
        strNumber = drAcc["phone"].ToString();
        strMobile = drAcc["mobilephone"].ToString();
        strSiteUrl = drAcc["siteurl"].ToString();
        strMoreSiteUrl = drAcc["moresiteurls"].ToString().Replace("\r\n", "\\n");
        strBet12UserName = drAcc["player_username"].ToString();
        strNetellerAccId = drAcc["Neteller_accountid"]. ToString();
        strMoneyBookerEmail = drAcc["moneybookers_email"].ToString();
        strChequeFullName = drAcc["cheque_fullname"].ToString();
        strChequeAddress = drAcc["cheque_address"].ToString();
        strChequeCity = drAcc["cheque_city"].ToString();
        strChequeState = drAcc["cheque_state"].ToString();
        strChequePostcode = drAcc["cheque_postal"].ToString();
        strChequeCountry = drAcc["cheque_country"].ToString();
        strBankName = drAcc["wire_bankname"].ToString();
        strBankAddress = drAcc["wire_bankaddress"].ToString();
        strBankCode = drAcc["wire_swiftcode"].ToString();
        strBankAccName = drAcc["wire_accountname"].ToString();
        strBankAccNumber = drAcc["wire_accountno"].ToString();
        strPreferLang = drAcc["PreferredLanguage"].ToString();
        currencyCode = drAcc["Currency"].ToString();
        strMethod = AffCommon.GetIndex(drAcc["preferredpayment"].ToString());
        strPlayerAccount = strMethod;
        ltrUsername.Text = drAcc["username"].ToString();
        strIsCurrencyEditable = drAcc["IsCurrencyEditable"].ToString();
        strCurrency = drAcc["Currency"].ToString();
        //Load more urls
        string strUrls = drAcc["MoreSiteURLs"].ToString();
        if (strUrls.Trim().Length > 0)
        {
            string strTpl = TemplateHandler.LoadTemplate("UserInfo/MoreUrls.htm");
            TemplateHandler.Assign(ref strTpl, "siteurl", Translator.Get("WebsiteURL"));
            TemplateHandler.Assign(ref strTpl, "del", Translator.Get("Del"));

            string[] strMoreUrls = strUrls.Split(';');
            if (strMoreUrls.Length > 0)
            {
                StringBuilder strBuild = new StringBuilder();
                int index = 2;
                for (int i = 0; i < strMoreUrls.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strMoreUrls[i].Trim()))
                    {
                        string strTmp = strTpl;
                        TemplateHandler.Assign(ref strTmp, "moreurl-val", strMoreUrls[i]);
                        TemplateHandler.Assign(ref strTmp, "i", index.ToString());
                        strBuild.Append(strTmp);
                        index++;
                    }                        
                }
                strWebsites = strBuild.ToString();
            }
        }
        #endregion
    }
}


namespace Fanex.Web.MYM.Customer
{
    using System;
    using System.Collections;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using System.Globalization;

    public partial class SignUp : BasePage
    {
        protected string strMinWire = "0";
        protected string strMinNetteller = "0";
        protected string strMinCheque = "0";
        protected string strMinMoneyBooker = "0";
        protected string strSecureQuestion = "";
        protected string currencyList = string.Empty;
        protected string languageList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            
           /* List<int> l = new List<int>();
            //l.Add(1);

            var m = l.Max();
            Response.Write(m);*/
            
            if (AppSession.IsAuthenticated)
                Response.Redirect("UserInfo/Account_Overview.aspx");
            ltrRegisForm.Text = Translator.Get("RegisForm");
            ltrCompleteForm.Text = Translator.Get("CompleteForm");
            ltrRequireInfo.Text = Translator.Get("RequireInfo");
            ltrContactInfo.Text = Translator.Get("ContactInfo");
            //ltrNickname.Text = Translator.Get("Nickname");
            ltrFName.Text = Translator.Get("FName");
            ltrLName.Text = Translator.Get("LName");
            ltrGender.Text = Translator.Get("Gender");
            ltrEmail.Text = Translator.Get("Email");
            ltrReEmail.Text = Translator.Get("ReEmail");
            ltrContactNumber.Text = Translator.Get("ContactNumber");
            ltrMobile.Text = Translator.Get("Mobile");
            ltrLang.Text = Translator.Get("Language");
            ltrCurrency.Text = Translator.Get("Currency");
            ltrPrefer.Text = Translator.Get("PreferLang");
            ltrMailOption.Text = Translator.Get("EmailFormat");
            ltrPlainText.Text = Translator.Get("PlainText");
            ltrHtmlFormat.Text = Translator.Get("HtmlFormat");
            ltrMailingInfor.Text = Translator.Get("MailingInfor");
            ltrAddress.Text = Translator.Get("Address");
            ltrCity.Text = Translator.Get("City");
            ltrCountry.Text = Translator.Get("Country");
            ltrPostcode.Text = Translator.Get("PostCode");
            ltrState.Text = Translator.Get("State");
            ltrSiteinfor.Text = Translator.Get("WebsiteInformat");
            ltrSiteurl.Text = Translator.Get("WebsiteURL");
            ltrPaymentInfor.Text = Translator.Get("PaymentInfor");
            ltrPaymentMethod.Text = Translator.Get("PaymentMethod");
            ltrAccountPlayerAccount.Text = Translator.Get("PlayerAcc");
            ltr12UserName.Text = Translator.Get("Bet12Username");// +" " + Translator.Get("Bet12");

            currencyList = HtmlHelper.DropDownList(AppSettings.AffCurrencies, "slCurrency", null, false);
            languageList = HtmlHelper.DropDownList(AppSettings.BaseLanguages, "slPreferLang", null, false);

            try
            {
                Hashtable hsMinWidthdraw = PaymentRequest.MinWithdraw();
                strMinCheque = NumberFormat.DecFormat(hsMinWidthdraw["aff_mincheque"], 0);
                strMinMoneyBooker = NumberFormat.DecFormat(hsMinWidthdraw["aff_minmoneybookers"], 0);
                strMinNetteller = NumberFormat.DecFormat(hsMinWidthdraw["aff_minneteller"], 0);
                strMinWire = NumberFormat.DecFormat(hsMinWidthdraw["aff_minwire"], 0);
                strSecureQuestion = DNAide.Web.TemplateHandler.LoadTemplate("UserInfo/SecureQuestion_" + AppSession.Language + ".htm");

            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message);
            }
        }
    }
}

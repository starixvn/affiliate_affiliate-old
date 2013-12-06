namespace Fanex.Web.ITCBet.Customer
{
    using System;
    using System.Text;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Resources;
    

    public partial class Controls_Footer : BaseUserControl
    {
        protected string strMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateScriptMessage();
        }

        void GenerateScriptMessage()
        {
            string _nl = Environment.NewLine;
            hlkProgram.Text = Translator.Get("ProgDetails");
            hlkCondition.Text = Translator.Get("TermCondition");
            hlkContactUs.Text = Translator.Get("ContactUs");
            hlkFaqs.Text = Translator.Get("Faqs");

            //hlkFAQ.Text = Resources.Translator.Get("Faqs;
            string strIsRequire = Translator.Get("IsRequired").Replace("\"", "'");
            string strIsInvalid = Translator.Get("IsInvalid").Replace("\"", "'");
            StringBuilder _s = new StringBuilder();
            _s.Append("var ErrMsg = {" + _nl); // global message
            _s.Append("Term: \"" + Translator.Get("TermErr").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsgentryvalidcode: \"" + Translator.Get("ErrmsgEntryValidCode").Replace("\"", "'") + "\"," + _nl);
            _s.Append("SecurityQuestion: \"" + Translator.Get("errSecQuestionEmpty") + "\"," + _nl);
            _s.Append("ContactNumber: \"" + Translator.Get("errContactNumberEmpty") + "\"," + _nl);
            _s.Append("MobileNumber: \"" + Translator.Get("errMobileNumberEmpty") + "\"," + _nl);
            _s.Append("YourAnswer: \"" + Translator.Get("errYourAnswer") + "\"," + _nl);
            _s.Append("Nickname: \"" + Translator.Get("NicknameIsInvalid") + "\"," + _nl);
            _s.Append("NicknameRequired: \"" + Translator.Get("NicknameIsRequired") + "\"," + _nl);
            _s.Append("FName: \"" + Translator.Get("FirstNameIsRequired") + "\"," + _nl);
            _s.Append("LName: \"" + Translator.Get("LastNameIsRequired") + "\"," + _nl);
            _s.Append("ErrGender: \"" + Translator.Get("ErrGender") + "\"," + _nl);
            _s.Append("Email: \"" + Translator.Get("EmailAddressIsRequired") + "\"," + _nl);
            _s.Append("EmailValid: \"" + Translator.Get("EmailAddressIsInvalid") + "\"," + _nl);
            _s.Append("EmailMatch: \"" + Translator.Get("Emailaddressest").Replace("\"", "'") + "\"," + _nl);
            _s.Append("Address: \"" + Translator.Get("AddressIsRequired") + "\"," + _nl);
            _s.Append("City: \"" + Translator.Get("CityIsRequired") + "\"," + _nl);
            _s.Append("State: \"" + Translator.Get("StateIsRequired") + "\"," + _nl);
            _s.Append("PostCode : \"" + Translator.Get("PostcodeZiIsRequired") + "\"," + _nl);
            _s.Append("Country : \"" + Translator.Get("CountryErr").Replace("\"", "'") + "\"," + _nl);
            _s.Append("SiteUrl : \"" + Translator.Get("WebsiteURLIsRequired") + "\"," + _nl);
            _s.Append("Currency : \"" + Translator.Get("CurrencyIsRequired") + "\"," + _nl);
            _s.Append("PreferLang : \"" + Translator.Get("PreferLangErr").Replace("\"", "'") + "\"," + _nl);
            _s.Append("UserNameRequire : \"" + Translator.Get("BETUserNameIsRequired") + "\"," + _nl);
            _s.Append("Neteller : \"" + Translator.Get("NetellerAccount") + "\"," + _nl);
            _s.Append("Moneybooker : \"" + Translator.Get("MoneyBooker").Replace("\"", "'") + " " + Translator.Get("Email1") + " " + strIsRequire + "\"," + _nl);
            _s.Append("MoneybookerValid : \"" + Translator.Get("MoneybookersEma") + "\"," + _nl);
            _s.Append("BankName : \"" + Translator.Get("BankNameisrequi") + "\"," + _nl);
            _s.Append("BankAddress : \"" + Translator.Get("BankAddressisre") + "\"," + _nl);
            _s.Append("BankCode : \"" + Translator.Get("BankABA_Swift_SIsRequired") + "\"," + _nl);
            _s.Append("BankAccName : \"" + Translator.Get("BankAccountNameIsRequired") + "\"," + _nl);
            _s.Append("BankAccNumber : \"" + Translator.Get("BankAccountNumbIsRequired") + "\"," + _nl);
            string strCheque = Translator.Get("Cheque").Replace("\"", "'");
            _s.Append("ChequeFullname : \"" + Translator.Get("ChequeFullNamei") + "\"," + _nl);
            _s.Append("ChequeAddress : \"" + Translator.Get("ChequeAddressis") + "\"," + _nl);
            _s.Append("ChequeCity : \"" + Translator.Get("ChequeCityisreq") + "\"," + _nl);
            _s.Append("ChequeState : \"" + Translator.Get("ChequeStateisre") + "\"," + _nl);
            
            _s.Append("ChequePostcode : \"" + Translator.Get("ChequePostcode_") + "\"," + _nl);
            _s.Append("OldPass : \"" + Translator.Get("OldPasswordisre") + "\"," + _nl);
            _s.Append("NewPass : \"" + Translator.Get("NewPasswordisre") + "\"," + _nl);
            _s.Append("RePass : \"" + Translator.Get("ReenterNewPassw") + "\"," + _nl);

            _s.Append("ChequeCountry : \"" + Translator.Get("ChequeCountryis") + "\"," + _nl);

            _s.Append("Name: \"" + Translator.Get("NameIsInvalid") + "\"," + _nl);
            _s.Append("Comment: \"" + Translator.Get("CommentsIsInvalid") + "\"," + _nl);
            _s.Append("Subject: \"" + Translator.Get("SubjectIsInvalid") + "\"," + _nl);
            _s.Append("RootUrl : \"" + AppSettings.FullDomainName + "\"," + _nl);

            _s.Append("language: \"" + "\"" + _nl);



            _s.Append("};");

            _s.Append("var gMsg = {" + _nl); // global message
            _s.Append("     entryusername: \"" + Translator.Get("Username").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     entrypassword: \"" + Translator.Get("Password").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     entryvalidation: \"" + Translator.Get("Validation").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsgentryvalidcode: \"" + Translator.Get("ErrmsgEntryValidCode").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsgentryusername: \"" + Translator.Get("ErrmsgEntryUsername").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsgentrypassword: \"" + Translator.Get("ErrmsgEntryPassword").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsgsqlcharacter: \"" + Translator.Get("ErrmsgSqlCharacter").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsghtmlsyntax: \"" + Translator.Get("ErrmsgHtmlSyntax").Replace("\"", "'") + "\"," + _nl);
            _s.Append("     Errmsgsqlinjection: \"" + Translator.Get("ErrmsgSqlInjection").Replace("\"", "'") + "\"" + _nl);
            _s.Append("};");
            strMsg = _s.ToString();
        }
    }
}
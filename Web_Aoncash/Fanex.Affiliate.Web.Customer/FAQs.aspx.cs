namespace Fanex.Web.Aoncash.Customer
{
    using System;
    using System.Collections;
    using DNAide.Core;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;

    public partial class FAQs : BasePage
    {
        protected int Netteller = 0;
        protected int Moneybookers = 0;
        protected int Bank = 0;
        protected int Cheque = 0;
        public string AppForm = "";
        public string MailContact = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.IsAuthenticated)
            {
                divJoin.Visible = false;
                AppForm = Translator.Get("RegisForm").ToLower();
            }
            else
            {
              
                AppForm = "<a href='Signup.aspx'>{AppForm}</a>".Replace("{AppForm}", Translator.Get("RegisForm").ToLower());
            }

            Hashtable hsPayment = PaymentRequest.MinWithdraw();
            Netteller = CastAide.AsInt32(hsPayment["aff_minneteller"]);
            Moneybookers = CastAide.AsInt32(hsPayment["aff_minmoneybookers"]);
            Bank = CastAide.AsInt32(hsPayment["aff_minwire"]);
            Cheque = CastAide.AsInt32(hsPayment["aff_mincheque"]);
            MailContact = "<a href='mailto:{mail}'>{mail}</a>".Replace("{mail}", AppSettings.MailContact);
        }
    }
}
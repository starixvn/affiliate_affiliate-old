using System;
using System.Web;
using DNAide.Web;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;

public partial class PaymentRequest : BasePageLogIn
{
    protected double dBalance = 0;
    protected double dMinAmt = 0;
    protected string strBalance = string.Empty;
    protected string strMinAmt = string.Empty;
    protected string strPaymentMethod = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        ltrListRequest.Text = Fanex.Affiliate.Core.Customer.GenerateHTML.PaymentRequest.PaymentList(ref strBalance, ref strMinAmt, ref strPaymentMethod,
            ref dBalance, ref dMinAmt);
        // strPayment = PaymentRequest.PaymentMethodSel(Fanex.Affiliate.Core.AppSession.CustID);//.ToLower();
        if (string.Compare(strPaymentMethod.ToLower(), "player", true) != 0)
        {
            strPaymentMethod = Translator.Get("ErrPaymentMethod").Replace("{method}", strPaymentMethod.Replace("Wire", Translator.Get("BankTransfer")));
        }
    }
}


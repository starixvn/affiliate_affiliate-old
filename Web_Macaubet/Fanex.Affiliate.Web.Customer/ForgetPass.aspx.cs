namespace Fanex.Web.Macaubet.Customer
{
    using System;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;

    public partial class ForgetPass : BasePage
    {
        public string submit, reset, emailrequire, emailinvalid;
        protected void Page_Load(object sender, EventArgs e)
        {

            string tplSuc = TemplateHandler.LoadTemplate("LoginDetailSucc.htm");
            TemplateHandler.Assign(ref tplSuc, "Yourlogindetail", Translator.Get("Yourlogindetail"));
            TemplateHandler.Assign(ref tplSuc, "Pleasecheckyour", Translator.Get("Pleasecheckyour"));
            TemplateHandler.Assign(ref tplSuc, "Foranyreasonyou", Translator.Get("Foranyreasonyou"));
            ltrmsgsucc.Text = tplSuc;

            ltryouremail.Text = Translator.Get("YourEmail");
            ltrtitleforget.Text = Translator.Get("TitelForgetPass");
            submit = Translator.Get("Submit");
            reset = Translator.Get("Reset");

            ltrtitelrecoveruspw.Text = Translator.Get("ForgetPassword");
            emailrequire = Translator.Get("ErrEmailRequire");
            emailinvalid = Translator.Get("ErrEmailInvalid");
        }
    }
}
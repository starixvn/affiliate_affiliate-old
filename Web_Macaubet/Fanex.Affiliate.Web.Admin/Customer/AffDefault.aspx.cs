namespace Fanex.Web.Macaubet.Admin.Customer
{
    using System;
    using DNAide.Core;

    public partial class Default : System.Web.UI.Page
    {
        protected string strUsername = string.Empty;
        protected string Pageid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int CustId = CastAide.AsInt32(Request["custid"], 0);
            strUsername = Request["user"];
            Pageid = Request["pageid"];
        }
    }
}
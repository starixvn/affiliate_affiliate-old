
namespace Fanex.Web.Bwinning.Customer
{
    using System;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;

    public partial class Controls_LeftPanel : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] strArr = AppSession.UserName.Split(' ');
            ltrUserName.Text = strArr[0];
        }
    }
}
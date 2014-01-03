using System;
using DNAide.Web;
using Fanex.Affiliate.Core;
public partial class SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UMTime_From = string.Empty, UMTime_To = string.Empty;
        bool isDownTime = AppCommon.GetDownTime(out UMTime_From, out UMTime_To);

        if (isDownTime)
        {
            Response.Redirect("~/UnderMaintenance.aspx");
            Response.End();
            return;
        }
    }
}

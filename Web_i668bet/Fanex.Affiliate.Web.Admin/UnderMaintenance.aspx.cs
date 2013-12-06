using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Core;
using Fanex.Affiliate.Core;

public partial class UnderMaintenance : System.Web.UI.Page
{
    protected string UMTime_From = string.Empty;
    protected string UMTime_To = string.Empty;

    protected override void OnLoad(EventArgs e)
    {
        bool isDownTime = AppCommon.GetDownTime(out UMTime_From, out UMTime_To);
        //UMTime_From = DateTime.Now.ToString("hh:mm tt"); 
        //DateTime.Now.ToString();
        //UMTime_To = DateTime.Now.AddHours(2).ToString("hh:mm tt");
        //return;

        if (!isDownTime)
        {
            Response.Redirect("~/SignIn.aspx");
            Response.End();
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(UMTime_From) || string.IsNullOrEmpty(UMTime_To))
            {
                UMTime_From = CastAide.AsString(Request.QueryString["from"], string.Empty);
                UMTime_To = CastAide.AsString(Request.QueryString["to"], string.Empty);
            }
        }
        
        base.OnLoad(e);
    }
}

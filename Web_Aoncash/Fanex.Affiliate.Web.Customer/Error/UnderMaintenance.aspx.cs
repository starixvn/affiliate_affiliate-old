using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Affiliate.Core;
using DNAide.Core;

namespace Fanex.Web.Aoncash.Customer.Error
{
    public partial class UnderMaintenance : System.Web.UI.Page
    {
        protected string UMTime_From = string.Empty;
        protected string UMTime_To = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isDownTime = AppCommon.GetDownTime(out UMTime_From, out UMTime_To);

            if (!isDownTime)
            {
                Response.Redirect("~/Root.aspx");
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

            //base.OnLoad(e);
        }
    }
}
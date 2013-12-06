using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DNAide.Core;
using Fanex.Affiliate.DataObjects;

public partial class DepMemberDetailUpd : System.Web.UI.Page
{
    protected string DepositUsername = string.Empty;
    protected string IBCUsername = string.Empty;
    protected int IBCCustId = 0;
    protected int AffId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string DepositUsername = Request["txtDepUsername"];
            string IBCUsername = Request["txtIBCUsername"];
            string ErrMsg = string.Empty;
            int ErrCode = 0;
            int IBCCustId = CastAide.AsInt32(Request["txtIBCCustId"]);
            int AffId = CastAide.AsInt32(Request["txtAffId"]);

            if (IBCCustId == 0 || AffId == 0)
                return;

            Affiliate.Update12BetPlayerStatusToDeposit(AffId, DepositUsername, IBCCustId, IBCUsername, out ErrCode, out ErrMsg);
            if (ErrCode != 0)
            {
                Response.Write("<script type='text/javascript'>alert('" + ErrMsg.Replace("'", string.Empty) + "')</script>");
            }
        }
        catch (Exception ex)
        {
            DNAide.Web.DNAideLog.Write(ex.Message);
#if DEBUG
            throw;
#endif
        }
    }
}

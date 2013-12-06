using System;
using System.Data;
using DNAide.Web;
using DNAide.Utils;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class PaymentUpdate : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Payment/PaymentUpdate.htm");
        AppCommon.AssignToAdminTemplate(ref strTpl,
        "PaymentInfo", "daterequest", "status", "username", "email", "fname", "lname",
        "PaymentMethod", "Amount", "remark", "approve", "reject", "update", "cancel",
        "ApproveErr", "completeddate", "UpdatedBy");

        TemplateHandler.Assign(ref strTpl, "page", Request["page"]);
        
        if (!string.IsNullOrEmpty(Request["transid"]))
        {
            Int64 intTransId = CastAide.AsInt64(Request["transid"], -1);
            DataTable dtb = Fanex.Affiliate.DataObjects.PaymentRequest.PaymentRequestById(intTransId);
            if (dtb.Rows.Count > 0)
            {
                DataRow dr = dtb.Rows[0];
                TemplateHandler.Assign(ref strTpl, "lang", dr["preferredlanguage"]);
                TemplateHandler.Assign(ref strTpl, "transid-val", dr["transid"].ToString());
                TemplateHandler.Assign(ref strTpl, "custid-val", dr["affid"].ToString());
                TemplateHandler.Assign(ref strTpl, "daterequest-val", dr["transdate"].ToString());
                TemplateHandler.Assign(ref strTpl, "username-val", dr["affusername"].ToString());
                TemplateHandler.Assign(ref strTpl, "email-val", dr["email"].ToString());
                TemplateHandler.Assign(ref strTpl, "fname-val", dr["firstname"].ToString());
                TemplateHandler.Assign(ref strTpl, "lname-val", dr["lastname"].ToString());
                TemplateHandler.Assign(ref strTpl, "preferred-val", dr["PreferredPayment"].ToString());
                ////NEW//TemplateHandler.Assign(ref strTpl, "amt-val", AdminCommon.FormatNumber(dr["amt"], dr["Prefix"].ToString()));
                TemplateHandler.Assign(ref strTpl, "amt-val", CurrencyUtils.Format(dr["amt"], dr["affcurrency"].ToString(), null));
                TemplateHandler.Assign(ref strTpl, "hdamt-val", dr["transferamount"]);
                TemplateHandler.Assign(ref strTpl, "currency", dr["affcurrency"].ToString());
                TemplateHandler.Assign(ref strTpl, "completeddate-val", DataFormat.FormatDateTimeEn12(dr["completeddate"]));
                TemplateHandler.Assign(ref strTpl, "creator-val", dr["adminname"].ToString());

                if (string.Compare(dr["status"].ToString(), "pending", true) == 0)
                    TemplateHandler.ReplaceBlock(ref strTpl, "others", string.Empty);
                else
                    TemplateHandler.ReplaceBlock(ref strTpl, "pending", string.Empty);

                TemplateHandler.Assign(ref strTpl, "status-val", AdminCommon.GerStatus(dr["status"].ToString()));

                StatusGer(ref strTpl, dr);
                
                TemplateHandler.Assign(ref strTpl, "12bet-val", dr["username2"].ToString());
                TemplateHandler.Assign(ref strTpl, "transferto", "Transfer To");
                TemplateHandler.Assign(ref strTpl, "transferto-val", dr["ibcusername"].ToString());
                TemplateHandler.Assign(ref strTpl, "transfer-amount", "Transfer Amount");

                var amount = Math.Abs(CastAide.AsDouble(dr["transferamount"].ToString(), 0));
                var currencycode = dr["playercurrencycode"].ToString();
                TemplateHandler.Assign(ref strTpl, "transfer-amount-val", CurrencyUtils.Format(amount,currencycode,null));

                this.HtmlContent = strTpl;
            }
            else
            {
                this.HtmlContent = "<h3>Invalid Transaction</h3>";
            }
        }
        else
        {
        }
    }

    void StatusGer(ref string strTpl, DataRow dr)
    {
        switch (dr["status"].ToString().ToLower())
        {
            case "pending":
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "submitcss", "buttonlogin");
                TemplateHandler.Assign(ref strTpl, "chkappoved", "");
                TemplateHandler.Assign(ref strTpl, "chkrejected", "");
                TemplateHandler.Assign(ref strTpl, "disable-val", "");
                break;
            case "cancelled":
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "submitcss", "disabled");
                TemplateHandler.Assign(ref strTpl, "chkappoved", string.Empty);
                TemplateHandler.Assign(ref strTpl, "chkrejected", string.Empty);
                TemplateHandler.Assign(ref strTpl, "disable-val", "disabled");
                break;
            case "rejected":
                ////NEW//MISING
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", AppCommon.CutLongText(dr["AdminDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", AppCommon.CutLongText(dr["AffDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                ////NEW//MISING
                TemplateHandler.Assign(ref strTpl, "submitcss", "disabled");
                TemplateHandler.Assign(ref strTpl, "chkappoved", string.Empty);
                TemplateHandler.Assign(ref strTpl, "chkrejected", "checked");
                TemplateHandler.Assign(ref strTpl, "disable-val", "disabled");
                break;
            case "approved":
                ////NEW//MISING
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", AppCommon.CutLongText(dr["AdminDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", AppCommon.CutLongText(dr["AffDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                ////NEW//MISING
                TemplateHandler.Assign(ref strTpl, "submitcss", "disabled");
                TemplateHandler.Assign(ref strTpl, "chkappoved", "checked");
                TemplateHandler.Assign(ref strTpl, "chkrejected", string.Empty);
                TemplateHandler.Assign(ref strTpl, "disable-val", "disabled");
                break;
        }
    }

}

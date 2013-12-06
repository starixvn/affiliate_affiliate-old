namespace Fanex.Web.ITCBet.Admin.Others
{
    using System;
    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Data;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;

    public partial class ViewEmailDetails : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strContent = TemplateHandler.LoadTemplate("Others/ViewEmail.htm");
            TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
            AppCommon.AssignToAdminTemplate(ref strContent,
            "Delete",
            "Cancel",
            "Subject",
            "From",
            "SentDate",
            "Content",
            "ConfirmDeleteEmail");

            int mid = CastAide.AsInt32(Request["mid"], 1);
            DbResult r = Fanex.Affiliate.DataObjects.EmailHistory.EmailHistorySel(mid, string.Empty, string.Empty, string.Empty, null, null, AppSettings.PageSize, 1);

            if (Convert.ToInt32(r.Outputs["nrows"]) > 0)
            {
                DataRow row = r.ResultSet.Tables[0].Rows[0];
                TemplateHandler.Assign(ref strContent, "Subject-val", row["subject"].ToString());
                TemplateHandler.Assign(ref strContent, "Sender-val", row["sender"].ToString());
                TemplateHandler.Assign(ref strContent, "Email-val", row["email"].ToString());
                TemplateHandler.Assign(ref strContent, "Subject-val", row["subject"].ToString());
                TemplateHandler.Assign(ref strContent, "Sent-val", AdminCommon.IBCDateTimeFormat(row["createdate"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref strContent, "Content-val", row["content"].ToString());
                TemplateHandler.Assign(ref strContent, "mid", row["mid"].ToString());
            }
            this.HtmlContent = strContent;
        }
    }
}

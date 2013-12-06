namespace Fanex.Web.Aoncash.Admin.GroupSetting
{
    using System;
    using DNAide.Core;
    using Fanex.Affiliate.Core;

    public partial class AssignGroup : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.IsGetAllCss = false;
            int affID = CastAide.AsInt32(Request["affID"], -1);
            bool isApproved = DNAide.Core.CastAide.AsBoolean(Request["IsApproved"], false);
            string affName = DNAide.Core.CastAide.AsString(Request["affName"], string.Empty);
            string MailOption = Request["mailoption"];
            bool isMultiSelect = DNAide.Core.CastAide.AsBoolean(Request["ismultiselect"]);

            var currency = CastAide.AsInt32(Request["currency"],0);

            this.HtmlContent = Fanex.Affiliate.Core.Admin.GenerateHTML.GroupSetting.GetAssignmentForm(affName, affID, isApproved, MailOption, currency, isMultiSelect);
        }
    }
}

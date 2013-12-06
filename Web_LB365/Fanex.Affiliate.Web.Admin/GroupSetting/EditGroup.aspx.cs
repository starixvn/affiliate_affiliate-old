namespace Fanex.Web.LiveBet365.AffiliateAdmin.GroupSetting
{
    using System;
    using Fanex.Affiliate.Core;

    public partial class EditGroup : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.IsGetAllCss = false;
            string result = string.Empty;
            string groupid = Request["groupid"];
            if (string.IsNullOrEmpty(groupid))
            {
                result = "There is no information available!";
                this.HtmlContent = result;
                return;
            }
            int groupID = -1;
            try
            {
                groupID = Convert.ToInt32(groupid);

            }
            catch
            {
                result = "There is no information available!";
            }
            if (string.IsNullOrEmpty(result))
            {
                result = Fanex.Affiliate.Core.Admin.GenerateHTML.GroupSetting.GetGroupSettingForEdit(groupID);
            }
            this.HtmlContent = result;
        }
    }
}

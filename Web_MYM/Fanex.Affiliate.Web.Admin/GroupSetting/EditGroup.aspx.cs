namespace Fanex.Web.MYM.Admin.GroupSetting
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
                result = Fanex.Affiliate.Resources.Admin.NoInfo;
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
                result = Fanex.Affiliate.Resources.Admin.NoInfo;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = Fanex.Affiliate.Core.Admin.GenerateHTML.GroupSetting.GetGroupSettingForEdit(groupID);
            }
            this.HtmlContent = result;
        }
    }
}

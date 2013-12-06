namespace Fanex.Web.Bwinning.Admin.GroupSetting
{
    using System;
    using Fanex.Affiliate.Core;

    public partial class AddGroup : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.IsGetAllCss = false;
            string result = string.Empty;
            string groupid = Request["groupid"];
            bool isApproved = DNAide.Core.CastAide.AsBoolean(Request["IsApproved"], false);

            string fName = DNAide.Core.CastAide.AsString(Request["fname"], string.Empty);
            string affName = DNAide.Core.CastAide.AsString(Request["username"], string.Empty);
            string language = DNAide.Core.CastAide.AsString(Request["lang"], string.Empty);
            string email = DNAide.Core.CastAide.AsString(Request["email"], string.Empty);



            int groupID = -1;
            try
            {
                groupID = Convert.ToInt32(groupid);

            }
            catch
            {
                groupID = -1;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = Fanex.Affiliate.Core.Admin.GenerateHTML.GroupSetting.GetGroupSettingForAddNew(groupID, isApproved, fName, affName, language, email);
            }
            this.HtmlContent = result;
        }

    }
}

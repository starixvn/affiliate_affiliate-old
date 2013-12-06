using System;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class Settings_Update : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Settings/Update.htm");
        AppCommon.AssignToAdminTemplate(ref settingContent,
            "Key", "Type", "Description", "Value", "Add","Update", "Cancel", "PlainText");
        var data = Settings.GetSetting(int.Parse(Request["id"]));
        var row = data.ResultSet.Tables[0].Rows[0];

        TemplateHandler.Assign(ref settingContent, "Id", Request["id"]);
        TemplateHandler.Assign(ref settingContent, "SName", row["SName"]);
        TemplateHandler.Assign(ref settingContent, "SValue", row["SValue"]);
        TemplateHandler.Assign(ref settingContent, "SDescription", row["Description"]);
        this.HtmlContent = settingContent;
    }
}

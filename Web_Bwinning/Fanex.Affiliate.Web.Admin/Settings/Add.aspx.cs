using System;
using DNAide.Web;
using System.Data;
using System.Collections.Generic;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;

public partial class Settings_Add : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Settings/Add.htm");
        AppCommon.AssignToAdminTemplate(ref settingContent,
            "Key", "Type", "Description", "Value", "Add", "Cancel","PlainText",
            "InputSettingVal","InputKeyName");
        var data = Settings.GetSetting("ConfigurationTypeList");
        IList<string> typeList = Common.GetStringIList(data.ResultSet.Tables[0].Rows[0]["SValue"].ToString(), ",");
        string types = string.Empty;
        foreach (var item in typeList)
        {
            types += string.Format("<option value='{0}'>{1}</option>", item, item);
        }
        TemplateHandler.Assign(ref settingContent, "Types", types);
        this.HtmlContent = settingContent;
    }
}

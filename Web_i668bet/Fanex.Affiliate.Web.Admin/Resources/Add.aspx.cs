using System;
using DNAide.Web;
using System.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Helper;

public partial class Resources_Add : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Resources/Add.htm");

        AppCommon.AssignToAdminTemplate(ref settingContent, 
            "Add", "Key", "Type", "Language", "Description", "Value", "PlainText", "Cancel",
            "InputKeyName","InpuResourceVal");
        
        var types = Resource.GetResourceTypes();
        string typeString = string.Empty;
        foreach (DataRow item in types.ResultSet.Tables[0].Rows)
        {
            typeString += string.Format("<option value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
        }
        TemplateHandler.Assign(ref settingContent, "Types", typeString);


        TemplateHandler.Assign(ref settingContent, "Languages",
        HtmlHelper.DropDownList(AppSettings.BaseLanguages, "lang", null, Fanex.Affiliate.Resources.Admin.All, null));


        this.HtmlContent = settingContent;
    }
}

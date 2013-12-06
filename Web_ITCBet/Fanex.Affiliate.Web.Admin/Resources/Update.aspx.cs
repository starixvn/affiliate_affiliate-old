using System;
using DNAide.Web;
using System.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.Core.Helper;

public partial class Resources_Update : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Resources/Update.htm");
        AppCommon.AssignToAdminTemplate(ref settingContent,
            "Update", "Key", "Type", "Language", "Description", "Value", "PlainText", "Cancel", "PlainText");
        var data = Resource.GetResource(new Guid(Request["id"].ToString()));
        var row = data.ResultSet.Tables[0].Rows[0];

        TemplateHandler.Assign(ref settingContent, "Id", Request["id"]);
        TemplateHandler.Assign(ref settingContent, "SName", row["ResourceKey"]);
        TemplateHandler.Assign(ref settingContent, "SValue", row["ResourceValue"]);
        TemplateHandler.Assign(ref settingContent, "SDescription", row["Description"]);

        var types = Resource.GetResourceTypes();
        string typeString = string.Empty;
        foreach (DataRow item in types.ResultSet.Tables[0].Rows)
        {
            if (item["Key"].ToString() == row["RefID"].ToString())
            {
                typeString += string.Format("<option selected='selected' value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
            }
            else
            {
                typeString += string.Format("<option value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
            }
        }
        TemplateHandler.Assign(ref settingContent, "Types", typeString);

        //var languages = Resource.GetResourceLanguages();
        //string langString = string.Empty;
        //foreach (DataRow item in languages.ResultSet.Tables[0].Rows)
        //{
        //    if (item["Key"].ToString() == Request["lang"])
        //    {
        //        langString += string.Format("<option selected='selected' value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
        //        break;
        //    }
        //    //else
        //    //{
        //    //    langString += string.Format("<option value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
        //    //}
        //}
        TemplateHandler.Assign(ref settingContent, "Languages",
            HtmlHelper.DropDownList(AppSettings.BaseLanguages, "lang", Request["lang"], Fanex.Affiliate.Resources.Admin.All, null));


        this.HtmlContent = settingContent;
    }

}

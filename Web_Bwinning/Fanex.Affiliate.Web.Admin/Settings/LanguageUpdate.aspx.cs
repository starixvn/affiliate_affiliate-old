using System;
using DNAide.Web;
using Fanex.Affiliate.DataObjects;
using System.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Helper;

public partial class LanguageUpdate : Fanex.Affiliate.Core.BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] columns = new string[] { "LanguageID", "LanguageName" };
        
        string settingContent = TemplateHandler.LoadTemplate("Language/Update.htm");

        AppCommon.AssignToAdminTemplate(ref settingContent,
          "ID","Name", "Country",  "Cancel");

        var id = Request["id"];
        var countryid = string.Empty;
       
        if (id == null) // Add
        {
            foreach (var col in columns)
            {
                TemplateHandler.Assign(ref settingContent, col, String.Empty);
            }
            TemplateHandler.Assign(ref settingContent, "Disabled", string.Empty);
            TemplateHandler.Assign(ref settingContent, "Action", Fanex.Affiliate.Resources.Admin.Add);
        }
        else // Update
        {
            var data = Language.Get(id);
            var row = data.ResultSet.Tables[0].Rows[0];

            foreach (var col in columns)
            {
                TemplateHandler.Assign(ref settingContent, col, row[col]);
            }
            TemplateHandler.Assign(ref settingContent, "Disabled", "disabled=true");
            TemplateHandler.Assign(ref settingContent, "Action", Fanex.Affiliate.Resources.Admin.Update);
            countryid = row["CountryID"].ToString();
        }
         TemplateHandler.Assign(ref settingContent, "Countries", 
            HtmlHelper.DropDownList(AppSettings.BaseCountries,"countries",countryid,false));
        this.HtmlContent = settingContent;
    }
}

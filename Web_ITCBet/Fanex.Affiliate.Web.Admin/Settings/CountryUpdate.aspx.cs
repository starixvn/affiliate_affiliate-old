using System;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using System.Data;

public partial class CountryUpdate : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] columns = new string[] { "CountryID", "CountryName", "Description", "Icon", "DisplayOrder" };
        
        string settingContent = TemplateHandler.LoadTemplate("Country/Update.htm");

        AppCommon.AssignToAdminTemplate(ref settingContent,
            "ID", "Name", "Description", "Icon", "DisplayOrder", "IsEnabled","Cancel");
        var id = Request["id"];
        if (id == null) // Add
        {
            foreach (var col in columns)
            {
                TemplateHandler.Assign(ref settingContent, col+"-val", String.Empty);
            }
            TemplateHandler.Assign(ref settingContent, "Action", Fanex.Affiliate.Resources.Admin.Add);
        }
        else // Update
        {
            var data = Country.GetCountry(id);
            var row = data.ResultSet.Tables[0].Rows[0];

            foreach (var col in columns)
            {
                TemplateHandler.Assign(ref settingContent, col+"-val", row[col]);
            }
            TemplateHandler.Assign(ref settingContent, "checked-val",  bool.Parse(row["isenabled"].ToString()) ? "checked": string.Empty);
            TemplateHandler.Assign(ref settingContent, "Action", Fanex.Affiliate.Resources.Admin.Update);
        }
        this.HtmlContent = settingContent;
    }
}

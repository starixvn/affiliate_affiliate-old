using System;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;

public partial class AffTypeUpdate : Fanex.Affiliate.Core.BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] columns = new string[] { "TypeID", "Name", "Description" };
        
        string settingContent = TemplateHandler.LoadTemplate("AffType/Update.htm");

        AppCommon.AssignToTemplate(ref settingContent,
        "Name", "Description", "BettypeList",  "Cancel");


        var id = Request["id"];
        if (id == null) // Add
        {
            foreach (var col in columns)
            {
                TemplateHandler.Assign(ref settingContent, col+"-val", String.Empty);
            }
            TemplateHandler.Assign(ref settingContent, "Action", "Add");
        }
        else // Update
        {
            var data = AffType.Get(id);
            var row = data.ResultSet.Tables[0].Rows[0];

            foreach (var col in columns)
            {
                TemplateHandler.Assign(ref settingContent, col + "-val", row[col]);
            }
            TemplateHandler.Assign(ref settingContent, "Action", "Update");
        }
        this.HtmlContent = settingContent;
    }
}

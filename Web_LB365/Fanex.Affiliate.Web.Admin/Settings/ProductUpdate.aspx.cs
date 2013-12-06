using System;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using System.Data;

public partial class ProductUpdate : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] columns = new string[] { "ProductID", "ProductName", "Description", "BettypeList" };
        
        string settingContent = TemplateHandler.LoadTemplate("Product/Update.htm");

        AppCommon.AssignToTemplate(ref settingContent,
          "Name", "Description", "BettypeList", "Cancel");

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
            var data = Product.Get(int.Parse(id));
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

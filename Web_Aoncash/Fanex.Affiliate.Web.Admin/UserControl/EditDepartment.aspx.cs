using System;
using System.Web;
using DNAide.Web;
using DNAide.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class EditDepartment : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/EditDepartment.html");
        string depid = Request["depid"];
        TemplateHandler.Assign(ref list, "Department", Fanex.Affiliate.Resources.Translator.Get("Department"));
        TemplateHandler.Assign(ref list, "Description", Fanex.Affiliate.Resources.Translator.Get("Description"));
        TemplateHandler.Assign(ref list, "submit", Fanex.Affiliate.Resources.Translator.Get("Submit"));
        TemplateHandler.Assign(ref list, "cancel", Fanex.Affiliate.Resources.Translator.Get("Cancel"));
        TemplateHandler.Assign(ref list, "depid", depid);
        //TemplateHandler.Assign(ref list, "submenu", Common.GenSubMenu(new string[] { "#" }, new string[] { "{DepartmentName}" }, 321, 0));
        DbResult dbr = null;
        dbr = UserControl.GetListDepartment(depid, AdminSession.IsIT);
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            TemplateHandler.Assign(ref list, "DepartmentName", dbr.ResultSet.Tables[0].Rows[0]["groupname"]);
            TemplateHandler.Assign(ref list, "DesciptionName", dbr.ResultSet.Tables[0].Rows[0]["groupdesc"]);             
        }
        this.HtmlContent = list;
    }
}

using System;
using System.Web;
using DNAide.Web;
using Fanex.Affiliate.Core;

public partial class AddDepartment : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/AddDepartment.html");
        AppCommon.AssignToTemplate(ref list, "Department", "Description", "submit", "cancel");
        
        //TemplateHandler.Assign(ref list, "submenu", Common.GenSubMenu(new string[] { "ListUser.aspx", "AddUser.aspx", "AddUserExist.aspx", "ListDepartment.aspx", "AddDepartment.aspx" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("AdministratorList, Fanex.Affiliate.Resources.Translator.Get("AddAdministrator, Fanex.Affiliate.Resources.Translator.Get("AddAdministratorExist, Fanex.Affiliate.Resources.Translator.Get("DepartmentList, Fanex.Affiliate.Resources.Translator.Get("AddDepartment }, 731, 4));
        this.HtmlContent = list;

    }
}

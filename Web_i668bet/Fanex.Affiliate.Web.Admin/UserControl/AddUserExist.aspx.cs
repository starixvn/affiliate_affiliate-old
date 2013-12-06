using System;
using System.Data;
using System.Web;
using DNAide.Web;
using DNAide.Data;
using System.Text;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class AddUserExist : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/AddUserExist.html");
        TemplateHandler.Assign(ref list, "UserList", Fanex.Affiliate.Resources.Translator.Get("AdministratorList"));
        TemplateHandler.Assign(ref list, "AddUser", Fanex.Affiliate.Resources.Translator.Get("AddAdministrator"));
        TemplateHandler.Assign(ref list, "AddUserExist", Fanex.Affiliate.Resources.Translator.Get("AddAdministratorExist"));
        TemplateHandler.Assign(ref list, "ListDepartment", Fanex.Affiliate.Resources.Translator.Get("DepartmentList"));        
        TemplateHandler.Assign(ref list, "AddDepartment", Fanex.Affiliate.Resources.Translator.Get("AddDepartment"));

        TemplateHandler.Assign(ref list, "username", Fanex.Affiliate.Resources.Translator.Get("Username"));       
        TemplateHandler.Assign(ref list, "department", Fanex.Affiliate.Resources.Translator.Get("Department"));
        TemplateHandler.Assign(ref list, "Add", Fanex.Affiliate.Resources.Translator.Get("Add"));
        TemplateHandler.Assign(ref list, "Cancel", Fanex.Affiliate.Resources.Translator.Get("Cancel"));
        //TemplateHandler.Assign(ref list, "submenu", Common.GenSubMenu(new string[] { "ListUser.aspx", "AddUser.aspx", "AddUserExist.aspx", "ListDepartment.aspx", "AddDepartment.aspx" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("AdministratorList, Fanex.Affiliate.Resources.Translator.Get("AddAdministrator, Fanex.Affiliate.Resources.Translator.Get("AddAdministratorExist, Fanex.Affiliate.Resources.Translator.Get("DepartmentList, Fanex.Affiliate.Resources.Translator.Get("AddDepartment }, 731, 2));
        StringBuilder res_opt = new StringBuilder();
        DbResult dbr = UserControl.GetSelectDepartment(AdminSession.IsIT);        
        string _option = "<Option value='{opt_value}'>{opt_name}</Option>";            
        foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
        {
            string option = _option;
            TemplateHandler.Assign(ref option, "opt_value", dr["groupid"]);
            TemplateHandler.Assign(ref option, "opt_name", dr["groupname"]);
            res_opt.Append(option); 
        }
        TemplateHandler.Assign(ref _option, "opt_value", "0");
        TemplateHandler.Assign(ref _option, "opt_name", "[Select Dept]");
        TemplateHandler.Assign(ref list, "option",_option + res_opt.ToString());       
        this.HtmlContent = list;
    }
}

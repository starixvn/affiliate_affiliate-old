using System;
using System.Data;
using System.Web;
using DNAide.Web;
using DNAide.Data;
using System.Text;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.Resources;

public partial class AddUser : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/AddUser.html");

        AppCommon.AssignToTemplate(ref list,
        "username","password","firstname","lastname","department","Add","Cancel");

        //TemplateHandler.Assign(ref list, "submenu", Common.GenSubMenu(new string[] { "ListUser.aspx", "AddUser.aspx", "AddUserExist.aspx", "ListDepartment.aspx", "AddDepartment.aspx" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("AdministratorList, Fanex.Affiliate.Resources.Translator.Get("AddAdministrator, Fanex.Affiliate.Resources.Translator.Get("AddAdministratorExist, Fanex.Affiliate.Resources.Translator.Get("DepartmentList, Fanex.Affiliate.Resources.Translator.Get("AddDepartment }, 731, 1));
        StringBuilder res_opt = new StringBuilder();
        DbResult dbr = UserControl.GetSelectDepartment(AdminSession.IsIT);        
        string _option = "<Option value='{opt_value}'>{opt_name}</Option>";
    
        foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
        {
            string option = _option;
            TemplateHandler.Assign(ref option, "opt_value", dr["groupid"]);
            TemplateHandler.Assign(ref option, "opt_name", dr["groupname"]);
            TemplateHandler.Assign(ref option, "selected", "selected");
            res_opt.Append(option); 
        }
        TemplateHandler.Assign(ref _option, "opt_value", "0");
        TemplateHandler.Assign(ref _option, "opt_name", "[Select Dept]"); 
        TemplateHandler.Assign(ref list, "option",_option + res_opt.ToString());
        TemplateHandler.Assign(ref list, "username-valitation", Translator.Get("ErrValidUserName"));     
        
        this.HtmlContent = list;
    }
}

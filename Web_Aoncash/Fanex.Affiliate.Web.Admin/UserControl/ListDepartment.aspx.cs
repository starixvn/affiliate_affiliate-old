using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Text;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class ListDepartment : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("UserControl/ListDepartment.html");
        //TemplateHandler.Assign(ref list, "UserList", Fanex.Affiliate.Resources.Translator.Get("AdministratorList);
        //TemplateHandler.Assign(ref list, "AddUser", Fanex.Affiliate.Resources.Translator.Get("AddAdministrator);
        //TemplateHandler.Assign(ref list, "AddUserExist", Fanex.Affiliate.Resources.Translator.Get("AddAdministratorExist);
        //TemplateHandler.Assign(ref list, "ListDepartment", Fanex.Affiliate.Resources.Translator.Get("DepartmentList);
        //TemplateHandler.Assign(ref list, "AddDepartment", Fanex.Affiliate.Resources.Translator.Get("AddDepartment);

        AppCommon.AssignToAdminTemplate(ref list,
        "Department",
        "NoOfUser",
        "Description",
        "RolePolicy",
        "Edit",
        "Del","AddDepartment","UpdateStaSucc","ChooseAtLeast","ConfirmDelete");

        TemplateHandler.Assign(ref list, "themepath", AppSettings.ThemeDir);
        //TemplateHandler.Assign(ref list, "submenu", Common.GenSubMenu(new string[] { "ListUser.aspx", "AddUser.aspx", "AddUserExist.aspx", "ListDepartment.aspx", "AddDepartment.aspx" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("AdministratorList, Fanex.Affiliate.Resources.Translator.Get("AddAdministrator, Fanex.Affiliate.Resources.Translator.Get("AddAdministratorExist, Fanex.Affiliate.Resources.Translator.Get("DepartmentList + " [{numDep}]", Fanex.Affiliate.Resources.Translator.Get("AddDepartment }, 881, 3));

        DbResult dbr = null;
        dbr = UserControl.GetListDepartment(null, AdminSession.IsIT);
        string _content = TemplateHandler.CopyBlock(list, "content");
        TemplateHandler.Assign(ref _content, "over", AdminCommon.RowBgOver);
        string _listcontent = "";
        StringBuilder strcontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
            {
                _listcontent = _content;
                counter++;
                TemplateHandler.Assign(ref _listcontent, "color", (counter % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref _listcontent, "No_Value", counter.ToString());
                TemplateHandler.Assign(ref _listcontent, "Department_Value", dr["groupname"]);
                if (string.Compare(CastAide.AsString(dr["groupname"]), "None", true) == 0)
                {
                    TemplateHandler.RemoveBlock(ref _listcontent, "lmt-DepNone");
                }
                if (CastAide.AsInt32(dr["nouser"]) <= 0)
                {
                    TemplateHandler.ReplaceBlock(ref _listcontent, "lmt-nouser", dr["nouser"].ToString());
                }
                else
                {
                    TemplateHandler.Assign(ref _listcontent, "NoOfUser_Value", dr["nouser"]);
                }
                TemplateHandler.Assign(ref _listcontent, "Description_Value", dr["groupdesc"]);
                TemplateHandler.Assign(ref _listcontent, "RolePolicy_Value", Fanex.Affiliate.Resources.Translator.Get("RolePolicy"));
                TemplateHandler.Assign(ref _listcontent, "Edit", Fanex.Affiliate.Resources.Translator.Get("Edit"));
                TemplateHandler.Assign(ref _listcontent, "Del", Fanex.Affiliate.Resources.Translator.Get("Del"));
                TemplateHandler.Assign(ref _listcontent, "depid", dr["groupid"]);
                strcontent.Append(_listcontent);
            }
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref _listcontent, "content", AdminCommon.GenNoInformation("7"));
        }
        TemplateHandler.Assign(ref list, "numDep", counter.ToString());
        TemplateHandler.ReplaceBlock(ref list, "content", strcontent.ToString());
        TemplateHandler.Assign(ref list, "submenu", SubMenu.GenSubMenu(SubMenu.AdminLink,
                                                                        SubMenu.AdminText,
                                                                        1, counter, Fanex.Affiliate.Resources.Translator.Get("Administrator"),
                                                                        "i_admin.png"));



        this.HtmlContent = list;
    }
}

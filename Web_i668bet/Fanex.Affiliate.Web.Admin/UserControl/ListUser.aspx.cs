using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;

public partial class ListUser : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("UserControl/ListUser.html");
                        
        TemplateHandler.Assign(ref list, "ViewAllAccount", Fanex.Affiliate.Resources.Admin.ViewAllAccount);

        TemplateHandler.Assign(ref list, "keyword-text", Fanex.Affiliate.Resources.Admin.Keyword);
        TemplateHandler.Assign(ref list, "firstletter", Fanex.Affiliate.Resources.Admin.FirstLetter);

        TemplateHandler.Assign(ref list, "fletter-text", Fanex.Affiliate.Resources.Admin.FirstLetter);
        TemplateHandler.Assign(ref list, "Department-val", Fanex.Affiliate.Resources.Admin.Department);
        TemplateHandler.Assign(ref list, "ChangeStatusMess-val", Fanex.Affiliate.Resources.Admin.ChangeStateMess);

        int dep = 0;

        string username = (string.IsNullOrEmpty(Request["txtKeyword"])) ? "" : Request["txtKeyword"];
        string firstletter = (string.IsNullOrEmpty(Request["alp"])) ? "All" : Request["alp"];

        TemplateHandler.Assign(ref list, "keyword", username);

        dep = (string.IsNullOrEmpty(Request["depid"]) ? 0 : CastAide.AsInt32(Request["depid"]));        
        TemplateHandler.Assign(ref list, "alphabet", AppCommon.GetListAlPhabet(firstletter));
        TemplateHandler.RemoveBlock(ref list, "lmt_chk");
        TemplateHandler.Assign(ref list, "numcol", "9");
            
        
        DbResult dbr = UserControl.GetListUser(dep, username, firstletter, AdminSession.IsIT);

        TemplateHandler.Assign(ref list, "_Value", username);
        TemplateHandler.Assign(ref list, "No", "#");
        AppCommon.AssignToAdminTemplate(ref list,
        "search","Department", "Username", "FirstName", "LastName", "Status", "Creator",
        "CreatedDate", "Policy", "Password","ChangePass","Submit");

        StringBuilder copycontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            DataTable dtb = dbr.ResultSet.Tables[0];
            string strContent = TemplateHandler.CopyBlock(list, "content");
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
               
                counter++;
                string content = strContent;
                TemplateHandler.Assign(ref content, "No_Value", CastAide.AsString(i + 1));
                TemplateHandler.Assign(ref content, "UserName_Value", dtb.Rows[i]["username"]);
                TemplateHandler.Assign(ref content, "FirstName_Value", dtb.Rows[i]["firstname"]);
                TemplateHandler.Assign(ref content, "LastName_Value", dtb.Rows[i]["lastname"]);
                TemplateHandler.Assign(ref content, "Status_Value", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? Fanex.Affiliate.Resources.Admin.Open : Fanex.Affiliate.Resources.Admin.Closed);
                //TemplateHandler.Assign(ref content, "checked", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "Checked" : string.Empty);
                TemplateHandler.Assign(ref content, "css-div", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "ic-check" : "ic-uncheck");

                TemplateHandler.Assign(ref content, "Creator_Value", dtb.Rows[i]["creator"]);
                TemplateHandler.Assign(ref content, "CreatedDate_Value", AdminCommon.IBCDateTimeFormat(dtb.Rows[i]["createdate"], DTFormatType.DT24h));
                
                TemplateHandler.Assign(ref content, "Department_Value", dtb.Rows[i]["groupname"]);
                
                TemplateHandler.Assign(ref content, "custid", dtb.Rows[i]["custid"]);
                TemplateHandler.Assign(ref content, "status", CastAide.AsBoolean(dtb.Rows[i]["closed"]));
                if (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false)
                {
                    TemplateHandler.Assign(ref content, "color", (counter % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);                    
                    TemplateHandler.Assign(ref content, "Class-val", string.Empty);
                }
                else
                {
                    //TemplateHandler.Assign(ref content, "Color", "#cfcfcf");
                    TemplateHandler.ReplaceBlock(ref content, "reset", String.Empty);
                    TemplateHandler.Assign(ref content, "color", AdminCommon.RowBgClosed);
                }
                TemplateHandler.Assign(ref content, "Depid", dtb.Rows[i]["groupid"]);
                TemplateHandler.Assign(ref content, "isAdmin", "1");
                copycontent.Append(content);
            }
            
            TemplateHandler.ReplaceBlock(ref list, "content", copycontent.ToString());
            TemplateHandler.Assign(ref list, "Policy_Value", Fanex.Affiliate.Resources.Admin.AccountPolicy);
            TemplateHandler.Assign(ref list, "disabled", string.Empty);
            
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref list, "content", "<tr><td colspan='10' class='c'>" + Fanex.Affiliate.Resources.Admin.NoInfo + "</td></tr>");
            TemplateHandler.Assign(ref list, "disabled", "disabled");
        }

        TemplateHandler.Assign(ref list, "submenu", SubMenu.GenSubMenu(SubMenu.AdminLink,
                                                                        SubMenu.AdminText,
                                                                        0, counter, SubMenu.AffText[0],
                                                                        "i_admin.png"));
        TemplateHandler.Assign(ref list, "AddAcc-val", Fanex.Affiliate.Resources.Admin.AddAdministrator);
        TemplateHandler.Assign(ref list, "AddExistedAcc-val", Fanex.Affiliate.Resources.Admin.AddAdministratorExist);
        TemplateHandler.Assign(ref list, "numUser", counter.ToString());
        TemplateHandler.Assign(ref list, "DeptId_value", dep);
        
        this.HtmlContent = list;
    }
}

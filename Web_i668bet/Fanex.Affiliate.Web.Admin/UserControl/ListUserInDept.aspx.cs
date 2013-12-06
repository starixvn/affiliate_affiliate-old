using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;

public partial class UserControl_ListUserInDept : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("UserControl/ListUserInDept.htm");

        //assigne static annotation
        //TemplateHandler.Assign(ref content, "ViewAllAccount", Fanex.Affiliate.Resources.Translator.Get("ViewAllAccount);
        //TemplateHandler.Assign(ref content, "Department", Fanex.Affiliate.Resources.Translator.Get("Department);
        AppCommon.AssignToAdminTemplate(ref content,
        "keyword", "firstletter", "search", "UserName", "FirstName", "LastName",
        "Status", "Creator", "CreatedDate", "Policy", "Password", "AccountPolicy", "Update", "ChangeStateMess",
        "Select","All","None");
        //TemplateHandler.Assign(ref content, "Reset_Value", Fanex.Affiliate.Resources.Translator.Get("Reset);


        //retrive params from request
        int depip = CastAide.AsInt32(Request["depid"], 0);
        string depname = CastAide.AsString(Request["depname"], string.Empty).Trim();
        string username = CastAide.AsString(Request["keyword"], "").Trim();
        string firstletter = CastAide.AsString(Request["alp"], "All");

        //assign dynamic annotation
        TemplateHandler.Assign(ref content, "fletter-val", firstletter.ToString());
        TemplateHandler.Assign(ref content, "alphabet", AppCommon.GetListAlPhabet(firstletter));
        TemplateHandler.Assign(ref content, "_Value", username);
        TemplateHandler.Assign(ref content, "Dep_value", depname);
        TemplateHandler.Assign(ref content, "depid-val", depip.ToString());
        TemplateHandler.Assign(ref content, "isReload", string.IsNullOrEmpty(Request["isReload"]) ? "0" : Request["isReload"]);

        DbResult dbr = UserControl.GetListUser(depip, username, firstletter, AdminSession.IsIT);

        StringBuilder copycontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            DataTable dtb = dbr.ResultSet.Tables[0];
            string strContent = TemplateHandler.CopyBlock(content, "content");
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                counter++;
                string row = strContent;
                //TemplateHandler.Assign(ref row, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref row, "No_Value", CastAide.AsString(i + 1));
                TemplateHandler.Assign(ref row, "UserName_Value", dtb.Rows[i]["username"]);
                TemplateHandler.Assign(ref row, "FirstName_Value", dtb.Rows[i]["firstname"]);
                TemplateHandler.Assign(ref row, "LastName_Value", dtb.Rows[i]["lastname"]);
                TemplateHandler.Assign(ref row, "Status_Value", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? Fanex.Affiliate.Resources.Admin.Open : Fanex.Affiliate.Resources.Admin.Closed);
                TemplateHandler.Assign(ref row, "div-class", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "chk ic-check" : "chk ic-uncheck");

                TemplateHandler.Assign(ref row, "Creator_Value", dtb.Rows[i]["creator"]);
                TemplateHandler.Assign(ref row, "CreatedDate_Value", AdminCommon.IBCDateTimeFormat(dtb.Rows[i]["createdate"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref row, "Policy_Value", Fanex.Affiliate.Resources.Translator.Get("AccountPolicy"));

                TemplateHandler.Assign(ref row, "custid", dtb.Rows[i]["custid"]);
                TemplateHandler.Assign(ref row, "status", CastAide.AsBoolean(dtb.Rows[i]["closed"]));
                if (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false)
                {
                    TemplateHandler.Assign(ref row, "color", (counter % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                    TemplateHandler.Assign(ref row, "Reset_Value", Fanex.Affiliate.Resources.Admin.Reset);
                }
                else
                {
                    TemplateHandler.Assign(ref row, "color", AdminCommon.RowBgClosed);
                    TemplateHandler.ReplaceBlock(ref row, "reset", string.Empty);
                }
                TemplateHandler.Assign(ref row, "depid", dtb.Rows[i]["groupid"]);
                TemplateHandler.Assign(ref row, "isAdmin", "1");
                copycontent.Append(row);
            }
            TemplateHandler.ReplaceBlock(ref content, "content", copycontent.ToString());
            TemplateHandler.Assign(ref content, "display-val", string.Empty);
            TemplateHandler.Assign(ref content, "totalAdmin-val", counter);

        }
        else
        {
            TemplateHandler.ReplaceBlock(ref content, "content", AdminCommon.NoInfo(10));
            TemplateHandler.Assign(ref content, "display-val", "none");
        }
        this.HtmlContent = content;
    }
}

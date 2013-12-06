using System;
using System.Data;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;

public partial class UserControl_EditAdminPolicy : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("UserControl/PolicyHeader.html");
        //acquire params from request
        int custId = CastAide.AsInt32(Request["custid"], -1);
        string username = CastAide.AsString(Request["us"], string.Empty);
        int depid = CastAide.AsInt32(Request["depid"], -1);
        string depname = CastAide.AsString(Request["depname"], string.Empty);

        //assign static entities in template 

        AppCommon.AssignToAdminTemplate(ref content,
        "Function", "IndividualPermission", "Read", "Host",
        "GroupPermission", "Update", "Cancel");

        TemplateHandler.Assign(ref content, "cols", "5");


        //public admin policy on to table

        string policyContent = TemplateHandler.LoadTemplate("UserControl/PolicyContent.html");
        string strContent = string.Empty;
        string strFuncId = string.Empty;
        string _strsubid = "";
        bool hasData = false;
        DbResult ds = UserControl.GetUserPolicy(custId);
        DataTable dtb = ds.ResultSet.Tables[0];

        if (ds != null)
        {
            if (ds.ResultSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    hasData = true;
                    string row = policyContent;
                    strFuncId += CastAide.AsString(dtb.Rows[i]["funcid"]) + ",";
                    username = CastAide.AsString(dtb.Rows[i]["username"]);
                    TemplateHandler.Assign(ref row, "{funcid}", CastAide.AsString(dtb.Rows[i]["funcid"]));

                    if (CastAide.AsInt32(dtb.Rows[i]["subfunccount"]) > 0)
                    {
                        string strsubid = string.Empty;
                        for (int j = 1; j < CastAide.AsInt32(dtb.Rows[i]["subfunccount"]) + 1; j++)
                        {
                            if (j == (CastAide.AsInt32(dtb.Rows[i]["subfunccount"])))
                            {
                                strsubid += CastAide.AsString(dtb.Rows[i + j]["funcid"]);
                            }
                            else
                            {
                                strsubid += CastAide.AsString(dtb.Rows[i + j]["funcid"]) + ";";
                            }
                        }
                        TemplateHandler.Assign(ref row, "{num_sub}", strsubid);


                        TemplateHandler.Assign(ref row, "{funcname}", CastAide.AsString(dtb.Rows[i]["funcdesc"]));
                        TemplateHandler.Assign(ref row, "{indent}", string.Empty);
                        TemplateHandler.Assign(ref row, "{color}", "#ffffff");

                        _strsubid = strsubid;
                    }
                    else
                    {
                        if (CastAide.AsInt32(dtb.Rows[i]["funcid"]) != CastAide.AsInt32(dtb.Rows[i]["parentid"]) && CastAide.AsInt32(dtb.Rows[i]["parentid"]) != 0)
                        {
                            TemplateHandler.Assign(ref row, "{funcname}", "<div class='indent'>&nbsp;</div>&nbsp;" + CastAide.AsString(dtb.Rows[i]["funcdesc"]));
                            TemplateHandler.Assign(ref row, "{indent}", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class='indent'>&nbsp;</div>");
                            TemplateHandler.Assign(ref row, "{color}", "#f0f1ec");

                            TemplateHandler.Assign(ref row, "{num_sub}", "S-" + CastAide.AsString(dtb.Rows[i]["parentid"]) + "_" + _strsubid);
                        }
                        else
                        {
                            TemplateHandler.Assign(ref row, "{funcname}", CastAide.AsString(dtb.Rows[i]["funcdesc"]));
                            TemplateHandler.Assign(ref row, "{indent}", string.Empty);
                            TemplateHandler.Assign(ref row, "{color}", "#ffffff");

                            TemplateHandler.Assign(ref row, "{num_sub}", "S-" + CastAide.AsString(dtb.Rows[i]["parentid"]));
                        }
                    }

                    TemplateHandler.Assign(ref row, "{funcidHR}", CastAide.AsString(dtb.Rows[i]["funcid"]));

                    if (CastAide.AsInt32(dtb.Rows[i]["policy"]) == 1)
                    {
                        TemplateHandler.Assign(ref row, "rdiv-class", "ic-check");
                        TemplateHandler.Assign(ref row, "hdiv-class", "ic-uncheck");
                    }
                    else if (CastAide.AsInt32(dtb.Rows[i]["policy"]) >= 2)
                    {
                        TemplateHandler.Assign(ref row, "rdiv-class", "ic-check");
                        TemplateHandler.Assign(ref row, "hdiv-class", "ic-check");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref row, "rdiv-class", "ic-uncheck");
                        TemplateHandler.Assign(ref row, "hdiv-class", "ic-uncheck");
                    }

                    //display policy of group which this account belong to
                    if (CastAide.AsInt32(dtb.Rows[i]["gpolicy"]) == 1)
                    {
                        TemplateHandler.Assign(ref row, "{gread-checked}", "checked=\"true\"");
                        TemplateHandler.Assign(ref row, "{ghost-checked}", "");
                    }
                    else if (CastAide.AsInt32(dtb.Rows[i]["gpolicy"]) >= 2)
                    {
                        TemplateHandler.Assign(ref row, "{gread-checked}", "checked=\"true\"");
                        TemplateHandler.Assign(ref row, "{ghost-checked}", "checked=\"true\"");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref row, "{gread-checked}", "");
                        TemplateHandler.Assign(ref row, "{ghost-checked}", "");
                    }

                    strContent += row;
                }
                TemplateHandler.Assign(ref content, "{groupid}", CastAide.AsString(ds.Outputs["groupid"]));
            }
            if (strFuncId.Length > 0) strFuncId = strFuncId.Substring(0, strFuncId.Length - 1);
            TemplateHandler.Assign(ref content, "{funcid}", strFuncId);

            TemplateHandler.Assign(ref content, "{arruser}", "");
            TemplateHandler.Assign(ref content, "{custid}", custId.ToString());
        }
        TemplateHandler.Assign(ref content, "{depname}", "<a href='#' onclick='ListUser(" + depid + ",\"" + depname + "\");'>" + depname + "</a>");
        TemplateHandler.Assign(ref content, "{user}", username);
        TemplateHandler.Assign(ref content, "{deptname}", depname);
        TemplateHandler.Assign(ref content, "{deptid}", depid);
        if (!hasData)
        {
            TemplateHandler.RemoveBlock(ref content, "{no-update}");
            strContent = AdminCommon.GenNoInformation("5");
        }
        TemplateHandler.Assign(ref content, "{content}", strContent);

        this.HtmlContent = content;
    }
}

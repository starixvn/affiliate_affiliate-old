using System;
using System.Data;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class UserControl_EditGroupPolicy : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("UserControl/PolicyHeader.html");
        
        //Assign value for static annotations
        TemplateHandler.RemoveBlock(ref content, "nonegpolicy");
        TemplateHandler.RemoveBlock(ref content, "individual");
        AppCommon.AssignToTemplate(ref content,
            "Function", "Read", "Host", "Update", "Cancel", "GroupPermission");

        //retrive param from request
        int depid = CastAide.AsInt32(Request["depid"], 0);
        string depname = CastAide.AsString(Request["depname"], string.Empty);

        TemplateHandler.Assign(ref content, "depname", depname);
        TemplateHandler.Assign(ref content, "deptname", depname);
        TemplateHandler.Assign(ref content, "deptid", depid.ToString());
        TemplateHandler.Assign(ref content, "cols", "3");

        //Publish group permission in table
        string policyContent = TemplateHandler.LoadTemplate("UserControl/PolicyContent.html");
        TemplateHandler.RemoveBlock(ref policyContent, "individual");           
        string strContent = string.Empty;
        string strFuncId = string.Empty;
        string _strsubid = "";
        DbResult dr = UserControl.GetGroupPolicy(depid);

        DataTable dtb = dr.ResultSet.Tables[0];
        if (dtb.Rows.Count > 0)
        {
            for (int i = 0; i < dtb.Rows.Count; i++)
            {

                string row = policyContent;
                strFuncId += CastAide.AsString(dtb.Rows[i]["funcid"]) + ",";
                depname = CastAide.AsString(dtb.Rows[i]["groupname"]);
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
                    TemplateHandler.Assign(ref row, "{rdiv-class}", "chk ic-check");
                    TemplateHandler.Assign(ref row, "{hdiv-class}", "chk ic-uncheck");
                }
                else if (CastAide.AsInt32(dtb.Rows[i]["policy"]) >= 2)
                {
                    TemplateHandler.Assign(ref row, "{rdiv-class}", "chk ic-check");
                    TemplateHandler.Assign(ref row, "{hdiv-class}", "chk ic-check");
                }
                else
                {
                    TemplateHandler.Assign(ref row, "{rdiv-class}", "chk ic-uncheck");
                    TemplateHandler.Assign(ref row, "{hdiv-class}", "chk ic-uncheck");
                }
                strContent += row;
            }
            if (strFuncId.Length > 0) strFuncId = strFuncId.Substring(0, strFuncId.Length - 1);
            TemplateHandler.Assign(ref content, "{funcid}", strFuncId);
            TemplateHandler.Assign(ref content, "{groupid}", depid.ToString());
            TemplateHandler.Assign(ref content, "{custid}", "");
            TemplateHandler.Assign(ref content, "{deptname}", depname);
            TemplateHandler.Assign(ref content, "{deptid}", depid.ToString());
            
        }
        else
        {
            TemplateHandler.RemoveBlock(ref content, "{no-update}");
            strContent = AdminCommon.GenNoInformation("3");
        }
        //TemplateHandler.RemoveBlock(ref content, "deplink");
        TemplateHandler.Assign(ref content, "{content}", strContent);

        this.HtmlContent = content;
    }
}

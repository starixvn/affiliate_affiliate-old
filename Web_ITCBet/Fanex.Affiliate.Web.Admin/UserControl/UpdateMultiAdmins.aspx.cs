using System;
using System.Data;
using System.Data.SqlClient;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class UserControl_UpdateMultiAdmins : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string arrCustId = CastAide.AsString(Request["custId"]);
        string arrUser = CastAide.AsString(Request["user"]);
        string dept = CastAide.AsString(Request["dept"]);
        string deptid = CastAide.AsString(Request["deptid"]);
        bool isListUser = string.IsNullOrEmpty(Request["isLstUser"]) ? false : CastAide.AsBoolean(Request["isLstUser"]);
        this.HtmlContent = GetMultUpdPolicy(arrCustId, arrUser, dept, deptid, isListUser);
    }
    private string GetMultUpdPolicy(string arrId, string arrUser, string dept, string deptid, bool isListUser)
    {
        string headerTpl = string.Empty;
        string rowTpl = string.Empty;
        headerTpl = TemplateHandler.LoadTemplate("UserControl/PolicyHeader.html");
        TemplateHandler.Assign(ref headerTpl, "{dept}", Fanex.Affiliate.Resources.Translator.Get("Department"));
        TemplateHandler.Assign(ref headerTpl, "{deptname}", dept);
        TemplateHandler.Assign(ref headerTpl, "{deptid}", deptid);

        TemplateHandler.Assign(ref headerTpl, "{acc}", "Account");
        

        AppCommon.AssignToAdminTemplate(ref headerTpl,
        "IndividualPermission", "Function",
        "GroupPermission", 
        "Read", 
        "Host", 
        "Update", 
        "Cancel");

        rowTpl = TemplateHandler.LoadTemplate("UserControl/PolicyContent.html");
        string strContent = string.Empty;
        string username = string.Empty;
        string strFuncId = string.Empty;
        string _strsubid = "";
        bool hasData = false;
        DbResult ds = UserControl.GetMultUpdPolicy(arrId);
        DataTable dtb = ds.ResultSet.Tables[0];
        if (ds != null)
        {
            if (ds.ResultSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.ResultSet.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.ResultSet.Tables[0].Rows[i];
                    hasData = true;
                    string row = rowTpl;
                    strFuncId += CastAide.AsString(dr["funcid"]) + ",";

                    TemplateHandler.Assign(ref row, "{funcid}", CastAide.AsString(dr["funcid"]));

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
                        if (CastAide.AsInt32(dtb.Rows[i]["funcid"]) != CastAide.AsInt32(dtb.Rows[i]["parentid"]))
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
                    //-------------


                    if (CastAide.AsInt32(dr["policy"]) == 1)
                    {
                        TemplateHandler.Assign(ref row, "rdiv-class", "chk ic-check");
                        TemplateHandler.Assign(ref row, "hdiv-class", "chk ic-uncheck");
                    }
                    else if (CastAide.AsInt32(dr["policy"]) >= 2)
                    {
                        TemplateHandler.Assign(ref row, "rdiv-class", "chk ic-check");
                        TemplateHandler.Assign(ref row, "hdiv-class", "chk ic-check");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref row, "rdiv-class", "chk ic-uncheck");
                        TemplateHandler.Assign(ref row, "hdiv-class", "chk ic-uncheck");
                    }
                    //display policy of group which this account belong to
                    if (CastAide.AsInt32(dr["gpolicy"]) == 1)
                    {
                        TemplateHandler.Assign(ref row, "{gread-checked}", "checked=\"true\"");
                        TemplateHandler.Assign(ref row, "{ghost-checked}", "");
                    }
                    else if (CastAide.AsInt32(dr["gpolicy"]) >= 2)
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
                TemplateHandler.Assign(ref headerTpl, "{groupid}", CastAide.AsString(ds.Outputs["groupid"]));
            }
            if (strFuncId.Length > 0) strFuncId = strFuncId.Substring(0, strFuncId.Length - 1);
            TemplateHandler.Assign(ref headerTpl, "{funcid}", strFuncId);
            TemplateHandler.Assign(ref headerTpl, "{custid}", arrId);
            TemplateHandler.Assign(ref headerTpl, "{arruser}", arrUser);
        }
        TemplateHandler.Assign(ref headerTpl, "{user}", arrUser);
        TemplateHandler.Assign(ref headerTpl, "{dep}", Fanex.Affiliate.Resources.Translator.Get("Department"));



        if (!isListUser)
        {
            TemplateHandler.Assign(ref headerTpl, "{depname}", "<a href='#' onclick='ListUser(" + deptid + ",\"" + dept + "\");'>" + dept + "</a>");
        }
        else
        {
            TemplateHandler.RemoveBlock(ref headerTpl, "deplink");
        }
        if (!hasData)
        {
            TemplateHandler.RemoveBlock(ref headerTpl, "{no-update}");
            strContent = AdminCommon.GenNoInformation("3");
        }
        TemplateHandler.Assign(ref headerTpl, "{content}", strContent);
        return headerTpl;
    }
}

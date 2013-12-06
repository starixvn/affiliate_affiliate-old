namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System.Web;
    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Web.SessionState;
    using System.Data;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Admin;
    using System;

    public class GroupPolicy : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int depid = string.IsNullOrEmpty(context.Request.Form["depid"]) ? 0 : CastAide.AsInt32(context.Request.Form["depid"]);
            string depname = string.IsNullOrEmpty(context.Request.Form["depname"]) ? "" : CastAide.AsString(context.Request.Form["depname"]);
            context.Response.Write(GetGroupPolicy(depid, depname));
        }
        private string GetGroupPolicy(int groupId, string groupname)
        {
            string headerTpl = string.Empty;
            string rowTpl = string.Empty;
            try
            {
                headerTpl = TemplateHandler.LoadTemplate("UserControl/PolicyHeader.html");
                TemplateHandler.Assign(ref headerTpl, "Function", Fanex.Affiliate.Resources.Translator.Get("Function"));

                TemplateHandler.Assign(ref headerTpl, "GroupPermission", Fanex.Affiliate.Resources.Translator.Get("GroupPermission"));
                TemplateHandler.Assign(ref headerTpl, "Read", Fanex.Affiliate.Resources.Translator.Get("Read"));
                TemplateHandler.Assign(ref headerTpl, "Host", Fanex.Affiliate.Resources.Translator.Get("Host"));
                TemplateHandler.Assign(ref headerTpl, "Update", Fanex.Affiliate.Resources.Translator.Get("Update"));
                TemplateHandler.Assign(ref headerTpl, "Cancel", Fanex.Affiliate.Resources.Translator.Get("Cancel"));

                TemplateHandler.Assign(ref headerTpl, "acc", Fanex.Affiliate.Resources.Translator.Get("Department"));
                //TemplateHandler.Assign(ref headerTpl, "user", "<a href='#' onclick='ListUser(" + groupId + ",\"" + groupname + "\");'>" + groupname + "</a>");
                TemplateHandler.Assign(ref headerTpl, "user", groupname);



                TemplateHandler.RemoveBlock(ref headerTpl, "individual");
                TemplateHandler.Assign(ref headerTpl, "cols", 3);

                TemplateHandler.Assign(ref headerTpl, "{dept}", Fanex.Affiliate.Resources.Translator.Get("Department"));
                rowTpl = TemplateHandler.LoadTemplate("UserControl/PolicyContent.html");
                TemplateHandler.RemoveBlock(ref rowTpl, "individual");
                string strContent = string.Empty;
                string strFuncId = string.Empty;
                string _strsubid = "";
                DbResult dr = UserControl.GetGroupPolicy(groupId);

                DataTable dtb = dr.ResultSet.Tables[0];
                if (dtb.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb.Rows.Count; i++)
                    {

                        string row = rowTpl;
                        strFuncId += CastAide.AsString(dtb.Rows[i]["funcid"]) + ",";
                        groupname = CastAide.AsString(dtb.Rows[i]["groupname"]);
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

                        if (CastAide.AsInt32(dtb.Rows[i]["policy"]) == 1)
                        {
                            TemplateHandler.Assign(ref row, "{read-checked}", "checked=\"true\"");
                            TemplateHandler.Assign(ref row, "{host-checked}", "");
                        }
                        else if (CastAide.AsInt32(dtb.Rows[i]["policy"]) >= 2)
                        {
                            TemplateHandler.Assign(ref row, "{read-checked}", "checked=\"true\"");
                            TemplateHandler.Assign(ref row, "{host-checked}", "checked=\"true\"");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref row, "{read-checked}", "");
                            TemplateHandler.Assign(ref row, "{host-checked}", "");
                        }
                        strContent += row;
                    }
                    if (strFuncId.Length > 0) strFuncId = strFuncId.Substring(0, strFuncId.Length - 1);
                    TemplateHandler.Assign(ref headerTpl, "{funcid}", strFuncId);
                    TemplateHandler.Assign(ref headerTpl, "{groupid}", groupId.ToString());
                    TemplateHandler.Assign(ref headerTpl, "{custid}", "");
                    TemplateHandler.Assign(ref headerTpl, "{deptname}", groupname);
                    TemplateHandler.Assign(ref headerTpl, "{deptid}", groupId.ToString());

                }
                else
                {
                    TemplateHandler.RemoveBlock(ref headerTpl, "{no-update}");
                    strContent = AdminCommon.GenNoInformation("3");
                }
                TemplateHandler.RemoveBlock(ref headerTpl, "deplink");
                TemplateHandler.Assign(ref headerTpl, "{content}", strContent);
            }
            catch(Exception ex)
            {
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
#if DEBUG
                throw;
#endif
            }
            return headerTpl;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}

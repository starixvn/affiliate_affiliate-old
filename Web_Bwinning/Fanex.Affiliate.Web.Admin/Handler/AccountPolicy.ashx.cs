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

    public class AccountPolicy : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int custid = string.IsNullOrEmpty(context.Request.Form["custid"]) ? 0 : CastAide.AsInt32(context.Request.Form["custid"]);
            string deptname = string.IsNullOrEmpty(context.Request.Form["depname"]) ? "" : context.Request.Form["depname"];
            string deptid = string.IsNullOrEmpty(context.Request.Form["depid"]) ? "" : context.Request.Form["depid"];
            bool isListUser = string.IsNullOrEmpty(context.Request.Form["isLstUser"]) ? false : CastAide.AsBoolean(context.Request.Form["isLstUser"]);
            context.Response.Write(GetAccountPolicy(custid, deptname, deptid, isListUser));
        }
        private string GetAccountPolicy(int custId, string deptname, string deptid, bool isListUser)
        {
            string headerTpl = string.Empty;
            string rowTpl = string.Empty;
            try
            {
                headerTpl = TemplateHandler.LoadTemplate("UserControl/PolicyHeader.html");
                // TemplateHandler.Assign(ref headerTpl, "{dept}", Fanex.Affiliate.Resources.Translator.Get("Department);
                TemplateHandler.Assign(ref headerTpl, "{dep}", Fanex.Affiliate.Resources.Translator.Get("Department"));
                TemplateHandler.Assign(ref headerTpl, "Function", Fanex.Affiliate.Resources.Translator.Get("Function"));
                TemplateHandler.Assign(ref headerTpl, "IndividualPermission", Fanex.Affiliate.Resources.Translator.Get("IndividualPermission"));
                TemplateHandler.Assign(ref headerTpl, "GroupPermission", Fanex.Affiliate.Resources.Translator.Get("GroupPermission"));
                TemplateHandler.Assign(ref headerTpl, "Read", Fanex.Affiliate.Resources.Translator.Get("Read"));
                TemplateHandler.Assign(ref headerTpl, "Host", Fanex.Affiliate.Resources.Translator.Get("Host"));
                TemplateHandler.Assign(ref headerTpl, "Update", Fanex.Affiliate.Resources.Translator.Get("Update"));
                TemplateHandler.Assign(ref headerTpl, "Cancel", Fanex.Affiliate.Resources.Translator.Get("Cancel"));

                TemplateHandler.Assign(ref headerTpl, "{acc}", "Account");
                TemplateHandler.Assign(ref headerTpl, "cols", 5);
                rowTpl = TemplateHandler.LoadTemplate("UserControl/PolicyContent.html");
                string strContent = string.Empty;
                string username = string.Empty;
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
                            string row = rowTpl;
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
                        TemplateHandler.Assign(ref headerTpl, "{groupid}", CastAide.AsString(ds.Outputs["groupid"]));
                    }
                    if (strFuncId.Length > 0) strFuncId = strFuncId.Substring(0, strFuncId.Length - 1);
                    TemplateHandler.Assign(ref headerTpl, "{funcid}", strFuncId);

                    TemplateHandler.Assign(ref headerTpl, "{arruser}", "");
                    TemplateHandler.Assign(ref headerTpl, "{custid}", custId.ToString());
                }
                //if (isListUser)
                //{
                //    TemplateHandler.Assign(ref headerTpl, "{depname}", deptname);

                //}
                //else
                //{
                //    TemplateHandler.Assign(ref headerTpl, "{depname}", "<a href='#' onclick='ListUser(" + deptid + ",\"" + deptname + "\");'>" + deptname + "</a>");
                //}
                TemplateHandler.Assign(ref headerTpl, "{depname}", "<a href='#' onclick='ListUser(" + deptid + ",\"" + deptname + "\");'>" + deptname + "</a>");
                TemplateHandler.Assign(ref headerTpl, "{user}", username);
                TemplateHandler.Assign(ref headerTpl, "{deptname}", deptname);
                TemplateHandler.Assign(ref headerTpl, "{deptid}", deptid);
                TemplateHandler.Assign(ref headerTpl, "{islistuser}", isListUser);
                if (!hasData)
                {
                    TemplateHandler.RemoveBlock(ref headerTpl, "{no-update}");
                    strContent = AdminCommon.GenNoInformation("5");
                }
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

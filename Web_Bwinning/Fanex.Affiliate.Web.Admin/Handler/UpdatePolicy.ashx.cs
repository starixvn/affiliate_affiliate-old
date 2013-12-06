namespace Fanex.Web.Bwinning.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Data;
    using DNAide.Core;
    using Fanex.Affiliate.DataObjects;
    using System;

    public class UpdatePolicy : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Write("<script language=\"javascript\" type=\"text/javascript\">");
            try
            {
                string custId = context.Request["custId"] as string ?? string.Empty;
                string groupId = context.Request["groupId"] as string ?? string.Empty;
                string dept = context.Request["dept"] as string ?? string.Empty;
                string deptid = context.Request["deptid"] as string ?? string.Empty;
                if (!custId.Equals(""))
                {
                    UpdateAdmin(context, custId, dept, deptid);
                }
                else if (custId.Equals("") && !groupId.Equals(""))
                {
                    UpdateGroup(context, groupId, dept, deptid);
                }
            }
            catch(Exception ex)
            {
                Fanex.Affiliate.Core.AppCommon.SendEmail(ex);
#if DEBUG
                throw;
#endif
            }
            context.Response.Write("parent.ibcWnd.close();</script>");

        }
        private void UpdateAdmin(HttpContext context, string custId, string dept, string deptid)
        {
            string groupPolicies = string.Empty;
            string[] id = null;
            DbResult dr = null;
            string funcId = context.Request["arrFuncId"] as string ?? string.Empty;
            string arrUser = context.Request["user"] as string ?? string.Empty;
            string[] arrFuncId = null;
            if (funcId.Length > 0)
            {
                arrFuncId = funcId.Split(',');
            }
            id = custId.Split(',');
            if (id.Length > 0)
            {
                for (int k = 0; k < id.Length; k++)
                {
                    if (id[k] != "")
                    {
                        for (int i = 0; i < arrFuncId.Length; i++)
                        {
                            string policy = string.Empty;
                            policy += id[k] + "," + arrFuncId[i];
                            if (CastAide.AsBoolean(context.Request["hdchkR_" + arrFuncId[i]], false) && CastAide.AsBoolean(context.Request["hdchkH_" + arrFuncId[i]], false))
                            {
                                policy += ",3";
                            }
                            else if (CastAide.AsBoolean(context.Request["hdchkR_" + arrFuncId[i]], false))
                            {
                                policy += ",1";
                            }
                            else if (CastAide.AsBoolean(context.Request["hdchkH_" + arrFuncId[i]], false))
                            {
                                policy += ",2";
                            }
                            else
                            {
                                policy += ",0";
                            }
                            policy += ";";
                            groupPolicies += policy;
                        }
                    }
                }
                dr = UserControl.UpdateUserPolicy(groupPolicies, Fanex.Affiliate.Core.AppSession.CustID);

                if (CastAide.AsInt32(dr.Outputs["errcode"]) != 0)
                {
                    context.Response.ContentType = "text/html";
                    context.Response.Write("parent.ErrAlter(\"" + Fanex.Affiliate.Resources.Translator.Get("UpdateRolePolicyErr") + "\");");
                }
                else
                {
                    if (arrUser.Equals(""))
                    {
                        context.Response.Write("parent.ListPolicyUser(" + custId.ToString() + ",'" + dept + "','" + deptid + "',false);");
                    }
                    else
                    {
                        context.Response.Write("parent.OpenUpdateMult('" + deptid + "','" + dept + "');");
                    }
                    context.Response.Write("parent.SuccAlter('" + Fanex.Affiliate.Resources.Translator.Get("UpdateRolePolicySucc") + "');");
                }
            }
        }

        private void UpdateGroup(HttpContext context, string groupId, string dept, string deptid)
        {
            string groupPolicies = string.Empty;
            DbResult dbr = null;
            string funcId = context.Request["arrFuncId"] as string ?? string.Empty;
            string[] arrFuncId = null;
            if (funcId.Length > 0)
            {
                arrFuncId = funcId.Split(',');
            }
            for (int i = 0; i < arrFuncId.Length; i++)
            {
                if (arrFuncId[i] != "")
                {
                    string policy = string.Empty;
                    policy += groupId + "," + arrFuncId[i];
                    if (CastAide.AsBoolean(context.Request["hdchkR_" + arrFuncId[i]], false) && CastAide.AsBoolean(context.Request["hdchkH_" + arrFuncId[i]], false))
                    {
                        policy += ",3";
                    }
                    else if (CastAide.AsBoolean(context.Request["hdchkR_" + arrFuncId[i]], false))
                    {
                        policy += ",1";
                    }
                    else if (CastAide.AsBoolean(context.Request["hdchkH_" + arrFuncId[i]], false))
                    {
                        policy += ",2";
                    }
                    else
                    {
                        policy += ",0";
                    }
                    policy += ";";
                    groupPolicies += policy;
                }
            }
            dbr = UserControl.UpdateGroupPolicy(groupPolicies, Fanex.Affiliate.Core.AppSession.CustID);
            if (CastAide.AsInt32(dbr.Outputs["errcode"]) != 0)
            {
                context.Response.Write("parent.ErrAlter('" + dbr.Outputs["errmsg"] + "');");
            }
            else
            {
                //context.Response.Write("parent.ListPolicyDep(" + groupId.ToString() + ",'" + dept + "');");
                context.Response.Write("parent.SuccAlter('" + Fanex.Affiliate.Resources.Translator.Get("UpdateRolePolicySucc") + "');");
            }
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

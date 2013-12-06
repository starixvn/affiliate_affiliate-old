namespace Fanex.Web.i668bet.Admin.Handler
{
    using System.Web;
    using System.Web.SessionState;
    using DNAide.Data;
    using DNAide.Web;
    using DNAide.Core;
    using System.Text;
    using System.Data;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core;
    public class ListUser : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            string list = TemplateHandler.LoadTemplate("UserControl/ListUserContent.html");

            int dep = (string.IsNullOrEmpty(context.Request["depid"]) ? 0 : CastAide.AsInt32(context.Request["depid"]));
            string depname = (string.IsNullOrEmpty(context.Request["depname"]) ? "" : context.Request["depname"]);

            string username = (string.IsNullOrEmpty(context.Request["us"])) ? "All" : context.Request["us"];
            string firstletter = (string.IsNullOrEmpty(context.Request["fl"])) ? "All" : context.Request["fl"];
            TemplateHandler.Assign(ref list, "search", Fanex.Affiliate.Resources.Translator.Get("Search"));
            TemplateHandler.Assign(ref list, "alphabet", AppCommon.GetListAlPhabet(firstletter));


            TemplateHandler.Assign(ref list, "Dep_value", depname);
            TemplateHandler.Assign(ref list, "DeptId_value", dep.ToString());

            TemplateHandler.Assign(ref list, "numcol", "8");
            DbResult dbr = UserControl.GetListUser(dep, username, firstletter);
            TemplateHandler.Assign(ref list, "_Value", username);

            TemplateHandler.Assign(ref list, "Department", Fanex.Affiliate.Resources.Translator.Get("Department"));
            TemplateHandler.Assign(ref list, "No", "#");
            TemplateHandler.Assign(ref list, "UserName", Fanex.Affiliate.Resources.Translator.Get("Username"));
            TemplateHandler.Assign(ref list, "FirstName", Fanex.Affiliate.Resources.Translator.Get("FirstName"));
            TemplateHandler.Assign(ref list, "LastName", Fanex.Affiliate.Resources.Translator.Get("LastName"));
            TemplateHandler.Assign(ref list, "Status", Fanex.Affiliate.Resources.Translator.Get("Status"));
            TemplateHandler.Assign(ref list, "Creator", Fanex.Affiliate.Resources.Translator.Get("Creator"));
            TemplateHandler.Assign(ref list, "CreatedDate", Fanex.Affiliate.Resources.Translator.Get("CreateDate"));
            TemplateHandler.Assign(ref list, "Policy", Fanex.Affiliate.Resources.Translator.Get("Policy"));
            TemplateHandler.Assign(ref list, "Reset", Fanex.Affiliate.Resources.Translator.Get("Password"));
            TemplateHandler.Assign(ref list, "ViewAllAccount", Fanex.Affiliate.Resources.Translator.Get("ViewAllAccount"));

            TemplateHandler.Assign(ref list, "keyword", Fanex.Affiliate.Resources.Translator.Get("Keyword"));
            TemplateHandler.Assign(ref list, "firstletter", Fanex.Affiliate.Resources.Translator.Get("FirstLetter"));
            TemplateHandler.Assign(ref list, "isAdmin", "1");

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
                    TemplateHandler.Assign(ref content, "color", (counter % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                    TemplateHandler.Assign(ref content, "No_Value", CastAide.AsString(i + 1));
                    TemplateHandler.Assign(ref content, "UserName_Value", dtb.Rows[i]["username"]);
                    TemplateHandler.Assign(ref content, "FirstName_Value", dtb.Rows[i]["firstname"]);
                    TemplateHandler.Assign(ref content, "LastName_Value", dtb.Rows[i]["lastname"]);
                    TemplateHandler.Assign(ref content, "Status_Value", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? Fanex.Affiliate.Resources.Translator.Get("Open") : Fanex.Affiliate.Resources.Translator.Get("Closed"));
                    TemplateHandler.Assign(ref content, "checked", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "Checked" : string.Empty);

                    TemplateHandler.Assign(ref content, "Creator_Value", dtb.Rows[i]["creator"]);
                    TemplateHandler.Assign(ref content, "CreatedDate_Value", dtb.Rows[i]["createdate"]);
                    TemplateHandler.Assign(ref content, "Policy_Value", Fanex.Affiliate.Resources.Translator.Get("AccountPolicy"));

                    TemplateHandler.Assign(ref content, "custid", dtb.Rows[i]["custid"]);
                    TemplateHandler.Assign(ref content, "status", CastAide.AsBoolean(dtb.Rows[i]["closed"]));
                    if (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false)
                    {
                        TemplateHandler.Assign(ref content, "Color", "#ffffff");
                        TemplateHandler.Assign(ref content, "Reset_Value", Fanex.Affiliate.Resources.Translator.Get("Reset"));
                    }
                    else
                    {
                        TemplateHandler.Assign(ref content, "Color", "#cfcfcf");
                        TemplateHandler.ReplaceBlock(ref content, "reset", Fanex.Affiliate.Resources.Translator.Get("Reset"));
                    }
                    TemplateHandler.Assign(ref content, "Depid", dtb.Rows[i]["groupid"]);
                    copycontent.Append(content);
                }
                TemplateHandler.ReplaceBlock(ref list, "content", copycontent.ToString());
                TemplateHandler.Assign(ref list, "disabled", string.Empty);

            }
            else
            {
                TemplateHandler.ReplaceBlock(ref list, "content", AdminCommon.NoInfo(10));
                TemplateHandler.Assign(ref list, "disabled", "disabled");
            }


            TemplateHandler.Assign(ref list, "numUser", counter.ToString());

            context.Response.Write(list);
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

using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.Core.Helper;

public partial class UserInfo_MemberListAll : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MemListAll.htm");
        //TemplateHandler.Assign(ref strTpl, "CurrentBalanceMem", Fanex.Affiliate.Resources.Translator.Get("CurrentBalance);
        AppCommon.AssignToAdminTemplate(ref strTpl,
        "action", "Edit", "username", "fname", "lname", "Reset", "Reset", "password", "Back",
        "LastLogging", "Status", "search", "affiliate", "remove","AssignMemToAff","AffType","Username","Currency");

        TemplateHandler.Assign(ref strTpl, "CreateDate", "First Deposit Date");
        TemplateHandler.Assign(ref strTpl, "RegisteredDate", Admin.registerdate);

        string username = string.Empty, affUsername = string.Empty;
        int totalPages = 1, pageIndex = 0, NRows = 0;//affid = -1
        int width = 900;

        TemplateHandler.Assign(ref strTpl, "pagepadd", "padl0");
        string strContent = TemplateHandler.CopyBlock(strTpl, "content");

        username = string.IsNullOrEmpty(Request["txtUsername"]) ? string.Empty : Request["txtUsername"].Trim();
        affUsername = string.IsNullOrEmpty(Request["txtAffUsername"]) ? string.Empty : Request["txtAffUsername"].Trim();

        int afftype = CastAide.AsInt32(Request["afftypelist"], -1);

        var hideAffType = AppSettings.BaseAffTypes.Count < 2;

        if (hideAffType)
        {            
            TemplateHandler.RemoveBlock(ref strTpl, "afftype");
            TemplateHandler.RemoveBlock(ref strContent, "afftype");
        }
        else
            TemplateHandler.Assign(ref strTpl, "afftypelist",
                HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypelist", afftype, hideAffType ? null : "All types", null, hideAffType));


        pageIndex = CastAide.AsInt32(Request["page"], 1);

        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        TemplateHandler.Assign(ref strTpl, "width", width);

        //check sample data of search criteria
        username = (String.Compare(username, string.Empty, true) == 0) ? null : username;

        int intPage = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        DataTable dtbResult = Member.SelMemInfoAll(affUsername, username,afftype, ref pageIndex, intPageSize, ref totalPages, ref NRows);


        StringBuilder strBuild = new StringBuilder();
        /*bool isHost = Common.isHost(Fanex.Affiliate.Core.AppSession.CustID, "[Affiliate]");
        if (!isHost)
        {
            TemplateHandler.RemoveBlock(ref strTpl, "Remove1");
            TemplateHandler.RemoveBlock(ref strContent, "Remove2");
        }*/
        if (dtbResult.Rows.Count > 0)
        {
            int i = (pageIndex - 1) * intPageSize;// hardcode pagesize for member/player list
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            foreach (DataRow dr in dtbResult.Rows)
            {
                i++;
                string strTmp = strContent;
                
                TemplateHandler.Assign(ref strTmp, "color", (i % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "number", i.ToString());
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                // TemplateHandler.Assign(ref strTmp, "IBCCustID-val", dr["ibccustid"].ToString());
                    
                TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"].ToString());
                TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"].ToString());
                TemplateHandler.Assign(ref strTmp, "currency-val", dr["currency"].ToString());
                // TemplateHandler.Assign(ref strTmp, "CurrentBalanceMem-val", Common.FormatNumber(dr["CurrBalance"]));
                TemplateHandler.Assign(ref strTmp, "RegisteredDate-val", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dr["RegisteredDate"]));
                TemplateHandler.Assign(ref strTmp, "CreateDate-val", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dr["createddate"]));
                TemplateHandler.Assign(ref strTmp, "affiliate-val", dr["affusername"]);
                if(!hideAffType)
                TemplateHandler.Assign(ref strTmp, "afftype-val", dr["afftypename"]);
                strBuild.Append(strTmp);
            }
        }
        var isShowAssignMemToAff = AppSettings.SystemName.Equals("livebet365",StringComparison.OrdinalIgnoreCase) ||
            AppSettings.SystemName.Equals("itcbet", StringComparison.OrdinalIgnoreCase);
        TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPages, 
            pageIndex, intPageSize, NRows, "aPageTop", "txtPageTop",
            isShowAssignMemToAff ? new string[] { Admin.AssignMemToAff } : new string[] {},
            isShowAssignMemToAff ? new string[] { "DoCal('AddMemberToAffiliate.aspx')" } : new string[] { },
            width, false, true, 
            true, false));


        TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPages, pageIndex, intPageSize, NRows, "aPageBottom", "txtPageBottom", width, false, false));
        if (NRows == 0)
        {
            TemplateHandler.Assign(ref strTpl, "pagingcss", "hide");
            strBuild.Append(AdminCommon.NoInfo(7));
        }

        //Hung submenu      

        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, 2, NRows, string.Empty, "i_member.png"));
            
            
        //Add search bar
        string strUserName = CastAide.AsString(Request["txtUsername"], string.Empty);

        TemplateHandler.Assign(ref strTpl, "username-val", username);

        strUserName = ((string.Compare(strUserName, Fanex.Affiliate.Resources.Translator.Get("Username"), true) == 0 ? string.Empty : strUserName));

        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        TemplateHandler.Assign(ref strTpl, "affusername-val", affUsername);
        TemplateHandler.Assign(ref strTpl, "page", pageIndex);
        TemplateHandler.Assign(ref strTpl, "pagesize", intPageSize);
        TemplateHandler.Assign(ref strTpl, "nummem-val", NRows);

        TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
        this.HtmlContent = strTpl;
    }
}

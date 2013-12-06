using System;
using DNAide.Core;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Helper;
using Fanex.Affiliate.Resources;
using Fanex.Affiliate.Core.Admin;

public partial class AffiliateList : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/AffList.htm");
        #region Assign submenu
        string strStatus = CastAide.AsString(Request["status"], "all");

        int currTab = 1;
        int width = 700;
        bool isFavor = false;
        bool isSelectAll = false;
        bool isClosed = false;
        bool lockedOut = false;
        switch (strStatus.ToLower())
        {
            case "pending":
                //currTab = 2;                
                TemplateHandler.RemoveBlock(ref strTemplate, "GroupNameSearch");
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
                TemplateHandler.RemoveBlock(ref strTemplate, "pass");
                TemplateHandler.RemoveBlock(ref strTemplate, "approved1");
                TemplateHandler.Assign(ref strTemplate, "tabpending", string.Empty);
                TemplateHandler.Assign(ref strTemplate, "userdate", Admin.registerdate);
                TemplateHandler.ReplaceBlock(ref strTemplate, "pending", Translator.Get("Pending") + " [<span id='nummem'>{nummem-val}</span>]");
                break;
            case "approved":
            case "lockedout":
            case "closed":
            case "all":
                isFavor = CastAide.AsInt32(Request["favor"], 0) == 1 ? true : false;
                isClosed = (strStatus.ToLower() == "closed" ? true : false);
                lockedOut = (strStatus.ToLower() == "lockedout" ? true : false);

                if (strStatus.ToLower() == "all")
                {
                    strStatus = string.Empty;
                }
                else
                {
                    strStatus = "approved";
                }

                isSelectAll = true;
                //currTab = isFavor ? 0 : 1;
                //currTab = isClosed ? 4 : currTab;
                //currTab = lockedOut ? 5 : currTab;
                TemplateHandler.Assign(ref strTemplate, "groupname-text", "Group Name");
                TemplateHandler.Assign(ref strTemplate, "tabapproved", string.Empty);
                TemplateHandler.Assign(ref strTemplate, "userdate", Admin.approvaldate);
                TemplateHandler.Assign(ref strTemplate, "pendingearning", Translator.Get("PendingComm"));
                TemplateHandler.Assign(ref strTemplate, "balancebroughtforward", Translator.Get("BalanceForward"));
                TemplateHandler.ReplaceBlock(ref strTemplate, "approved", Translator.Get("Approved") + " [<span id='nummem'>{nummem-val}</span>]");

                //TemplateHandler.RemoveBlock(ref strTemplate, "checkbox");
                if (!AdminSession.IsReportAccess)
                {
                    TemplateHandler.RemoveBlock(ref strTemplate, "report");
                }
                width = 1100;
                break;
            case "disapproved":
                //currTab = 3;
                TemplateHandler.RemoveBlock(ref strTemplate, "GroupNameSearch");
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
                TemplateHandler.RemoveBlock(ref strTemplate, "pass");
                TemplateHandler.RemoveBlock(ref strTemplate, "approved1");
                TemplateHandler.Assign(ref strTemplate, "tabdisapproved", string.Empty);
                TemplateHandler.Assign(ref strTemplate, "userdate", Admin.disapprovaldate);
                TemplateHandler.ReplaceBlock(ref strTemplate, "disapproved", Translator.Get("Disapproved") + " [<span id='nummem'>{nummem-val}</span>]");

                break;
            default:
                TemplateHandler.RemoveBlock(ref strTemplate, "GroupNameSearch");
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
                TemplateHandler.RemoveBlock(ref strTemplate, "pass");
                TemplateHandler.Assign(ref strTemplate, "userdate", Admin.registerdate);
                TemplateHandler.Assign(ref strTemplate, "tabpending", string.Empty);
                TemplateHandler.ReplaceBlock(ref strTemplate, "pending", Translator.Get("Pending") + " [{nummem}]");
                break;
        }
        currTab = (isFavor ? 0 : 1);
        if (isFavor)
            TemplateHandler.RemoveBlock(ref strTemplate, "notfavor");

        TemplateHandler.Assign(ref strTemplate, "tabdisapproved", "tabbottom");
        TemplateHandler.Assign(ref strTemplate, "tabapproved", "tabbottom");
        TemplateHandler.Assign(ref strTemplate, "tabpending", "tabbottom");
        TemplateHandler.Assign(ref strTemplate, "disapproved", Translator.Get("Disapproved"));
        TemplateHandler.Assign(ref strTemplate, "approved", Translator.Get("Approved"));
        TemplateHandler.Assign(ref strTemplate, "pending", Translator.Get("Pending"));
        #endregion

        TemplateHandler.Assign(ref strTemplate, "width", width);
        AppCommon.AssignToAdminTemplate(ref strTemplate,
        "pending", "approved",  "disapproved", "username", "email", "id", "fname", "lname", "language", 
        "CommissionGroup", "com", "member", "password", "total", "report", "Unlock", "no","status","afftype",
        "Currency","SearchBy","AllStatus","Approved","Pending","Disapproved","Closed","LockedOut",
        "ChangeGroup");
        //TemplateHandler.Assign(ref strTemplate, "reportdetails", Translator.Get("ReportDetails);

        TemplateHandler.Assign(ref strTemplate, "isLockedOut-val", Request["isLockedOut"]);


        //Search content
        int nummem = 0;
        int totalPage = 0;
        int intID = -1;

        try
        {
            intID = CastAide.AsInt32(Request["txtID"], -1);
        }
        catch
        {
            intID = -2;
            TemplateHandler.Assign(ref strTemplate, "id", Request["txtID"]);
        }
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        string strUserName = CastAide.AsString(Request["txtUsername"], string.Empty).Trim();
        string strEmail = CastAide.AsString(Request["txtEmail"], string.Empty).Trim();
        string groupName = CastAide.AsString(Request["txtGroupName"], string.Empty).Trim();
        int groupID = CastAide.AsInt32(Request["groupid"], -1);

        int afftype = CastAide.AsInt32(Request["afftypelist"], -1);

        var hideAffType = AppSettings.BaseAffTypes.Count < 2;

        if (hideAffType)
        {
            TemplateHandler.Assign(ref strTemplate, "afftypelist", String.Empty);
            TemplateHandler.RemoveBlock(ref strTemplate, "afftype");
        }else
            TemplateHandler.Assign(ref strTemplate, "afftypelist",
                HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypelist", afftype, hideAffType ? null : "All types", null, hideAffType));

        TemplateHandler.Assign(ref strTemplate, "GroupID_val", groupID);
        TemplateHandler.Assign(ref strTemplate, "username-val", string.IsNullOrEmpty(strUserName) ? "User Name" : strUserName);
        TemplateHandler.Assign(ref strTemplate, "email-val", string.IsNullOrEmpty(strEmail) ? "Email Address" : strEmail);
        TemplateHandler.Assign(ref strTemplate, "id-val", intID == -1 ? "ID" : intID.ToString());

        strUserName = ((string.Compare(strUserName, "User Name", true) == 0 ? string.Empty : strUserName));
        strEmail = ((string.Compare(strEmail, "Email Address", true) == 0 ? string.Empty : strEmail));
        groupName = ((string.Compare(groupName, "Group Name", true) == 0 ? string.Empty : groupName));

        var currency =  CastAide.AsInt32(Request["currency"],0);

        TemplateHandler.Assign(ref strTemplate, "currencies",
            HtmlHelper.DropDownList(AppSettings.AffCurrencies, "currency", currency,Admin.Currency));

        //if (strStatus == string.Empty)
        //{
        //    TemplateHandler.Assign(ref strTemplate, "groupname-val", string.Empty);
        //}

        TemplateHandler.Assign(ref strTemplate, "groupname-val", groupName);

        string strContent = Fanex.Affiliate.Core.Admin.GenerateHTML.AffiliateList.GetAffList(groupID, ref groupName, strUserName, strEmail, intID, strStatus,afftype, isClosed, lockedOut, intPage, intPageSize, out nummem, ref totalPage, isFavor, currency);
        if (string.Compare(strStatus, "approved", true) == 0 || string.IsNullOrEmpty(strStatus))
        {
           // TemplateHandler.Assign(ref strTemplate, "groupname-val", groupName);
            if (lockedOut)
            {
                TemplateHandler.Assign(ref strTemplate, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", new string[] { 
                Translator.Get("Unlock") },
                    new string[] { "LockedOutGroup();" 
                }, width, isSelectAll, true, true, false));
                TemplateHandler.RemoveBlock(ref strTemplate, "changegroup");
            }
            else
            {
                TemplateHandler.Assign(ref strTemplate, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", new string[] { 
                Admin.ChangeGroup },
                    new string[] { "ChangeGroup();"
                }, width, isSelectAll, true, true, false));
                TemplateHandler.RemoveBlock(ref strTemplate, "locked");
            }

        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", null, null, width, isSelectAll, true, false, false));
            TemplateHandler.RemoveBlock(ref strTemplate, "bottombtn");
        }
        TemplateHandler.Assign(ref strTemplate, "paging-bottom", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", width, isSelectAll, false));
        if (nummem == 0)//No row return
        {
            TemplateHandler.Assign(ref strTemplate, "pagingcss", "hide");
            TemplateHandler.RemoveBlock(ref strTemplate, "bottombtn");

            if (isFavor)//for favorite
            {
                if (Request["isclk"] == "1")//is click on menu header
                {
                    Response.Write("<script type='text/javascript'>alert('"+Fanex.Affiliate.Resources.Admin.FavoriteListEmpty+"'); location.href = 'AffiliateList.aspx?status=approved';</script>");
                    return;
                }
                if (string.IsNullOrEmpty(groupName) && string.IsNullOrEmpty(strUserName) && string.IsNullOrEmpty(strEmail) && intID == -1 && currency == 0 && strStatus == "all")//no info and no search
                    Response.Write("<script type='text/javascript'>location.href = 'AffiliateList.aspx?status=approved';</script>");
            }
        }

        //Sub menu        
        TemplateHandler.Assign(ref strTemplate, "submenu", 
            SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, 
            currTab, nummem, Translator.Get("AffList"), "i_affiliateslist.png"));


        TemplateHandler.Assign(ref strTemplate, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        TemplateHandler.Assign(ref strTemplate, "status-val", strStatus.ToLower());
        TemplateHandler.Assign(ref strTemplate, "page", intPage);
        TemplateHandler.Assign(ref strTemplate, "pagesize", intPageSize);
        TemplateHandler.Assign(ref strTemplate, "nummem-val", nummem);
        TemplateHandler.Assign(ref strTemplate, "favor-val", Request["favor"]);
        TemplateHandler.Assign(ref strTemplate, "isclosed-val", isClosed);

        TemplateHandler.ReplaceBlock(ref strTemplate, "content", strContent);
        this.HtmlContent = strTemplate;

    }
    string RefreshIcon(string strStatus)
    {
        return "&nbsp;&nbsp;<span class=\"refresh\" title=\"refresh\" onclick=\"Refresh('AffiliateList.aspx?status=" + strStatus + "')\">&nbsp;</span>";
    }
}

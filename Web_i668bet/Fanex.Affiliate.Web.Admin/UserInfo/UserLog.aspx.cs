using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Data;
using System;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;

public partial class UserInfo_UserLog : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/UserLog.htm");
        int affID = AdminSession.AffId;
        int logType = CastAide.AsInt32(Request["sllogtype"], 0);
        int pageIndex = CastAide.AsInt32(Request["page"], 1);
        DateTime dateFrom = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddMonths(-6));
        DateTime dateTo = CastAide.AsDateTime(Request["tdate"], DateTime.Now);

        int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int pageCount = 0, rowCount = 0;
        AppCommon.AssignToAdminTemplate(ref strTpl, "type", "From", "To", "search",
            "UpdateBy", "id", "date", "username", "action", "description", "IP", "Submit"); 

        TemplateHandler.Assign(ref strTpl, "sllogtype", Fanex.Affiliate.Core.Admin.GenerateHTML.UserLog.GetLogTypeList(logType, true));
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "fdate-val", dateFrom.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate-val", dateTo.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        // Content
        TemplateHandler.ReplaceBlock(ref strTpl, "content", Fanex.Affiliate.Core.Admin.GenerateHTML.UserLog.GetLogList(affID, logType, dateFrom, dateTo, pageIndex, pageSize, ref pageCount, ref rowCount));

        // Paging        
        String pagingTop = "", pagingBottom = "";
        if (pageCount > 0)
        {
            pagingTop = AdminCommon.GenPaging(pageCount, pageIndex, pageSize, rowCount, "aPageTop", "txtPageTop", "{100%}", false, true);
            pagingBottom = AdminCommon.GenPaging(pageCount, pageIndex, pageSize, rowCount, "aPageBottom", "txtPageBottom", "{100%}", false, false);
            pagingTop = pagingTop.Replace("{100%}px", "100%");
            pagingBottom = pagingBottom.Replace("{100%}px", "100%");
        }

        TemplateHandler.Assign(ref strTpl, "paging-top", pagingTop);
        TemplateHandler.Assign(ref strTpl, "paging-bottom", pagingBottom);

        TemplateHandler.Assign(ref strTpl, "page-val}", pageIndex);

        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 7, 0, SubMenu.AffApprovedText[7], "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;

    }
}

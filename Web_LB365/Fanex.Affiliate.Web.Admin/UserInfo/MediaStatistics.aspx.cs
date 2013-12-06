using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Data;
using System;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;

public partial class UserInfo_MediaStatistics : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics.htm");
        int custid = AdminSession.AffId;
        
        TemplateHandler.RemoveBlock(ref strTpl, "StatisticsDetailContent");

        AppCommon.AssignToTemplate(ref strTpl,
        "TrackingStatistics", "TrackingCode", "Submit",  "Month", "TotalClicks", "UniqueClicks", "Date");

        TemplateHandler.Assign(ref strTpl, "slTrackingCode", Fanex.Affiliate.Core.Admin.GenerateHTML.TrackingSystem.GetTrackingCodeList(custid,true));
        TemplateHandler.ReplaceBlock(ref strTpl, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(custid,null));

        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 5, 0, Fanex.Affiliate.Resources.Translator.Get("TrackingStatistics"), "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;
        
    }
}

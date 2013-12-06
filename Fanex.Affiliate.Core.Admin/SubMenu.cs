// -----------------------------------------------------------------------
// <copyright file="SubMenu.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin
{
    using System.Text;
    using DNAide.Web;

    /// <summary>
    /// Summary description for SubMenu
    /// </summary>
    public class SubMenu
    {
        public static string[] AffLink = new string[] { "../UserInfo/AffiliateList.aspx?favor=1&isclk=1", "../UserInfo/AffiliateList.aspx?status=all", "../UserInfo/MemberListAll.aspx", "../Others/EmailHistory.aspx" };
        public static string[] AffText{
            get
            {
                return new string[] { Resources.Admin.MyFavorite, Resources.Admin.Affiliate, Resources.Admin.MemList, Resources.Admin.EmailHistory };
            }
        }

        public static string[] MediaAffLink = new string[] { "../UserInfo/MediaAffList.aspx?favor=1&isclk=1", "../UserInfo/MediaAffList.aspx", "../UserInfo/MediaAffList.aspx?isClosed=true", "../Report/MediaReportDetail.aspx",
                                                    "../Report/AffiliateReport.aspx?type=2", "../Report/MemberStatisticsReport.aspx?type=2", "../Report/SignUpActive.aspx?type=2", "../Report/Commission.aspx?type=2" };
        public static string[] MediaAffText
        {
            get
            {
                return new string[] { Resources.Admin.MyFavorite, Resources.Admin.MediaAff, Resources.Admin.Closed, Resources.Admin.ReportDetails, Resources.Admin.AffiliateReport, Resources.Admin.MemberStatistics };
            }
        }

        public static string[] AffApprovedLink = new string[] { "../UserInfo/EarningsHistory.aspx", "../UserInfo/AffiliateUpd.aspx", "../UserInfo/note.aspx", "../UserInfo/PlayersDetail.aspx", "../UserInfo/Adjustment.aspx", "../UserInfo/MediaStatistics_New.aspx", "../UserInfo/MemberList.aspx", "../UserInfo/UserLog.aspx" };
        public static string[] AffApprovedText
        {
            get
            {
                return new string[] { Resources.Admin.StatementHistory, Resources.Admin.Edit, Resources.Admin.Note, Resources.Admin.PlayersDetail, Resources.Admin.Adjustment, Resources.Admin.TrackingStatistics, Resources.Admin.Member, Resources.Admin.ViewLog };
            }
        }
        public static string[] AffPendingLink = new string[] { "AffiliateUpd.aspx?approved=0", "note.aspx?approved=0" };
        public static string[] AffPendingText
        {
            get
            {
                return new string[] { Resources.Admin.Edit, Resources.Admin.Note };
            }
        }
        public static string[] MediaCustomerLink = new string[] { "../UserInfo/EarningsHistory.aspx", "../UserInfo/AffiliateUpd.aspx", "../UserInfo/note.aspx", "../UserInfo/PlayersDetail.aspx", "../UserInfo/MediaGallery.aspx", "../UserInfo/MediaStatistics_New.aspx", "../UserInfo/MemberList.aspx", "../UserInfo/UserLog.aspx" };
        public static string[] MediaCustomerText
        {
            get
            {
                return new string[] { Resources.Admin.StatementHistory, Resources.Admin.Edit, Resources.Admin.Note, Resources.Admin.PlayersDetail, Resources.Admin.MediaGallery, Resources.Admin.TrackingStatistics, Resources.Admin.Member, Resources.Admin.ViewLog };
            }
        }
        public static string[] AdminLink = new string[] { "ListUser.aspx", "ListDepartment.aspx" };
        public static string[] AdminText
        {
            get
            {
                return new string[] { Resources.Admin.AdministratorList, Resources.Admin.DepartmentList };
            }
        }
        public static string[] ReportLink = new string[] { "../Report/MemberReport.aspx", "../Report/AffiliateReport.aspx", "../Report/MemberStatisticsReport.aspx", "../Report/AffiliateQMReport.aspx", "../Report/Commission.aspx" };
        public static string[] ReportText
        {
            get
            {
                return new string[] { Resources.Admin.ReportDetails, Resources.Admin.AffiliateReport, Resources.Admin.MemberStatistics, Resources.Admin.QMReport, Resources.Admin.Commission };
            }
        }
        public static string[] DomainLink = new string[] { "AffiliateDomain.aspx", "MemberDomain.aspx", "DomainControl.aspx" };
        public static string[] DomainText
        {
            get
            {
                return new string[] { Resources.Admin.AffiliateDomains, Resources.Admin.MemberDomains, Resources.Admin.DomainControl };
            }
        }
        //@VinhTrinh
        public static string GenSubMenu(string[] arrUrl, string[] arrText, int currMenu, int numRows, string strPageName, string iconFilePath)
        {
            string strTpl = TemplateHandler.LoadTemplate("SubMenu1.htm");
            string strMenuItem = TemplateHandler.CopyBlock(strTpl, "menu");
            TemplateHandler.Assign(ref strTpl, "pagename", string.Empty);
            TemplateHandler.Assign(ref strTpl, "ThemePath", AppSettings.ThemeDir);
            TemplateHandler.Assign(ref strTpl, "iconFilePath-val", iconFilePath);
            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < arrText.Length; i++)
            {
                string strTmp = strMenuItem;
                TemplateHandler.Assign(ref strTmp, "url", arrUrl[i]);
                if (i == currMenu)
                {
                    string strText = (numRows == 0 ? arrText[i] : arrText[i] + " [" + numRows.ToString() + "]");
                    TemplateHandler.Assign(ref strTmp, "text", strText);
                }
                else
                    TemplateHandler.Assign(ref strTmp, "text", arrText[i]);

                TemplateHandler.Assign(ref strTmp, "curr", (i == currMenu ? "id=\"current\"" : string.Empty));
                strBuild.AppendLine(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "menu", strBuild.ToString());
            return strTpl;
        }
        //@VinhTrinh
        public static string GenSubMenu(string[] arrUrl, string[] arrText, int currMenu, string strText, string strPageName, string iconFilePath)
        {
            string strTpl = TemplateHandler.LoadTemplate("SubMenu1.htm");
            string strMenuItem = TemplateHandler.CopyBlock(strTpl, "menu");
            TemplateHandler.Assign(ref strTpl, "pagename", string.Empty);
            TemplateHandler.Assign(ref strTpl, "ThemePath", AppSettings.ThemeDir);
            TemplateHandler.Assign(ref strTpl, "iconFilePath-val", iconFilePath);
            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < arrText.Length; i++)
            {
                string strTmp = strMenuItem;
                TemplateHandler.Assign(ref strTmp, "url", arrUrl[i]);
                if (i == currMenu)
                {
                    TemplateHandler.Assign(ref strTmp, "curr", (i == currMenu ? "id=\"current\"" : string.Empty));
                    TemplateHandler.Assign(ref strTmp, "text", arrText[i] + "(" + strText + ")");
                }
                else
                    TemplateHandler.Assign(ref strTmp, "text", arrText[i]);

                strBuild.AppendLine(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "menu", strBuild.ToString());
            return strTpl;
        }
    }

}

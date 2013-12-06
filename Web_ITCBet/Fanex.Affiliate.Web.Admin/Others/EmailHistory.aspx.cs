namespace Fanex.Web.ITCBet.Admin.Others
{
    using System;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;

    public partial class EmailHistory : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string content = TemplateHandler.LoadTemplate("Others/EmailHistory.htm");
            AppCommon.AssignToAdminTemplate(ref content,
            "EmailHistory", "SearchBy", "All", "Sender", "Email", "Affiliate",
            "UnknownAffiliate", "From", "To", "Submit", "Delete", "ID", "View",
            "Subject", "SentDate", "IP", "Browser", "EmailDetail", "Sent",
            "Content", "ConfirmDeleteEmail", "NoEmailSelected", "Cancel","Delete",
            "DataChanged");

            TemplateHandler.Assign(ref content, "ServerDateTime", DateTime.Now.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref content, "themepath", AppSettings.ThemeDir);
            TemplateHandler.Assign(ref content, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

            string name = string.Empty, email = string.Empty, affiliateName = string.Empty;

            int minNo = 0, maxNo = 0, totalEmail = 0, totalpage = 0;
            int currPage = CastAide.AsInt32(Request["page"], 1);//get the current page from hidden field in form
            int currPageSize = CastAide.AsInt32(Request["pagesize"], 20);//get the current page from hidden field in form
            int searchOption = CastAide.AsInt32(Request["slSearchOption"], -1);
            //bool isUnknown = false;
            object msgId = null;
            string strSearchText = CastAide.AsString(Request["txtSearchText"], string.Empty).Trim();

            switch (searchOption)
            {
                case -1:
                    break;
                case 0:
                    name = strSearchText;
                    break;
                case 1:
                    email = strSearchText;
                    break;
                case 2:
                    affiliateName = strSearchText;
                    break;
                case -2:
                    msgId = -1;
                    break;
                default:
                    break;
            }
            DateTime fdate = DateTime.Now, tdate = DateTime.Now;
            string hasDate = CastAide.AsString(Request["hdchkDate"], string.Empty);

            if (hasDate == "on")
            {
                fdate = CastAide.AsDateTime(Request["fdate"], null);
                tdate = CastAide.AsDateTime(Request["tdate"], null);
                TemplateHandler.ReplaceBlock(ref content, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.EmailHistory.GetEmailHistoryList(msgId, name, email, affiliateName, fdate, tdate, currPage, ref minNo, ref maxNo, ref totalEmail, currPageSize, ref totalpage));
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref content, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.EmailHistory.GetEmailHistoryList(msgId, name, email, affiliateName, null, null, currPage, ref minNo, ref maxNo, ref totalEmail, currPageSize, ref totalpage));
            }

            TemplateHandler.Assign(ref content, "hdSearchOption-val", searchOption.ToString());
            TemplateHandler.Assign(ref content, "hdSearchText-val", strSearchText);
            TemplateHandler.Assign(ref content, "hdchkDate-val", hasDate);
            TemplateHandler.Assign(ref content, "hdfDate-val", fdate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref content, "hdtDate-val", tdate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref content, "hdTotalEmail-val", totalEmail.ToString());

            TemplateHandler.Assign(ref content, "minNo", minNo.ToString());
            TemplateHandler.Assign(ref content, "maxNo", maxNo.ToString());
            TemplateHandler.Assign(ref content, "page", currPage);
            TemplateHandler.Assign(ref content, "submenu", SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, 3, totalEmail, Fanex.Affiliate.Resources.Translator.Get("EmailHistory"), "i_email_history.png"));
            if (totalEmail != 0)
            {
                TemplateHandler.Assign(ref content, "paging-top", AdminCommon.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageTop", "txtPageTop", new string[] { Fanex.Affiliate.Resources.Admin.Delete }, new string[] { "DeleteSelectedMessage();" }, 1020, true, true, true, false));
                TemplateHandler.Assign(ref content, "paging-bottom", AdminCommon.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageBottom", "txtPageBottom", 1020, true, false));
            }
            else
            {
                TemplateHandler.Assign(ref content, "paging-top", "");
                TemplateHandler.Assign(ref content, "paging-bottom", "");
            }
            this.HtmlContent = content;
        }
    }
}

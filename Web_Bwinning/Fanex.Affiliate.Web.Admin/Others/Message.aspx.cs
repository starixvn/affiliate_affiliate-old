namespace Fanex.Web.Bwinning.Admin.Others
{
    using System;
    using DNAide.Core;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;

    public partial class HotNews : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string hotNewsContent = TemplateHandler.LoadTemplate("Others/Message.htm");
            TemplateHandler.Assign(ref hotNewsContent, "themepath", AppSettings.ThemeDir);

            AppCommon.AssignToAdminTemplate(ref hotNewsContent,
            "MessageHistory", "UpdatedBy", "SearchBy","All","Normal","Important","From",
            "To","Submit","PostDate","Message","Delete","Add","AddEmptyMessError","Update",
            "Edit","Cancel","UpdateDate","Creator","MessageInformation","CurrentMessage",
            "ConfirmDeleteMessage","ConfirmUpdateMessage","NoMessSelected","DataChanged");

            TemplateHandler.Assign(ref hotNewsContent, "ServerDateTime", DateTime.Now.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref hotNewsContent, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);


            int minNo = 0, maxNo = 0, totalMess = 0;
            int intPage = CastAide.AsInt32(Request["page"], 1);
            int iPageSize = CastAide.AsInt32(Request["pagesize"], 20);
            int iTotalPage = 0;
            string hasDate = CastAide.AsString(Request["hdchkDate"], string.Empty);
            int severity = CastAide.AsInt32(Request["slSearchOption"], -1);
            DateTime fDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now);
            DateTime tDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
            if (hasDate == "on")
            {
                TemplateHandler.ReplaceBlock(ref hotNewsContent, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.HotNews.GetHotNewsContentList(severity, fDate, tDate, intPage, iPageSize, ref minNo, ref maxNo, ref totalMess, ref iTotalPage));
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref hotNewsContent, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.HotNews.GetHotNewsContentList(severity, null, null, intPage, iPageSize, ref minNo, ref maxNo, ref totalMess, ref iTotalPage));
            }

            //set top and bottom paging
            TemplateHandler.Assign(ref hotNewsContent, "hdchkDate-val", hasDate);
            TemplateHandler.Assign(ref hotNewsContent, "submenu", SubMenu.GenSubMenu(new string[] { "Message.aspx" },
                new string[] { Fanex.Affiliate.Resources.Translator.Get("MessageHistory") }, 
                0, totalMess, Fanex.Affiliate.Resources.Translator.Get("Message"), "i_message.png"));
            if (totalMess != 0)
            {
                TemplateHandler.Assign(ref hotNewsContent, "paging-top", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageTop", "txtPageTop",
                    new string[] { Fanex.Affiliate.Resources.Admin.Del, Fanex.Affiliate.Resources.Admin.Add, Fanex.Affiliate.Resources.Admin.CurrentMessage }, new string[] { "DeleteSelectedMessage();", "DoCal('ViewMessDetails.aspx?id=','Add new message', 600, 500)", "DoCal('ViewMessDetails.aspx?id=-1','Current Message Detail', 600, 500)" }, 1100, true, true, true, false));
                TemplateHandler.Assign(ref hotNewsContent, "paging-bottom", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageBottom", "txtPageBottom", 1100, true, false));
            }
            else
            {
                TemplateHandler.Assign(ref hotNewsContent, "paging-top", "");
                TemplateHandler.Assign(ref hotNewsContent, "paging-bottom", "");
            }

            TemplateHandler.Assign(ref hotNewsContent, "page", intPage.ToString());
            TemplateHandler.Assign(ref hotNewsContent, "pagesize", iPageSize.ToString());
            TemplateHandler.Assign(ref hotNewsContent, "hdSearchOption-val", severity.ToString());
            TemplateHandler.Assign(ref hotNewsContent, "hdfDate-val", fDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref hotNewsContent, "hdtDate-val", tDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref hotNewsContent, "hdTotalMess-val", totalMess.ToString());
            TemplateHandler.Assign(ref hotNewsContent, "minNo", minNo.ToString());
            TemplateHandler.Assign(ref hotNewsContent, "maxNo", maxNo.ToString());
            this.HtmlContent = hotNewsContent;
        }
    }
}

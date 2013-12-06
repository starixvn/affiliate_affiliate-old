using System;
using DNAide.Web;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class UserInfo_Note : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int intCustId = AdminSession.AffId;
        
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/NoteList.htm");
        if (string.Compare(Request["ischild"], "1", true) == 0)
            TemplateHandler.ReplaceBlock(ref strTpl, "accoverview", string.Empty);

        AppCommon.AssignToTemplate(ref strTpl,
           "note", "accoverview", "add", "cancel", "Delete");
        

        TemplateHandler.Assign(ref strTpl, "ischild", Request["ischild"]);
        TemplateHandler.Assign(ref strTpl, "custid", intCustId.ToString());
        int pageIndex = CastAide.AsInt32(Request["page"], 1);
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        bool hasData = false;
        int rows = 0;
        TemplateHandler.ReplaceBlock(ref strTpl, "notelist", Fanex.Affiliate.Core.Admin.GenerateHTML.Note.GenNoteList(intCustId, pageIndex, CastAide.AsInt32(Request["slPageSize"], 20), ref hasData, ref rows));
        if(!hasData)
            TemplateHandler.RemoveBlock(ref strTpl, "nocontent");
        string strUrlParam = "?approved=" + Request["approved"];
        TemplateHandler.Assign(ref strTpl, "approved-val", Request["approved"]);
        
        #region Load SubPage
        string subPage = string.Empty;
        if (string.Compare(Request["approved"], "0", true) == 0)
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffPendingLink, SubMenu.AffPendingText, 1, rows, Fanex.Affiliate.Resources.Translator.Get("Note"), "i_customer.png"));
        else
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 2, rows, Fanex.Affiliate.Resources.Translator.Get("Note"), "i_customer.png"));

        #endregion

        this.HtmlContent = strTpl;    

    }
}

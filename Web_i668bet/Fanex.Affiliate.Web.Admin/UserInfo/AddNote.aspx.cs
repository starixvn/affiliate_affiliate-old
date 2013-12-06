using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;

public partial class UserInfo_AddNote : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/AddNote.htm");
        int action = CastAide.AsInt32(Request["action"], 1);
        AppCommon.AssignToAdminTemplate(ref strTpl, "add", "update", "cancel", "content");
        TemplateHandler.Assign(ref strTpl, "custid-val", AdminSession.AffId);
        if (action == 1)//add
        {
            TemplateHandler.RemoveBlock(ref strTpl, "update");
            TemplateHandler.Assign(ref strTpl, "content-val", string.Empty);
        }
        else if (action == 2)//update
        {
            TemplateHandler.RemoveBlock(ref strTpl, "add");
            int noteId = CastAide.AsInt32(Request["noteid"], 0);
            TemplateHandler.Assign(ref strTpl, "noteid-val", noteId);
            if (noteId == 0)//noteid is not correct
                return;
            //Load note content
            DbResult rs = Affiliate.SelNoteDetail(AdminSession.AffId, noteId);
            DataRow dr = rs.ResultSet.Tables[0].Rows[0];
            TemplateHandler.Assign(ref strTpl, "content-val", dr["note"]);
        }
        this.HtmlContent = strTpl;
    }
}

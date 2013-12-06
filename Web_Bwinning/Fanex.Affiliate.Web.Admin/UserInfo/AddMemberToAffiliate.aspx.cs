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
using System.Xml.Linq;
using DNAide.Web;
using Fanex.Affiliate.Core;

public partial class UserInfo_AddMemberToAffiliate : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("UserInfo/AddMemberToAffiliate.htm");

        AppCommon.AssignToAdminTemplate(ref settingContent, "Member", "Affiliate","Cancel");
        //TemplateHandler.Assign(ref settingContent, "Types", types);
        this.HtmlContent = settingContent;
    }
}

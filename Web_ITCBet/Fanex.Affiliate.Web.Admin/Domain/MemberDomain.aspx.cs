namespace Fanex.Web.ITCBet.Admin.Domain
{
    using System;
    using System.Data;
    using System.Text;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    public partial class MemberDomain : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string list = TemplateHandler.LoadTemplate("Domain/12BetDomain.html");

            AppCommon.AssignToAdminTemplate(ref list,
              "Domain", "Creator", "CreatedDate", "Status", "Edit", "Del","AddMemberSiteDomain");

            TemplateHandler.Assign(ref list, "themepath", AppSettings.ThemeDir);

            DbResult dbr = null;
            dbr = Fanex.Affiliate.DataObjects.Domain.GetListMemberDomain();
            string _content = TemplateHandler.CopyBlock(list, "content");
            TemplateHandler.Assign(ref _content, "over", AppCommon.RowBgOver);
            string _listcontent = "";
            StringBuilder strcontent = new StringBuilder();
            int counter = 0;
            if (dbr.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
                {
                    _listcontent = _content;
                    counter++;
                    TemplateHandler.Assign(ref _listcontent, "color", (counter % 2 == 0) ? AppCommon.RowBgEven : AppCommon.RowBgOdd);
                    TemplateHandler.Assign(ref _listcontent, "No_Value", counter.ToString());
                    TemplateHandler.Assign(ref _listcontent, "12BetDomain_Value", dr["DomainName"]);
                    TemplateHandler.Assign(ref _listcontent, "Creator_Value", dr["Creator"]);
                    TemplateHandler.Assign(ref _listcontent, "CreatedDate_Value", CastAide.AsDateTime(dr["CreateDate"].ToString()).ToShortDateString());                    
                    TemplateHandler.Assign(ref _listcontent, "12betdomainid", dr["DomainID"]);
                    strcontent.Append(_listcontent);
                }
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref _listcontent, "content", AppCommon.NoInfo(7));
            }

            TemplateHandler.ReplaceBlock(ref list, "content", strcontent.ToString());
            TemplateHandler.Assign(ref list, "submenu", SubMenu.GenSubMenu(SubMenu.DomainLink,
                                                                            SubMenu.DomainText,
                                                                            1, counter, Fanex.Affiliate.Resources.Translator.Get("Administrator"),
                                                                            "i_domain.jpg"));
            TemplateHandler.Assign(ref list, "ChooseConfirm", Fanex.Affiliate.Resources.Translator.Get("ConfirmDelDomain"));            


            this.HtmlContent = list;
        }
    }
}
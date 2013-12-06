namespace Fanex.Web.Macaubet.Admin.Domain
{
    using System;
    using System.Data;
    using System.Text;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core.Admin.GenerateHTML;

    public partial class Domain_DomainControl : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControl.htm");

            AppCommon.AssignToAdminTemplate(ref strTpl,
            "AffDomain",
            "Market",
            "RedirectDomain",
            "Creator",
            "CreatedDate",
            "Status",
            "Submit",
            "AddNew",
            "Edit",
            "Del",
            "Country");

            string country = Request["slCountry"];
            TemplateHandler.Assign(ref strTpl, "slMarket", Domain.GenCountryList(country, true));
            DbResult rs = Fanex.Affiliate.DataObjects.Domain.DomainControlSel(country);
            StringBuilder strBuild = new StringBuilder();

            string rowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            TemplateHandler.Assign(ref rowTpl, "over", AppCommon.RowBgOver);
            if (rs.ResultSet.Tables[0].Rows.Count > 0)
            {
                int noRow = 0;
                foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
                {
                    string rowContent = rowTpl;
                    noRow++;
                    TemplateHandler.Assign(ref rowContent, "color", noRow % 2 == 0 ? AppCommon.RowBgEven : AppCommon.RowBgOdd);
                    TemplateHandler.Assign(ref rowContent, "no", noRow);
                    TemplateHandler.Assign(ref rowContent, "ID-val", dr["ID"]);
                    TemplateHandler.Assign(ref rowContent, "AffDomainId-val", dr["AffDomainId"]);
                    TemplateHandler.Assign(ref rowContent, "AffDomain-val", dr["AffDomain"]);
                    TemplateHandler.Assign(ref rowContent, "RedirectDomainId-val", dr["RedirectDomainId"]);
                    TemplateHandler.Assign(ref rowContent, "RedirectDomain-val", dr["RedirectDomain"]);
                    TemplateHandler.Assign(ref rowContent, "MarketId-val", dr["CountryId"]);
                    TemplateHandler.Assign(ref rowContent, "Market-val", dr["CountryName"]);
                    TemplateHandler.Assign(ref rowContent, "Creator-val", dr["Creator"]);
                    TemplateHandler.Assign(ref rowContent, "CreatedDate-val", CastAide.AsDateTime(dr["CreateDate"].ToString()).ToShortDateString());
                    TemplateHandler.Assign(ref rowContent, "Status-value", dr["Status"]);
                    TemplateHandler.Assign(ref rowContent, "Status-val", CastAide.AsBoolean(dr["Status"]) ? "ic-check" : "ic-uncheck");

                    strBuild.Append(rowContent);
                }
            }
            else
            {
                strBuild.Append(AppCommon.NoInfo(9));
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.DomainLink, SubMenu.DomainText, 2, 0, "Domain Control", "i_domain.jpg"));
            this.HtmlContent = strTpl;
        }
    }
}
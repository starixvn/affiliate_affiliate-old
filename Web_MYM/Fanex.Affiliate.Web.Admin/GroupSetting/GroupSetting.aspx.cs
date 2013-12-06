namespace Fanex.Web.MYM.Admin.GroupSetting
{
    using System;
    using System.Text;
    using DNAide.Web;
    using DNAide.Core;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;

    public partial class GroupSetting : BasePageTpl
    {
        string strTpl = string.Empty;
        StringBuilder strListRow = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            string content = TemplateHandler.LoadTemplate("GroupSetting/GroupSetting.htm");
            //  TemplateHandler.Assign(ref content, "No", Fanex.Affiliate.Resources.Translator.Get("No);
            /* TemplateHandler.Assign(ref content, "Bonus", Fanex.Affiliate.Resources.Translator.Get("Bonus);
             TemplateHandler.Assign(ref content, "Adjustment", Fanex.Affiliate.Resources.Translator.Get("Adjustment);
             TemplateHandler.Assign(ref content, "MKT", Fanex.Affiliate.Resources.Translator.Get("MKT);
             TemplateHandler.Assign(ref content, "PSP", Fanex.Affiliate.Resources.Translator.Get("PSP);*/
            AppCommon.AssignToAdminTemplate(ref content,
            "GroupName", "Name", "Description", "Creator", "CreatedDate", "Policy", "Product", 
            "Tier", "MinAmount", "MaxAmount", "MinQM", "MaxQM", "Comm", 
            "Deductions", "Action", "QualifiedAmount","Currency",
            "NoOfAffiliate", "AddNewGroup", "From", "To", "Submit",
            "ConfirmDelGroup");

            TemplateHandler.Assign(ref content, "ServerDateTime", DateTime.Now.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref content, "themepath", AppSettings.ThemeDir);
            TemplateHandler.Assign(ref content, "over", AdminCommon.RowBgOver);


            int minNo = 0, maxNo = 0, totalRows = 0, totalpage = 0;



            int currPage = CastAide.AsInt32(Request["page"], 1);//get the current page from hidden field in form
            int currPageSize = CastAide.AsInt32(Request["slPageSize"], AppSettings.PageSize);//get the current page from hidden field in form
            TemplateHandler.Assign(ref content, "pagesize_val", currPageSize);
            TemplateHandler.Assign(ref content, "timer_val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

            string strGroupName = CastAide.AsString(Request["txtGroupName"], string.Empty).Trim();
            TemplateHandler.Assign(ref content, "txtGroupName_val", strGroupName);


            object fdate = null, tdate = null;
            int hasDate = CastAide.AsInt32(Request["hdchkDate"], 0);

            if (hasDate > 0)
            {
                fdate = CastAide.AsDateTime(Request["fdate"], null);
                tdate = CastAide.AsDateTime(Request["tdate"], null);
            }
            TemplateHandler.ReplaceBlock(ref content, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.GroupSetting.GetGroupSettingList(strGroupName, fdate, tdate, currPage, ref minNo, ref maxNo, ref totalRows, currPageSize, ref totalpage));

            TemplateHandler.Assign(ref content, "hdchkDate_val", hasDate.ToString());
            TemplateHandler.Assign(ref content, "hdfDate_val", CastAide.AsDateTime(fdate, DateTime.Now).ToShortDateString());
            TemplateHandler.Assign(ref content, "hdtDate_val", CastAide.AsDateTime(tdate, DateTime.Now).ToShortDateString());
            TemplateHandler.Assign(ref content, "hdTotalRows_val", totalRows);

            TemplateHandler.Assign(ref content, "minNo", minNo.ToString());
            TemplateHandler.Assign(ref content, "maxNo", maxNo.ToString());
            TemplateHandler.Assign(ref content, "page_val", currPage);


            TemplateHandler.Assign(ref content, "submenu", SubMenu.GenSubMenu(new string[] { "GroupSetting.aspx" }, 
                new string[] { Fanex.Affiliate.Resources.Admin.CommissionGroup }, 0, totalRows, Fanex.Affiliate.Resources.Admin.CommissionGroup, "i_commg.png"));

            if (totalRows != 0)
            {
                TemplateHandler.Assign(ref content, "paging-top", 
                    AdminCommon.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageTop", "txtPageTop",
                    new string[] { Fanex.Affiliate.Resources.Admin.AddNewGroup }, 
                    new string[] { "AddNewGroup();" }, 1100, false, true, true, false));
                TemplateHandler.Assign(ref content, "paging-bottom", AdminCommon.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageBottom", "txtPageBottom", 1100, false, false));
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

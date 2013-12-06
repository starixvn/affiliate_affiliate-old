using System;
using DNAide.Web;
using DNAide.Core;
using System.Collections.Generic;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;

public partial class Settings_Default : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Settings/Settings.htm");
        
        AppCommon.AssignToAdminTemplate(ref settingContent,
        "type", "Value", "Description", "Update", "Delete", "Add", "Submit","SearchBy","Key",
        "ConfirmDelConfiguration","SelConfiguration","DataChanged");

        TemplateHandler.Assign(ref settingContent, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

        int minNo = 0, maxNo = 0, totalMess = 0;
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int iPageSize = CastAide.AsInt32(Request["pagesize"], 20);
        int iTotalPage = 0;
        string hasDate = CastAide.AsString(Request["hdchkDate"], string.Empty);
        int severity = CastAide.AsInt32(Request["slSearchOption"], -1);
        DateTime fDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now);
        DateTime tDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);

        string query = Request["query"];
        TemplateHandler.Assign(ref settingContent, "query", query);

        string value = Request["value"];
        TemplateHandler.Assign(ref settingContent, "value", value);

        string type = Request["type"];
        var data = Settings.GetSetting("ConfigurationTypeList");
        IList<string> typeList = Common.GetStringIList(data.ResultSet.Tables[0].Rows[0]["SValue"].ToString(), ",");
        string types = string.Format("<option value=''>{0}</option>", Fanex.Affiliate.Resources.Translator.Get("All"));
        foreach (var item in typeList)
        {
            if (item == type)
            {
                types += string.Format("<option selected='selected' value='{0}'>{1}</option>", item, item);
            }
            else
            {
                types += string.Format("<option value='{0}'>{1}</option>", item, item);
            }

        }
        TemplateHandler.Assign(ref settingContent, "Types", types);

        TemplateHandler.ReplaceBlock(ref settingContent, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.Settings.GenSettingsHtml(query, type, value, intPage, iPageSize, ref minNo, ref maxNo, ref totalMess, ref iTotalPage));

        //set top and bottom paging
        TemplateHandler.Assign(ref settingContent, "hdchkDate-val", hasDate);
        TemplateHandler.Assign(ref settingContent, "submenu", SubMenu.GenSubMenu(
                            new string[] { "Default.aspx", "Country.aspx",
                                            "Product.aspx", "Language.aspx",
                                            "AffType.aspx"},
                            new string[] {  Fanex.Affiliate.Resources.Admin.Configurations,
                                            Fanex.Affiliate.Resources.Admin.Country,
                                            Fanex.Affiliate.Resources.Admin.Product,
                                            Fanex.Affiliate.Resources.Admin.Language,
                                            Fanex.Affiliate.Resources.Admin.AffType,
                                            },
                            0, totalMess, Fanex.Affiliate.Resources.Translator.Get("Message"), "i_settings.png"));
        if (totalMess != 0)
        {
            TemplateHandler.Assign(ref settingContent, "paging-top", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageTop", "txtPageTop",
                new string[] { Fanex.Affiliate.Resources.Admin.Delete, Fanex.Affiliate.Resources.Admin.Add },
                new string[] {  "deleteConfigurations();", 
                                "DoCal('Add.aspx','Add new configuration', 630, 600)" },
                1100, true, true, true, false));
            TemplateHandler.Assign(ref settingContent, "paging-bottom", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageBottom", "txtPageBottom", 1100, true, false));
        }
        else
        {
            TemplateHandler.Assign(ref settingContent, "paging-top", "");
            TemplateHandler.Assign(ref settingContent, "paging-bottom", "");
        }

        TemplateHandler.Assign(ref settingContent, "page", intPage.ToString());
        TemplateHandler.Assign(ref settingContent, "pagesize", iPageSize.ToString());
        TemplateHandler.Assign(ref settingContent, "hdSearchOption-val", severity.ToString());
        TemplateHandler.Assign(ref settingContent, "hdfDate-val", fDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref settingContent, "hdtDate-val", tDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref settingContent, "hdTotalMess-val", totalMess.ToString());
        TemplateHandler.Assign(ref settingContent, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref settingContent, "maxNo", maxNo.ToString());
        this.HtmlContent = settingContent;
    }
}

using System;
using DNAide.Web;
using DNAide.Core;
using System.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.Core.Helper;

public partial class Resources_Default : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Resources/Resources.htm");

        AppCommon.AssignToTemplate(ref settingContent,
        "Name", "Type", 
        "Language", 
        "Value", 
        "Description", 
        "Update", 
        "Delete", 
        "Add",                 
        "SearchBy",
        "Submit");
        TemplateHandler.Assign(ref settingContent, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

        TemplateHandler.Assign(ref settingContent, "Radio-NotTranslation", Fanex.Affiliate.Resources.Translator.Get("ExcelNotTranslation"));
        TemplateHandler.Assign(ref settingContent, "Radio-All", Fanex.Affiliate.Resources.Translator.Get("ExcelAllTranslation"));

        int minNo = 0, maxNo = 0, totalMess = 0;
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int iPageSize = CastAide.AsInt32(Request["pagesize"], 100);
        int iTotalPage = 0;

        //build resource types
        string type = Request["type"];
        var types = Resource.GetResourceTypes();
        string typeString = string.Format("<option value=''>{1}</option>", string.Empty, Fanex.Affiliate.Resources.Translator.Get("All"));
        foreach (DataRow item in types.ResultSet.Tables[0].Rows)
        {
            if (item["Key"].ToString() == type)
            {
                typeString += string.Format("<option selected='selected' value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
            }
            else
            {
                typeString += string.Format("<option value='{0}'>{1}</option>", item["Key"], item["TypeName"]);
            }
        }

        TemplateHandler.Assign(ref settingContent, "Types", typeString);
        
        
        string lang = Request["lang"];
        //var languages = Resource.GetResourceLanguages();

        TemplateHandler.Assign(ref settingContent, "Languages", 
            HtmlHelper.DropDownList(AppSettings.BaseLanguages, "lang", lang, true));
        
        string query = Request["query"];
        TemplateHandler.Assign(ref settingContent, "query", query);
        TemplateHandler.Assign(ref settingContent, "type", type);

        string value = Request["value"];
        TemplateHandler.Assign(ref settingContent, "value", value);

        TemplateHandler.ReplaceBlock(ref settingContent, "Content", Fanex.Affiliate.Core.Admin.GenerateHTML.Resource.GenHtml(query, value, type, lang, intPage, iPageSize, ref minNo, ref maxNo, ref totalMess, ref iTotalPage));

        //set top and bottom paging
        TemplateHandler.Assign(ref settingContent, "submenu", SubMenu.GenSubMenu(new string[] { "Default.aspx" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("Translations") }, 0, totalMess, Fanex.Affiliate.Resources.Translator.Get("Message"), "i_tran.png"));
        if (totalMess != 0)
        {
            TemplateHandler.Assign(ref settingContent, "paging-top", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageTop", "txtPageTop",
                new string[] { "Delete", "Add" },
                new string[] {  "deleteTranslations();", 
                                "DoCal('Add.aspx','Add new translation', 630, 650)" },
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

        TemplateHandler.Assign(ref settingContent, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref settingContent, "maxNo", maxNo.ToString());
        this.HtmlContent = settingContent;
    }
}

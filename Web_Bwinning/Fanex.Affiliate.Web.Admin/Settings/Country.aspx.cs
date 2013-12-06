using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Core.Utility;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;


public partial class CountryIndex : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingContent = TemplateHandler.LoadTemplate("Country/Default.htm");

        AppCommon.AssignToAdminTemplate(ref settingContent,
        "Name", "Description", "Icon", "DisplayOrder", "IsEnabled",
        "Update", "Delete", "Add", "SearchBy", "Submit", "ConfirmDelCountry","SelCountry");
        // TemplateHandler.Assign(ref settingContent, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

        int minNo = 0, maxNo = 0, totalMess = 0;
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int iPageSize = CastAide.AsInt32(Request["pagesize"], 20);
        int iTotalPage = 0;        

        string query = Request["query"];
        TemplateHandler.Assign(ref settingContent, "query", query);        

        TemplateHandler.ReplaceBlock(ref settingContent, "Content", GenSettingsHtml(query, intPage, iPageSize, ref minNo, ref maxNo, ref totalMess, ref iTotalPage));

        //set top and bottom paging        
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
                           1, totalMess, Fanex.Affiliate.Resources.Translator.Get("Message"), "i_settings.png"));
        if (totalMess != 0)
        {
            TemplateHandler.Assign(ref settingContent, "paging-top", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageTop", "txtPageTop", 
                new string[] { Fanex.Affiliate.Resources.Admin.Delete, Fanex.Affiliate.Resources.Admin.Add }, 
                new string[] {  "deleteCountries();", 
                                "DoCal('CountryUpdate.aspx','Add country', 630, 400)" }, 
                1100, true, true, true, false));
            TemplateHandler.Assign(ref settingContent, "paging-bottom", AdminCommon.GenPaging(iTotalPage, intPage, iPageSize, maxNo, "aPageBottom", "txtPageBottom",
                1100, true, false));
        }
        else
        {
            TemplateHandler.Assign(ref settingContent, "paging-top", "");
            TemplateHandler.Assign(ref settingContent, "paging-bottom", "");
        }

        TemplateHandler.Assign(ref settingContent, "page", intPage.ToString());
        TemplateHandler.Assign(ref settingContent, "pagesize", iPageSize.ToString());        
        TemplateHandler.Assign(ref settingContent, "hdTotalMess-val", totalMess.ToString());
        TemplateHandler.Assign(ref settingContent, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref settingContent, "maxNo", maxNo.ToString());
        this.HtmlContent = settingContent;
    }

    public string GenSettingsHtml(string query, int curpage, int pageSize, ref int minNo, ref int maxNo, ref int totalMess, ref int intTotalPage)
    {
        DbResult result = null;
        int nRows = 0;
        result = Fanex.Affiliate.DataObjects.Country.GetCountries(query, curpage, pageSize);
        nRows = CastAide.AsInt32(result.Outputs["nrows"]);
        totalMess = nRows;
        int cols = 10;
        minNo = 0;
        maxNo = 0;

        if (nRows > 0)
        {
            int pagesize = pageSize;
            string settings = TemplateHandler.LoadTemplate("Country/Default.htm");
            string strContent = TemplateHandler.CopyBlock(settings, "Content");
            //TemplateHandler.Assign(ref strContent, "ViewDetail", Resources.Translator.Get("ViewDetail);
            DataTable dtbResult = result.ResultSet.Tables[0];
            int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            minNo = (curpage - 1) * pagesize + intNo + 1;
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            foreach (DataRow dr in dtbResult.Rows)
            {
                string strTmp = strContent;
                intNo++;

                TemplateHandler.Assign(ref strTmp, "No-val", (curpage - 1) * pagesize + intNo);
                TemplateHandler.Assign(ref strTmp, "ID-val", dr["CountryID"].ToString());

                TemplateHandler.Assign(ref strTmp, "Name-val", dr["CountryName"]);
                TemplateHandler.Assign(ref strTmp, "Description-val", dr["Description"]);
                TemplateHandler.Assign(ref strTmp, "Icon-val", Common.Snippet(dr["Icon"].ToString(), 100));
                TemplateHandler.Assign(ref strTmp, "DisplayOrder-val", dr["DisplayOrder"]);
                try
                {
                    TemplateHandler.Assign(ref strTmp, "IsEnabled-class", (Boolean.Parse(dr["IsEnabled"].ToString()) ? "ic-check" : "ic-uncheck"));
                }
                catch (Exception err)
                {
                    TemplateHandler.Assign(ref strTmp, "IsEnabled-class", "ic-uncheck");
                }

                if ((intNo % 2) == 0)
                {
                    TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgEven);
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgOdd);
                }

                strBuild.Append(strTmp);

            }
            maxNo = (curpage - 1) * pagesize + intNo;
            intTotalPage = totalpage;

            return strBuild.ToString();
        }
        return AdminCommon.NoInfo(cols);

    }
}

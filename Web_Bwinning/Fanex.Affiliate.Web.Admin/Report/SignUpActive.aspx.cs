using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Helper;

public partial class Report_SignUpActive : BasePageTpl
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/SignUpActiveReport.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "aff-txt", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Fanex.Affiliate.Resources.Translator.Get("From"));
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Fanex.Affiliate.Resources.Translator.Get("To"));
        TemplateHandler.Assign(ref strTpl, "search", Fanex.Affiliate.Resources.Translator.Get("Search"));
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);        
        TemplateHandler.Assign(ref strTpl, "affiliate", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
        
        TemplateHandler.Assign(ref strTpl, "signup", Fanex.Affiliate.Resources.Translator.Get("SignUp"));
        TemplateHandler.Assign(ref strTpl, "activemember", Fanex.Affiliate.Resources.Translator.Get("ActiveMember"));
    }

    void GenerateHTML()
    {
        TemplateHandler.Assign(ref strTpl, "listlang", 
            HtmlHelper.DropDownList(AppSettings.BaseLanguages,"listlang",null,"Select Language"));

        string strUsername = CastAide.AsString(Request["affname"]);
        string strPreferLang = Request["slPreferLang"];
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intPage = CastAide.AsInt32(Request["page"], 1);

        TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "affname", strUsername);

        DbResult rs = Affiliate.ActiveMemberReport(dtmFDate, dtmTDate, isGetAll, strUsername, strPreferLang, 0, intPageSize, intPage);
          
        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
        int nummem = CastAide.AsInt32(rs.Outputs["nrows"]);

        if (dtb.Rows.Count > 0)
        {
            DbResult rs1;
            if (!string.IsNullOrEmpty(strUsername))
                rs1 = Affiliate.SignUpMembersReport(CastAide.AsInt32(dtb.Rows[0]["affid"]), dtmFDate, dtmTDate, isGetAll);
            else
                rs1 = Affiliate.SignUpMembersReport(-1, dtmFDate, dtmTDate, isGetAll);
            DataTable dtb1 = new DataTable();
            if (rs1.ResultSet.Tables.Count > 0)
                dtb1 = rs1.ResultSet.Tables[0];

            int intCount = (intPage - 1) * intPageSize + 1;
            int intTotal = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;


            StringBuilder strListRow = new StringBuilder();
            //Hashtable hsTotal = new Hashtable();


            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                int total = CastAide.AsInt32(dr["Active"]);

                intTotal += total;

                TemplateHandler.Assign(ref strTmp, "no", intCount);
                intCount++;

                TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                TemplateHandler.Assign(ref strTmp, "activemember-val", dr["active"]);
                // TemplateHandler.Assign(ref strTmp, "type-val", 0);
                if (CastAide.AsBoolean(dr["isclosed"]))
                    TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgClosed);
                else
                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                //sign up
                TemplateHandler.Assign(ref strTmp, "signup-val",GetSignUp( dtb1,int.Parse(dr["affid"].ToString())));

                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());

                
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", AdminCommon.NoInfo(4));
        }
        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 4, nummem, SubMenu.ReportText[2], "i_report.png"));
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", 550, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", 550, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
    }
    public int GetSignUp(DataTable dt, int affId)
    {
        try
        {
            return CastAide.AsInt32(dt.Select("AffiliateID=" + affId.ToString())[0]["TotalSignUp"]);
        }
        catch
        {
            return 0;
        }
       
    }
}

using System;
using System.Collections;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;

public partial class UserInfo_MemberList : BasePageTpl
{
    string strTemplate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTemplate = TemplateHandler.LoadTemplate("UserInfo/MemberList.htm");

        AssignText();

        //Search content
        DateTime dtmTmp = DateTime.Now.AddDays(-(DateTime.Now.Day -1));
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], dtmTmp);
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intMemberType = CastAide.AsInt32(Request["slType"], 0);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);

        TemplateHandler.Assign(ref strTemplate, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTemplate, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTemplate, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTemplate, "type-val", intMemberType);

        LoadData(dtmFDate, dtmTDate, intMemberType, isGetAll);
        //Load submenu
        TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 6, 0, SubMenu.AffApprovedText[6], "i_customer.png"));

        this.HtmlContent = strTemplate;
    }

    void AssignText()
    {

        AppCommon.AssignToAdminTemplate(ref strTemplate,
              "member", "select", "Deposited", "NonDeposited", "From", "To", "username","Select",
              "FirstName", "LastName", "email", "registerdate", "currency", "searchby","description","GetAll");
        TemplateHandler.Assign(ref strTemplate, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTemplate, "over", AdminCommon.RowBgOver);
    }

    void LoadData(DateTime dtmFDate, DateTime dtmTDate, int intMemberType, bool isGetAll)
    {
        int width = 800;
        int intTotalPages = 0;
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);

        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "page", intPage);

        //DataTable dtbCurrency = DataHandler.ExchangeRate.Exchange_Sel(DateTime.Now);

        //Hashtable hsCurrency = ConvertDataTableToHashTable(dtbCurrency, "ExchangeId", "Currency");

        DataTable dt = Member.SelMemberListByAffId(AdminSession.AffId, dtmFDate, dtmTDate, intMemberType, isGetAll, intPage, intPageSize, ref intTotalPages);
        int intNumMem = dt.Rows.Count;
        StringBuilder strBuild = new StringBuilder();
        if (intNumMem > 0)
        {
            int intcount = (intPage - 1) * intPageSize;
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            foreach (DataRow dr in dt.Rows)
            {
                intcount++;
                string strTmp = strContent;
                string rowBg;
                if (DNAide.Core.CastAide.AsBoolean(dr["IsRemoved"]))
                {
                    TemplateHandler.Assign(ref strTmp, "rowinfo", "Member was removed");
                    rowBg = "#cccccc";
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "rowinfo", string.Empty);
                    rowBg = (intcount % 2 == 0 ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                }
                TemplateHandler.Assign(ref strTmp, "color", rowBg);
                TemplateHandler.Assign(ref strTmp, "no", intcount);
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"]);
                TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"]);
                TemplateHandler.Assign(ref strTmp, "email-val", dr["email"]);
                TemplateHandler.Assign(ref strTmp, "currency-val", dr["currency"]);
                TemplateHandler.Assign(ref strTmp, "registerdate-val", dr["registereddate"]);
                TemplateHandler.Assign(ref strTmp, "description-val", dr["description"]);
                strBuild.Append(strTmp);
            }
            TemplateHandler.Assign(ref strTemplate, "paging-top", AdminCommon.GenPaging(intTotalPages, intPage, intPageSize, 0, "aPageTop", "txtPageTop", width, false, true));
            TemplateHandler.Assign(ref strTemplate, "paging-bottom", AdminCommon.GenPaging(intTotalPages, intPage, intPageSize, 0, "aPageBottom", "txtPageBottom", width, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTemplate, "paging-bottom", string.Empty);
            strBuild.Append(AdminCommon.NoInfo(7));
        }
        TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
    }

    Hashtable ConvertDataTableToHashTable(DataTable dtIn, string keyField, string valueField)
    {
        Hashtable htOut = new Hashtable();
        foreach (DataRow drIn in dtIn.Rows)
        {
            htOut.Add(drIn[keyField].ToString(), drIn[valueField].ToString());
        }
        return htOut;
    }

}

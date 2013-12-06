using System;
using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;

public partial class UserInfo_EarningsHistory : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrencyUtils.SetDefaultCurrency(AdminSession.AffCurrency);
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/EarningsHistory.htm");
        TemplateHandler.RemoveBlock(ref strTpl, "total");
        int affID = AdminSession.AffId;
       
        int transType=CastAide.AsInt32(Request["transtype"],-1);
        AppCommon.AssignToAdminTemplate(ref strTpl,                
        "Date", "type",  "amount",  "status",  "description", "creator", "total",
        "update", "From", "To", "search", "getall", "transid", "Submit");

        TemplateHandler.Assign(ref strTpl, "custid", affID.ToString());
        TemplateHandler.Assign(ref strTpl, "username", AdminSession.AffUsername);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
      
        TemplateHandler.Assign(ref strTpl, "ischild", Request["ischild"]);
      

        int totalPages = 1, pageIndex = 0, numRows = 0, pageSize = 0;
        pageIndex = CastAide.AsInt32(Request["page"], 1);
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        object dtmFDate = CastAide.AsDateTime(Request["fromdate"], DateTime.Now.AddMonths(-6));
        object dtmTDate = CastAide.AsDateTime(Request["todate"], DateTime.Now);
        bool isGetAll = CastAide.AsBoolean(Request["getAll"], false);
        TemplateHandler.Assign(ref strTpl, "getAll-val", isGetAll);

        TemplateHandler.Assign(ref strTpl, "fdate-val", CastAide.AsDateTime(dtmFDate).ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate-val", CastAide.AsDateTime(dtmTDate).ToString("MM/dd/yyyy"));

        //Get data
        DataTable dtb = new DataTable();
        DbResult rs = Statement.SelStatementHistory(affID, transType, isGetAll ? null : dtmFDate, isGetAll ? null : dtmTDate, pageIndex, pageSize, ref totalPages);

        if (rs != null && rs.ResultSet.Tables.Count > 0)
        {
            dtb = rs.ResultSet.Tables[0];
            numRows = CastAide.AsInt32(rs.Outputs["nrows"]);
        }
        else
        {
            numRows = 0;
        }

            
        StringBuilder strBuild = new StringBuilder();
        if (dtb!=null && dtb.Rows.Count > 0)
        {
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            int rowNo = (pageIndex - 1) * pageSize;
            bool isCheckAll = false;
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            foreach (DataRow dr in dtb.Rows)
            {
                rowNo++;
                string strTmp = strContent;
                int transtype = CastAide.AsInt32(dr["transtype"]);
                DateTime dtmMonthDate = CastAide.AsDateTime(dr["monthdate"]);

                switch (transtype)
                {
                    case 203:
                        TemplateHandler.ReplaceBlock(ref strTmp, "amount-val", string.Empty);
                        TemplateHandler.ReplaceBlock(ref strTmp, "transid-val", CurrencyUtils.Format(dr["amount"]));//Common.FormatBalanceNumber(dr["amount"], AppSession.Prefix));
                        break;
                    case 202:
                        TemplateHandler.ReplaceBlock(ref strTmp, "amount-val", string.Empty);
                        break;
                    case 0:
                        TemplateHandler.ReplaceBlock(ref strTmp, "transid-val", string.Empty);
                        TemplateHandler.Assign(ref strTmp, "amount-val",CurrencyUtils.Format(dr["amount"]));// Common.FormatBalanceNumber(dr["amount"], AppSession.Prefix));
                        break;
                    default:
                        TemplateHandler.ReplaceBlock(ref strTmp, "transid-val", string.Empty);
                        break;
                }

                if (transtype == 0 || string.Compare(dr["status"].ToString(), "pending", true) != 0)// do not allow transfer
                {
                    TemplateHandler.ReplaceBlock(ref strTmp, "status-val", AdminCommon.GerStatus(dr["status"]));
                    TemplateHandler.ReplaceBlock(ref strTmp, "updatestatus", string.Empty);
                }
                else if (transtype == 201)//Exists bettype = 105 and status != peding => allow transfer.
                    isCheckAll = true;
                if (DateTime.Compare(dtmMonthDate, AppSettings.SystemStartDate) < 0 || transtype == 202)
                    TemplateHandler.ReplaceBlock(ref strTmp, "amount-val", CurrencyUtils.Format(dr["amount"])); //Common.FormatBalanceNumber(dr["amount"], AppSession.Prefix));

                TemplateHandler.Assign(ref strTmp, "color", GetRowStyle(dr["status"].ToString()));
                TemplateHandler.Assign(ref strTmp, "no", rowNo);
                TemplateHandler.Assign(ref strTmp, "transid-val", dr["transid"]);
                TemplateHandler.Assign(ref strTmp, "monthdate-val", (transtype == 202 || transtype == 203) ? dtmMonthDate.ToString("dd-MMM-yyyy") : dtmMonthDate.ToString("MMM-yyyy"));
                TemplateHandler.Assign(ref strTmp, "month-val", CastAide.AsDateTime(dr["monthdate"]).ToString("MM/dd/yyyy"));
                TemplateHandler.Assign(ref strTmp, "bettype-val", GetTransType(dr["transtype"]));
                TemplateHandler.Assign(ref strTmp, "creator-val", dr["creator"]);
                TemplateHandler.Assign(ref strTmp, "amount-val",CurrencyUtils.Format(dr["amount"]));// Common.FormatBalanceNumber(dr["amount"], AppSession.Prefix));
                TemplateHandler.Assign(ref strTmp, "disabled", string.Empty);
                TemplateHandler.Assign(ref strTmp, "status-val", AdminCommon.GerStatus(dr["status"]));
                TemplateHandler.Assign(ref strTmp, "description-val", dr["description"].ToString());
                strBuild.Append(strTmp);
            }

            TemplateHandler.Assign(ref strTpl, "disabled", isCheckAll ? "" : "disabled");
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPages, pageIndex, pageSize, numRows, "aPageTop", "txtPageTop", 750, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPages, pageIndex, pageSize, numRows, "aPageBottom", "txtPageBottom", 750, false, false));
        }
        else
        {
            strBuild.Append(AdminCommon.NoInfo(8));
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
        TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0,
            Fanex.Affiliate.Resources.Translator.Get("StatementHistory"), "i_customer.png"));
        //Paging
        
        TemplateHandler.Assign(ref strTpl, "page", pageIndex);
        TemplateHandler.Assign(ref strTpl, "pagesize", pageSize);
        #endregion

        this.HtmlContent = strTpl;

    }
    string GetTransType(object transType)
    {
        switch (CastAide.AsInt32(transType))
        {
            case 201:
                return "Monthly Winlost";
            case 202:
                return "Payment Request";
            case 203:
                return "Adjustment";           
            default :
                return "Transaction";
        }
    }
    string GetRowStyle(string strStatus)
    {
        switch (strStatus.ToLower())
        {
            case "cancelled":
            case "rejected":
                return AdminCommon.RowBgClosed;
                //return "Closed";
            case "approved":
            case "b/f":
                return AdminCommon.RowBgOdd;
                //return "Approved";
            case "running":
            case "current":
                return AdminCommon.RowBgPending;
                //return "Pending";
            default:
                return AdminCommon.RowBgPending; ;// return "Pending";
        }
    }
}

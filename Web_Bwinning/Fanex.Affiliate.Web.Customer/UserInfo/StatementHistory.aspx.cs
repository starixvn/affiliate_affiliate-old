using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Customer;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Resources;

public partial class UserInfo_StatementHistory : BasePageLogIn
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ltrStaHis.Text = Translator.Get("StatementHistory");

        string strTpl = TemplateHandler.LoadTemplate("UserInfo/StatementHistory.htm");
        #region load language
        TemplateHandler.Assign(ref strTpl, "monthdate", Translator.Get("Date"));
        TemplateHandler.Assign(ref strTpl, "type", Translator.Get("Type"));
        TemplateHandler.Assign(ref strTpl, "amount", Translator.Get("Amount"));
        TemplateHandler.Assign(ref strTpl, "status", Translator.Get("Status"));
        TemplateHandler.Assign(ref strTpl, "creator", Translator.Get("Creator"));
        TemplateHandler.Assign(ref strTpl, "description", Translator.Get("Description"));
        TemplateHandler.Assign(ref strTpl, "total", Translator.Get("Total"));
        #endregion
        string strStatus = Request["status"];
        int intPageIndex = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intTotalPage = 0;
        int affID = Fanex.Affiliate.Core.AppSession.CustID;
        int transType = CastAide.AsInt32(Request["transtype"], -1);

        DbResult result = Statement.SelStatementHistory(affID, transType, null, null, intPageIndex, intPageSize, ref intTotalPage);
        //DbResult result = Statement.HistorySel(strStatus, intPageIndex, ref intTotalPage);
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count>0)
        {
            DataTable dtb = result.ResultSet.Tables[0];
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            //decimal dcTotal = 0;
            StringBuilder strBuild = new StringBuilder();
            int rowNo = (intPageIndex - 1) * intPageSize;
            bool isCheckAll = false;

            string strRowStyle = "bgcgy";//bgwhite
            string strMouseStyle = "b";//bgwhite
            string amount=string.Empty;
            foreach (DataRow dr in dtb.Rows)
            {                
                string strTmp = strContent;
                //dcTotal += CastAide.AsDecimal(dr["amount"]);
                DateTime dtmMonthDate = CastAide.AsDateTime(dr["monthdate"]);
                int intTransType = CastAide.AsInt32(dr["transtype"]);
                rowNo++;
                //amount = CastAide.AsDouble(dr["amount"]) >= 0 ? CurrencyUtils.Format(dr["amount"]) : "<span class='NegativeBalance'>" + CurrencyUtils.Format(dr["amount"]) + "</span>";
                amount = CurrencyUtils.Format(dr["amount"], "<span class='NegativeBalance'>{0}</span>");
                if (intTransType == 202 || intTransType == 203|| DateTime.Compare(dtmMonthDate, AppSettings.SystemStartDate) < 0)
                    TemplateHandler.ReplaceBlock(ref strTmp, "amount-block", amount);
                else
                    TemplateHandler.Assign(ref strTmp, "amount-block", amount);

                strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                strMouseStyle = (strMouseStyle == "b") ? "a" : "b";
                TemplateHandler.Assign(ref strTmp, "no", rowNo);
                TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                TemplateHandler.Assign(ref strTmp, "transid-val", dr["transid"]);
                //TemplateHandler.Assign(ref strTmp, "monthdate-val", (intTransType == 202 || intTransType == 203) ? dtmMonthDate.ToString("dd-MMM-yyyy") : dtmMonthDate.ToString("MMM-yyyy"));
                //TemplateHandler.Assign(ref strTmp, "month-val", CastAide.AsDateTime(dr["monthdate"]).ToString("MM/dd/yyyy"));
                TemplateHandler.Assign(ref strTmp, "monthdate-val", (intTransType == 202 || intTransType == 203) ? AffCommon.ConvertDateTime(dtmMonthDate, "-", true, false) : AffCommon.ConvertDateTime(dtmMonthDate, "-", false, false));
                TemplateHandler.Assign(ref strTmp, "month-val", AffCommon.ConvertDateTime(dr["monthdate"], "/", true, false));
                TemplateHandler.Assign(ref strTmp, "bettype-val", GetTransType(dr["transtype"]));
                TemplateHandler.Assign(ref strTmp, "creator-val", dr["creator"]);
                TemplateHandler.Assign(ref strTmp, "amount-val", amount);
                TemplateHandler.Assign(ref strTmp, "status-val", AffCommon.GerStatus(dr["status"]));
                //string strDescription = (intTransType == 202 ? dr["description"].ToString() : string.Empty);
                TemplateHandler.Assign(ref strTmp, "description-val", dr["description"]);

                strBuild.Append(strTmp);
            }
            //strBuild.Append(Common.GenPagingFooter("", intTotalPage, intPageIndex, 6));
            TemplateHandler.Assign(ref strTpl, "page", intPageIndex);
            TemplateHandler.Assign(ref strTpl, "paging-top", AppCommon.GenPaging(intTotalPage, intPageIndex, intPageSize, rowNo, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AppCommon.GenPaging(intTotalPage, intPageIndex, intPageSize, rowNo, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
            //TemplateHandler.Assign(ref strTpl, "earning-val", Common.FormatNumber(dcTotal));
            TemplateHandler.Assign(ref strTpl, "disabled", isCheckAll ? "" : "disabled");
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "content", AffCommon.NoInfo(6));
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
        TemplateHandler.Assign(ref strTpl, "PageSize-val", intPageSize);
        ltrContent.Text = strTpl;

    }
    string GetTransType(object intTransType)
    {
        switch (CastAide.AsInt32(intTransType))
        {           
            case 203:
                return Translator.Get("Adjustment");
            case 201:
                return Translator.Get("MonthlyWinlost");;
            case 202:
                return Translator.Get("PaymentRequest");
            default :
                return Translator.Get("Transaction");
        }
    }
}



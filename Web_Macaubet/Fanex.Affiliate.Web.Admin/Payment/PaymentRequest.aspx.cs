using System;
using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using DNAide.Utils;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;
using Fanex.Affiliate.Core.Helper;
using Fanex.Affiliate.Resources;

public partial class PaymentRequest : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Payment/PaymentRequest.htm");
        
        AppCommon.AssignToAdminTemplate(ref strTpl,
        "search", "paymentrequest", "transid", "DateRequest", "Username", "refno", "Amount",
        "status", "all", "netteller", "moneybooker", "banktrans", "cheque",
        "approved", "rejected", "cancelled", "From", "To", "dateerr", "getall", "username",
        "searchby", "others", "Currency","completeddate","creator","Submit");
        //TemplateHandler.Assign(ref strTpl, "submenu", AdminCommon.GenSubMenu(new string[] { "#" }, new string[] { Fanex.Affiliate.Resources.Translator.Get("PaymentRequest + " [{nummem}]" }, 801, 0));        

        string strSearchBy = string.Empty;
        string strFrom = string.Empty;
        string strTo = string.Empty;
        string strUsername = string.Empty;
        bool isGetAll = false;
        string strStatus = string.Empty;
        int page = string.IsNullOrEmpty(Request["page"]) ? 1 : CastAide.AsInt32(Request["page"], 1);
        int iPagesize = string.IsNullOrEmpty(Request["slPageSize"]) ? 20 : CastAide.AsInt32(Request["slPageSize"], 1);
        
        strSearchBy = string.IsNullOrEmpty(Request["slSearch"]) ? "0" : Request["slSearch"];
        int currency = CastAide.AsInt32(Request["slCurrency"],0);
        currency = strSearchBy != "0" ? 0 : currency;

        isGetAll = Request["chkGetAll"] == "on" ? true : false;
        strUsername = Request["txtUsername"];
        if (!String.IsNullOrEmpty(strUsername))
            strUsername = strUsername.Trim();

        strStatus = Request["slStatus"];
        DateTime dtmNow = DateTime.Now;
        dtmNow = GenStartOfDate(dtmNow);
        DateTime dtmFrom = CastAide.AsDateTime(Request["fdate"], dtmNow.AddDays(-1));
        DateTime dtmTo = CastAide.AsDateTime(Request["tdate"], dtmNow);

        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        TemplateHandler.Assign(ref strTpl, "search-val", strSearchBy);         
        TemplateHandler.Assign(ref strTpl, "username-val", strUsername);
        TemplateHandler.Assign(ref strTpl, "page", page);
        TemplateHandler.Assign(ref strTpl, "pagesize", iPagesize);
        TemplateHandler.Assign(ref strTpl, "processing", "Processing");
        //Begin list currency
        //var lsCurrency = AdminCommon.GetListSupportedCurrency();
         TemplateHandler.Assign(ref strTpl, "lscurrency",
             HtmlHelper.DropDownList(AppSettings.AffCurrencies, "slCurrency", currency, true));
             //Common.GenDropdownList(lsCurrency, lsCurrency, "slCurrency", false, "Select Currency", currency));
        //End list currency

        TemplateHandler.Assign(ref strTpl, "isgetall", isGetAll ? "checked" : string.Empty);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFrom.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTo.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "curStatus", strStatus);//get current status selected
        //Response.Write("<script type='text/javascript'>alert('" + isGetAll.ToString() + "');</script>");
        DbResult rs = Fanex.Affiliate.DataObjects.PaymentRequest.PaymentRequestList(strSearchBy, strUsername, strStatus, dtmFrom, dtmTo, isGetAll,currency, page, iPagesize);

        int intMaxPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
        int intNumRows = CastAide.AsInt32(rs.Outputs["nrows"]);
        int intPageId = page;
        TemplateHandler.Assign(ref strTpl, "submenu", 
            SubMenu.GenSubMenu(new string[] { "PaymentRequest.aspx" }, 
            new string[] { Admin.Payment }, 0, intNumRows, Admin.PaymentRequest, "i_pay_re.png"));
            
        if (intNumRows > 0)
        {
            DataTable dtb= rs.ResultSet.Tables[0];
            string strContentTpl = TemplateHandler.CopyBlock(strTpl, "content");
            StringBuilder strContent = new StringBuilder();
            int intRowNo = (intPageId - 1) * iPagesize;
            TemplateHandler.Assign(ref strContentTpl, "over", AdminCommon.RowBgOver);
            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strContentTpl;
                intRowNo++;
                var rowCurrency =   dr["Currency"].ToString();
                TemplateHandler.Assign(ref strTmp, "color", GetRowStyle(dr["status"].ToString()));
                TemplateHandler.Assign(ref strTmp, "no", intRowNo);
                TemplateHandler.Assign(ref strTmp, "transid-val", dr["transid"].ToString());
                TemplateHandler.Assign(ref strTmp, "transdate-val", AdminCommon.IBCDateTimeFormat(dr["transdate"],DTFormatType.DT24h));
                TemplateHandler.Assign(ref strTmp, "uname-val", dr["username"].ToString());
                TemplateHandler.Assign(ref strTmp, "currency-val",rowCurrency);
                ////NEW//TemplateHandler.Assign(ref strTmp, "amt-val", AdminCommon.FormatNumber(dr["amt"], strPrefix));
                TemplateHandler.Assign(ref strTmp, "amt-val", CurrencyUtils.Format(dr["amt"],rowCurrency,null));
                TemplateHandler.Assign(ref strTmp, "status-val", AdminCommon.GerStatus(dr["status"].ToString()));
                if (dr["status"].ToString().ToLower() == "pending")
                {
                    TemplateHandler.Assign(ref strTmp, "completeddate-val", "--");
                    TemplateHandler.Assign(ref strTmp, "creator-val", "--");
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "completeddate-val", AdminCommon.IBCDateTimeFormat(dr["completeddate"],DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "creator-val", dr["creator"].ToString());
                }
                strContent.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strContent.ToString());
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(intMaxPage, intPageId, iPagesize, intNumRows, "aPageTop", "txtPageTop", 800, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(intMaxPage, intPageId, iPagesize, intNumRows, "aPageBottom", "txtPageBottom", 800, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", "");
            TemplateHandler.Assign(ref strTpl, "paging-bottom", "");
            TemplateHandler.ReplaceBlock(ref strTpl, "content", AdminCommon.NoInfo(8));
        }
        TemplateHandler.Assign(ref strTpl, "nummem", intNumRows);
        this.HtmlContent = strTpl;
    }

    DateTime GenStartOfDate(DateTime dtmDate)
    {
        return CastAide.AsDateTime(dtmDate.ToString("MM/dd/yyyy"), dtmDate);
    }

    string GetRowStyle(string strStatus)
    {
        switch (strStatus.ToLower())
        {
            case "cancelled":
            case "rejected":
                return AdminCommon.RowBgClosed;//return "Closed";
            case "approved":
                return AdminCommon.RowBgOdd;// return "Approved";
            case "pending":
                return AdminCommon.RowBgPending;//return "Pending";
            default:
                return AdminCommon.RowBgPending;
        }
    }
}

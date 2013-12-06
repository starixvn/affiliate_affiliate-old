using DNAide.Web;
using DNAide.Core;
using System.Data;
namespace Fanex.Affiliate.Core.Customer.GenerateHTML
{
    using DNAide.Data;
    using System.Text;
    using System;
    using System.Globalization;

    public class PaymentRequest
    {
        public static string PaymentList(ref string strBalance, ref string strMinAmt,
              ref string strPaymentMethod, ref double dBalance,ref double dMinAmt)
        {
            CurrencyUtils.SetDefaultCurrency(AppSession.Currency);

            string strListRequest = TemplateHandler.LoadTemplate("UserInfo/PaymentRequest.htm");
            //Assign header
            TemplateHandler.Assign(ref strListRequest, "title", Resources.Translator.Get("RecentPayment"));
            TemplateHandler.Assign(ref strListRequest, "refno", Resources.Translator.Get("RefNo"));
            TemplateHandler.Assign(ref strListRequest, "daterequest", Resources.Translator.Get("DateRequest"));
            TemplateHandler.Assign(ref strListRequest, "requestamount", Resources.Translator.Get("RequestAmount"));
            TemplateHandler.Assign(ref strListRequest, "paymentmethod", Resources.Translator.Get("PaymentMethod"));
            TemplateHandler.Assign(ref strListRequest, "complete", Resources.Translator.Get("CompletedDate"));
            TemplateHandler.Assign(ref strListRequest, "status", Resources.Translator.Get("Status"));
            TemplateHandler.Assign(ref strListRequest, "description", Resources.Translator.Get("Description"));
            TemplateHandler.Assign(ref strListRequest, "cancelpayout", Resources.Translator.Get("CancelRequest"));
            //Get data and assign list request
            DbResult result = DataObjects.PaymentRequest.GetInfo(Fanex.Affiliate.Core.AppSession.CustID);

            var format = "<span class='NegativeBalance'>{0}</span>";
            dBalance = CastAide.AsDouble(result.Outputs["balbroughtforward"]);
            dMinAmt = CastAide.AsDouble(result.Outputs["minamount"]);
            strBalance = CurrencyUtils.Format(dBalance, format);
            strMinAmt = CurrencyUtils.Format(dMinAmt, format);
            strPaymentMethod = result.Outputs["preferredpayment"].ToString();

            string strContent = TemplateHandler.CopyBlock(strListRequest, "content");
            StringBuilder Content = new StringBuilder();
            #region Get Payment history
            DataTable dtb = result.ResultSet.Tables[0];
            bool hasPendingRequest = false;
            int numRows = 0;
            if (dtb.Rows.Count > 0)
            {
                string strRowStyle = "bgcgy";//bgwhite
                string strMouseStyle = "b";//bgwhite
                foreach (DataRow dr in dtb.Rows)
                {
                    if (dr["status"].ToString().ToLower().CompareTo("pending") == 0)
                    {
                        hasPendingRequest = true;
                        TemplateHandler.Assign(ref strListRequest, "pending", Resources.Translator.Get("PendingRequest"));
                        TemplateHandler.Assign(ref strListRequest, "daterequest-pend", dr["transdate"].ToString());
                        TemplateHandler.Assign(ref strListRequest, "requestamount-pend", CurrencyUtils.Format(dr["Amount"]));
                        TemplateHandler.Assign(ref strListRequest, "requestamount-pend-val", CastAide.AsDecimal(dr["Amount"],AppSettings.NumberFormat));

                        TemplateHandler.Assign(ref strListRequest, "status-pend", GerStatus(dr["status"].ToString()));
                        TemplateHandler.Assign(ref strListRequest, "transid-pend", dr["transid"].ToString());
                        //TemplateHandler.Assign(ref strListRequest, "winlostdate-pend", CastAide.AsDateTime(dr["transdate"]).ToString("MM/dd/yyyy"));
                        TemplateHandler.Assign(ref strListRequest, "winlostdate-pend", AffCommon.ConvertDateTime(dr["transdate"], "/", true, false));
                        TemplateHandler.Assign(ref strListRequest, "ispending", "1");
                    }
                    else
                    {
                        numRows++;
                        strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                        strMouseStyle = (strMouseStyle == "b") ? "a" : "b";
                        string strTmp = strContent;
                        // int curTransId = CastAide.AsInt32(dr["transid"]);
                        TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                        TemplateHandler.Assign(ref strTmp, "id", string.Empty);
                        TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                        TemplateHandler.Assign(ref strTmp, "refno-val", dr["transid"].ToString());
                        TemplateHandler.Assign(ref strTmp, "status-val", GerStatus(dr["status"].ToString()));
                        //TemplateHandler.Assign(ref strTmp, "daterequest-val", CastAide.AsDateTime(dr["transdate"]).ToString("MM/dd/yyyy"));
                        TemplateHandler.Assign(ref strTmp, "daterequest-val", AffCommon.ConvertDateTime(dr["transdate"], "/", true, false));
                        TemplateHandler.Assign(ref strTmp, "requestamount-val",  CurrencyUtils.Format(dr["amount"]));
                             //AffCommon.FormatNumber(CastAide.AsDecimal(dr["amount"])));
                        TemplateHandler.Assign(ref strTmp, "paymentmethod-val", strPaymentMethod.Replace("Wire", Resources.Translator.Get("BankTransfer")));
                        //TemplateHandler.Assign(ref strTmp, "complete-val", CastAide.AsDateTime(dr["completeddate"]).ToString("MM/dd/yyyy"));
                        TemplateHandler.Assign(ref strTmp, "complete-val", AffCommon.ConvertDateTime(dr["completeddate"], "/", true, false));
                        TemplateHandler.Assign(ref strTmp, "description-val", Fanex.Core.Utility.Common.BreakLongString(dr["AffDesc"].ToString(), 30));
                        Content.Append(strTmp);
                    }
                }
                if (numRows == 0)
                    Content.Append(AffCommon.NoInfo(6));

            }
            else
                Content.Append(AffCommon.NoInfo(6));
            #endregion

            if (!hasPendingRequest)
            {
                TemplateHandler.Assign(ref strListRequest, "ispending", "0");
                TemplateHandler.RemoveBlock(ref strListRequest, "pending");
            }

            TemplateHandler.ReplaceBlock(ref strListRequest, "content", Content.ToString());
            return strListRequest;
        }
       
        static string GerStatus(string strStatus)
        {
            switch (strStatus.ToLower())
            {
                case "pending":
                    return Resources.Translator.Get("Proccessing");
                case "completed":
                    return Resources.Translator.Get("Approved");
                case "cancelled":
                    return Resources.Translator.Get("Cancelled");
                case "approved":
                    return Resources.Translator.Get("Approved");
                case "rejected":
                    return Resources.Translator.Get("Rejected");
                default:
                    return Resources.Translator.Get("Unknown");
            }
        }
        public static string PaymentErr(int ErrCode, string ErrMsg)
        {
            if (ErrCode == 1005)
                return ErrMsg;
            if (ErrCode > 1000 && ErrCode < 1008)
                return Resources.Translator.Get("PaymentErr" + ErrCode);
            
            return string.Empty;
        }
    }
}

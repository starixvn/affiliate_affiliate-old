using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Configuration;
using System.Web;
using Fanex.IndoPaul.DataObjects;
namespace Fanex.IndoPaul.Core.Affiliate.GenerateHTML
{
    public class PaymentRequest
    {
        public static string PaymentList( ref string strBalance, ref string strMinAmt,ref string strPaymentMethod)
        {
            string strListRequest = TemplateHandler.LoadTemplate("UserInfo/PaymentRequest.htm");
            //Assign header
            TemplateHandler.Assign(ref strListRequest, "title", Resources.Lang.RecentPayment);
            TemplateHandler.Assign(ref strListRequest, "refno", Resources.Lang.RefNo);
            TemplateHandler.Assign(ref strListRequest, "daterequest", Resources.Lang.DateRequest);
            TemplateHandler.Assign(ref strListRequest, "requestamount", Resources.Lang.RequestAmount);
            TemplateHandler.Assign(ref strListRequest, "paymentmethod", Resources.Lang.PaymentMethod);
            TemplateHandler.Assign(ref strListRequest, "complete", Resources.Lang.CompletedDate);
            TemplateHandler.Assign(ref strListRequest, "status", Resources.Lang.Status);
            TemplateHandler.Assign(ref strListRequest, "description", Resources.Lang.Description);
            TemplateHandler.Assign(ref strListRequest, "cancelpayout", Resources.Lang.CancelRequest);
            //Get data and assign list request
            DbResult result = DataObjects.PaymentRequest.GetInfo(AppSettings.Session.CustID);
           
            strBalance = Common.FormatNumber(result.Outputs["balbroughtforward"]);
            strMinAmt = Common.FormatNumber(result.Outputs["minamount"]);
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
                        TemplateHandler.Assign(ref strListRequest, "pending", Resources.Lang.PendingRequest);
                        TemplateHandler.Assign(ref strListRequest, "daterequest-pend", dr["transdate"].ToString());
                        TemplateHandler.Assign(ref strListRequest, "requestamount-pend", Common.FormatNumber(dr["Amount"]));
                        TemplateHandler.Assign(ref strListRequest, "status-pend", GerStatus(dr["status"].ToString()));
                        TemplateHandler.Assign(ref strListRequest, "transid-pend", dr["transid"].ToString());
                        //TemplateHandler.Assign(ref strListRequest, "winlostdate-pend", CastAide.AsDateTime(dr["transdate"]).ToString("MM/dd/yyyy"));
                        TemplateHandler.Assign(ref strListRequest, "winlostdate-pend", Common.ConvertDateTime(dr["transdate"], "/", true, false));
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
                        TemplateHandler.Assign(ref strTmp, "daterequest-val", Common.ConvertDateTime(dr["transdate"], "/", true, false));
                        TemplateHandler.Assign(ref strTmp, "requestamount-val", Common.FormatNumber(CastAide.AsDecimal(dr["amount"])));
                        TemplateHandler.Assign(ref strTmp, "paymentmethod-val", strPaymentMethod.Replace("Wire", Resources.Lang.BankTransfer));
                        //TemplateHandler.Assign(ref strTmp, "complete-val", CastAide.AsDateTime(dr["completeddate"]).ToString("MM/dd/yyyy"));
                        TemplateHandler.Assign(ref strTmp, "complete-val", Common.ConvertDateTime(dr["completeddate"], "/", true, false));
                        TemplateHandler.Assign(ref strTmp, "description-val", Common.BreakLongString(dr["AffDesc"].ToString(),30));
                        Content.Append(strTmp);
                    }
                }
                if(numRows == 0)
                    Content.Append(Common.NoInfo(6));

            }
            else
                Content.Append(Common.NoInfo(6));
            #endregion
            
           if(!hasPendingRequest)
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
                    return Resources.Lang.Proccessing;
                case "completed":
                    return Resources.Lang.Approved;
                case "cancelled":
                    return Resources.Lang.Cancelled;
                case "approved":
                    return Resources.Lang.Approved;
                case "rejected":
                    return Resources.Lang.Rejected;
                default:
                    return Resources.Lang.Unknown;
            }
        }
        public static string PaymentErr(int ErrCode, string ErrMsg)
        {
            switch (ErrCode)
            {
                case 1001:
                    return Resources.Lang.PaymentErr1001;
                case 1002:
                    return Resources.Lang.PaymentErr1002;
                case 1003:
                    return Resources.Lang.PaymentErr1003;
                case 1004:
                    return Resources.Lang.PaymentErr1004;
                case 1005:
                    return ErrMsg;
                case 1006:
                    return Resources.Lang.PaymentErr1006;
                case 1007:
                    return Resources.Lang.PaymentErr1007;
                default:
                    return "";
            }
        }
    }
}

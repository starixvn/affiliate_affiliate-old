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


public partial class UserInfo_CustomersInfo : BasePageLogIn
{
    protected string strCaption = null;
    protected string strContent1 = null; 
    protected void Page_Load(object sender, EventArgs e)
    {

        string strTpl = TemplateHandler.LoadTemplate("UserInfo/CustomersInfo.htm");
        #region load language

        AppCommon.AssignToTemplate(ref strTpl, "username", "fname", "lname", "RegisteredDate","FirstDepositDate","currency");

        strCaption = Translator.Get("CustomersInfo");
        #endregion
        string strStatus = Request["status"];
        int intPageIndex = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intTotalPage = 0;
        int intRows = 0;

        int affID = Fanex.Affiliate.Core.AppSession.CustID;
        int transType = CastAide.AsInt32(Request["transtype"], -1);

        DataTable dtbResult = Member.SelMemInfoAll(AppSession.UserName, null,-1, ref intPageIndex, intPageSize, ref intTotalPage,ref intRows);

        if (dtbResult.Rows.Count > 0)
        {            
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            //decimal dcTotal = 0;
            StringBuilder strBuild = new StringBuilder();
            int rowNo = (intPageIndex - 1) * intPageSize;
            bool isCheckAll = false;

            string strRowStyle = "bgcgy";//bgwhite
            string strMouseStyle = "b";//bgwhite            
          
            int i = (intPageIndex - 1) * intPageSize;
            foreach (DataRow dr in dtbResult.Rows)
            {
                i++;
                string strTmp = strContent;
                TemplateHandler.Assign(ref strTmp, "color", (i % 2 == 0) ? "bgwhite" : "bgcgy");
                TemplateHandler.Assign(ref strTmp, "number", i.ToString());
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                
                TemplateHandler.Assign(ref strTmp, "currency-val", dr["currency"].ToString());

                TemplateHandler.Assign(ref strTmp, "RegisteredDate-val", string.Format("{0:MM/dd/yyyy}", dr["RegisteredDate"]));
                TemplateHandler.Assign(ref strTmp, "FirstDepositDate-val", string.Format("{0:MM/dd/yyyy}", dr["createddate"]));
                
                strBuild.Append(strTmp);
            }
            //strBuild.Append(Common.GenPagingFooter("", intTotalPage, intPageIndex, 6));
            TemplateHandler.Assign(ref strTpl, "page", intPageIndex);
            TemplateHandler.Assign(ref strTpl, "paging-top", AppCommon.GenPaging(intTotalPage, intPageIndex, intPageSize, rowNo, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AppCommon.GenPaging(intTotalPage, intPageIndex, intPageSize, rowNo, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
            //TemplateHandler.Assign(ref strTpl, "earning-val", Common.FormatNumber(dcTotal));
            TemplateHandler.Assign(ref strTpl, "disabled", isCheckAll ? "" : "disabled");

            TemplateHandler.Assign(ref strTpl, "style", strRowStyle);
            TemplateHandler.Assign(ref strTpl, "style1", strMouseStyle);
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "content", AffCommon.NoInfo(6));
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
        TemplateHandler.Assign(ref strTpl, "PageSize-val", intPageSize);
        strContent1 = strTpl;

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

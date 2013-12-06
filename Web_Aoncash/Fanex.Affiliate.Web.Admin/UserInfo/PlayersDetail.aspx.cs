using System;
using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;
using Fanex.Affiliate.DataObjects;
using Fanex.Core.Utility;


public partial class PlayersDetail : BasePageTpl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrencyUtils.SetDefaultCurrency(AdminSession.AffCurrency);
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/PlayersDetail.htm");

        #region Load language
        AppCommon.AssignToAdminTemplate(ref strTpl,
        "username", "ibcusername", "turnover", "winlost", "withdrawal", "deposit", "total",
        "playersdetail", "bonus", "Adjustment", "PSP", "description", "ProductType");
        #endregion        

        //Get data
        int intCustId = AdminSession.AffId;
        int productId = CastAide.AsInt32(Request["slProduct"], 0);
        int width = 750;
        if (productId == 2)
        {
            TemplateHandler.RemoveBlock(ref strTpl, "sportsbook");
            width = 550;
        }
        string strMonth = Request["month"];
        TemplateHandler.Assign(ref strTpl, "month-val", strMonth);
        TemplateHandler.Assign(ref strTpl, "slProducts", AdminCommon.GenListProduct(productId));

        int pageIndex = 1, totalPages = 0, pageSize = 0;
        pageIndex = CastAide.AsInt32(Request["page"], 1);
        pageSize = CastAide.AsInt32(Request["pagesize"], 200);
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        TemplateHandler.Assign(ref strTpl, "pagesize", pageSize);
        TemplateHandler.Assign(ref strTpl, "width", width);

        DbResult result = Affiliate.SelPlayersDetail(intCustId, productId, strMonth, pageIndex, pageSize, ref totalPages);
        
        StringBuilder strBuild = new StringBuilder();
        if (result.ResultSet.Tables.Count>0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
	    DataTable dtb = result.ResultSet.Tables[0];	
            //Assign row content
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            //  decimal dcTurnover = 0, dcWinlost = 0, dcDeposit = 0;
            int intcount = (pageIndex - 1) * pageSize;// hardcode pagesize for member/player list
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
            
            foreach (DataRow dr in dtb.Rows)
            {
                var currency = dr["affcurrency"].ToString();
                
                intcount++;
                string strTmp = strContent;
                if (string.IsNullOrEmpty(CastAide.AsString(dr["description"], String.Empty)))
                    TemplateHandler.Assign(ref strTmp, "color", (intcount % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);
                else
                {
                    TemplateHandler.Assign(ref strTmp, "rowinfo", "Member was removed");                    
                    TemplateHandler.Assign(ref strTmp, "color", "#cccccc");
                }
                TemplateHandler.Assign(ref strTmp, "no-val", intcount);
                //make sure remove 12bet$ in the first position of strUsername2
                //string ibcUsername = dr["ibcusername"].ToString();
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"]);
              //  TemplateHandler.Assign(ref strTmp, "ibcusername-val", ibcUsername);
                TemplateHandler.Assign(ref strTmp, "turnover-val", CurrencyUtils.Format(dr["turnover"], currency,null));
                TemplateHandler.Assign(ref strTmp, "winlost-val", CurrencyUtils.Format(dr["winlost"], currency, null));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", CurrencyUtils.Format(dr["withdrawal"], currency, null));
                TemplateHandler.Assign(ref strTmp, "deposit-val", CurrencyUtils.Format(dr["deposit"], currency, null));
                TemplateHandler.Assign(ref strTmp, "bonus-val", CurrencyUtils.Format(dr["bonus"], currency, null));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", CurrencyUtils.Format(dr["adjustment"], currency, null));
                TemplateHandler.Assign(ref strTmp, "psp-val", CurrencyUtils.Format(dr["psp"], currency, null));
                TemplateHandler.Assign(ref strTmp, "description-val", dr["description"]);
                strBuild.Append(strTmp);
            }
            //strBuild.Append(Common.GenPagingFooter("PlayersDetail.aspx", totalPages, pageIndex, 5));
            TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPages, pageIndex, pageSize, dtb.Rows.Count, "aPageTop", "txtPageTop", width, false, true));
            //TemplateHandler.Assign(ref strTpl, "paging-top", AdminCommon.GenPaging(totalPages, pageIndex, pageSize, dtb.Rows.Count, "aPageTop", "txtPageTop", new string[] { "[ALL]", "sportsbook", "Casino" }, new string[] { "all", "sportsbook", "casino" }, 662, false, true, true, false));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", AdminCommon.GenPaging(totalPages, pageIndex, pageSize, dtb.Rows.Count, "aPageBottom", "txtPageBottom", width, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "pagingcss", "hide");
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
            strBuild.Append(AdminCommon.NoInfo(6));
        }

        TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());       
       
        #region Load SubPage
        //string subPage = Common.GenSubPage(5, string.Empty, " (" + (string.IsNullOrEmpty(Request["month"]) ? Fanex.Affiliate.Resources.Translator.Get("All : CastAide.AsDateTime(Request["month"]).ToString("MMMM yyyy")) + ")");
        //TemplateHandler.Assign(ref subPage, "ContentPage", strTpl);
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 3, string.IsNullOrEmpty(Request["month"]) ? Fanex.Affiliate.Resources.Translator.Get("All") : CastAide.AsDateTime(Request["month"]).ToString("MMMM yyyy"), Fanex.Affiliate.Resources.Translator.Get("PlayersDetail"), "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;
    }
}

namespace Fanex.Web.Bwinning.Admin.Home
{
    using System;
    using System.Data;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using System.Linq;
    using Fanex.Affiliate.Resources;
    using System.Text;

    public partial class Home : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrencyUtils.SetDefaultCurrency(AdminSettings.SystemCurrency.Value);

            var home = Fanex.Affiliate.Resources.Translator.Get("Home");
           string content = TemplateHandler.LoadTemplate("Home.htm");
            TemplateHandler.Assign(ref content, "submenu", SubMenu.GenSubMenu(new string[] { "Home.aspx" },
                new string[] { home }, 0, 0, home, "i_home.png"));

            AppCommon.AssignToAdminTemplate(ref content, new string[]{"TaskWaitingApp", "Affiliate", "PaymentRequest", "AffStatisticsAll", "AffInPro",
            "Pending", "Approved", "Disapproved", "Closed", "MemStatAll", "Deposit", "NonDeposit", 
            "TotalDeposit", "TotalWinlost", "TotalBonus", "Adjustment", "TotalPSPFees", "TotalNetWinlost",
            "MemInAffPro", "BannerStatc", "AffMonthStatc", "MemStatcMonth", "BannerInPro", "ImageBanner",
            "FlashBanner", "MediaImageBanner", "MediaFlashBanner", "Username", "LoggedIn", "LastRequest", 
            "Role", "WeHave", "LockedOut","TotalAdjustment","CurrOnlineList","Page","PageSize","IP"}); 
            
            TemplateHandler.Assign(ref content, "HomeDesc", Admin.HomeDesc);            
            TemplateHandler.Assign(ref content, "themepath", AppSettings.ThemeDir);            
            
            //dynamic data render
            //Decimal dcPSPToday = DataHandler.Member.GetPSPToday();
            DbResult rs = Fanex.Affiliate.DataObjects.Home.GetGenInforFromAff();
            DataTable dtb = rs.ResultSet.Tables[0];
            DataTable dtbBanners = rs.ResultSet.Tables[1];
            DataRow dr = dtb.Rows[0];


            // banner statistics 

            var bannerNum = 0;
            var sbBanner = new StringBuilder();

            var firstRow = true;
            foreach (DataRow row in dtbBanners.Rows)
            {

                var  typename = Fanex.Affiliate.Resources.Admin.ResourceManager.GetString(row["TypeName"].ToString());
                if (String.IsNullOrEmpty(typename))
                    typename = row["TypeName"].ToString();
                else
                    typename = typename.ToLower();
                
                bannerNum += CastAide.AsInt32(row["BannerNum"], 0);
                if (!firstRow)
                    sbBanner.Append(" / ");

                sbBanner.AppendFormat(@"<span " + (firstRow ? "style='padding-left: 10px;'" : "") + " class='text_blu_bl'><a href='../Media/Listbanner.aspx?typeoption=1&amp;afftypeoption={0}'>{1}</a></span> {2} {3}",
                    row["TypeID"], row["ImageBannerNum"], typename, Fanex.Affiliate.Resources.Admin.ImageBanner);

                sbBanner.AppendFormat(@" / <span class='text_blu_bl'><a href='../Media/Listbanner.aspx?typeoption=2&amp;afftypeoption={0}'>{1}</a></span> {2} {3}",
                    row["TypeID"], row["FlashBannerNum"], typename, Fanex.Affiliate.Resources.Admin.FlashBanner);
                firstRow = false;
            }

            TemplateHandler.Assign(ref content, "BannerInPro-val", bannerNum);
            TemplateHandler.Assign(ref content, "BannerStatistics", sbBanner.ToString());
            Decimal dcPSPAll = CastAide.AsDecimal(dr["PSPFeeAll"]);
            Decimal dcPSPMonth = CastAide.AsDecimal(dr["PSPFeeMonth"]);

            TemplateHandler.Assign(ref content, "AffNum-val", CastAide.AsInt32(dr["PendingAllNum"], 0));
            TemplateHandler.Assign(ref content, "PaymentReqNum-val", CastAide.AsInt32(dr["PaymentReqNum"], 0));

            TemplateHandler.Assign(ref content, "AffInProAll-val", CastAide.AsInt32(dr["AffInProAllNum"], 0));
            TemplateHandler.Assign(ref content, "PendingAll-val", CastAide.AsInt32(dr["PendingAllNum"], 0));
            TemplateHandler.Assign(ref content, "ApprovedAll-val", CastAide.AsInt32(dr["ApprovedAllNum"], 0));
            TemplateHandler.Assign(ref content, "DisapprovedAll-val", CastAide.AsInt32(dr["DisapprovedAllNum"], 0));

            TemplateHandler.Assign(ref content, "Deposit-val", CastAide.AsInt32(dr["DepositMemAllNum"], 0));
            TemplateHandler.Assign(ref content, "TotalDepositAll-val", CurrencyUtils.Format(dr["DepositAll"]));
            TemplateHandler.Assign(ref content, "TotalWinlostAll-val", CurrencyUtils.Format(dr["WinlostAll"]));
            TemplateHandler.Assign(ref content, "TotalBonusAll-val", CurrencyUtils.Format(dr["BonusAll"]));
            //TemplateHandler.Assign(ref content, "TotalPSPFeesAll-val", AdminCommon.FormatNumber(dcPSPAll + dcPSPToday));
            TemplateHandler.Assign(ref content, "TotalPSPFeesAll-val", CurrencyUtils.Format(dcPSPAll));
            TemplateHandler.Assign(ref content, "TotalAdjustmentAll-val", CurrencyUtils.Format(dr["AdjustmentAll"]));
            TemplateHandler.Assign(ref content, "TotalNetWinlostAll-val", CurrencyUtils.Format(dr["NetWinlostAll"]));            

            TemplateHandler.Assign(ref content, "AffInProMoth-val", CastAide.AsInt32(dr["AffInProMonthNum"], 0));
            TemplateHandler.Assign(ref content, "PendingMonth-val", CastAide.AsInt32(dr["PendingMonthNum"], 0));
            TemplateHandler.Assign(ref content, "ApprovedMonth-val", CastAide.AsInt32(dr["ApprovedMonthNum"], 0));
            TemplateHandler.Assign(ref content, "DisapprovedMonth-val", CastAide.AsInt32(dr["DisapprovedMonthNum"], 0));
            TemplateHandler.Assign(ref content, "ClosedMonth-val", CastAide.AsInt32(dr["ClosedMonthNum"], 0));
            TemplateHandler.Assign(ref content, "ClosedAllNum-val", CastAide.AsInt32(dr["ClosedAllNum"], 0));
            TemplateHandler.Assign(ref content, "LockedAll-val", CastAide.AsInt32(dr["LockedAllNum"], 0));
            TemplateHandler.Assign(ref content, "LockedMonth-val", CastAide.AsInt32(dr["LockedMonthNum"], 0));

            TemplateHandler.Assign(ref content, "DepositMonth-val", CastAide.AsInt32(dr["DepositMemMonthNum"], 0));
            TemplateHandler.Assign(ref content, "TotalDepositMonth-val", CurrencyUtils.Format(dr["DepositMonth"]));
            TemplateHandler.Assign(ref content, "TotalWinlostMonth-val", CurrencyUtils.Format(dr["WinlostMonth"]));
            TemplateHandler.Assign(ref content, "TotalBonusMonth-val", CurrencyUtils.Format(dr["BonusMonth"]));
            TemplateHandler.Assign(ref content, "TotalPSPFeesMonth-val", CurrencyUtils.Format(dcPSPMonth));
            //TemplateHandler.Assign(ref content, "TotalPSPFeesMonth-val", AdminCommon.FormatNumber(dcPSPMonth + dcPSPToday));
            TemplateHandler.Assign(ref content, "TotalAdjustmentMonth-val", CurrencyUtils.Format(dr["AdjustmentMonth"]));
            TemplateHandler.Assign(ref content, "TotalNetWinlostMonth-val", CurrencyUtils.Format(dr["NetWinlostMonth"]));

            //DbResult rs1 = Fanex.Affiliate.DataObjects.Home.GetGenInforFromDeposit();
            //DataTable dtb1 = rs1.ResultSet.Tables[0];
            //DataRow dr1 = dtb1.Rows[0];
            int nonDepositMemAllNum, NonDepositMemMonthNum;
            nonDepositMemAllNum = CastAide.AsInt32(dr["MemInProAllNum"], 0) - CastAide.AsInt32(dr["DepositMemAllNum"], 0);
            NonDepositMemMonthNum = CastAide.AsInt32(dr["NonDepositMemMonthNum"], 0);


            TemplateHandler.Assign(ref content, "MemInAffProAll-val", CastAide.AsInt32(dr["MemInProAllNum"], 0));
            TemplateHandler.Assign(ref content, "NonDeposit-val", nonDepositMemAllNum);
            TemplateHandler.Assign(ref content, "MemInAffProMonth-val", CastAide.AsInt32(dr["MemInProMonthNum"], 0));
            TemplateHandler.Assign(ref content, "NonDepositMonth-val", NonDepositMemMonthNum);

            int pagesize = CastAide.AsInt32(Request["pagesize"], 20);
            int page = CastAide.AsInt32(Request["page"], 1);
            int Nrows = 0, totalpages=0;
            int NrowOnCurPage = 0;
            DbResult olRs;
            DataTable oldtb = new DataTable() ;

            while (NrowOnCurPage == 0)
            {
                olRs = null;
                olRs = Fanex.Affiliate.DataObjects.Home.GetOnlineList(pagesize, page, AdminSession.IsIT);
                totalpages = CastAide.AsInt32(olRs.Outputs["totalpages"]);
                Nrows = CastAide.AsInt32(olRs.Outputs["NRows"]);
                oldtb = olRs.ResultSet.Tables[0];
                if(page == 1)//Break out if page is equal 1
                    NrowOnCurPage = -1;
                else
                    NrowOnCurPage = oldtb.Rows.Count;
                if (NrowOnCurPage == 0)
                {
                    page = totalpages;
                }
            }

            if (Request["pagesize"] == null)
            {
                TemplateHandler.Assign(ref content, "ToggleOnlineList", "-");
            }
            else
            {
                TemplateHandler.Assign(ref content, "ToggleOnlineList", "+");
            }

            string olRows = string.Empty;
            string olRow = TemplateHandler.CopyBlock(content, "OLRow");
            int No = 0;
            foreach (DataRow r in oldtb.Rows)
            {
                string temp = olRow;
                TemplateHandler.Assign(ref temp, "No-val", ++No + (page-1)*pagesize);
                TemplateHandler.Assign(ref temp, "logged-in-val", AdminCommon.IBCDateTimeFormat(r["LoggedIn"],DTFormatType.DT24h));
                TemplateHandler.Assign(ref temp, "lastrequest-val", AdminCommon.IBCDateTimeFormat(r["LastRequest"],DTFormatType.DT24h));
                TemplateHandler.Assign(ref temp, "IP-val", r["IP"].ToString());
                TemplateHandler.Assign(ref temp, "ipchecker-url", AdminCommon.GetIPCheckerURL(r["IP"].ToString()));
                if (r["RoleName"].ToString() == "Admin")
                {
                    TemplateHandler.Assign(ref temp, "Username-val", r["username"].ToString());
                    TemplateHandler.Assign(ref temp, "Type-val", Fanex.Affiliate.Resources.Translator.Get("Administrator"));

                }
                else
                {
                    TemplateHandler.Assign(ref temp, "Username-val", "<a href=\"../Customer/AffDefault.aspx?user=" + r["username"].ToString() + "\"> " + r["username"].ToString() + "</a>");
                    TemplateHandler.Assign(ref temp, "Type-val", Fanex.Affiliate.Resources.Translator.Get("Affiliate"));
                }
                if ((No % 2) == 0)
                    TemplateHandler.Assign(ref temp, "color", AdminCommon.RowBgEven);
                else
                    TemplateHandler.Assign(ref temp, "color", AdminCommon.RowBgOdd);
                TemplateHandler.Assign(ref temp, "over", AdminCommon.RowBgOver);
                olRows += temp;
            }
            TemplateHandler.ReplaceBlock(ref content, "OLRow", olRows);
            TemplateHandler.Assign(ref content, "paging-top", AdminCommon.GenPaging(totalpages, page, pagesize, Nrows, "aPageTop", "txtPageTop", 950, false, true));
            TemplateHandler.Assign(ref content, "paging-bottom", AdminCommon.GenPaging(totalpages, page, pagesize, Nrows, "aPageBottom", "txtPageBottom", 950, false, false));
            TemplateHandler.Assign(ref content, "pagesize", pagesize);
            TemplateHandler.Assign(ref content, "page", page);
            TemplateHandler.Assign(ref content, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "60" : Request["slTime"]);
            this.HtmlContent = content;
            }
        }
    }


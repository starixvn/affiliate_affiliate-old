namespace Fanex.Web.LiveBet365.Affiliate.UserInfo
{
    using System;
    using System.Data;
    using System.Text;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    

    public partial class Account_Overview : BasePageLogIn
    {
        protected string strTotalqualifedmembers = "N/A";
        protected string strProduct = "N/A";
        protected string slProduct = String.Empty;
        protected string strMonths = string.Empty;
        protected string strYears = string.Empty;
        protected string slMonth = string.Empty;
        protected string slYear = string.Empty;
        protected string strTierSetting = string.Empty;
        public const int MAXQM = 1000000000;
        public const Int64 MAXAMOUNT = 100000000000000;
        public string flashPath = string.Empty;
        public string showPopup = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {                        
            #region Load language
            ltrAccOverview.Text = Translator.Get("AccOverview");
            ltrNewSignup.Text = Translator.Get("NewSignup");
            ltrTotalPlay.Text = Translator.Get("TotalPlay");
            //ltrComm.Text = Translator.Get("CurrentCommissiSetting");
            //BalanceBroughtText.Text = Translator.Get("BalForward");
            ltrTotalActive.Text = Translator.Get("TotalActive");
            ltrTotalDeposit.Text = Translator.Get("TotalDeposit");
            ltrTurnover.Text = Translator.Get("Turnover");
            ltrDepositUp.Text = Translator.Get("TotalDeposit");
            GerMonthSel();
            #endregion

            CurrencyUtils.SetDefaultCurrency(AppSession.Currency);

            DateTime dtmFirstOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (!string.IsNullOrEmpty(Request["slMonth"]) && !string.IsNullOrEmpty(Request["slYear"]))
                dtmFirstOfMonth = new DateTime(Convert.ToInt32(Request["slYear"]), Convert.ToInt32(Request["slMonth"]), 1);
            strMonths = dtmFirstOfMonth.Month.ToString();
            strYears = dtmFirstOfMonth.Year.ToString();
            //ltrStatic.Text = Translator.Get("Statistic") + " " + Translator.Get("For") + " " + dtmFirstOfMonth.ToString("MMMM-yyyy");
            ltrStatic.Text = Translator.Get("Statistic") + " " + Translator.Get("For") + " " + AffCommon.ConvertDateTime(dtmFirstOfMonth, "-", false, false);
            ltrStaticUp.Text = Translator.Get("Statistic") + " " + Translator.Get("UpToNow");

            /*DbResult dep_result = UserAff.SelAffOverview_DEPOSIT(Fanex.Affiliate.Core.AppSession.CustID, dtmFirstOfMonth);
            ltrSignup.Text = CastAide.AsInt32(dep_result.Outputs["newsignup"], 0).ToString();
            ltrTotalPlayer.Text = CastAide.AsInt32(dep_result.Outputs["totalplayer"], 0).ToString();*/
            DbResult result = UserAff.SelAffOverview(Fanex.Affiliate.Core.AppSession.CustID, dtmFirstOfMonth);

            if (result.ResultSet.Tables.Count > 0)
            {

                var table = result.ResultSet.Tables[0];
                strProduct = table.Rows[0]["product"].ToString();
                strTotalqualifedmembers = table.Rows[0]["totalqualifiedmembers"].ToString();
                slProduct = HtmlHelper.OptionList(table, "product", "product", null, null, true);
            }  

            ltrSignup.Text = CastAide.AsInt32(result.Outputs["newsignup"], 0).ToString();
            ltrTotalPlayer.Text = CastAide.AsInt32(result.Outputs["totalplayer"], 0).ToString();
            ltrTotalActiveVal.Text = CastAide.AsInt32(result.Outputs["totalactiveplayer"], 0).ToString();
            ltrTotalDepositVal.Text = CurrencyUtils.Format(CastAide.AsDecimal(result.Outputs["totaldeposit"]));

            ltrDepositUpVal.Text = CurrencyUtils.Format(CastAide.AsDecimal(result.Outputs["totaldepositup"]));
            ltrTurnoverVal.Text = CurrencyUtils.Format(CastAide.AsDecimal(result.Outputs["totalturnoverup"]));

            //BalanceBroughtVal.Text = CastAide.AsDouble(result.Outputs["balbroughtforward"]) >= 0 ? CurrencyUtils.Format(result.Outputs["balbroughtforward"]) : "<span class='NegativeBalance'>" + CurrencyUtils.Format(result.Outputs["balbroughtforward"]) + "</span>";

            ltrTurnoverMonth.Text = CurrencyUtils.Format(result.Outputs["totalturnover"]);
            //With totalQM=1--1, it means this month did not have the theory of QM
            
            //set flash path
            string lang = AppSession.Language;
            switch (lang)
            {
                case "vi-VN":
                    flashPath = "../Flash/Vietnamese/vn.swf";
                    break;
                case "es-ES":
                    flashPath = "../Flash/Spanish/spanish.swf";
                    break;
                case "zh-CN":
                    flashPath = "../Flash/Simplified/sc.swf";
                    break;
                case "zh-TW":
                    flashPath = "../Flash/Tradition/tc.swf";
                    break;
                case "id-ID":
                    flashPath = "../Flash/Indonesian/indo.swf";
                    break;
                case "th-TH":
                    flashPath = "../Flash/Thai/thai.swf";
                    break;
                default:
                    flashPath = "../Flash/English/en.swf";
                    break;
            }
            if (AppSession.IsAuthenticated && AffSession.FirsTimeLoginSucc)
            {
                showPopup = "";
                AffSession.FirsTimeLoginSucc = false;
            }
            else
            {
                showPopup = "none";
            }

           // strTierSetting = GenCommGroupSettingDetails();
        }
        //private void GerMonthYearSelect()
        //{
        //    DateTime now = DateTime.Now;
        //    ArrayList arrMonth = new ArrayList();
        //    arrMonth.Add(now.ToString("MMMM"));
        //    ArrayList arrYear = new ArrayList();
        //    arrYear.Add(now.ToString("yyyy"));
        //    strMonths = Common.GenerateSelectObj("slMonth", arrMonth, arrMonth, "");
        //    slYear = AppCommon.GenYearDropDown("slYear", DateTime.Now.Year); // Common.GenerateSelectObj("slYear", arrYear, arrYear, ""); ;
        //}
        private void GerMonthSel()
        {
            slMonth = AppCommon.GenMonthDropDown("slMonth", 1);
            slYear = AppCommon.GenYearDropDown("slYear", DateTime.Now.Year);
         
        }
        private String GenCommGroupSettingDetails()
        {
            string tbl_CommSetting = TemplateHandler.LoadTemplate("UserInfo/CommGroupSetting.htm");
            DbResult result = null;


            //Assign text header of table
            TemplateHandler.Assign(ref tbl_CommSetting, "Product", Translator.Get("Product"));
            TemplateHandler.Assign(ref tbl_CommSetting, "Tier", Translator.Get("Tier"));
            TemplateHandler.Assign(ref tbl_CommSetting, "Commission", Translator.Get("Comm"));
            TemplateHandler.Assign(ref tbl_CommSetting, "QM", Translator.Get("QualifyingMem"));
            TemplateHandler.Assign(ref tbl_CommSetting, "ComNetWin", Translator.Get("NetWinLoss"));
            TemplateHandler.Assign(ref tbl_CommSetting, "Currency", CurrencyUtils.GetCurrencySymbol());
            TemplateHandler.Assign(ref tbl_CommSetting, "MinAmount_Explain", Translator.Get("MinAmt_explain"));
            TemplateHandler.Assign(ref tbl_CommSetting, "MinQM_Explain", Translator.Get("MinQM_explain"));
            

            result = CommGroup.GetCommGroupSettingByAffId(Fanex.Affiliate.Core.AppSession.CustID);

            if (result.ResultSet.Tables.Count < 5 || result.ResultSet.Tables[0].Rows.Count <= 0
                || result.ResultSet.Tables[1].Rows.Count <= 0
                //|| result.ResultSet.Tables[2].Rows.Count <= 0
                || result.ResultSet.Tables[3].Rows.Count <= 0
                || result.ResultSet.Tables[4].Rows.Count <= 0)
            {
                return string.Empty;
            }

            var Product = result.ResultSet.Tables[3];
            var DeductionType = result.ResultSet.Tables[4];
            var commGroup = result.ResultSet.Tables[0];
            var GroupSetting = result.ResultSet.Tables[1];
            var DeductionSetting = result.ResultSet.Tables[2];

            var mainRow = TemplateHandler.CopyBlock(tbl_CommSetting, "Mainrow");
            //string deductionOption = TemplateHandler.CopyBlock(tbl_CommSetting, "DeductionOption");
            var tierRow = TemplateHandler.CopyBlock(tbl_CommSetting, "subrow");
            var ret = new StringBuilder();
            int noProducts = 0;

            foreach (DataRow product in Product.Rows)
            {
                
                if (GroupSetting.Select("ProductID=" + product["ProductID"]).Length >= 1)
                {
                    string _productSetting = mainRow;
                    TemplateHandler.Assign(ref _productSetting, "products-type", product["ProductName"]);
                    //TemplateHandler.Assign(ref _productSetting, "turnover-val", String.Format("{0:0.00}", product["QMTurnOver"]));

                    noProducts++;
                    TemplateHandler.Assign(ref _productSetting, "class-main", (noProducts % 2 == 0) ? "tbl_cs" : "tbl_spb");
                    TemplateHandler.Assign(ref _productSetting, "class", (noProducts % 2 == 0) ? "tbl_01" : "tbl_td_01");

                    DataRow[] drDeductionType = DeductionType.Select("ProductID=" + product["ProductID"]);

                    var deductionList = new StringBuilder();
                    var tierList = new StringBuilder();

                    //Load Tier Setting
                    var Tiers = GroupSetting.Select("ProductID=" + product["ProductID"]);

                    //TemplateHandler.Assign(ref _productSetting, "tierno-val", Tiers.Length);

                    int noTier = 0;
                    int totalShownTier = Tiers.Length;
                    foreach (DataRow tier in Tiers)
                    {
                        if (CastAide.AsDouble(tier["Commission"]) != 0 || Tiers.Length == 1)
                        {
                            if (noTier == 0)
                            {
                                TemplateHandler.Assign(ref _productSetting, "tier-no1", ++noTier);
                                //Only allow to delete last tier

                                TemplateHandler.Assign(ref _productSetting, "minam-val1", CurrencyUtils.Format(tier["MinAmount"]));//( String.Format("{0:0.00}", Convert.ToInt64(tier["MinAmount"])));
                                TemplateHandler.Assign(ref _productSetting, "minqm-val1", Convert.ToInt32(tier["MinQM"]));
                                if (noTier <= totalShownTier - 1)
                                {
                                    TemplateHandler.Assign(ref _productSetting, "maxqm-val1", " - " + GetMaxQMDisplay(tier["MaxQM"]));
                                    TemplateHandler.Assign(ref _productSetting, "maxam-val1", " - " + CurrencyUtils.Format(Convert.ToInt64((tier["MaxAmount"])) + 0.99));
                                }
                                else
                                {
                                    TemplateHandler.Assign(ref _productSetting, "maxqm-val1", GetMaxQMDisplay(tier["MaxQM"]));
                                    TemplateHandler.Assign(ref _productSetting, "maxam-val1", GetMaxAmountDisplay(tier["MaxAmount"]));
                                }
                                TemplateHandler.Assign(ref _productSetting, "Commission-val1", (Convert.ToDouble(tier["Commission"]) * 100).ToString() + "%");
                            }
                            else
                            {
                                string _tierRow = tierRow;
                                TemplateHandler.Assign(ref _tierRow, "tier-no", ++noTier);
                                TemplateHandler.Assign(ref _tierRow, "class", (noProducts % 2 == 0) ? "tbl_01" : "tbl_td_01");

                                //Only allow to delete last tier

                                //TemplateHandler.Assign(ref _tierRow, "minam-val", AffCommon.FormatNumber(Convert.ToInt64(tier["MinAmount"])));//String.Format("{0:0.00}", Convert.ToInt64(tier["MinAmount"])));
                                TemplateHandler.Assign(ref _tierRow, "minam-val", CurrencyUtils.Format(tier["MinAmount"]));//String.Format("{0:0.00}", Convert.ToInt64(tier["MinAmount"])));
                                TemplateHandler.Assign(ref _tierRow, "minqm-val", Convert.ToInt32(tier["MinQM"]));
                                if (noTier <= totalShownTier - 1)
                                {
                                    TemplateHandler.Assign(ref _tierRow, "maxam-val", " - " + CurrencyUtils.Format(Convert.ToInt64(tier["MaxAmount"]) + 0.99));
                                    TemplateHandler.Assign(ref _tierRow, "maxqm-val", " - " + GetMaxQMDisplay(tier["MaxQM"]));
                                }
                                else
                                {
                                    TemplateHandler.Assign(ref _tierRow, "maxam-val", GetMaxAmountDisplay(tier["MaxAmount"]));
                                    TemplateHandler.Assign(ref _tierRow, "maxqm-val", GetMaxQMDisplay(tier["MaxQM"]));
                                }
                                TemplateHandler.Assign(ref _tierRow, "Commission-val", (Convert.ToDouble(tier["Commission"]) * 100).ToString() + "%");
                                tierList.Append(_tierRow);
                            }
                        }
                        else
                        {
                            totalShownTier--;
                        }
                    }
                    TemplateHandler.Assign(ref _productSetting, "tierno-val", noTier == 0 ? "" : noTier.ToString());
                    TemplateHandler.ReplaceBlock(ref _productSetting, "subrow", tierList.ToString());
                    /*
                    if (deductionList.Length == 0)
                    {
                        TemplateHandler.ReplaceBlock(ref _productSetting, "DeductionOption", string.Empty);
                    }
                    else
                    {

                        TemplateHandler.ReplaceBlock(ref _productSetting, "DeductionOption", deductionList.ToString());
                    }
                     */
                    if (totalShownTier == 0)
                    {
                        TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", AffCommon.NoInfo2(7));
                    }
                    //else
                    //{
                    //    TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", tierList.ToString());
                    //}
                    ret.Append(_productSetting);
                }
                //}
            }
            TemplateHandler.ReplaceBlock(ref tbl_CommSetting, "Mainrow", ret.ToString());
            return tbl_CommSetting;
        }
        public static string GetMaxQMDisplay(object max)
        {
            if (Convert.ToInt32(max) == MAXQM)
            {
                return "+";
            }
            return Convert.ToInt32(max).ToString();
        }
        public static string GetMaxAmountDisplay(object max)
        {
            if (Convert.ToInt64(max) == MAXAMOUNT)
            {
                return "+";
            }
            return String.Format("{0:0.00}", Convert.ToDouble(max) + 0.99);
        }
    }
}

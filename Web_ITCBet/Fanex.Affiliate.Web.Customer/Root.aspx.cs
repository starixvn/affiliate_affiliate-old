namespace Fanex.Web.ITCBet.Customer
{
    using System;
    using System.Web;
    using Fanex.Affiliate.DataObjects;

    using DNAide.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Text;
    using System.Data;
    using Fanex.Affiliate.Core.Customer;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Resources;


    public partial class Root : BasePage
    {
        protected string strContent = string.Empty;
        private const int MAXQM = 1000000000;
        private const Int64 MAXAMOUNT = 100000000000000;

        protected void Page_Load(object sender, EventArgs e)
        {
            strContent = Fanex.Affiliate.Resources.Translator.Get("ProgramDetails", "AffiliateContent");
            strContent = string.Format(strContent, GenCommGroupSettingDetails());
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

            result = CommGroup.GetCommGroupSettingByGroupId(101);

            if (result.ResultSet.Tables.Count < 0)    
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

        private static string GetMaxQMDisplay(object max)
        {
            if (Convert.ToInt32(max) == MAXQM)
            {
                return "+";
            }
            return Convert.ToInt32(max).ToString();
        }

        private static string GetMaxAmountDisplay(object max)
        {
            if (Convert.ToInt64(max) == MAXAMOUNT)
            {
                return "+";
            }
            return String.Format("{0:0.00}", Convert.ToDouble(max) + 0.99);
        }
    }
}

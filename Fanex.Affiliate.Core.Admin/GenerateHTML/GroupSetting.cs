// -----------------------------------------------------------------------
// <copyright file="GroupSetting.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.Script.Serialization;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Affiliate.Core.Admin.UI;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Model;
    using Fanex.Core.Utility;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GroupSetting
    {
        public const int MAXQM = 1000000000;
        public const Int64 MAXAMOUNT = 100000000000000;
        public static string GetMaxQMDisplay(object max)
        {
            if (Convert.ToInt32(max) == MAXQM)
            {
                return "∞";
            }
            return Common.FormatNumber1(Convert.ToInt32(max).ToString(), false);
        }
        public static string GetMaxAmountDisplay(object max)
        {
            if (Convert.ToInt64(max) == MAXAMOUNT)
            {
                return "∞";
            }
            return Common.FormatNumber1(Convert.ToInt64(max).ToString(), true);
        }
        public static string GetCommissionOptionList(decimal selectedCommission, string htmlTemplate)
        {
            StringBuilder list = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                string option = htmlTemplate;
                decimal val = Convert.ToDecimal(i) / 100;
                TemplateHandler.Assign(ref option, "option_value", DNAide.Utils.NumberFormat.DecFormat(val, 2));
                if (val == selectedCommission)
                {
                    TemplateHandler.Assign(ref option, "selected_val", "selected");
                }
                else
                {
                    TemplateHandler.Assign(ref option, "selected_val", string.Empty);
                }
                list.Append(option);
            }
            return list.ToString();
        }
        public static string GetGroupSettingList(string groupName, object fromdate, object todate, int curpage, ref int minNo, ref int maxNo, ref int totalRows, int pagesize, ref int totalpage)
        {
            DbResult result = null;
            int nRows = 0;
            int totalOfNoneGroup = 0;
            result = DataObjects.CommGroup.CommGroupSel(groupName, fromdate, todate, pagesize, curpage);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            totalOfNoneGroup = CastAide.AsInt32(result.Outputs["totalnonegroup"]);
            totalRows = nRows;
            int cols = 18;
            minNo = 0;
            maxNo = 0;
            if (totalOfNoneGroup > 0 || nRows > 0)
            {
                string groupSettingContent = TemplateHandler.LoadTemplate("GroupSetting/GroupSetting.htm");
                string masterRow = TemplateHandler.CopyBlock(groupSettingContent, "MasterRow");
                string policyDetailRow = TemplateHandler.CopyBlock(groupSettingContent, "PolicyDetailRow");
                string nextProductMasterRow = TemplateHandler.CopyBlock(groupSettingContent, "NextProductMasterRow");
                string deductionOption = TemplateHandler.CopyBlock(masterRow, "DeductionOption");

                var strBuild = new StringBuilder();
                if (totalOfNoneGroup > 0)
                {
                    string noneGroupRow = TemplateHandler.CopyBlock(groupSettingContent, "NoneGroupRow");
                    TemplateHandler.Assign(ref noneGroupRow, "No_val", 0);
                    TemplateHandler.Assign(ref noneGroupRow, "GroupName_val", "[None]");
                    TemplateHandler.Assign(ref noneGroupRow, "Description_val", "All private groups for affiliates");
                    TemplateHandler.Assign(ref noneGroupRow, "CreatedDate_val", string.Empty);
                    TemplateHandler.Assign(ref noneGroupRow, "Creator_val", string.Empty);
                    TemplateHandler.Assign(ref noneGroupRow, "NoOfAffiliate_val", totalOfNoneGroup);

                    TemplateHandler.Assign(ref noneGroupRow, "GroupID_val", 0);
                    TemplateHandler.Assign(ref noneGroupRow, "color", AdminCommon.RowBgEven);
                    TemplateHandler.Assign(ref noneGroupRow, "currency-val", String.Empty);
                    TemplateHandler.Assign(ref noneGroupRow, "qualifiedamount-val", String.Empty);
                    strBuild.Append(noneGroupRow);
                }
                if (nRows <= 0)
                    return strBuild.ToString();


                DataTable tblDeductionType = result.ResultSet.Tables[0];
                DataTable tblDeductionSetting = result.ResultSet.Tables[1];
                DataTable tblGroup = result.ResultSet.Tables[2];
                DataTable tblGroupDetail = result.ResultSet.Tables[3];
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                int intNo = 0;
                minNo = (curpage - 1) * pagesize + 1;
                int totalProduct = 0;
                foreach (DataRow dr in tblGroup.Rows)
                {
                    string strTmp = masterRow;
                    intNo++;
                    totalProduct++;
                    int no = (curpage - 1) * pagesize + intNo;
                    TemplateHandler.Assign(ref strTmp, "No_val", no);
                    TemplateHandler.Assign(ref strTmp, "GroupName_val", Common.CutLongText(dr["GroupName"].ToString(), 20));
                    TemplateHandler.Assign(ref strTmp, "Description_val", Common.CutLongText(dr["Description"].ToString(), 50));
                    TemplateHandler.Assign(ref strTmp, "CreatedDate_val", AdminCommon.IBCDateTimeFormat(dr["createddate"], DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "Creator_val", dr["Creator"].ToString());


                    TemplateHandler.Assign(ref strTmp, "currency-val", dr["currency"].ToString());
                    TemplateHandler.Assign(ref strTmp, "qualifiedamount-val", ((decimal)dr["qualifiedamount"]).ToString("N0"));

                    if (Convert.ToInt32(dr["NoOfAffiliate"]) > 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "NoOfAffiliate_val", dr["NoOfAffiliate"].ToString());
                    }
                    else
                    {
                        TemplateHandler.ReplaceBlock(ref strTmp, "link", dr["NoOfAffiliate"].ToString());
                    }


                    TemplateHandler.Assign(ref strTmp, "App_Themes", AppSettings.ThemeDir);
                    if (string.Compare(dr["GroupName"].ToString(), "NoCommission", true) == 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "GroupID_val", dr["GroupID"].ToString() + "_NoComm");
                        TemplateHandler.RemoveBlock(ref strTmp, "nocomm");
                    }
                    else
                        TemplateHandler.Assign(ref strTmp, "GroupID_val", dr["GroupID"]);
                    if (Convert.ToInt32(dr["NoOfAffiliate"]) > 0 || string.Compare(dr["GroupName"].ToString(), "Default", true) == 0 || string.Compare(dr["GroupName"].ToString(), "NoCommission", true) == 0/*|| !Convert.ToBoolean(dr["IsAllowedToDelete"])*/)
                    {
                        TemplateHandler.Assign(ref strTmp, "id_val", string.Empty);
                        TemplateHandler.Assign(ref strTmp, "btnDeleteClass_val", "icdelete1");

                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "id_val", "id='" + dr["GroupID"].ToString() + "_" + no.ToString() + "'");
                        TemplateHandler.Assign(ref strTmp, "btnDeleteClass_val", "icdelete");
                    }

                    if ((totalProduct % 2) == 0)
                        TemplateHandler.Assign(ref strTmp, "ProductsClass_val", "ProductEven");
                    else
                        TemplateHandler.Assign(ref strTmp, "ProductsClass_val", "ProductOdd");


                    if ((intNo % 2) == 0)
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgEven);
                    else
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgOdd);

                    DataRow[] rows = tblGroupDetail.Select("GroupID=" + dr["GroupID"].ToString());
                    TemplateHandler.Assign(ref strTmp, "TotalRowOfGroup", rows.Length);
                    int lastProduct = -1;
                    int firstProduct = -1;
                    int totalRowOfFirstProduct = 0;
                    StringBuilder subRows = new StringBuilder();
                    foreach (DataRow row in rows)
                    {
                        if (lastProduct == -1) //First Product Type
                        {
                            lastProduct = Convert.ToInt32(row["ProductID"]);
                            firstProduct = lastProduct;
                            totalRowOfFirstProduct++;
                            TemplateHandler.Assign(ref strTmp, "ProductName_val", row["ProductName"]);
                            TemplateHandler.Assign(ref strTmp, "Tier_val", row["TierID"]);
                            TemplateHandler.Assign(ref strTmp, "MinAmt_val", Common.FormatNumber1(Convert.ToInt64(row["MinAmount"]), true));
                            TemplateHandler.Assign(ref strTmp, "MaxAmt_val", GetMaxAmountDisplay(row["MaxAmount"]));
                            TemplateHandler.Assign(ref strTmp, "MinQM_val", Common.FormatNumber1(Convert.ToInt32(row["MinQM"]), false));
                            TemplateHandler.Assign(ref strTmp, "MaxQM_val", GetMaxQMDisplay(row["MaxQM"]));
                            TemplateHandler.Assign(ref strTmp, "Commission_val", DNAide.Utils.NumberFormat.DecFormat(Convert.ToDecimal(row["Commission"]), 2));

                            //Load Deduction Settings
                            DataRow[] deductions = tblDeductionType.Select("ProductID=" + row["ProductID"]);
                            StringBuilder deductionList = new StringBuilder();
                            foreach (DataRow deduction in deductions)
                            {
                                string _deductionOption = deductionOption;
                                TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", deduction["dtypedesc"]);
                                TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", deduction["dtypename"]);

                                DataView dv = tblDeductionSetting.DefaultView;
                                dv.RowFilter = "GroupID=" + dr["GroupID"];


                                if (dv.ToTable().Select("DeductionID=" + deduction["dtypeid"]).Length > 0)
                                {
                                    TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OnDisable);
                                }
                                else
                                {
                                    TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffDisable);
                                }

                                deductionList.Append(_deductionOption);
                            }
                            TemplateHandler.ReplaceBlock(ref strTmp, "DeductionOption", deductionList.ToString());


                        }
                        else
                        {
                            if (firstProduct == Convert.ToInt32(row["ProductID"]))
                                totalRowOfFirstProduct++;
                            if (lastProduct == Convert.ToInt32(row["ProductID"])) //raise a sub policyrow
                            {
                                string p = policyDetailRow;
                                TemplateHandler.Assign(ref p, "Tier_val", row["TierID"]);
                                TemplateHandler.Assign(ref p, "MinAmt_val", Common.FormatNumber1(Convert.ToInt64(row["MinAmount"]), true));
                                TemplateHandler.Assign(ref p, "MaxAmt_val", GetMaxAmountDisplay(row["MaxAmount"]));
                                TemplateHandler.Assign(ref p, "MinQM_val", Common.FormatNumber1(Convert.ToInt32(row["MinQM"]), false));
                                TemplateHandler.Assign(ref p, "MaxQM_val", GetMaxQMDisplay(row["MaxQM"]));
                                TemplateHandler.Assign(ref p, "Commission_val", DNAide.Utils.NumberFormat.DecFormat(Convert.ToDecimal(row["Commission"]), 2));
                                if ((totalProduct % 2) == 0)
                                    TemplateHandler.Assign(ref p, "ProductsClass_val", "ProductEven");
                                else
                                    TemplateHandler.Assign(ref p, "ProductsClass_val", "ProductOdd");
                                subRows.Append(p);
                            }
                            else //Next Product Type Rows
                            {
                                totalProduct++;
                                lastProduct = Convert.ToInt32(row["ProductID"]);
                                string nextProduct = nextProductMasterRow;
                                TemplateHandler.Assign(ref nextProduct, "ProductName_val", row["ProductName"]);
                                TemplateHandler.Assign(ref nextProduct, "Tier_val", row["TierID"]);
                                TemplateHandler.Assign(ref nextProduct, "MinAmt_val", Convert.ToInt64(row["MinAmount"]));
                                TemplateHandler.Assign(ref nextProduct, "MaxAmt_val", GetMaxAmountDisplay(row["MaxAmount"]));
                                TemplateHandler.Assign(ref nextProduct, "MinQM_val", Convert.ToInt32(row["MinQM"]));
                                TemplateHandler.Assign(ref nextProduct, "MaxQM_val", GetMaxQMDisplay(row["MaxQM"]));
                                TemplateHandler.Assign(ref nextProduct, "Commission_val", DNAide.Utils.NumberFormat.DecFormat(Convert.ToDecimal(row["Commission"]), 2));

                                DataRow[] deductions = tblDeductionType.Select("ProductID=" + row["ProductID"]);
                                StringBuilder deductionList = new StringBuilder();
                                foreach (DataRow deduction in deductions)
                                {
                                    string _deductionOption = deductionOption;
                                    TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", deduction["dtypedesc"]);
                                    TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", deduction["dtypename"]);

                                    DataView dv = tblDeductionSetting.DefaultView;
                                    dv.RowFilter = "GroupID=" + dr["GroupID"];


                                    if (dv.ToTable().Select("DeductionID=" + deduction["dtypeid"]).Length > 0)
                                    {
                                        TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OnDisable);
                                    }
                                    else
                                    {
                                        TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffDisable);
                                    }
                                    deductionList.Append(_deductionOption);
                                }
                                TemplateHandler.ReplaceBlock(ref nextProduct, "DeductionOption", deductionList.ToString());

                                if ((totalProduct % 2) == 0)
                                    TemplateHandler.Assign(ref nextProduct, "ProductsClass_val", "ProductEven");
                                else
                                    TemplateHandler.Assign(ref nextProduct, "ProductsClass_val", "ProductOdd");
                                DataRow[] temp = tblGroupDetail.Select("GroupID=" + dr["GroupID"].ToString() + " and ProductID=" + row["ProductID"]);
                                TemplateHandler.Assign(ref nextProduct, "TotalRowOfProduct", temp.Length);
                                subRows.Append(nextProduct);
                            }


                        }
                    }
                    TemplateHandler.Assign(ref strTmp, "TotalRowOfFirstProduct", totalRowOfFirstProduct);
                    strBuild.Append(strTmp);
                    strBuild.Append(subRows.ToString());

                }
                maxNo = (curpage - 1) * pagesize + intNo;
                return strBuild.ToString();
            }
            return AdminCommon.NoInfo(cols);

        }
        public static string GetGroupSettingForEdit(int groupID)
        {
            //DbResult commonSetting = null;          
            //commonSetting = DataObjects.GroupSetting.CommGroupSelCommonSetting();
            DbResult setting = null;
            setting = DataObjects.CommGroup.CommGroupSelDetail(groupID);
            if (setting.ResultSet.Tables.Count < 5 || setting.ResultSet.Tables[0].Rows.Count <= 0)
            {
                return Fanex.Affiliate.Resources.Admin.NoInfo;
            }
            var settingTemplate = TemplateHandler.LoadTemplate("GroupSetting/GroupSettingEdit.htm");
            if (groupID < 1000)
            {
                TemplateHandler.RemoveBlock(ref settingTemplate, "PrivateGroup");
                TemplateHandler.Assign(ref settingTemplate, "AffName_val", string.Empty);
            }
            else
            {
                TemplateHandler.RemoveBlock(ref settingTemplate, "NormalGroup");
            }



            var js = new JavaScriptSerializer();


            TemplateHandler.Assign(ref settingTemplate, "ProductStatusColor_val", js.Serialize(ProductStatusColor.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconProductStatus_val", js.Serialize(IconProductStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "ProductTabSwitchIcon_val", js.Serialize(ProductTabSwitchIcon.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "ProductTabSwitch_val", js.Serialize(ProductTabSwitch.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "CheckboxStatus_val", js.Serialize(CheckboxStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconEdit_val", js.Serialize(IconEdit.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconDelete_val", js.Serialize(IconDelete.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconAddTier_val", js.Serialize(IconAddTier.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconSave_val", js.Serialize(IconSave.GetInstance()));

            TemplateHandler.Assign(ref settingTemplate, "RequiedExplain_val", Resources.Translator.Get("RequiedExplain"));
            TemplateHandler.Assign(ref settingTemplate, "EnableProductExplainTpl_val", Resources.Translator.Get("EnableProductExplain"));
            TemplateHandler.Assign(ref settingTemplate, "DisableProductExplainTpl_val", Resources.Translator.Get("DisableProductExplain"));
            TemplateHandler.Assign(ref settingTemplate, "noInfo_val", AdminCommon.NoInfo(7));


            TemplateHandler.Assign(ref settingTemplate, "maxlengthGroupName_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthDescription_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthAmount_val", 19);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthQM_val", 13);
            TemplateHandler.Assign(ref settingTemplate, "maximumQMNumber_val", MAXQM);
            TemplateHandler.Assign(ref settingTemplate, "maximumAmountNumber_val", MAXAMOUNT);

            AppCommon.AssignToAdminTemplate(ref settingTemplate, "GroupName", "Description", "ActiveAll", "Add",
                "QualifiedAmount", "Currency", "Action", "Save","GroupNameEmpty","ValidQualifiedAmount",
                "EnableProduct","ConfirmUpdateGroup","InvalidSettingFor","MaxTierAllow","LastTierSettingHaveMaxValue",
                "TierSettingAtLeastTier","ConfirmRemoveFromGroup","InputCurrencyValue");

            var productSetting = TemplateHandler.CopyBlock(settingTemplate, "ProductSetting");

            AppCommon.AssignToTemplate(ref settingTemplate, "GroupName", "Description", "ActiveAll");
            TemplateHandler.Assign(ref settingTemplate,"Submit",Resources.Admin.Save);

            AppCommon.AssignToAdminTemplate(ref productSetting,
                "Deductions", "TierSetting", "Tier", "MinAmount", "MaxAmount", "MinQM",
                "MaxQM", "Comm", "Action", "AddTier", "Edit", "Delete", "MinQMExplain",
                "MaxQMExplain", "MinAmountExplain", "MaxAmountExplain");

            

            string deductionOption = TemplateHandler.CopyBlock(productSetting, "DeductionOption");
            string tierRow = TemplateHandler.CopyBlock(productSetting, "TierRow");
            string slOption = TemplateHandler.CopyBlock(tierRow, "SelectOption");

            string tierRowTpl = tierRow;
            TemplateHandler.ReplaceBlock(ref tierRowTpl, "SelectOption", GetCommissionOptionList(0, slOption));
            TemplateHandler.Assign(ref settingTemplate, "TierRowTpl_val", js.Serialize(tierRowTpl));

            DataTable Product = setting.ResultSet.Tables[3];
            DataTable DeductionType = setting.ResultSet.Tables[4];
            DataTable CommGroup = setting.ResultSet.Tables[0];
            DataTable GroupSetting = setting.ResultSet.Tables[1];
            DataTable DeductionSetting = setting.ResultSet.Tables[2];

            TemplateHandler.Assign(ref settingTemplate, "GroupName_val", CommGroup.Rows[0]["GroupName"]);
            TemplateHandler.Assign(ref settingTemplate, "Description_val", CommGroup.Rows[0]["Description"]);
            if (string.Compare(CommGroup.Rows[0]["GroupName"].ToString(), "Default", true) == 0)
            {
                TemplateHandler.Assign(ref settingTemplate, "txtGroupName_disabled_val", "disabled");
            }
            else
            {
                TemplateHandler.Assign(ref settingTemplate, "txtGroupName_disabled_val", string.Empty);
            }

            /*smith*/
            TemplateHandler.Assign(ref settingTemplate, "qualifiedAmount-val", ((decimal)CommGroup.Rows[0]["QualifiedAmount"]).ToString("N0"));

            GroupSettingObj obj = GroupSettingObj.GetInstance();
            obj.id = groupID;
            obj.description = CommGroup.Rows[0]["Description"].ToString();
            obj.groupName = CommGroup.Rows[0]["GroupName"].ToString();

            /*Johhny fix dropdownlist*/
            //var currencies = AdminCommon.GetListSupportedCurrency();
            //TemplateHandler.Assign(ref settingTemplate, "currencies", 
            // Common.GenDropdownList(currencies, currencies, "currencies", !CastAide.AsBoolean(CommGroup.Rows[0]["IsCurrencyEditable"]), string.Empty, CommGroup.Rows[0]["Currency"].ToString()));

            var currencies = AppSettings.AffCurrencies; // AdminCommon.GetListSupportedCurrency();
            var a = !CastAide.AsBoolean(CommGroup.Rows[0]["IsCurrencyEditable"]);
            var selCurrency = CommGroup.Rows[0]["ExchangeID"].ToString();
            TemplateHandler.Assign(ref settingTemplate, "currencies",
                HtmlHelper.DropDownList(currencies, "currencies", selCurrency, false, "w100",true));// !CastAide.AsBoolean(CommGroup.Rows[0]["IsCurrencyEditable"])));

            
            //var currencyObj = AppSettings.AffSystemSupportedCurrencies.Where(x => x.Id == selCurrency).FirstOrDefault() as CurrencyObj;
            //if (currencyObj != null && currencyObj.MultipliedBy > 1)
            //{                            
            //    TemplateHandler.Assign(ref settingTemplate, "CurrencyNotifications", 
            //        AdminCommon.CreateCurrencyNotificationLine(currencyObj.Value,currencyObj.MultipliedBy));
            //}
            //else
            //{
            //    TemplateHandler.Assign(ref settingTemplate, "CurrencyNotifications","");
            //}
            
            StringBuilder result = new StringBuilder();
            IList<int> ProductIDArr = new List<int>();
            bool isFirstProductActive = true;
            int activeProducts = 0, noProducts = 0;
            foreach (DataRow product in Product.Rows)
            {
                ProductSettingObj productSettingObj = ProductSettingObj.GetInstance();
                productSettingObj.id = Convert.ToInt32(product["ProductID"]);
                productSettingObj.productName = product["ProductName"].ToString();

                ProductIDArr.Add(Convert.ToInt32(product["ProductID"]));
                string _productSetting = productSetting;
                TemplateHandler.Assign(ref _productSetting, "ProductName", product["ProductName"]);
                string copyText = Resources.Admin.CopyTierSettingFor + " " + product["ProductName"];
                //TemplateHandler.Assign(ref copyText, "ProductName_val", product["ProductName"]);
                TemplateHandler.Assign(ref _productSetting, "CopyAllText", copyText);
                TemplateHandler.Assign(ref _productSetting, "ProductID_val", product["ProductID"]);

                noProducts++;

                DataRow[] drDeductionType = DeductionType.Select("ProductID=" + product["ProductID"]);

                StringBuilder deductionList = new StringBuilder();
                StringBuilder tierList = new StringBuilder();
                //Product is enabled
                if (GroupSetting.Select("ProductID=" + product["ProductID"]).Length >= 1)
                {
                    productSettingObj.isEnabled = true;
                    activeProducts++;
                    TemplateHandler.Assign(ref _productSetting, "product_color_val", ProductStatusColor.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _productSetting, "icon_enable_val", IconProductStatus.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _productSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffEnable);
                    string iconExplain = Resources.Translator.Get("DisableProductExplain");
                    TemplateHandler.Assign(ref iconExplain, "ProductName_val", product["ProductName"]);
                    TemplateHandler.Assign(ref _productSetting, "icon_enable_explain_val", iconExplain);


                    if (isFirstProductActive)
                    {
                        isFirstProductActive = false;
                        TemplateHandler.Assign(ref _productSetting, "collapse_expand_val", ProductTabSwitchIcon.GetInstance().Expand);
                        TemplateHandler.Assign(ref _productSetting, "tab_content_display_val", ProductTabSwitch.GetInstance().Expand);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref _productSetting, "collapse_expand_val", ProductTabSwitchIcon.GetInstance().Collapse);
                        TemplateHandler.Assign(ref _productSetting, "tab_content_display_val", ProductTabSwitch.GetInstance().Collapse);
                    }
                    //Load Deduction Settings
                    foreach (DataRow dtype in drDeductionType)
                    {
                        DeductionTypeObj deductionObj = DeductionTypeObj.GetInstance();
                        deductionObj.id = Convert.ToInt32(dtype["dtypeid"]);
                        deductionObj.name = dtype["dtypename"].ToString();

                        string _deductionOption = deductionOption;
                        TemplateHandler.Assign(ref _deductionOption, "ProductID_val", product["ProductID"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionID_val", dtype["dtypeid"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", dtype["dtypedesc"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", dtype["dtypename"]);

                        if (DeductionSetting.Select("DeductionID=" + dtype["dtypeid"]).Length > 0)
                        {
                            TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OnEnable);
                            deductionObj.isCheck = true;
                            deductionObj.deductedPercentage = CastAide.AsDouble(DeductionSetting.Select("DeductionID=" + dtype["dtypeid"])[0]["deductedpercentage"]);
                        }
                        else
                        {
                            TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffEnable);
                            deductionObj.isCheck = false;
                            deductionObj.deductedPercentage = CastAide.AsDouble(dtype["deductedpercentage"]);
                        }
                        deductionList.Append(_deductionOption);
                        productSettingObj.deductions.Add(deductionObj);
                    }
                    //Load Tier Setting
                    DataRow[] Tiers = GroupSetting.Select("ProductID=" + product["ProductID"]);

                    bool isFirstTier = true;
                    foreach (DataRow tier in Tiers)
                    {
                        string _tierRow = tierRow;
                        productSettingObj.minAmounts.Add(Convert.ToInt64(tier["MinAmount"]));
                        productSettingObj.maxAmounts.Add(Convert.ToInt64(tier["MaxAmount"]));
                        productSettingObj.minQMs.Add(Convert.ToInt32(tier["MinQM"]));
                        productSettingObj.maxQMs.Add(Convert.ToInt32(tier["MaxQM"]));
                        productSettingObj.commissions.Add(DNAide.Utils.NumberFormat.DecFormat(tier["Commission"], 2));

                        TemplateHandler.Assign(ref _tierRow, "ProductID_val", product["ProductID"]);
                        //Only allow to delete last tier

                        TemplateHandler.Assign(ref _tierRow, "tierID_val", tier["tierid"]);
                        TemplateHandler.Assign(ref _tierRow, "txtMinAmount_val", Common.FormatNumber1(Convert.ToInt64(tier["MinAmount"]), true));
                        TemplateHandler.Assign(ref _tierRow, "txtMaxAmount_val", GetMaxAmountDisplay(tier["MaxAmount"]));
                        TemplateHandler.Assign(ref _tierRow, "txtMinQM_val", Common.FormatNumber1(Convert.ToInt32(tier["MinQM"]), false));
                        TemplateHandler.Assign(ref _tierRow, "txtMaxQM_val", GetMaxQMDisplay(tier["MaxQM"]));
                        TemplateHandler.ReplaceBlock(ref _tierRow, "SelectOption", GetCommissionOptionList(Convert.ToDecimal(tier["Commission"]), slOption));
                        if (GetMaxAmountDisplay(tier["MaxAmount"]) == "∞")
                        {
                            TemplateHandler.Assign(ref _tierRow, "chkMaxClass_val", CheckboxStatus.GetInstance().OnEnable);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxQM_val", "disabled");
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxAmount_val", "disabled");
                            TemplateHandler.Assign(ref _tierRow, "icon_delete_status_val", IconDelete.GetInstance().Enabled);
                        }
                        else
                        {
                            TemplateHandler.Assign(ref _tierRow, "chkMaxClass_val", CheckboxStatus.GetInstance().OffDisable);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxQM_val", string.Empty);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxAmount_val", string.Empty);
                            TemplateHandler.Assign(ref _tierRow, "icon_delete_status_val", IconDelete.GetInstance().Disabled);
                        }

                        if (isFirstTier)
                        {
                            isFirstTier = false;
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinQM_val", string.Empty);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinAmount_val", string.Empty);
                        }
                        else
                        {
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinQM_val", "disabled");
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinAmount_val", "disabled");
                        }
                        tierList.Append(_tierRow);
                    }

                }
                else //Product is disabled
                {
                    productSettingObj.isEnabled = false;
                    TemplateHandler.Assign(ref _productSetting, "product_color_val", ProductStatusColor.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _productSetting, "icon_enable_val", IconProductStatus.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _productSetting, "collapse_expand_val", ProductTabSwitchIcon.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _productSetting, "tab_content_display_val", ProductTabSwitch.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _productSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffDisable);

                    string iconExplain = Resources.Translator.Get("EnableProductExplain");
                    TemplateHandler.Assign(ref iconExplain, "ProductName_val", product["ProductName"]);
                    TemplateHandler.Assign(ref _productSetting, "icon_enable_explain_val", iconExplain);

                    //Load Deduction Settings
                    foreach (DataRow dtype in drDeductionType)
                    {
                        DeductionTypeObj deductionObj = DeductionTypeObj.GetInstance();
                        deductionObj.id = Convert.ToInt32(dtype["dtypeid"]);
                        deductionObj.name = dtype["dtypename"].ToString();
                        deductionObj.isCheck = false;
                        deductionObj.deductedPercentage = CastAide.AsDouble(dtype["deductedpercentage"]);
                        productSettingObj.deductions.Add(deductionObj);


                        string _deductionOption = deductionOption;



                        TemplateHandler.Assign(ref _deductionOption, "ProductID_val", product["ProductID"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionID_val", dtype["dtypeid"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", dtype["dtypedesc"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", dtype["dtypename"]);
                        TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffEnable);
                        deductionList.Append(_deductionOption);
                    }
                    //Load TierList=no info
                    TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", AdminCommon.NoInfo(7));
                    //Disable Button AddTier
                    TemplateHandler.Assign(ref _productSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);

                }

                if (deductionList.Length == 0)
                {
                    TemplateHandler.ReplaceBlock(ref _productSetting, "Deduction", string.Empty);
                }
                else
                {

                    TemplateHandler.ReplaceBlock(ref _productSetting, "DeductionOption", deductionList.ToString());
                }
                if (tierList.Length == 0)
                {
                    TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", AdminCommon.NoInfo(7));
                    TemplateHandler.Assign(ref _productSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);

                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", tierList.ToString());
                    TemplateHandler.Assign(ref _productSetting, "btnAddStatus_val", IconAddTier.GetInstance().Disabled);
                }

                result.Append(_productSetting);
                obj.productSetting.Add(productSettingObj);
            }

            if (activeProducts != 0 && activeProducts == noProducts)
            {
                TemplateHandler.Assign(ref settingTemplate, "chkActiveAllClass_val", CheckboxStatus.GetInstance().OnEnable);
            }
            else
            {
                TemplateHandler.Assign(ref settingTemplate, "chkActiveAllClass_val", CheckboxStatus.GetInstance().OffEnable);
            }



            TemplateHandler.ReplaceBlock(ref settingTemplate, "ProductSetting", result.ToString());
            TemplateHandler.Assign(ref settingTemplate, "GroupSetting_val", js.Serialize(obj));

            //string t = js.Serialize(obj);

            return settingTemplate;
        }
        public static string GetGroupSettingForAddNew(int groupID, bool isApproved, string fName, string affName, string language, string email)
        {
            DbResult commonSetting = null;
            commonSetting = CommGroup.CommGroupSelCommonSetting();

            if (commonSetting.ResultSet.Tables.Count < 2 || commonSetting.ResultSet.Tables[0].Rows.Count <= 0)
            {
                return Fanex.Affiliate.Resources.Admin.NoInfo;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            GroupSettingObj obj = GroupSettingObj.GetInstance();
            obj.isEdited = false;
            obj.id = groupID;
            if (isApproved)
            {
                obj.isApproved = true;
                obj.fName = fName;
                obj.affName = affName;
                obj.language = language;
                obj.email = email;
            }

            string settingTemplate = TemplateHandler.LoadTemplate("GroupSetting/GroupSettingEdit.htm");
            TemplateHandler.Assign(ref settingTemplate, "Submit", Resources.Admin.Add);
            if (groupID > 0)
            {
                TemplateHandler.RemoveBlock(ref settingTemplate, "NormalGroup");
                obj.description = "[None]";
                obj.groupName = "[None]";
            }
            else
            {
                obj.description = string.Empty;
                obj.groupName = string.Empty;
            }



            TemplateHandler.Assign(ref settingTemplate, "ProductStatusColor_val", js.Serialize(ProductStatusColor.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconProductStatus_val", js.Serialize(IconProductStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "ProductTabSwitchIcon_val", js.Serialize(ProductTabSwitchIcon.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "ProductTabSwitch_val", js.Serialize(ProductTabSwitch.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "CheckboxStatus_val", js.Serialize(CheckboxStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconEdit_val", js.Serialize(IconEdit.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconDelete_val", js.Serialize(IconDelete.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconAddTier_val", js.Serialize(IconAddTier.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconSave_val", js.Serialize(IconSave.GetInstance()));

            TemplateHandler.Assign(ref settingTemplate, "RequiedExplain_val", Resources.Translator.Get("RequiedExplain"));
            TemplateHandler.Assign(ref settingTemplate, "EnableProductExplainTpl_val", Resources.Translator.Get("EnableProductExplain"));
            TemplateHandler.Assign(ref settingTemplate, "DisableProductExplainTpl_val", Resources.Translator.Get("DisableProductExplain"));
            TemplateHandler.Assign(ref settingTemplate, "noInfo_val", AdminCommon.NoInfo(7));


            TemplateHandler.Assign(ref settingTemplate, "maxlengthGroupName_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthDescription_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthAmount_val", 19);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthQM_val", 13);
            TemplateHandler.Assign(ref settingTemplate, "maximumQMNumber_val", MAXQM);
            TemplateHandler.Assign(ref settingTemplate, "maximumAmountNumber_val", MAXAMOUNT);

            /*Johhny fix dropdownlist*/
            var currencies = AppSettings.AffCurrencies; // AdminCommon.GetListSupportedCurrency();
            var selCurrency = Affiliate.AffInfo(groupID).Rows.Count > 0 ? Affiliate.AffInfo(groupID).Rows[0]["Currency"].ToString() : "";
            TemplateHandler.Assign(ref settingTemplate, "currencies",
                HtmlHelper.DropDownList(currencies, "currencies", selCurrency, false, "w100", (groupID > 0 ? true : false)));
                //Common.GenDropdownList(currencies, currencies, "currencies",
                //(groupID > 0 ? true : false), string.Empty,
                //Affiliate.AffInfo(groupID).Rows.Count > 0 ? Affiliate.AffInfo(groupID).Rows[0]["Currency"].ToString() : ""));

            string productSetting = TemplateHandler.CopyBlock(settingTemplate, "ProductSetting");

            
            AppCommon.AssignToAdminTemplate(ref settingTemplate, "GroupName", "Description", "ActiveAll", "Add",
                "QualifiedAmount", "Currency", "Action", "Save", "GroupNameEmpty", "ValidQualifiedAmount",
                "EnableProduct", "ConfirmUpdateGroup", "InvalidSettingFor", "MaxTierAllow", "LastTierSettingHaveMaxValue",
                "TierSettingAtLeastTier", "ConfirmRemoveFromGroup","InputCurrencyValue");
            /*smith*/
            TemplateHandler.Assign(ref settingTemplate, "qualifiedAmount-val", string.Empty);
            //for edit: TemplateHandler.Assign(ref settingTemplate, "disable-currency", "disabled='disabled'");
            
            AppCommon.AssignToAdminTemplate(ref productSetting,
            "Deductions", "TierSetting", "Tier", "MinAmount", "MaxAmount", "MinQM",
            "MaxQM", "Comm", "Action", "AddTier", "Edit", "Delete", "MinQMExplain",
            "MaxQMExplain", "MinAmountExplain", "MaxAmountExplain","Action"); 

            string deductionOption = TemplateHandler.CopyBlock(productSetting, "DeductionOption");
            string tierRow = TemplateHandler.CopyBlock(productSetting, "TierRow");
            string slOption = TemplateHandler.CopyBlock(tierRow, "SelectOption");



            string tierRowTpl = tierRow;
            TemplateHandler.ReplaceBlock(ref tierRowTpl, "SelectOption", GetCommissionOptionList(0, slOption));
            TemplateHandler.Assign(ref settingTemplate, "TierRowTpl_val", js.Serialize(tierRowTpl));

            DataTable Product = commonSetting.ResultSet.Tables[0];
            DataTable DeductionType = commonSetting.ResultSet.Tables[1];


            TemplateHandler.Assign(ref settingTemplate, "GroupName_val", string.Empty);
            TemplateHandler.Assign(ref settingTemplate, "Description_val", string.Empty);


            StringBuilder result = new StringBuilder();
            IList<int> ProductIDArr = new List<int>();
            bool IsFirstProduct = true;

            foreach (DataRow product in Product.Rows)
            {
                ProductSettingObj productSettingObj = ProductSettingObj.GetInstance();
                productSettingObj.id = Convert.ToInt32(product["ProductID"]);
                productSettingObj.productName = product["ProductName"].ToString();

                ProductIDArr.Add(Convert.ToInt32(product["ProductID"]));
                string _productSetting = productSetting;
                TemplateHandler.Assign(ref _productSetting, "ProductName", product["ProductName"]);
                string copyText = Resources.Admin.CopyTierSettingFor + " " +product["ProductName"];
                //TemplateHandler.Assign(ref copyText, "ProductName_val", product["ProductName"]);

                TemplateHandler.Assign(ref _productSetting, "CopyAllText", copyText);
                TemplateHandler.Assign(ref _productSetting, "ProductID_val", product["ProductID"]);

                DataRow[] drDeductionType = DeductionType.Select("ProductID=" + product["ProductID"]);

                StringBuilder deductionList = new StringBuilder();

                if (IsFirstProduct)
                {
                    productSettingObj.isEnabled = true;
                    IsFirstProduct = false;
                    TemplateHandler.Assign(ref _productSetting, "product_color_val", ProductStatusColor.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _productSetting, "icon_enable_val", IconProductStatus.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _productSetting, "collapse_expand_val", ProductTabSwitchIcon.GetInstance().Expand);
                    TemplateHandler.Assign(ref _productSetting, "tab_content_display_val", ProductTabSwitch.GetInstance().Expand);
                    TemplateHandler.Assign(ref _productSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffEnable);
                }
                else
                {
                    productSettingObj.isEnabled = false;
                    TemplateHandler.Assign(ref _productSetting, "product_color_val", ProductStatusColor.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _productSetting, "icon_enable_val", IconProductStatus.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _productSetting, "collapse_expand_val", ProductTabSwitchIcon.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _productSetting, "tab_content_display_val", ProductTabSwitch.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _productSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffDisable);
                }

                string iconExplain = Resources.Translator.Get("EnableProductExplain");
                TemplateHandler.Assign(ref iconExplain, "ProductName_val", product["ProductName"]);
                TemplateHandler.Assign(ref _productSetting, "icon_enable_explain_val", iconExplain);

                //Load Deduction Settings
                foreach (DataRow dtype in drDeductionType)
                {
                    DeductionTypeObj deductionObj = DeductionTypeObj.GetInstance();
                    deductionObj.id = Convert.ToInt32(dtype["dtypeid"]);
                    deductionObj.name = dtype["dtypename"].ToString();
                    deductionObj.isCheck = false;
                    deductionObj.deductedPercentage = CastAide.AsDouble(dtype["deductedpercentage"]);
                    productSettingObj.deductions.Add(deductionObj);

                    string _deductionOption = deductionOption;
                    TemplateHandler.Assign(ref _deductionOption, "ProductID_val", product["ProductID"]);
                    TemplateHandler.Assign(ref _deductionOption, "DeductionID_val", dtype["dtypeid"]);
                    TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", dtype["dtypedesc"]);
                    TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", dtype["dtypename"]);
                    TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffEnable);
                    deductionList.Append(_deductionOption);
                }
                //Load TierList=no info
                TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", AdminCommon.NoInfo(7));
                //Disable Button AddTier
                TemplateHandler.Assign(ref _productSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);


                if (deductionList.Length == 0)
                {
                    TemplateHandler.ReplaceBlock(ref _productSetting, "Deduction", string.Empty);
                }
                else
                {

                    TemplateHandler.ReplaceBlock(ref _productSetting, "DeductionOption", deductionList.ToString());
                }
                TemplateHandler.ReplaceBlock(ref _productSetting, "TierRow", AdminCommon.NoInfo(7));
                TemplateHandler.Assign(ref _productSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);

                result.Append(_productSetting);
                obj.productSetting.Add(productSettingObj);
            }




            TemplateHandler.Assign(ref settingTemplate, "chkActiveAllClass_val", CheckboxStatus.GetInstance().OffEnable);


            TemplateHandler.ReplaceBlock(ref settingTemplate, "ProductSetting", result.ToString());
            TemplateHandler.Assign(ref settingTemplate, "GroupSetting_val", js.Serialize(obj));

            //string t = js.Serialize(obj);

            return settingTemplate;
        }
        public static string GetAssignmentForm(string affName, int affID, bool isApproved, string MailOption,
           int currency, bool isMultiSelect = false)
        {
            string assignTemplate = TemplateHandler.LoadTemplate("GroupSetting/GroupAssignment.htm");
            AppCommon.AssignToAdminTemplate(ref assignTemplate,
                "Assignee", "AssignCommGroup", "AddNew", "Save", "CommissionGroup",
                "ChooseGroup","ConfirmChangeGroup","MsgApproveAffiliate","AddNewGroup","UsePrivateGroup");
            TemplateHandler.Assign(ref assignTemplate, "ListAff_val", affName);
            TemplateHandler.Assign(ref assignTemplate, "mailoption", MailOption);
            if (!isApproved)
            {
                TemplateHandler.RemoveBlock(ref assignTemplate, "ApproveAffiliate");
                TemplateHandler.Assign(ref assignTemplate, "IsApproved_val", "false");
            }
            else
            {
                TemplateHandler.Assign(ref assignTemplate, "IsApproved_val", "true");
            }


            JavaScriptSerializer js = new JavaScriptSerializer();
            TemplateHandler.Assign(ref assignTemplate, "CheckboxStatus_val", js.Serialize(CheckboxStatus.GetInstance()));
            string optionTemplate = TemplateHandler.CopyBlock(assignTemplate, "SelectOption");
            StringBuilder list = new StringBuilder();

            DbResult result = CommGroup.CommGroupSelListNormal(affID, currency);
            int groupid = CastAide.AsInt32(result.Outputs["groupid"], -1);
            TemplateHandler.Assign(ref assignTemplate, "OldGroupID_val", groupid);

            foreach (DataRow row in result.ResultSet.Tables[0].Rows)
            {
                if (/*isMultiSelect &&*/ row["GroupName"].ToString().ToLower().Contains("none"))
                {
                    continue;
                }

                string option = optionTemplate;

                if (groupid != -1 && groupid == CastAide.AsInt32(row["groupid"]))
                {
                    TemplateHandler.Assign(ref option, "selected_val", "selected");
                }
                else
                {
                    TemplateHandler.Assign(ref option, "selected_val", string.Empty);
                }
                TemplateHandler.Assign(ref option, "option_value", row["groupid"]);
                TemplateHandler.Assign(ref option, "option_name", row["groupname"]);
                list.Append(option);
            }
            if (affID > 0)
            {
                if (groupid > 1000)
                {
                    TemplateHandler.Assign(ref assignTemplate, "slCommGroupDisabled_val", "disabled");
                    TemplateHandler.Assign(ref assignTemplate, "chkBoxClass_val", CheckboxStatus.GetInstance().OnEnable);
                }
                else
                {
                    TemplateHandler.Assign(ref assignTemplate, "slCommGroupDisabled_val", string.Empty);
                    TemplateHandler.Assign(ref assignTemplate, "chkBoxClass_val", CheckboxStatus.GetInstance().OffEnable);
                }
            }
            else
            {
                TemplateHandler.Assign(ref assignTemplate, "slCommGroupDisabled_val", string.Empty);
                TemplateHandler.RemoveBlock(ref assignTemplate, "PrivateGroup");

            }
            if (groupid > 1000)
            {
                TemplateHandler.Assign(ref assignTemplate, "isUsePrivateGroup_val", "true");
            }
            else
            {
                TemplateHandler.Assign(ref assignTemplate, "isUsePrivateGroup_val", "false");
            }
            TemplateHandler.ReplaceBlock(ref assignTemplate, "SelectOption", list.ToString());
            return assignTemplate;
        }
    }
}

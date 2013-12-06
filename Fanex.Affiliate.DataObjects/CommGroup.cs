/// <summary>
/// Summary description for CommGroup
/// </summary>
namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections;
    using DNAide.Data;
    using System.Web;
    using System.Text;
    using Fanex.Affiliate.Model;

    public class CommGroup
    {
        public CommGroup()
        {
        }

        public static DbResult GetCommGroupSettingByAffId(int affid)
        {
            Hashtable param = new Hashtable();
            param.Add("affid", affid);
            return DbHelper.Execute("Aff_CommGroupSelDetail", param, DBType.AFFILIATE);
        }

        public static DbResult GetCommGroupSettingByGroupId(int groupid)
        {
            Hashtable param = new Hashtable();
            param.Add("groupid", groupid);
            return DbHelper.Execute("Aff_CommGroupSelDetail", param, DBType.AFFILIATE);
        }
        public static DbResult CommGroupSel(string strGroupName, object fromdate, object todate, int pagesize, int pageindex)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("groupname", HttpContext.Current.Server.HtmlEncode(strGroupName));
            if (fromdate != null && todate != null)
            {
                hsParams.Add("fromdate", fromdate);
                hsParams.Add("todate", Convert.ToDateTime(todate).AddDays(1).AddMilliseconds(-3));
            }
            hsParams.Add("totalnonegroup", 0);
            hsParams.Add("pagesize", Convert.ToInt32(pagesize));
            hsParams.Add("pageindex", pageindex);
            hsParams.Add("totalpages", 0);
            hsParams.Add("nrows", 0);
            return DbHelper.Execute("Aff_CommGroupSel", hsParams, DBType.AFFILIATE);
        }
        public static DbResult CommGroupDel(int groupID, string ip, string lang = "en-US")
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", HttpContext.Current.Session["CustID"]);
            hsParams.Add("groupid", groupID);
            hsParams.Add("ip", ip);
            hsParams.Add("language", lang);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupDel", hsParams, DBType.AFFILIATE);
        }
        //public static DbResult CommGroupSelAll(string strGroupName)
        //{
        //    return DbHelper.Execute("Aff_CommGroupSelAll", null, DBType.AFFILIATE);
        //}
        public static DbResult CommGroupSelDetail(int groupID)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("groupid", groupID);
            return DbHelper.Execute("Aff_CommGroupSelDetail", hsParams, DBType.AFFILIATE);
        }
        public static DbResult CommGroupSelCommonSetting()
        {
            return DbHelper.Execute("Aff_CommGroupSelCommonSetting", null, DBType.AFFILIATE);
        }

        public static DbResult CommGroupIns(GroupSettingObj obj, string ip,
           int currency, decimal qualifiedAmount, string lang = "en-US")
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", HttpContext.Current.Session["CustID"]);

            hsParams.Add("groupid", obj.id);
            if (obj.isApproved)
            {
                hsParams.Add("isapproved", 1);
                hsParams.Add("password", Sunplus.Agent.Utilities.Encryption.CfsEnCode(obj.password.Trim()));
            }

            hsParams.Add("groupname", HttpContext.Current.Server.HtmlEncode(obj.groupName));
            hsParams.Add("description", HttpContext.Current.Server.HtmlEncode(obj.description));
            StringBuilder ProductIDList = new StringBuilder(), deductionList = new StringBuilder(), deductedPercentageList = new StringBuilder(), tierList = new StringBuilder(), minAmountList = new StringBuilder()
                              , maxAmountList = new StringBuilder(), minQMList = new StringBuilder(), maxQMList = new StringBuilder(), commissionList = new StringBuilder();
            ProductSettingObj copyFromProduct = GetCopyProductSetting(obj);
            foreach (ProductSettingObj s in obj.productSetting)
            {
                if (s.isEnabled)
                {
                    var t = s;
                    if (copyFromProduct != null)
                    {
                        t = copyFromProduct;
                    }
                    ProductIDList.Append(s.id.ToString());

                    ProductIDList.Append(";");
                    tierList.Append(t.commissions.Count.ToString());
                    tierList.Append(";");
                    foreach (DeductionTypeObj d in s.deductions)
                    {
                        if (d.isCheck)
                        {
                            deductionList.Append(d.id.ToString());
                            deductionList.Append(",");
                            deductedPercentageList.Append(d.deductedPercentage.ToString());
                            deductedPercentageList.Append(",");
                        }
                    }
                    for (int i = 0; i < t.commissions.Count; i++)
                    {
                        minAmountList.Append(t.minAmounts[i].ToString());
                        minAmountList.Append(",");
                        maxAmountList.Append(t.maxAmounts[i].ToString());
                        maxAmountList.Append(",");
                        minQMList.Append(t.minQMs[i].ToString());
                        minQMList.Append(",");
                        maxQMList.Append(t.maxQMs[i].ToString());
                        maxQMList.Append(",");
                        commissionList.Append(t.commissions[i]);
                        commissionList.Append("@");
                    }
                    minAmountList.Remove(minAmountList.Length - 1, 1);
                    maxAmountList.Remove(maxAmountList.Length - 1, 1);
                    minQMList.Remove(minQMList.Length - 1, 1);
                    maxQMList.Remove(maxQMList.Length - 1, 1);
                    commissionList.Remove(commissionList.Length - 1, 1);
                    minAmountList.Append(";");
                    maxAmountList.Append(";");
                    minQMList.Append(";");
                    maxQMList.Append(";");
                    commissionList.Append(";");

                }
            }
            ProductIDList.Remove(ProductIDList.Length - 1, 1);
            tierList.Remove(tierList.Length - 1, 1);
            if (deductionList.Length > 0)
            {
                deductionList.Remove(deductionList.Length - 1, 1);
                deductedPercentageList.Remove(deductedPercentageList.Length - 1, 1);
            }
            minAmountList.Remove(minAmountList.Length - 1, 1);
            maxAmountList.Remove(maxAmountList.Length - 1, 1);
            minQMList.Remove(minQMList.Length - 1, 1);
            maxQMList.Remove(maxQMList.Length - 1, 1);
            commissionList.Remove(commissionList.Length - 1, 1);

            hsParams.Add("productidlist", ProductIDList.ToString());
            hsParams.Add("deductionlist", deductionList.ToString());
            hsParams.Add("deductedpercentagelist", deductedPercentageList.ToString());
            hsParams.Add("tierlist", tierList.ToString());
            hsParams.Add("minamountlist", minAmountList.ToString());
            hsParams.Add("maxamountlist", maxAmountList.ToString());
            hsParams.Add("minqmlist", minQMList.ToString());
            hsParams.Add("maxqmlist", maxQMList.ToString());
            hsParams.Add("commissionlist", commissionList.ToString());
            hsParams.Add("mailoption", false);
            hsParams.Add("afftype", 0);
            hsParams.Add("ip", ip);
            hsParams.Add("language", lang);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            /*smith*/
            hsParams.Add("currency", currency);
            hsParams.Add("qualifiedamount", qualifiedAmount);

            return DbHelper.Execute("Aff_CommGroupIns", hsParams, DBType.AFFILIATE);
        }
        public static ProductSettingObj GetCopyProductSetting(GroupSettingObj obj)
        {
            if (obj.copyProductID != -1)
            {
                foreach (var s in obj.productSetting)
                {
                    if (s.isEnabled && s.id == obj.copyProductID)
                        return s;
                }
            }
            return null;
        }
        public static DbResult CommGroupUpd(GroupSettingObj obj, string ip,
           int currency, decimal qualifiedAmount, string lang = "en-US")
        {
            var hsParams = new Hashtable();
            hsParams.Add("userid", HttpContext.Current.Session["CustID"]);
            hsParams.Add("groupid", obj.id);
            hsParams.Add("groupname", HttpContext.Current.Server.HtmlEncode(obj.groupName));
            hsParams.Add("description", HttpContext.Current.Server.HtmlEncode(obj.description));
            StringBuilder productIDList = new StringBuilder(), deductionList = new StringBuilder(), deductedPercentageList = new StringBuilder(), tierList = new StringBuilder(), minAmountList = new StringBuilder()
                              , maxAmountList = new StringBuilder(), minQMList = new StringBuilder(), maxQMList = new StringBuilder(), commissionList = new StringBuilder();
            ProductSettingObj copyFromProduct = GetCopyProductSetting(obj);
            foreach (ProductSettingObj s in obj.productSetting)
            {
                if (s.isEnabled)
                {
                    var t = s;
                    if (copyFromProduct != null)
                    {
                        t = copyFromProduct;
                    }
                    productIDList.Append(s.id.ToString());

                    productIDList.Append(";");
                    tierList.Append(t.commissions.Count.ToString());
                    tierList.Append(";");
                    foreach (DeductionTypeObj d in s.deductions)
                    {
                        if (d.isCheck)
                        {
                            deductionList.Append(d.id.ToString());
                            deductionList.Append(",");
                            deductedPercentageList.Append(d.deductedPercentage.ToString());
                            deductedPercentageList.Append(",");
                        }
                    }
                    for (int i = 0; i < t.commissions.Count; i++)
                    {
                        minAmountList.Append(t.minAmounts[i].ToString());
                        minAmountList.Append(",");
                        maxAmountList.Append(t.maxAmounts[i].ToString());
                        maxAmountList.Append(",");
                        minQMList.Append(t.minQMs[i].ToString());
                        minQMList.Append(",");
                        maxQMList.Append(t.maxQMs[i].ToString());
                        maxQMList.Append(",");
                        commissionList.Append(t.commissions[i]);
                        commissionList.Append("@");
                    }
                    minAmountList.Remove(minAmountList.Length - 1, 1);
                    maxAmountList.Remove(maxAmountList.Length - 1, 1);
                    minQMList.Remove(minQMList.Length - 1, 1);
                    maxQMList.Remove(maxQMList.Length - 1, 1);
                    commissionList.Remove(commissionList.Length - 1, 1);
                    minAmountList.Append(";");
                    maxAmountList.Append(";");
                    minQMList.Append(";");
                    maxQMList.Append(";");
                    commissionList.Append(";");

                }
            }
            productIDList.Remove(productIDList.Length - 1, 1);
            tierList.Remove(tierList.Length - 1, 1);
            if (deductionList.Length > 0)
            {
                deductionList.Remove(deductionList.Length - 1, 1);
                deductedPercentageList.Remove(deductedPercentageList.Length - 1, 1);
            }
            minAmountList.Remove(minAmountList.Length - 1, 1);
            maxAmountList.Remove(maxAmountList.Length - 1, 1);
            minQMList.Remove(minQMList.Length - 1, 1);
            maxQMList.Remove(maxQMList.Length - 1, 1);
            commissionList.Remove(commissionList.Length - 1, 1);

            hsParams.Add("productidlist", productIDList.ToString());
            hsParams.Add("deductionlist", deductionList.ToString());
            hsParams.Add("deductedpercentagelist", deductedPercentageList.ToString());
            hsParams.Add("tierlist", tierList.ToString());
            hsParams.Add("minamountlist", minAmountList.ToString());
            hsParams.Add("maxamountlist", maxAmountList.ToString());
            hsParams.Add("minqmlist", minQMList.ToString());
            hsParams.Add("maxqmlist", maxQMList.ToString());
            hsParams.Add("commissionlist", commissionList.ToString());
            hsParams.Add("ip", ip);
            hsParams.Add("language", lang);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            /*smith*/
            hsParams.Add("currency", currency);
            hsParams.Add("qualifiedamount", qualifiedAmount);

            return DbHelper.Execute("Aff_CommGroupUpd", hsParams, DBType.AFFILIATE);
        }

        public static DbResult CommGroupSelListNormal(int affID,int currency)
        {
            var hsParams = new Hashtable();
            if (affID != -1)
            {
                hsParams.Add("affid", affID);
            }
            hsParams.Add("groupid", -1);
            if(currency>0)
            hsParams.Add("currency", currency);
            return DbHelper.Execute("Aff_CommGroupSelListNormal", hsParams, DBType.AFFILIATE);
        }
        public static DbResult CommGroupAssign(int groupID, string affIDList, string ip, string lang = "en-US")
        {
            var hsParams = new Hashtable
                               {
                                   {"userid", HttpContext.Current.Session["CustID"]},
                                   {"groupid", groupID},
                                   {"affidlist", affIDList},
                                   {"ip", ip},
                                   {"language", lang}, 
                                   {"errcode", 0},
                                   {"errmsg", string.Empty}
                               };
            return DbHelper.Execute("Aff_CommGroupAssign", hsParams, DBType.AFFILIATE);
        }

    }
}

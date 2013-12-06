// -----------------------------------------------------------------------
// <copyright file="AffiliateList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using System;
    using System.Data;
    using System.Text;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Web;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.DataObjects;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AffiliateList
    {
        #region Aff List content
        public static string GetAffList(int groupID, ref string groupName, string username, 
            string email, int custid, string status, int afftype, bool isClosed, bool lockedOut, 
            int curpage, int pagesize, out int nummem, ref int totalpage, bool isFavor, int currency)
        {
            nummem = 0;
            DbResult result = null;
            //if (lockedOut)
            //{
            //    result = Affiliate.ListAff(groupID, groupName, username, email, custid, status, isClosed, lockedOut, curpage, pagesize, currency);
            //    nummem = CastAide.AsInt32(result.Outputs["nrows"]);
            //}
            //else 
                if (custid != -2)
            {
                if (!isFavor)
                    result = Affiliate.ListAff(groupID, groupName, username, email, custid, status, afftype, isClosed, lockedOut, curpage, pagesize, currency);
                else
                    result = Affiliate.ListAffFavorie(groupID, groupName, username, email, custid, status, afftype, isClosed, lockedOut, curpage, pagesize, currency);
                nummem = CastAide.AsInt32(result.Outputs["nrows"]);
            }
            int cols = 12;
            string strTemplate = TemplateHandler.LoadTemplate("UserInfo/AffList.htm");

            var hideAffType = AppSettings.BaseAffTypes.Count < 2;
            if (hideAffType)
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "afftype");
            }
            else
            AppCommon.AssignToTemplate(ref strTemplate, "afftype");
            
            if (!AdminSession.IsReportAccess)
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "report");
                cols = 11;
            }
            if (string.IsNullOrEmpty(groupName))
            {
                groupName = string.Empty;
            }

            if (status.ToLower() == "pending" || status.ToLower() == "disapproved")
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "approved1");
                cols = 5;
            }
            //else if(String.IsNullOrWhiteSpace(status) || status.ToLower() == "all"){
            //    TemplateHandler.RemoveBlock(ref strTemplate, "checkbox");
            //}

            if (nummem > 0)
            {
                groupName = CastAide.AsString(result.Outputs["groupname"], string.Empty);
                TemplateHandler.Assign(ref strTemplate, "Reset", Resources.Admin.Reset);
                TemplateHandler.Assign(ref strTemplate, "view", Resources.Admin.View);
                TemplateHandler.Assign(ref strTemplate, "Edit", Resources.Admin.Edit);

                string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
                DataTable dtbResult = result.ResultSet.Tables[0];
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                decimal dcCurrentBalance = 0;
                int intMemNum = 0;
                TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);
                foreach (DataRow dr in dtbResult.Rows)
                {
                    string strTmp = strContent;
                    intNo++;
                    string strStatus = status;// dr["status"].ToString();
                    bool isClose = CastAide.AsBoolean(dr["isclosed"]);
                    if (isClose)
                    {
                        strStatus = "Closed";
                        TemplateHandler.Assign(ref strTmp, "color", AdminCommon.RowBgClosed);
                    }

                    dcCurrentBalance += CastAide.AsDecimal(dr["balancebf"]);

                    if (CastAide.AsInt32(dr["GroupId"]) == 0)
                    {
                        TemplateHandler.RemoveBlock(ref strTmp, "displaycheckbox");
                    }

                    intMemNum = CastAide.AsInt32(dr["noofmember"]);
                    if (intMemNum == 0)
                        TemplateHandler.ReplaceBlock(ref strTmp, "nummem", intMemNum.ToString());
                    TemplateHandler.Assign(ref strTmp, "no", (curpage - 1) * pagesize + intNo);

                    TemplateHandler.Assign(ref strTmp, "custid", dr["AffID"].ToString());
                    TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                    //TemplateHandler.Assign(ref strTmp, "nickname-val", dr["username2"].ToString());
                    if (status == "pending")
                        TemplateHandler.Assign(ref strTmp, "userdate-val", AdminCommon.IBCDateTimeFormat(dr["registereddate"], DTFormatType.DT24h));
                    else
                        TemplateHandler.Assign(ref strTmp, "userdate-val", AdminCommon.IBCDateTimeFormat(dr["ApprovedDate"], DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "fname-val", AppCommon.cutLongText(dr["firstname"].ToString(), 23));
                    TemplateHandler.Assign(ref strTmp, "lname-val", AppCommon.cutLongText(dr["lastname"].ToString(), 23));
                    TemplateHandler.Assign(ref strTmp, "currency-val", dr["currency"].ToString());
                    TemplateHandler.Assign(ref strTmp, "language-val", AdminCommon.GerFullTextLang(dr["preferredlanguage"]));

                    TemplateHandler.Assign(ref strTmp, "member-val", intMemNum);
                    TemplateHandler.Assign(ref strTmp, "favorcss", CastAide.AsInt32(dr["favorite"]) == 0 ? "favor-uncheck" : "favor-check");
                    //if (dr.Table.Columns.Contains("Status"))
                    //{
                        //hide fav
                        if (dr["Status"].ToString() == "Disapproved" ||
                            dr["Status"].ToString() == "Pending")
                        {
                            TemplateHandler.RemoveBlock(ref strTmp, "favxx");
                        }

                        //bg color enable
                        switch (dr["Status"].ToString().ToLower())
                        {
                            case "pending":
                                TemplateHandler.Assign(ref strTmp, "color", "#ddedc3");
                                break;
                            case "disapproved":
                                TemplateHandler.Assign(ref strTmp, "color", "#b0b0b0");
                                break;
                        }
                        TemplateHandler.Assign(ref strTmp, "status-val", dr["Status"]);
                        if(!hideAffType)
                            TemplateHandler.Assign(ref strTmp, "afftype-val", dr["AffTypeName"]);
                    //}
                    if (dr.Table.Columns.Contains("IsLocked"))
                    {
                        if (CastAide.AsBoolean(dr["IsLocked"]))
                            TemplateHandler.Assign(ref strTmp, "color", "#CCFFFF");
                    }
                    TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? AdminCommon.RowBgEven : AdminCommon.RowBgOdd);

                    if (string.Compare(strStatus, "Pending", true) != 0 && string.Compare(strStatus, "Disapproved", true) != 0)
                    {
                        var currencySymbol = dr["currency"].ToString();                        
                        var culture = CurrencyUtils.GetCulture(currencySymbol);
                        double value = 0;                        
                        Double.TryParse(dr["balancebf"].ToString(),out value);
                        var numberFormat = culture.NumberFormat.CurrencySymbol + "{0:#,##0.00}";
                        var currencyString = CurrencyUtils.Format(value, culture);// String.Format(numberFormat, value);
                        if(value < 0)
                            TemplateHandler.Assign(ref strTmp, "balancebroughtforward-color", "red");

                        TemplateHandler.Assign(ref strTmp, "balancebroughtforward-val", currencyString);
                    }
                    if (string.Compare(strStatus, Status.Approved, true) != 0)
                    {
                        TemplateHandler.ReplaceBlock(ref strTmp, "no-pass", string.Empty);
                    }
                    if (status.ToLower() == "approved" || status.ToLower() == string.Empty)
                    {
                        TemplateHandler.Assign(ref strTmp, "groupid-val", dr["groupid"].ToString());
                        if (Convert.ToInt32(dr["groupid"]) > 1000)
                        {
                            TemplateHandler.RemoveBlock(ref strTmp, "PublicGroup");
                        }
                        else
                        {
                            TemplateHandler.RemoveBlock(ref strTmp, "PrivateGroup");
                        }
                        TemplateHandler.Assign(ref strTmp, "group-val", Common.CutLongText(dr["groupname"].ToString(), 20));

                    }
                    TemplateHandler.Assign(ref strTmp, "isAdmin", "0");
                    strBuild.Append(strTmp);

                }

                return strBuild.ToString();
            }
            else
            {
                return AdminCommon.NoInfo(cols);
            }
        }
        #endregion
    }
}

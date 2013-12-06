
namespace Fanex.Affiliate.Core.Admin.GenerateHTML
{
    using DNAide.Web;
    using System.Data;
    using DNAide.Data;
    using System.Text;
    using System;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    /// <summary>
    /// Generate content for ViewLog
    /// Written by : Jacob on 23/04/2010
    /// </summary>
    public class UserLog
    {

        private const int COMMISSION_GROUP = 1;
        private const int USER_INFO = 2;
        private const int TRANSACTION = 3;
        private const int PAYOUT = 4;
        private const int PERMISSION = 5;
        private const int PLAYER = 6;
        private const int BANNER = 7;
        private const int EMAIL = 8;
        private const int MESSAGE = 9;
        private const int EXCHANGE_RATE = 10;
        private const int USER_NOTE = 11;
        private const int LOGIN_ADMIN = 12;
        private const int LOGIN_AFFILIATE = 13;
        private const int LOGOUT_ADMIN = 14;
        private const int LOGOUT_AFFILIATE = 15;
        private const int PASSWORD = 16;
        private const int ADJUSTMENT = 17;
        private const int MEMBER = 18;
        private const int USER_CONTROL = 19;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logDetail"></param>
        /// <param name="logType"></param>
        /// <returns></returns>
        public static String DecorateLogDescription(String logDetail, int logType)
        {
            switch (logType)
            {
                case MEMBER:
                    // Decorate the Log Remove member. IBCCustID=1906258 
                    Regex regex = new Regex(@"^Remove member. username=(\d*)$");
                    Match match = regex.Match(logDetail);
                    if (match.Success && match.Groups.Count > 1)
                    {
                        string username = match.Groups[1].Value;                     
                        if (!string.IsNullOrEmpty(username))                        
                        {

                            logDetail = String.Format("Remove member: {0}", username);
                        }
                    }
                    break;
                case COMMISSION_GROUP: break;
                case USER_INFO: break;
                case TRANSACTION: break;
                case PAYOUT: break;
                case PERMISSION: break;
                case PLAYER: break;
                case BANNER: break;
                case EMAIL: break;
                case MESSAGE: break;
                case EXCHANGE_RATE: break;
                case USER_NOTE: break;
                case LOGIN_ADMIN: break;
                case LOGIN_AFFILIATE: break;
                case LOGOUT_ADMIN: break;
                case LOGOUT_AFFILIATE: break;
                case PASSWORD: break;
                case ADJUSTMENT: break;
                case USER_CONTROL: break;
                default: break;
            }

            return logDetail;
        }
        public static string GetLogTypeList(int currentLogType, bool isGenAll)
        {
            DbResult logTypeList = DataObjects.DbLog.LogTypeSel();
            StringBuilder result = new StringBuilder();
            string opt = TemplateHandler.LoadTemplate("Option.htm");
            TemplateHandler.Assign(ref opt, "id", "slLogType");

            string optionList = TemplateHandler.CopyBlock(opt, "optionList");
            if (isGenAll)
            {
                string optionAll = optionList;
                string noneValue = string.Empty;
                String selected = currentLogType == 0 ? "selected" : "";

                TemplateHandler.Assign(ref optionAll, "Optvalue", "0");
                TemplateHandler.Assign(ref optionAll, "Optname", "[" + Resources.Admin.All + "]");
                TemplateHandler.Assign(ref optionAll, "selected", selected);
                result.Append(optionAll);
            }

            //List<int> affiliateLogType = new List<int>();

            //affiliateLogType.AddRange(new int[]{COMMISSION_GROUP	,
            //                            USER_INFO	,
            //                            TRANSACTION	,
            //                            PAYOUT	,
            //                            PLAYER	,
            //                            BANNER	,
            //                            EMAIL	,
            //                            USER_NOTE	,
            //                            LOGIN_AFFILIATE	,
            //                            LOGOUT_AFFILIATE	,
            //                            PASSWORD	,
            //                            ADJUSTMENT	,
            //                            MEMBER	,
            //                        });

            if (logTypeList.ResultSet.Tables.Count > 0)
            {
                DataTable dt = logTypeList.ResultSet.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    int typeID = Convert.ToInt32(dr["TypeID"]);
                    //if (affiliateLogType.Contains(typeID))
                    //{
                    string option = optionList;
                    String selected = typeID == currentLogType ? "selected" : "";

                    TemplateHandler.Assign(ref option, "Optvalue", dr["TypeID"].ToString());
                    TemplateHandler.Assign(ref option, "Optname", dr["TypeName"].ToString());
                    TemplateHandler.Assign(ref option, "selected", selected);
                    result.Append(option);
                    //}
                }
            }

            TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
            return opt;
        }

        /// <summary>
        /// Show the Log list
        /// Group same refno log entry to same group
        /// </summary>
        /// <param name="affID"></param>
        /// <param name="logType"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static string GetLogList(int affID, int logType, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize, ref int pageCount, ref int rowCount)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/UserLog.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "content");
            TemplateHandler.Assign(ref strContent, "over", AdminCommon.RowBgOver);

            DbResult result = DataObjects.DbLog.LogSel(affID, logType, dateFrom, dateTo, pageIndex, pageSize);

            pageCount = Convert.IsDBNull(result.Outputs["pagecount"]) ? 0 : Convert.ToInt32(result.Outputs["pagecount"]);
            rowCount = Convert.IsDBNull(result.Outputs["rowcount"]) ? 0 : Convert.ToInt32(result.Outputs["rowcount"]);

            int cols = 8;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = (pageIndex - 1) * pageSize;
                StringBuilder strBuild = new StringBuilder();
                if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
                {
                    DataRowCollection resultRows = result.ResultSet.Tables[0].Rows;

                    int currentRefNo = 0;
                    string bgColor = AdminCommon.RowBgEven;
                    for (int i = 0; i < resultRows.Count; i++)
                    {
                        DataRow currentRow = resultRows[i];

                        string strTmp = strContent;

                        // Decorate Log detail
                        String logDetail = Convert.IsDBNull(currentRow["Details"]) ? "" : currentRow["Details"].ToString();
                        logDetail = logDetail.Replace("\\n", "<br/>");
                        logDetail = Regex.Replace(logDetail, @"(.{70})", "$1<br/>", RegexOptions.IgnoreCase);
                        logDetail = DecorateLogDescription(logDetail, Convert.ToInt32(currentRow["TypeID"]));

                        int refNo = (int)currentRow["RefNo"];
                        // Check if need to group rows
                        bool isNewGroup = false;
                        bool isNewRow = refNo != currentRefNo;
                        if (refNo != currentRefNo)
                        {
                            // New Group
                            bgColor = (bgColor == AdminCommon.RowBgEven ? AdminCommon.RowBgOdd : AdminCommon.RowBgEven);
                            currentRefNo = refNo;
                            // Check if need to group rows
                            if (i < resultRows.Count - 1)
                            {
                                int nextRefNo = (int)resultRows[i + 1]["RefNo"];
                                if (nextRefNo == currentRefNo)
                                {
                                    isNewGroup = true;
                                }
                            }
                        }
                        #region Template Assign
                        intNo++;
                        TemplateHandler.Assign(ref strTmp, "content_style", isNewRow ? "" : "display:none;");
                        //TemplateHandler.Assign(ref strTmp, "content_style", "");
                        TemplateHandler.Assign(ref strTmp, "Group_val", isNewRow ? "" : currentRefNo.ToString());
                        TemplateHandler.Assign(ref strTmp, "Toogle_Group_val", isNewGroup ? currentRefNo.ToString() : "");
                        TemplateHandler.Assign(ref strTmp, "themepath", AppSettings.ThemeDir);
                        TemplateHandler.Assign(ref strTmp, "expand_style", isNewGroup ? "" : "display:none;");
                        TemplateHandler.Assign(ref strTmp, "collapse_style", isNewGroup ? "display:none;" : "");

                        //TemplateHandler.Assign(ref strTmp, "Number_val", isNewRow ? intNo.ToString() : "");
                        TemplateHandler.Assign(ref strTmp, "Number_val", intNo.ToString());
                        TemplateHandler.Assign(ref strTmp, "RefNo_val", currentRow["RefNo"].ToString());
                        TemplateHandler.Assign(ref strTmp, "LoggedDate_val", AdminCommon.IBCDateTimeFormat(currentRow["LoggedDate"], DTFormatType.DT24h));
                        TemplateHandler.Assign(ref strTmp, "UserName_val", currentRow["UserName"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UserID_val", currentRow["AffID"].ToString());
                        TemplateHandler.Assign(ref strTmp, "TypeName_val", currentRow["TypeName"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Description_val", currentRow["Description"].ToString());
                        TemplateHandler.Assign(ref strTmp, "SPName_val", currentRow["SPName"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Details_val", logDetail);
                        TemplateHandler.Assign(ref strTmp, "IP_val", currentRow["IP"].ToString());
                        TemplateHandler.Assign(ref strTmp, "LastLoginIPCheckerURL-val", AdminCommon.GetIPCheckerURL(currentRow["IP"].ToString()));
                        TemplateHandler.Assign(ref strTmp, "color", bgColor);
                        #endregion

                        strBuild.Append(strTmp);
                    }
                }
                return intNo > 0 ? strBuild.ToString() : AdminCommon.NoInfo(cols);
            }
            return AdminCommon.NoInfo(cols);
        }
    }
}

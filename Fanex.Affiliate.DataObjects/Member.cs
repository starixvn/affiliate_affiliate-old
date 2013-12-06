// -----------------------------------------------------------------------
// <copyright file="Member.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using System.Data;
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;    
    using System;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Member
    {
        public static DataTable SelMemInfoAll(string strAffUsername, string strUsername,int afftype, ref int pageIndex, int intPageSize, ref int totalPages, ref int NRows)
        {
            if (strAffUsername.Contains(":"))
                strAffUsername = strAffUsername.Remove(strAffUsername.IndexOf(':')).Trim();
            DbResult dbr = null;
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(strAffUsername))
                hsParam.Add("affusername", strAffUsername);
            hsParam.Add("username", strUsername);
            if (afftype > 0)
                hsParam.Add("afftype", afftype);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("pagesize", intPageSize); // hardcode pagesize for member/player list
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            dbr = DbHelper.Execute("Aff_MemberListSel", hsParam, DBType.AFFILIATE);
            totalPages = CastAide.AsInt32(dbr.Outputs["totalpages"]);
            NRows = CastAide.AsInt32(dbr.Outputs["nrows"]);
            pageIndex = CastAide.AsInt32(dbr.Outputs["pageindex"]);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }
        public static decimal GetPSPToday()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("psp", 0);
            DbResult dbr = DbHelper.Execute("DEP_AffiliateGetPSP", hsParam, DBType.AFFILIATE);
            return CastAide.AsDecimal(dbr.Outputs["psp"]);
        }
        public static string DeleteMember(string username)
        {
            Hashtable param = new Hashtable();
            param.Add("username", username);
            param.Add("userid",HttpContext.Current.Session["CustID"]);
            param.Add("ip", Utilities.GetIPAddressOfClient());
            DbResult ret = DbHelper.Execute("Aff_MemberDel", param, DBType.AFFILIATE);
            return ret.Outputs["errmsg"].ToString();
        }
        /// <summary>
        /// Get member information
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static DataTable SelMemInfo(string username)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("username", username);
            DbResult dbr = DbHelper.Execute("Aff_MemberSel", hsParam, DBType.AFFILIATE);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }



        /// <summary>
        /// Get member from deposit DB
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static DataTable SelDepositMemInfo(int AffId, DateTime dtmFDate, DateTime dtmTDate, int intMemberType, bool isGetAll, int intPage, int intPageSize, ref int intTotalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affiliateid", AffId);
            if (!isGetAll)
            {
                hsParam.Add("fromdate", dtmFDate);
                hsParam.Add("todate", dtmTDate);
            }
            hsParam.Add("membertype", intMemberType);
            hsParam.Add("pageindex", intPage);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("totalpages", intTotalPages);
            DbResult dbr = DbHelper.Execute("DEP_AffiliateSelMemberList", hsParam, DBType.AFFILIATE);
            intTotalPages = CastAide.AsInt32(dbr.Outputs["totalpages"]);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }


        /// <summary>
        /// Get Meberlist depend on affid
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static DataTable SelMemberListByAffId(int AffId, DateTime dtmFDate, DateTime dtmTDate, int intMemberType, bool isGetAll, int intPage, int intPageSize, ref int intTotalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affiliateid", AffId);
            if (!isGetAll)
            {
                hsParam.Add("fromdate", dtmFDate);
                hsParam.Add("todate", dtmTDate);
            }
            hsParam.Add("membertype", intMemberType);
            hsParam.Add("pageindex", intPage);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("totalpages", intTotalPages);
            DbResult dbr = DbHelper.Execute("Aff_AffiliateSelMemberList", hsParam, DBType.AFFILIATE);
            intTotalPages = CastAide.AsInt32(dbr.Outputs["totalpages"]);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }


        /*smith*/
        public static string InsertMember(string affUserName, string userName, string depositCustId,
            string firstName, string lastName, string currency, string email, int ibcCustId,
            string ibcUserName, int agentId, DateTime? createdDate, DateTime registerDate, string btag, long clickid, string lang = "en-US")
        {
            Hashtable param = new Hashtable();
            param.Add("affusername", affUserName);
            param.Add("username", userName);
            param.Add("depositcustid", depositCustId);
            param.Add("firstname", firstName);
            param.Add("lastname", lastName);
            param.Add("currency", currency);
            param.Add("email", email);
            param.Add("ibccustid", ibcCustId);
            param.Add("ibcusername", ibcUserName);
            param.Add("agentid", agentId);
            if(!createdDate.Equals(DateTime.MinValue))
                param.Add("createddate", createdDate);
            param.Add("registerdate", registerDate);
            param.Add("btag", btag);
            param.Add("clickid", clickid);

            param.Add("ip", Utilities.GetIPAddressOfClient());
            param.Add("language", lang);
            param.Add("errcode", 0);
            param.Add("errmsg", string.Empty);

            DbResult data = DbHelper.Execute("Aff_CustomerInsMemberManually", param, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? true.ToString() : data.Outputs["errmsg"].ToString();
        }
        /*smith*/
        //public static DataRow MemberExistingCheck(string depUserName, string systemName)
        //{
        //    Hashtable param = new Hashtable();
        //    param.Add("depusername", depUserName);
        //    param.Add("systemname", systemName);

        //    DbResult data = DbHelper.Execute("Aff_IBCMemberExistingCheck", param, DBType.ARCHIVE);
        //    if (data.ResultSet.Tables != null && data.ResultSet.Tables[0].Rows.Count > 0)
        //    {
        //        return data.ResultSet.Tables[0].Rows[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /*joseph*/
        public static void CheckMemberExist(string memberName, string affName, ref int affid, ref string affStatus,ref bool isMemberExist)
        {
            Hashtable param = new Hashtable();
            param.Add("membername", memberName);
            param.Add("affname", affName);

            DbResult data = DbHelper.Execute("Aff_CheckMemberExist", param, DBType.AFFILIATE);
            
            affid = CastAide.AsInt32(data.Outputs["affid"],0);
            affStatus = CastAide.AsString(data.Outputs["affstatus"], String.Empty);                        
            isMemberExist = CastAide.AsBoolean(data.Outputs["ismemberexist"], false);                        
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Services
{
    using System;
    using System.Collections;    
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;
    using Fanex.Affiliate.DataObjects;
    using DNAide.Data;
    

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataAccess
    {
        private const string DB_NAME = "affiliate";

        public static void ImportMemberInfo(string xmlData, DateTime registeredDate, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            
            hsParam.Add("adminid", Common.AdminID);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("registereddate", registeredDate);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            DbResult rs = Fanex.Affiliate.DataObjects.DbHelper.Execute("Task_Aff_MemberInfoImportData", hsParam, DB_NAME);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);            
        }
        public static IEnumerable<string> GetMembersMissingCreatedDate()
        {
           DbResult rs =  Fanex.Affiliate.DataObjects.DbHelper.Execute("Aff_MemberInfoGetMissingCreatedDate", DB_NAME);
            
            if (rs.ResultSet.Tables.Count > 0 && rs.ResultSet.Tables[0].Rows.Count > 0)
            {
                return rs.ResultSet.Tables[0].Rows.Cast<DataRow>().Select( r => r[0].ToString()) ;
            }
            return null;
        }

        public static void UpdateFirstDepositDate(string xml, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();

            hsParam.Add("xmldata", xml);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult rs =  Fanex.Affiliate.DataObjects.DbHelper.Execute("Task_Aff_MemberInfoUpdateCreatedDate", hsParam, DB_NAME);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);
        }
    }
}

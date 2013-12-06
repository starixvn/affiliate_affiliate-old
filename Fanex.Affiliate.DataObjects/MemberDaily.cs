// -----------------------------------------------------------------------
// <copyright file="MemberDaily.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Data;
    using System.Collections;
    using DNAide.Data;
    using DNAide.Core;
    using Fanex.Affiliate.Model;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MemberDaily
    {

        public static DataTable GetMembers(DateTime? date)
        {
            if (!date.HasValue)
                return null;
            Hashtable hsParam = new Hashtable();

            hsParam.Add("date", date);

            DbResult rs = DbHelper.Execute("Aff_GetMembers", hsParam, "affiliate", 1200);
            return rs.ResultSet.Tables[0];
        }

        // Get memberdaily data
        public static DataTable GetPlayersWinlostByProduct(string xml, string usernamePrefix, DateTime? date)
        {
            /*  using (ObjectDb db = new ObjectDb("Aff_PlayersWinlostByProductSel"))
              {

                  var dataSet = db.ExecuteDataSet(new { LicenseeDBName = licenseeDBName, Prefix = usernamePrefix, Date = date });
                  var datatable = dataSet.Tables;

                  if (datatable.Count > 0)
                  {
                      return datatable[0];
                  }
                  return null;
              }         */
            Hashtable hsParam = new Hashtable();

            hsParam.Add("xml", xml);
            hsParam.Add("prefix", usernamePrefix);
            hsParam.Add("date", date);

            DbResult rs = DbHelper.Execute("Aff_PlayersWinlostByProductSel", hsParam, DBType.ARCHIVE);
            var datatable = rs.ResultSet.Tables[0];
            return datatable;
        }
        // Get memberdaily transaction data
        public static DataTable GetPlayersFunding(string licenseeDBName, string usernamePrefix, DateTime? date)
        {
            /* using (ObjectDb db = new ObjectDb("Aff_PlayersFundingSel"))
             {

                 var dataSet = db.ExecuteDataSet(new { LicenseeDBName = licenseeDBName, Prefix = usernamePrefix, Date = date });
                 var datatable = dataSet.Tables;

                 if (datatable.Count > 0)
                 {
                     return datatable[0];
                 }
                 return null;
             }    */

            Hashtable hsParam = new Hashtable();

            hsParam.Add("xml", licenseeDBName);
            hsParam.Add("prefix", usernamePrefix);
            hsParam.Add("date", date);

            DbResult rs = DbHelper.Execute("Aff_PlayersFundingSel", hsParam, DBType.ARCHIVE);
            var datatable = rs.ResultSet.Tables[0];
            return datatable;
        }

        public static void ImportMemberDaily(string xmldata, DateTime? date, ref int errCode, ref string errMsg)
        {
            /* using (ObjectDb objectDb = new ObjectDb("Task_Aff_MemberDailyImportData"))
             {
                 objectDb.AddInParameter("adminid", DbType.Int32, Common.AdminID);
                 objectDb.AddInParameter("xmldata", DbType.String, xmldata);
                 objectDb.AddInParameter("winlostdate", DbType.DateTime, date);
                 objectDb.AddOutParameter("errcode", DbType.Int32,10);
                 objectDb.AddOutParameter("errmsg", DbType.String, 255);

                 objectDb.ExecuteNonQuery();
                 var outparams = objectDb.GetOuputParams();
                 errCode = Convert.ToInt32(outparams["errcode"]);
                 errMsg = Convert.ToString(outparams["errmsg"]);              

             }    */
            Hashtable hsParam = new Hashtable();

            hsParam.Add("adminid", 9999);
            hsParam.Add("xmldata", xmldata);
            hsParam.Add("winlostdate", date);
            hsParam.Add("errcode", errCode);
            hsParam.Add("errmsg", errMsg);

            //return DbHelper.Execute("Aff_CasinoImportData", hsParam, Common.DBType.Affiliate);
            DbResult rs = DbHelper.Execute("Task_Aff_MemberDailyImportData", hsParam, DBType.AFFILIATE);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);

        }
        public static void ImportMemberDailyTransaction(string xmldata, DateTime? date, ref int errCode, ref string errMsg)
        {
            /* using (ObjectDb objectDb = new ObjectDb("Task_Aff_MemberDailyTransactionImportData"))
             {
                 objectDb.AddInParameter("adminid", DbType.Int32, Common.AdminID);
                 objectDb.AddInParameter("xmldata", DbType.String, xmldata);
                 objectDb.AddInParameter("winlostdate", DbType.DateTime, date);
                 objectDb.AddOutParameter("errcode", DbType.Int32, 10);
                 objectDb.AddOutParameter("errmsg", DbType.String, 255);

                 objectDb.ExecuteNonQuery();
                 var outparams = objectDb.GetOuputParams();
                 errCode = Convert.ToInt32(outparams["errcode"]);
                 errMsg = Convert.ToString(outparams["errmsg"]);

             }   */

            Hashtable hsParam = new Hashtable();

            hsParam.Add("adminid", 9999);
            hsParam.Add("xmldata", xmldata);
            hsParam.Add("winlostdate", date);
            hsParam.Add("errcode", errCode);
            hsParam.Add("errmsg", errMsg);

            //return DbHelper.Execute("Aff_CasinoImportData", hsParam, Common.DBType.Affiliate);
            DbResult rs = DbHelper.Execute("Task_Aff_MemberDailyTransactionImportData", hsParam, DBType.AFFILIATE);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);

        }
    }
}

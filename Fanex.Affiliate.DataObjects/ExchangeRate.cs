// -----------------------------------------------------------------------
// <copyright file="ExchangeRate.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Web;
    using System.Collections;
    using System.Data;
    using DNAide.Data;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ExchangeRate
    {
        #region Services 

        
        //insert TaskLog        	

        public static int InsertTaskLog(int taskID, DateTime runDate, string status)
        {

            Hashtable hsParam = new Hashtable();

            int logID = 0;
            hsParam.Add("taskid", taskID);
            hsParam.Add("rundate", runDate);
            hsParam.Add("status", status);
            hsParam.Add("logid", logID);


            DbResult rs = DbHelper.Execute("Aff_TaskLogIns", hsParam, "affiliate", 1200);
            logID = Convert.ToInt32(rs.Outputs["logid"]);
            return logID;
        }
        //Update Task Status

        public static void UpdateTaskStatus(int logID, string status)
        {

            Hashtable hsParam = new Hashtable();
            hsParam.Add("status", status);
            hsParam.Add("logid", logID);
            DbHelper.Execute("Aff_TaskLogUpdStatus", hsParam, "affiliate", 1200);
        }



        public static void ImportData(string xmlData, DateTime date, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("date", date);
            hsParam.Add("xml", xmlData);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            DbResult rs = DbHelper.Execute("Task_aff_DailyImportExchange", hsParam, DBType.AFFILIATE, 1200);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);
        }
        public static DataTable GetExchangeDailyOanda(DateTime date)
        {
            DbResult rs = DbHelper.Execute("ExchangeDailyOanda_SelByDate", "archive");

            Hashtable hsParam = new Hashtable();
            hsParam.Add("date",date);

            if (rs.ResultSet.Tables.Count > 0 && rs.ResultSet.Tables[0].Rows.Count > 0)
            {
                return rs.ResultSet.Tables[0];
            }
            return null;
        }


        #endregion

        public static string ToggleAutoUpdateRate(int exId)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("custid", HttpContext.Current.Session["CustID"]);
            hsParams.Add("exchangeid", exId);
            hsParams.Add("ip", Utilities.GetIPAddressOfClient());
            var data = DbHelper.Execute("Aff_ExchangeAutoRefreshUpd", hsParams, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }
        public static string UpdateRatesFromMarket(string ids)
        {
            var hsParams = new Hashtable
                               {
                                   {"custid", HttpContext.Current.Session["CustID"]},
                                   {"exchangeidlist", ids},
                                   {"ip", Utilities.GetIPAddressOfClient()}
                               };
            var data = DbHelper.Execute("Aff_CopyRateFromMarket", hsParams, DBType.AFFILIATE);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            return result == 0 ? "True" : data.Outputs["errmsg"].ToString();
        }
        public static DataTable Exchange_Sel(DateTime dtmTime)
        {
            var hsParams = new Hashtable();
            if (DateTime.Now.Subtract(dtmTime).Days == 0)
            {
                hsParams.Add("time", null);
            }
            else
            {
                hsParams.Add("time", dtmTime);
            }

            var data = DbHelper.Execute("Aff_ExchangeSel", hsParams, DBType.AFFILIATE);            
            return data.ResultSet.Tables[0];
        }
        public static DbResult Exchange_Update(int exId, decimal rate, string tocurrency)
        {
            var hsParams = new Hashtable();
            //hsParams.Add("affmaster", AppSettings.AffMaster);
            hsParams.Add("custid", HttpContext.Current.Session["CustID"]);
            hsParams.Add("exchangeid", exId);
            hsParams.Add("tocurrency", tocurrency);
            hsParams.Add("rate", rate);
            hsParams.Add("ip", Utilities.GetIPAddressOfClient());
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_ExchangeUpdate", hsParams, DBType.AFFILIATE);
        }

        public static DbResult AddExchanges(string currencies,string data)
        {
            var hsParams = new Hashtable();
            hsParams.Add("currencies", currencies);
            hsParams.Add("data", data);
            hsParams.Add("custid", HttpContext.Current.Session["CustID"]);
            hsParams.Add("ip", Utilities.GetIPAddressOfClient());
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AddExchanges", hsParams, DBType.AFFILIATE);            
        }


        public static DbResult AddAffCurrencies(string data)
        {
            var hsParams = new Hashtable();            
            hsParams.Add("data", data);
            hsParams.Add("custid", HttpContext.Current.Session["CustID"]);
            hsParams.Add("ip", Utilities.GetIPAddressOfClient());
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AddAffCurrencies", hsParams, DBType.AFFILIATE);   
        }
    }
}

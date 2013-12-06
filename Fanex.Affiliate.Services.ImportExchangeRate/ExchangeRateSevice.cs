// -----------------------------------------------------------------------
// <copyright file="Sevice.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Services
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using System.Threading;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Model;
    using DNAide.Web;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ExchangeRateSevice
    {
        internal static void ImportExchangeRate(DateTime date)
        {

            int errcode = 0;
            string errmsg = null;

            
            var xml = AppCommon.ConvertToXML(ExchangeRate.GetExchangeDailyOanda(date)).OuterXml;

            if (xml == "<root />")
                return;

            CacheFactory.GetCacheManager().Add("_IsTaskRunning", true);            

            ExchangeRate.ImportData(xml, date, ref errcode, ref errmsg);            
        }
        /// <summary>
        /// Get List Customer register on this date
        /// </summary>
        public static void ImportData(ScheduleTask _task)
        {
            string startTime, endTime;
            if (AppCommon.GetDownTime(out startTime, out endTime) || Convert.ToBoolean(CacheFactory.GetCacheManager().GetData("_IsTaskRunning")))
                return;

            if (Common.FromDate != null && Common.ToDate != null && !Convert.ToBoolean(CacheFactory.GetCacheManager().GetData("_hasRunImportFromTo")))
            {
                var date = Common.FromDate.Value;
                CacheFactory.GetCacheManager().Add("_IsTaskRunning", true);
                for (int i = 0; i <= (Common.ToDate.Value - Common.FromDate.Value).TotalDays; i++)
                {
                    int logID = TaskManager.InsertTaskLog(_task.TaskID, date, "Processing");
                    ExchangeRateSevice.ImportExchangeRate(date);
                    
                    date = date.AddDays(1);
                    TaskManager.UpdateTaskStatus(logID, "Completed");
                }
                CacheFactory.GetCacheManager().Add("_hasRunImportFromTo", true);
            }
            else
            {
                DateTime fullDateTime;
                var date = ServerDateTime.GetCurrenDate(out fullDateTime);
                var arr = _task.BeginTime.Split(':');
                var currentTotalSeconds = fullDateTime.Hour * 3600 + fullDateTime.Minute * 60;
                int logID = 0; //DataAccess.InsertTaskLog(_task.TaskID, date, "Processing");

                if (arr.Length > 1 && fullDateTime.Hour == Convert.ToInt32(arr[0]) && fullDateTime.Minute == Convert.ToInt32(arr[1]))
                {
                    DNAideLog.Write("Import Data case 1");
                    CacheFactory.GetCacheManager().Add("_IsTaskRunning", true);
                    var yesterday = date.AddDays(-1);
                    logID = TaskManager.InsertTaskLog(_task.TaskID, yesterday, "Processing");
                    ExchangeRateSevice.ImportExchangeRate(date);
                    TaskManager.UpdateTaskStatus(logID, "Completed");
                    Thread.Sleep(60000);
                }
                else if (arr.Length > 1 && (currentTotalSeconds - (Convert.ToInt32(arr[0]) * 3600 + Convert.ToInt32(arr[1]) * 60)) % _task.IntervalTime == 0)
                {
                    DNAideLog.Write("Import Data case 2");
                    CacheFactory.GetCacheManager().Add("_IsTaskRunning", true);
                    logID = TaskManager.InsertTaskLog(_task.TaskID, date, "Processing");
                    ExchangeRateSevice.ImportExchangeRate(date);
                    TaskManager.UpdateTaskStatus(logID, "Completed");
                    Thread.Sleep(60000);
                }
                else
                {
                    DNAideLog.Write("Do not do anything");
                }

            }
            CacheFactory.GetCacheManager().Add("_IsTaskRunning", false);          
        }
    }
}

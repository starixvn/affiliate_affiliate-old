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
    public static class MemberDailySevice
    {
         internal static void ImportMemberDaily(DateTime? date = null)
        {
            var xml = AppCommon.ConvertToXML(MemberDaily.GetMembers(date)).OuterXml;
           
             DNAideLog.Write("ImportMemberDaily.Xml=" + xml);

            var table = MemberDaily.GetPlayersWinlostByProduct(xml, Common.UsernamePrefix, date);
            //  var count = table.Rows.Count % 1000;
            var xmlDoc = AppCommon.ConvertToXML(table);
            DNAideLog.Write("GetPlayersWinlostByProduct.OuterXml=" + xmlDoc.OuterXml);
            int errcode = 0;
            string errmsg = null;
            MemberDaily.ImportMemberDaily(xmlDoc.OuterXml, date, ref errcode, ref errmsg);
            DNAideLog.Write("GetPlayersWinlostByProduct.errmsg=" + errmsg);
        }

        internal static void ImportMemberDailyTransaction(DateTime? date = null)
        {
            var xml = AppCommon.ConvertToXML(MemberDaily.GetMembers(date)).OuterXml;
            var table = MemberDaily.GetPlayersFunding(xml, Common.UsernamePrefix, date);
            //  var count = table.Rows.Count % 1000;
            var xmlDoc = AppCommon.ConvertToXML(table);
            int errcode = 0;
            string errmsg = null;
            MemberDaily.ImportMemberDailyTransaction(xmlDoc.OuterXml, date, ref errcode, ref errmsg);
        }

        public static void ImportData(ScheduleTask _task)
        {
            string startTime, endTime;            
            if (AppCommon.GetDownTime(out startTime, out endTime)||Convert.ToBoolean(CacheFactory.GetCacheManager().GetData("_IsTaskRunning")))
                return;

          if (Common.FromDate != null && Common.ToDate != null && !Convert.ToBoolean(CacheFactory.GetCacheManager().GetData("_hasRunImportFromTo")))
            {
                var date = Common.FromDate.Value;
                CacheFactory.GetCacheManager().Add("_IsTaskRunning", true);
                for (int i = 0; i <= (Common.ToDate.Value - Common.FromDate.Value).TotalDays; i++)
                {
                    int logID = TaskManager.InsertTaskLog(_task.TaskID, date, "Processing");
                    MemberDailySevice.ImportMemberDaily(date);
                    MemberDailySevice.ImportMemberDailyTransaction(date);
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
                    MemberDailySevice.ImportMemberDaily(yesterday);
                    MemberDailySevice.ImportMemberDailyTransaction(yesterday);
                    TaskManager.UpdateTaskStatus(logID, "Completed");
                    Thread.Sleep(60000);
                }
                else if (arr.Length > 1 && (currentTotalSeconds - (Convert.ToInt32(arr[0]) * 3600 + Convert.ToInt32(arr[1]) * 60)) % _task.IntervalTime == 0)
                {
                    DNAideLog.Write("Import Data case 2");
                    CacheFactory.GetCacheManager().Add("_IsTaskRunning", true);
                    logID = TaskManager.InsertTaskLog(_task.TaskID, date, "Processing");
                    MemberDailySevice.ImportMemberDaily(date);
                    MemberDailySevice.ImportMemberDailyTransaction(date);
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

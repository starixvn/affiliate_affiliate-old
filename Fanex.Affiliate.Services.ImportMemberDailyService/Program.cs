using System.ServiceProcess;
using System;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using DNAide.Web;
using Fanex.Affiliate.Model;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;

namespace Fanex.Affiliate.Services
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {          
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                 { 
                     new ImportMemberDailyService() 
                  };
                ServiceBase.Run(ServicesToRun);

            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message + "__" + ex.StackTrace);
            }
            /*
           Test();
            Console.ReadLine();*/
        

        }

        static void Test()
        {
            var date = DateTime.Now;
            
            MemberDailySevice.ImportMemberDailyTransaction(date);
            //string _license = Common.FullAffiliateDBName;
      
            // ScheduleTask _task;                   

            // CacheFactory.GetCacheManager().Add("_hasRunImportFromTo", false);

            // CacheFactory.GetCacheManager().Add("_IsTaskRunning", false);            

            // _task = TaskManager.GetTaskSchedule("Import Member Daily Data");

            // MemberDailySevice.ImportData(_task);


        }
    }
}

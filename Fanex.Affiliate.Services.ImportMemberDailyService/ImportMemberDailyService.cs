using System;
using System.ServiceProcess;
using System.Timers;

using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Fanex.Affiliate.Model;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;
using DNAide.Web;

namespace Fanex.Affiliate.Services
{
    public partial class ImportMemberDailyService : ServiceBase
    {
        private static System.Timers.Timer _timer;

        private static string _license = null;
      
        private static ScheduleTask _task;
        

        public ImportMemberDailyService()
        {            
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Peter@20130418 : replace LicenseeName by AffiliateDB Name
            //_license = DataAccess.GetLicenseeName();
            _license = Common.FullAffiliateDBName;

            CacheFactory.GetCacheManager().Add("_hasRunImportFromTo", false);

            CacheFactory.GetCacheManager().Add("_IsTaskRunning", false);


            _task = TaskManager.GetTaskSchedule("Import Member Daily Data");

            // ImportData();

            _timer = new System.Timers.Timer(60 * 1000);

            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            _timer.Enabled = true;     

        }

        protected override void OnStop()
        {

        }
       
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                DNAideLog.Write("OnTimedEvent at " + DateTime.Now);
                MemberDailySevice.ImportData(_task); 

            }
            catch (Exception ex)
            {
                DNAideLog.Write("Error: "+ ex.Message);
            }
           
        }

        
    }
}

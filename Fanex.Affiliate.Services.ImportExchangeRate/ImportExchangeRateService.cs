using System;
using System.ServiceProcess;
using DNAide.Web;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Fanex.Affiliate.Model;
using Fanex.Affiliate.DataObjects;
using System.Timers;
using Fanex.Affiliate.Core;

namespace Fanex.Affiliate.Services
{
    public partial class ImportExchangeService : ServiceBase
    {
        private static System.Timers.Timer _timer;
        private static ScheduleTask _task;

        public ImportExchangeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {

                CacheFactory.GetCacheManager().Add("_hasRunImportFromTo", false);

                CacheFactory.GetCacheManager().Add("_IsTaskRunning", false);


                _task = TaskManager.GetTaskSchedule("Import Exchange Daily");

                // ImportData();

                _timer = new System.Timers.Timer(60 * 1000);

                _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

                _timer.Enabled = true;

            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message + "__" + ex.StackTrace);
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                DNAideLog.Write("OnTimedEvent at " + DateTime.Now);
                ExchangeRateSevice.ImportData(_task);

            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message + "__" + ex.StackTrace);
            }
        }

    }
}

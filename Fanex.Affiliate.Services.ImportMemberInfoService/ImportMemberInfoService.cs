using System;
using System.ServiceProcess;
using DNAide.Web;
using System.Threading;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core;

namespace Fanex.Affiliate.Services
{
    public partial class ImportMemberInfoService : ServiceBase
    {
        private Timer serviceTimer;

        public ImportMemberInfoService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TimerCallback timerDelegate = new TimerCallback(DoWork);
            //call Dowork after the time is configured as TimeInterval in web.config
            serviceTimer = new Timer(timerDelegate, null, Common.DueTime * 1000, Common.Interval * 1000);
        }

        private void DoWork(object state)
        {
            DNAideLog.Write("OnTimedEvent at " + DateTime.Now);
            ImportData();
            UpdateData();
        }
        /// <summary>
        /// Get List Customer register on this date
        /// </summary>
        static public void ImportData()
        {
            try
            {
                //var item1 = Fanex.Affiliate.Core.ServiceWrapper.GetLicenseeCustInfo(Common.SystemName, "testAPI12");
                //var item2 = Fanex.Affiliate.Core.ServiceWrapper.GetLicenseeCustInfo(Common.SystemName, "testwithdraw");
                //var item3 = Fanex.Affiliate.Core.ServiceWrapper.GetLicenseeCustInfo(Common.SystemName, "johnny");
                //var item4 = Fanex.Affiliate.Core.ServiceWrapper.GetLicenseeCustInfo(Common.SystemName, "testapi22");
                //var item5 = Fanex.Affiliate.Core.ServiceWrapper.GetLicenseeCustInfo(Common.SystemName, "rossa");
                //return;
                int errcode = 0;
                string errmsg = null;

                //If there is no setting fromdate and todate => get data for current Date
                if (Common.FromDate == null || Common.ToDate == null)
                {

                    DateTime fullDateTime;
                    var date = ServerDateTime.GetCurrenDate(out fullDateTime);

                    var xml = Common.GetData(date).OuterXml;
                    DNAideLog.Write("SystemName:" + AppSettings.SystemName + ".Common.GetData=" + xml);

                    if (xml != "<root />")
                    {
                        DataAccess.ImportMemberInfo(xml, date, ref errcode, ref errmsg);
                        DNAideLog.Write("DataAccess.ImportMemberInfo=" + errmsg);
                    }
                    if (DateTime.Now.Hour == 1)//Run for yesterday
                    {
                        xml = Common.GetData(date.AddDays(-1)).OuterXml;
                        if (xml != "<root />")
                            DataAccess.ImportMemberInfo(xml, date.AddDays(-1), ref errcode, ref errmsg);
                    }

                }
                else
                {

                    var date = Common.FromDate.Value;
                    for (int i = 0; i <= (Common.ToDate.Value - Common.FromDate.Value).TotalDays; i++)
                    {
                        var xml = Common.GetData(date).OuterXml;
                        //Insert data into MemberInfo table
                        if (xml != "<root />")
                            DataAccess.ImportMemberInfo(xml, date, ref errcode, ref errmsg);
                        date = date.AddDays(1);
                    }
                }
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message +
                        "<br />SOURCE: " + ex.Source +
                        "<br />TARGETSITE: " + ex.TargetSite +
                        "<br />STACKTRACE: " + ex.StackTrace);
            }
        }

        public static void UpdateData()
        {
            try
            {
                //return;
                int errcode = 0;
                string errmsg = null;

                var list = DataAccess.GetMembersMissingCreatedDate();
                if (list != null)
                {
                    var xml = Common.GetFirstDepositDateData(list).OuterXml;
                    if (xml != "<root />")
                        DataAccess.UpdateFirstDepositDate(xml, ref errcode, ref errmsg);
                }
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message +
                        "<br />SOURCE: " + ex.Source +
                        "<br />TARGETSITE: " + ex.TargetSite +
                        "<br />STACKTRACE: " + ex.StackTrace);
            }
        }
    }
}

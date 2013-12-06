using System.ServiceProcess;
using System;
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
            //int errcode = 0;
            //string errmsg = null;

            //if (Common.FromDate == null || Common.ToDate == null)
            //{
            //    var xml = Common.GetData(DateTime.Now).OuterXml;
            //    if (xml != "<root />")
            //        DataAccess.ImportMemberInfo(xml, DateTime.Now, ref errcode, ref errmsg);
            //}
            //var date = Common.FromDate.Value;
            //for (int i = 0; i < (Common.ToDate.Value - Common.FromDate.Value).TotalDays; i++)
            //{
            //    var xml = Common.GetData(date).OuterXml;
            //    if (xml != "<root />")
            //        DataAccess.ImportMemberInfo(xml, date, ref errcode, ref errmsg);
            //    date.AddDays(1);
            //}


            //Console.Read();
            //int errcode = 0;
            //string errmsg = null;

            //var list = DataAccess.GetMembersMissingCreatedDate();
            //if (list != null)
            //{
            //    var xml = Common.GetFirstDepositDateData(list).OuterXml;
            //    if (xml != "<root />")
            //        DataAccess.UpdateFirstDepositDate(xml, ref errcode, ref errmsg);
            //}
            //Console.Read();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ImportMemberInfoService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
        
    }
}

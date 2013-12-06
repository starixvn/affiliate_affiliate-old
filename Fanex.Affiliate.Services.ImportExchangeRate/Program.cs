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
            var a = AppSettings.UMStartTime;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ImportExchangeService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
        
    }
}

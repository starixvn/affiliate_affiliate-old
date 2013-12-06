// -----------------------------------------------------------------------
// <copyright file="ScheduleTask.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.Model
{
    using System;
       /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScheduleTask
    {
        public int TaskID;
        public string TaskName;
        public string Description;
        public int Creator;
        public DateTime LastRun;
        public int Priority;
        public DateTime StartedDate;
        public string Schedule;
        public string Command;
        public string BeginTime;
        public int IntervalTime;

    }
}

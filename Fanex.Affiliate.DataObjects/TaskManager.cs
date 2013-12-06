// -----------------------------------------------------------------------
// <copyright file="TaskManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections;
    using DNAide.Data;
    using Fanex.Affiliate.Model;
    using DNAide.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TaskManager
    {
        //insert TaskLog        	

        public static int InsertTaskLog(int taskID, DateTime runDate, string status)
        {

            /*    int logID=0;

                using (ObjectDb objectDb = new ObjectDb("Aff_TaskLogIns"))
                {
                    objectDb.AddInParameter("taskid",DbType.Int32,taskID);
                    objectDb.AddInParameter("rundate", DbType.DateTime, runDate);
                    objectDb.AddInParameter("status", DbType.String, status);
                    objectDb.AddOutParameter("logid",DbType.Int32,10);
                
                    objectDb.ExecuteNonQuery();
                    var outparams = objectDb.GetOuputParams();
                    logID = Convert.ToInt32(outparams["logid"]);

                }      
                return logID;*/
            Hashtable hsParam = new Hashtable();

            int logID = 0;
            hsParam.Add("taskid", taskID);
            hsParam.Add("rundate", runDate);
            hsParam.Add("status", status);
            hsParam.Add("logid", logID);


            DbResult rs = DbHelper.Execute("Aff_TaskLogIns", hsParam, DBType.AFFILIATE);
            logID = Convert.ToInt32(rs.Outputs["logid"]);
            return logID;
        }
        //Update Task Status

        public static void UpdateTaskStatus(int logID, string status)
        {
            /*
            using (ObjectDb db = new ObjectDb("Aff_TaskLogUpdStatus"))
            {
                db.ExecuteNonQuery(new { Status = status, LogID = logID });                
            }      */
            Hashtable hsParam = new Hashtable();
            hsParam.Add("status", status);
            hsParam.Add("logid", logID);
            DbHelper.Execute("Aff_TaskLogUpdStatus", hsParam, DBType.AFFILIATE);
        }

        // Get Task Detail By Name
        public static ScheduleTask GetTaskSchedule(string taskName)
        {
            /*  ScheduleTask task = new ScheduleTask();

              using (ObjectDb db = new ObjectDb("Aff_TaskLogUpdStatus"))
              //using (ObjectDb db = new ObjectDb("Aff_TaskDetailSelByName",1200))
              {
                                
                  var dataSet = db.ExecuteDataSet(new { TaskName = taskName });
                  var datatable = dataSet.Tables;
                  if (datatable.Count > 0 && datatable[0].Rows.Count > 0)
                  {
                      task.BeginTime = datatable[0].Rows[0]["begintime"].ToString();
                      task.Command = datatable[0].Rows[0]["command"].ToString();
                      task.Creator =Convert.ToInt32(datatable[0].Rows[0]["creator"]);
                      task.Description =Convert.ToString(datatable[0].Rows[0]["description"]);
                      task.IntervalTime = Convert.ToInt32(datatable[0].Rows[0]["intervaltime"]);
                      task.LastRun = Convert.ToDateTime(datatable[0].Rows[0]["lastrun"]);
                      task.Priority = Convert.ToInt32(datatable[0].Rows[0]["priority"]);
                      task.Schedule = Convert.ToString(datatable[0].Rows[0]["schedule"]);
                      task.StartedDate = Convert.ToDateTime(datatable[0].Rows[0]["starteddate"]);
                      task.TaskID = Convert.ToInt32(datatable[0].Rows[0]["taskid"]);
                      task.TaskName = Convert.ToString(datatable[0].Rows[0]["taskname"]);
                  }
              }            
            
              return task;*/
            ScheduleTask task = new ScheduleTask();
            Hashtable hsParam = new Hashtable();

            hsParam.Add("taskname", taskName);

            DbResult rs = DbHelper.Execute("Aff_TaskDetailSelByName", hsParam, DBType.AFFILIATE);
            var datatable = rs.ResultSet.Tables;

            if (datatable.Count > 0 && datatable[0].Rows.Count > 0)
            {
                task.BeginTime = datatable[0].Rows[0]["begintime"].ToString();
                task.Command = datatable[0].Rows[0]["command"].ToString();
                task.Creator = CastAide.AsInt32(datatable[0].Rows[0]["creator"], 0);
                task.Description = CastAide.AsString(datatable[0].Rows[0]["description"], string.Empty);
                task.IntervalTime = CastAide.AsInt32(datatable[0].Rows[0]["intervaltime"], 0);
                task.LastRun = CastAide.AsDateTime(datatable[0].Rows[0]["lastrun"], DateTime.Now);
                task.Priority = CastAide.AsInt32(datatable[0].Rows[0]["priority"], 0);
                task.Schedule = CastAide.AsString(datatable[0].Rows[0]["schedule"], string.Empty);
                task.StartedDate = CastAide.AsDateTime(datatable[0].Rows[0]["starteddate"], DateTime.Now);
                task.TaskID = CastAide.AsInt32(datatable[0].Rows[0]["taskid"], 0);
                task.TaskName = CastAide.AsString(datatable[0].Rows[0]["taskname"], string.Empty);
            }
            return task;
        }
    }
}

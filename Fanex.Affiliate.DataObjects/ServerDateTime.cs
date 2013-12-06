namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections;
    using DNAide.Data;
    using Fanex.Affiliate.Model;
    using DNAide.Core;

    public class ServerDateTime
    {
        public static DateTime GetCurrentDBTime(out string GMT)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("dbtime", DateTime.Now);
            hsParam.Add("gmtoffset", "GMT");

            DbResult result = DNAide.Data.DbHelper.Execute("Aff_AdminSelDBTime", hsParam, DBType.AFFILIATE);
            GMT = result.Outputs["gmtoffset"].ToString();
            return Convert.ToDateTime(result.Outputs["dbtime"]);
        }

        public static DateTime GetCurrenDate(out DateTime fullDateTime)
        {
            /* using (ObjectDb db = new ObjectDb("Aff_TaskDetailSelByName"))
             {

                 var dataSet = db.ExecuteDataSet();
                 var datatable = dataSet.Tables;
                 fullDateTime = DateTime.Now;
                 if (datatable.Count > 0 && datatable[0].Rows.Count > 0)
                 {
                     fullDateTime = Convert.ToDateTime(datatable[0].Rows[0][1]);
                     return Convert.ToDateTime(datatable[0].Rows[0][0]);
                 }
                 return DateTime.Now;
             }  */

            DbResult rs = DbHelper.Execute("Aff_CurrentDate", DBType.AFFILIATE);
            var datatable = rs.ResultSet.Tables[0];
            fullDateTime = DateTime.Now;
            if (datatable.Rows.Count == 0)
                return DateTime.Now;
            fullDateTime = CastAide.AsDateTime(datatable.Rows[0][1], DateTime.Now);
            return CastAide.AsDateTime(datatable.Rows[0][0], DateTime.Now);

        }
    }
}


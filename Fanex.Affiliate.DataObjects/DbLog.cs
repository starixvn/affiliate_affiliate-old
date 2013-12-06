// -----------------------------------------------------------------------
// <copyright file="DbLog.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using System.Collections;
    using DNAide.Data;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DbLog
    {
        public static void WriteLog(string typeName, string spName, string details, int creator, int custid, string ip )
        {
            var hsParam = new Hashtable
                          {
                              {"creator", creator},
                              {"affid", custid},
                              {"typename", typeName},
                              {"spname", spName},
                              {"ip", ip},
                              {"refno", 0},
                              {"details", details}
                          };
            DbHelper.Execute("Aff_WriteLog", hsParam, DBType.AFFILIATE);
        }
        public static DbResult LogTypeSel()
        {
            return DbHelper.Execute("Aff_AffiliateSelLogType", new Hashtable(), DBType.AFFILIATE);
        }
        public static DbResult LogSel(int affiliateID, int logType, DateTime dateFrom, DateTime dateTo, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("logtype", logType);
            hsParam.Add("datefrom", dateFrom);
            hsParam.Add("dateto", dateTo);
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pagecount", 0);
            hsParam.Add("rowcount", 0);
            return DbHelper.Execute("Aff_AffiliateSelLog", hsParam, DBType.AFFILIATE);
        }

    }
}

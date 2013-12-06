// -----------------------------------------------------------------------
// <copyright file="DownloadService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System;
    using DNAide.Data;
    using DNAide.Core;
    using System.Collections;
    using Fanex.Affiliate.Model;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DownloadService
    {
        public static int UpdateMemberDailyData(DateTime date, string productId, string data)
        {
            DbResult dbr = null;
            Hashtable htb = new Hashtable();
            int result = 0;

            htb.Add("winlostdate", date);
            htb.Add("product", productId);
            htb.Add("data", data);
            htb.Add("rowCount", result);
            
            dbr = DbHelper.Execute("Task_Aff_MemberDailySummary2", htb, DBType.AFFILIATE);
            result = CastAide.AsInt32(dbr.Outputs["rowCount"]);
            return result;
        }


        public static bool CheckDateMonthTransfer(DateTime date)
        {
            bool result = false;
            DbResult dbr = null;
            Hashtable htb = new Hashtable();

            htb.Add("date", date);
            htb.Add("result", result);

            dbr = DbHelper.Execute("Aff_CheckDateMonthTransfer", htb, DBType.AFFILIATE);
            result = CastAide.AsBoolean(dbr.Outputs["result"]);
            
            return result;
        }
    }
}

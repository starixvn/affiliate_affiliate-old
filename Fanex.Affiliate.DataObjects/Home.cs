// -----------------------------------------------------------------------
// <copyright file="Home.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using DNAide.Data;
    using Fanex.Affiliate.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Home
    {
        public static DbResult GetGenInforFromAff()
        {
            return DbHelper.Execute("Aff_HomeGeneralInfo", DBType.AFFILIATE);
        }

        public static DbResult GetGenInforFromDeposit()
        {
            return DbHelper.Execute("DEP_AffiliateGeneralInfo", DBType.AFFILIATE);
        }

        public static DbResult GetOnlineList(int pageSize, int pageIndex, bool isIT = false)
        {
            Hashtable param = new Hashtable();
            param.Add("pagesize", pageSize);
            param.Add("pageindex", pageIndex);
            param.Add("isit", isIT);
            return DbHelper.Execute("Aff_UserOnlineList", param, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
    }
}

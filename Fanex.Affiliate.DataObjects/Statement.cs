
/// <summary>
/// Summary description for Statement
/// </summary>
/// 
namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using DNAide.Data;
    using DNAide.Core;
    using System;
    using Fanex.Affiliate.Model;

    public class Statement
    {
        public static DbResult SelStatementHistory(int affID, int transType, object dtmFDate, object dtmTDate, int pageIndex, int pageSize, ref int totalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affID);
            if (transType != -1)
                hsParam.Add("transtype", transType);
            if (dtmFDate != null && dtmTDate != null)
            {
                if (Convert.ToDateTime(dtmFDate).CompareTo(Convert.ToDateTime(dtmTDate)) <= 0)
                {
                    hsParam.Add("fdate", dtmFDate);
                    hsParam.Add("tdate", dtmTDate);
                }
                else
                {
                    totalPages = 0;
                    return null;
                }
            }
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("pagesize", pageSize);

            hsParam.Add("totalpages", 0);
            DbResult rs = DbHelper.Execute("Aff_StatementHistorySel", hsParam, DBType.AFFILIATE, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);
            return rs;
        }
        //@Vinh
        //Statement history
        //public static DbResult SelStatementHistory1(int affID, int transType, object dtmFDate, object dtmTDate, int pageIndex, int pageSize, ref int totalPages)
        //{
        //    Hashtable hsParam = new Hashtable();
        //    hsParam.Add("affid", affID);
        //    if (transType != -1)
        //        hsParam.Add("transtype", transType);
        //    if (dtmFDate != null && dtmTDate != null)
        //    {
        //        if (Convert.ToDateTime(dtmFDate).CompareTo(Convert.ToDateTime(dtmTDate)) < 0)
        //        {
        //            hsParam.Add("fdate", dtmFDate);
        //            hsParam.Add("tdate", dtmTDate);
        //        }
        //        else
        //        {
        //            totalPages = 0;
        //            return null;
        //        }
        //    }

        //    hsParam.Add("pageindex", pageIndex);
        //    hsParam.Add("pagesize", pageSize);
        //    hsParam.Add("totalpages", 0);
        //    DbResult rs = DbHelper.Execute("Aff_StatementHistorySel", hsParam, DBType.AFFILIATE);
        //    totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);
        //    return rs;
        //}

    }
}

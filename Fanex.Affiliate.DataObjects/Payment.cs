using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Payment
/// </summary>
namespace Fanex.IndoPaul.DataObjects
{
    public static class Payment
    {
        public static Hashtable MinWithdraw()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");

            return DbHelper.Execute("Aff_MinAmountSel", hsParam, DBType.AFFILIATE).Outputs;
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="Account.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using DNAide.Utils;
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;

    public class Account
    {
        public static int ChangePass(int intCustId, string strOldPass, string strNewPass, string strLang)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", intCustId);
            hsParam.Add("userpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strNewPass.Trim()));
            hsParam.Add("oldpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(strOldPass.Trim()));
            hsParam.Add("userid", intCustId);
            hsParam.Add("language", strLang);
            hsParam.Add("ip", Utilities.GetIPAddressOfClient());
            hsParam.Add("errcode", "0");
            DbResult result = DbHelper.Execute("Aff_AdminUpdPwd", hsParam, DBType.AFFILIATE);
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
    }
}

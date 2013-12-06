namespace Fanex.Affiliate.DataObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Data;    
    using Fanex.Core.Utility;
    using Fanex.Affiliate.Model;
    using Fanex.Affiliate.DataObjects.Model;


    /// <summary>
    /// Summary description for Domain
    /// </summary>

    public class Domain
    {
        public Domain()
        {
        }

        public static DbResult CountrySel()
        {
            return DbHelper.Execute("Aff_CountrySel", null, DBType.AFFILIATE);
        }

        public static DbResult DomainAffSel()
        {
            return DbHelper.Execute("Aff_DomainAffSel", null, DBType.AFFILIATE);
        }

        public static DbResult DomainControlSel(string countryID)
        {
            Hashtable hs = new Hashtable();
            if (!string.IsNullOrEmpty(countryID))
                hs.Add("countryid", countryID);
            return DbHelper.Execute("Aff_DomainControlSel", hs, DBType.AFFILIATE);
        }

        public static bool DomainControlIns(int custId, string countryID, string affDomainId, string redirectDomainId, bool status, ref int errCode, ref string errMsg, string lang = "en-US")
        {
            Hashtable hs = new Hashtable();
            hs.Add("countryid", countryID);
            hs.Add("affdomainid", affDomainId);
            hs.Add("memberdomainid", redirectDomainId);
            hs.Add("status", status);
            hs.Add("creator", custId);
            hs.Add("ip", Utilities.GetIPAddressOfClient());
            hs.Add("userid", custId);
            hs.Add("language", lang);
            hs.Add("errcode", 0);
            hs.Add("errmsg", string.Empty);
            DbResult rs = DbHelper.Execute("Aff_DomainControlIns", hs, DBType.AFFILIATE);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static bool DomainControlUpd(int custId, int id, string countryID, string affDomainId, string memberDomainId, bool status, ref int errCode, ref string errMsg, string lang = "en-US")
        {
            Hashtable hs = new Hashtable();
            hs.Add("id", id);
            hs.Add("countryid", countryID);
            hs.Add("affdomainid", affDomainId);
            hs.Add("memberdomainid", memberDomainId);
            hs.Add("status", status);
            hs.Add("userid", custId);
            hs.Add("ip", Utilities.GetIPAddressOfClient());
            hs.Add("language", lang);
            hs.Add("errcode", 0);
            hs.Add("errmsg", string.Empty);
            DbResult rs = DbHelper.Execute("Aff_DomainControlUpd", hs, DBType.AFFILIATE);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static bool DomainControlDel(int custId, int Id, ref int errCode, ref string errMsg, string lang = "en-US")
        {
            Hashtable hs = new Hashtable();
            hs.Add("id", Id);
            hs.Add("userid", custId);
            hs.Add("ip", Utilities.GetIPAddressOfClient());
            hs.Add("language", lang);
            hs.Add("errcode", 0);
            hs.Add("errmsg", string.Empty);
            DbResult rs = DbHelper.Execute("Aff_DomainControlDel", hs, DBType.AFFILIATE);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static DbResult GetListAffDomain()
        {
            DbResult dbr = null;
            try
            {

                dbr = DbHelper.Execute("Aff_DomainAffSel", null, DBType.AFFILIATE);
            }
            catch
            {

                throw;
            }
            return dbr;
        }
        public static DbResult UpdateAffDomain(int domainID, string domainName, int userID, string lang = "en-US")
        {

            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", Utilities.GetIPAddressOfClient());
                htb.Add("language", lang);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                // htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_DomainAffUpd", htb, DBType.AFFILIATE);
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult AddAffDomain(string domainName, int userID, string lang = "en-US")
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", 0);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", Utilities.GetIPAddressOfClient());
                htb.Add("language", lang);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_DomainAffIns", htb, DBType.AFFILIATE);

            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult DelAffDomain(int domainID, string domainName, int userID, string lang = "en-US")
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", Utilities.GetIPAddressOfClient());
                htb.Add("language", lang);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //  htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_DomainAffDel", htb, DBType.AFFILIATE);
            }
            catch
            {
                throw;
            }
            return dr;
        }

        public static DbResult GetListMemberDomain()
        {
            DbResult dbr = null;
            try
            {

                dbr = DbHelper.Execute("Aff_MemberDomainSel", null, DBType.AFFILIATE);
            }
            catch
            {

                throw;
            }
            return dbr;
        }
        public static DbResult UpdateMemberDomain(int domainID, string domainName, int userID, string lang = "en-US")
        {

            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", Utilities.GetIPAddressOfClient());
                htb.Add("language", lang);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                // htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_MemberDomainUpd", htb, DBType.AFFILIATE);
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult AddMemberDomain(string domainName, int userID, string lang = "en-US")
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", 0);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", Utilities.GetIPAddressOfClient());
                htb.Add("language", lang);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_MemberDomainIns", htb, DBType.AFFILIATE);

            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult DelMemberDomain(int domainID, string domainName, int userID, string lang = "en-US")
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("userid", userID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("ip", Utilities.GetIPAddressOfClient());
                htb.Add("language", lang);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //  htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_MemberDomainDel", htb, DBType.AFFILIATE);
            }
            catch
            {
                throw;
            }
            return dr;
        }

        public static IList<DomainObj> SelDomainsByAff(int custId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("affid", custId);
            DbResult rs = DbHelper.Execute("Aff_DomainAffClientSel", ht, DBType.AFFILIATE);
            IList<DomainObj> list = new List<DomainObj>();

            if (rs.ResultSet.Tables.Count > 0)
            {
                foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
                {
                    DomainObj item = new DomainObj();
                    item.DomainID = CastAide.AsInt32(dr["DomainId"]);
                    item.DomainName = CastAide.AsString(dr["DomainName"]);
                    list.Add(item);
                }
            }
            return list;
        }

    }
}

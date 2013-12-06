
/// <summary>
/// Summary description for Login
/// </summary>
/// 
namespace Fanex.Affiliate.DataObjects
{
    using System.Web;
    using DNAide.Data;
    using DNAide.Core;
    using System.Collections;
    using DNAide.Utils;
    using System.Configuration;
    using System.Data;
    using Fanex.Affiliate.Model;
    
    public class Auth
    {
        public static DbResult ExecLogin(string username, string password, string language, HttpContext hc, int inputAffType = 1)
        {
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("username", username);
                htb.Add("userpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(password));
                htb.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("serverid", CastAide.AsInt32(ConfigurationManager.AppSettings["ServerId"]));
                htb.Add("custid", 0);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                htb.Add("firstname", "");
                htb.Add("lastname", "");
                htb.Add("email", "");
                htb.Add("language", language);
                htb.Add("mailoption", false);
                htb.Add("preferlang", "en-US");
                htb.Add("prefix", "$");
                htb.Add("currency", "USD");
                htb.Add("inputafftype", inputAffType);
                htb.Add("afftype", 1);   
                
                DbResult dbr = DbHelper.Execute("Aff_AffiliateLogin", htb, DBType.AFFILIATE);
                return dbr;
            }
            catch
            {
                throw;
            }
        }

        public static int LoginOnuser(HttpContext hc,int roleid = 0)
        {
            Hashtable p = new Hashtable();
            p.Add("custid",HttpContext.Current.Session["CustID"]);
            p.Add("netsessionid", hc.Session.SessionID);
            p.Add("serverid", ConfigurationManager.AppSettings["ServerId"]);
            p.Add("on_userid", 0);
            p.Add("ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            p.Add("subaccid", roleid);
            p.Add("language",HttpContext.Current.Session["Language"]);

            DbResult dr = DbHelper.Execute("Aff_Checkin", p, DBType.AFFILIATE);
            return CastAide.AsInt32(dr.Outputs["on_userid"]);
        }

        public static int LoginOnuserInternal(HttpContext hc, int adminID)
        {
            Hashtable p = new Hashtable();
            p.Add("custid",HttpContext.Current.Session["CustID"]);
            p.Add("netsessionid", hc.Session.SessionID);
            p.Add("serverid", ConfigurationManager.AppSettings["ServerId"]);
            p.Add("on_userid", 0);
            p.Add("ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            p.Add("subaccid", adminID);
            p.Add("language",HttpContext.Current.Session["Language"]);

            DbResult dr = DbHelper.Execute("Aff_Checkin", p, DBType.AFFILIATE);
            return CastAide.AsInt32(dr.Outputs["on_userid"]);
        }

        public static void Logout(int OnUserId)
        {
            Hashtable p = new Hashtable();
            p.Add("on_userid", OnUserId);
            DbHelper.Execute("Aff_Logout", p, DBType.AFFILIATE);
        }
        public static int CheckEmailForget(string email, string valicode, ref int errcode, ref string errmsg, ref string username, ref string firstname, ref string lastname, ref string PreferredLanguage, ref bool MailOption)
        {
            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int custid = 0;
            htb.Add("email", email);
            htb.Add("affid", 0);
            htb.Add("validcode", valicode);
            //htb.Add("affmaster", AppSettings.AffMaster);
            htb.Add("language",HttpContext.Current.Session["Language"]);
            htb.Add("username", "");
            htb.Add("firstname", "");
            htb.Add("lastname", "");
            htb.Add("preferredlanguage", "");
            htb.Add("mailoption", false);
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            dbr = DbHelper.Execute("Aff_AffiliateForgotPwd", htb, DBType.AFFILIATE, 600);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            custid = CastAide.AsInt32(dbr.Outputs["affid"]);
            username = dbr.Outputs["username"].ToString();
            firstname = dbr.Outputs["firstname"].ToString();
            lastname = dbr.Outputs["lastname"].ToString();
            PreferredLanguage = CastAide.AsString(dbr.Outputs["preferredlanguage"], "en-US");
            MailOption = CastAide.AsBoolean(dbr.Outputs["mailoption"], false);
            return custid;
        }
        public static int ExecChangePassword(int custid, string validcode, string newpass, ref string errmsg, ref string email, ref string username, ref string firstname, ref string lastname, ref string strPreferredLanguage, ref bool mailOption)
        {
            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int errcode = 0;
            htb.Add("affid", custid);
            //htb.Add("affmaster", AppSettings.AffMaster);
            htb.Add("validcode", validcode);
            htb.Add("newpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(newpass.Trim()));
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            htb.Add("email", "");
            htb.Add("username", "");
            htb.Add("firstname", "");
            htb.Add("lastname", "");
            htb.Add("language",HttpContext.Current.Session["Language"]);
            dbr = DbHelper.Execute("Aff_AffiliateRecoverPwd", htb, DBType.AFFILIATE);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            email = dbr.Outputs["email"].ToString();
            username = dbr.Outputs["username"].ToString();
            firstname = dbr.Outputs["firstname"].ToString();
            lastname = dbr.Outputs["lastname"].ToString();
            strPreferredLanguage = dbr.Outputs["preferredlanguage"].ToString(); ;
            mailOption = CastAide.AsBoolean(dbr.Outputs["mailoption"].ToString());
            return errcode;
        }
        //20090202@DungTong Add to update security question
        public static int ExecUpdateSecurityQuestion(string question, string answer, ref string errmsg, HttpContext hc)
        {
            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int errcode = 0;
            try
            {
                htb.Add("affid",HttpContext.Current.Session["CustID"]);
                htb.Add("ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("errcode", 0);
                htb.Add("errmsg", "");
                htb.Add("username",HttpContext.Current.Session["UserName"]);
                htb.Add("question", question);
                htb.Add("answer", answer);
                htb.Add("language",HttpContext.Current.Session["Language"]);
                htb.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("serverid", CastAide.AsInt32(ConfigurationManager.AppSettings["ServerId"]));
                dbr = DbHelper.Execute("Aff_CustomerUdpSecurityQuestion", htb, DBType.AFFILIATE);
                errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
            }
            catch
            {
                throw;
            }
            return errcode;
        }


        public static bool ReCheckin()
        {
            try
            {
                Hashtable p = new Hashtable();
                p.Add("on_userid", HttpContext.Current.Session["On_User"]);
                p.Add("iskickedout", 0);
                DbResult dr = DNAide.Data.DbHelper.Execute("Aff_Recheckin", p, DBType.AFFILIATE);
                return CastAide.AsBoolean(dr.Outputs["iskickedout"]);
            }
            catch
            { throw; }
        }

        public static DbResult GetAdminNetSessionID(string onuserID, HttpContext hc)
        {
            Hashtable p = new Hashtable();
            p.Add("on_userid", onuserID);
            p.Add("netsessionid", string.Empty);
            return DbHelper.Execute("Aff_OnUserSelNetSessionID", p, DBType.AFFILIATE);
        }

        public static DbResult LoginAuthentication(int adminID, string username, HttpContext hc, int inputAffType = 1)
        {
            Hashtable p = new Hashtable();
            p.Add("adminid", adminID);
            p.Add("username", username);
            p.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
            p.Add("serverid", CastAide.AsInt32(ConfigurationManager.AppSettings["ServerId"]));
            p.Add("adminusername", string.Empty);
            p.Add("custid", 0);
            p.Add("errcode", 0);
            p.Add("errmsg", string.Empty);
            p.Add("firstname", "");
            p.Add("lastname", "");
            p.Add("email", "");
            p.Add("preferlang", "en-US");
            p.Add("language",HttpContext.Current.Session["Language"]);
            p.Add("prefix", "$");
            p.Add("afftype", 1);
            p.Add("inputafftype", inputAffType);
            p.Add("currency", "");
            return DbHelper.Execute("Aff_AffiliateInternalLogin", p, DBType.AFFILIATE);
        }

        public static int ExecLogin(ref string username, string password, ref string errmsg, HttpContext hc, ref int custid, ref int roleid, ref bool isIT, string lang)
        {

            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int errcode = 0;

            try
            {
                htb.Add("username", username);
                htb.Add("userpwd", Sunplus.Agent.Utilities.Encryption.CfsEnCode(password));

                htb.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("custid", 0);
                htb.Add("roleid", 0);
                htb.Add("errcode", 0);
                htb.Add("errmsg", "");
                htb.Add("language", lang);
                htb.Add("from", hc.Request.UserHostName);
                htb.Add("isit", 0);
                
                dbr = DbHelper.Execute("Aff_AdminLogin", htb, DBType.AFFILIATE);
                errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
                username = dbr.Outputs["username"].ToString();
                custid = CastAide.AsInt32(dbr.Outputs["custid"]);
                roleid = CastAide.AsInt32(dbr.Outputs["roleid"]);
                isIT = CastAide.AsBoolean(dbr.Outputs["isit"]);
            }
            catch
            {
                throw;
            }
            return errcode;
        }
       
     
        public static string AssignRoles(int custid)
        {
            string result = string.Empty;
            DbResult ds = UserControl.GetUserPolicy(custid);
            int counter = 0;
            if (ds != null)
            {
                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    counter++;
                    if (CastAide.AsInt32(dr["subfunccount"]) == 0)
                    {
                        if (counter == ds.ResultSet.Tables[0].Rows.Count)
                        {
                            result += dr["funcname"] + "-" + ((CastAide.AsInt32(dr["actual_policy"]) > 1) ? "1" : dr["actual_policy"]);
                        }
                        else
                        {
                            result += dr["funcname"] + "-" + ((CastAide.AsInt32(dr["actual_policy"]) > 1) ? "1" : dr["actual_policy"]) + "|";
                        }
                    }
                }
            }
            return result;
        }

    }
}

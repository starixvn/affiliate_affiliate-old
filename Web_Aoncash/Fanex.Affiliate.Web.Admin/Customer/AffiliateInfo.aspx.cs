namespace Fanex.Web.Aoncash.Admin.Customer
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.Security;
    using DNAide.Core;
    using DNAide.Data;
    using DNAide.Utils;
    using DNAide.Web;
    using Fanex.Affiliate.Core;
    using Fanex.Affiliate.Core.Admin;
    using Fanex.Affiliate.Core.Helper;
    using Fanex.Affiliate.DataObjects;
    using System.Linq;

    public partial class AffiliateInfo : BasePageTpl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTpl = TemplateHandler.LoadTemplate("Customer/AffiliateInfo.htm");
            
            AppCommon.AssignToAdminTemplate(ref strTpl,
               "Affiliate", "Fname", "Lname", "Gender", "ContactNumber", "Mobile",
               "Language", "SiteUrl", "LastLogin", "LastLoginIP", "Password", "Change",
              "CommissionGroup", "Comm", "BalanceForward", "Setting", "Status", "Closed",
               "LockedOut", "CurrOnlineList", "IP", "LastCheckIn", "ReportDetails", "AffType","Approved","Disapproved","Pending");


            CurrencyUtils.SetDefaultCurrency(AdminSettings.SystemCurrency.Value);

            //TemplateHandler.Assign(ref strTpl, "listlang", AdminCommon.GetListSupportedLang(false, false));
            TemplateHandler.Assign(ref strTpl, "listlang",
                HtmlHelper.DropDownList(AppSettings.BaseLanguages,"slPreferLang",null,false,null,false,
                "style='display: none;' onblur='LangTimeout(false);' onfocus='LangTimeout(true);' onchange='SubmitLang();'"));
            string strUsername = Request["username"];
            if (!string.IsNullOrEmpty(strUsername))
                AdminSession.AffUsername = strUsername.Trim();

            if (string.IsNullOrEmpty(strUsername) && string.IsNullOrEmpty(AdminSession.AffUsername))
            {
                TemplateHandler.Assign(ref strTpl, "username", string.Empty);
                TemplateHandler.ReplaceBlock(ref strTpl, "noinfo", string.Empty);
                TemplateHandler.Assign(ref strTpl, "noinfo-val", "1");
                TemplateHandler.Assign(ref strTpl, "isReload-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "togshow", "none");
                this.HtmlContent = strTpl;
                return;
            }
            TemplateHandler.Assign(ref strTpl, "username", AdminSession.AffUsername);
            TemplateHandler.Assign(ref strTpl, "pageid-val", Request["pageid"]);


            if (!string.IsNullOrEmpty(Request["isReload"]) && string.Compare(Request["isReload"], "0", true) != 0)//proccess refresh page
            {
                TemplateHandler.Assign(ref strTpl, "noinfo-val", "2");
                TemplateHandler.Assign(ref strTpl, "isReload-val", Request["isReload"]);
            }
            TemplateHandler.Assign(ref strTpl, "isReload-val", "0");

            var hideAfftype = AppSettings.BaseAffTypes.Count < 2;
            if (hideAfftype)
                TemplateHandler.RemoveBlock(ref strTpl, "afftype");

            DbResult result = Affiliate.SelAffInfo_LeftPanel(AdminSession.AffUsername);
            if (result.ResultSet.Tables.Count > 0)
            {
                DataTable dtbInfo = result.ResultSet.Tables[0];
                if (dtbInfo.Rows.Count > 0)
                {
                    DataRow dr = dtbInfo.Rows[0];
                    
                    TemplateHandler.Assign(ref strTpl, "noinfo-val", "0");
                    AdminSession.AffCurrency = CastAide.AsString(dr["currency"], String.Empty);
                    AdminSession.AffType = CastAide.AsInt32(dr["afftype"], 0);

                    TemplateHandler.Assign(ref strTpl, "url", GerUrl(AdminSession.AffUsername, AdminSession.AffType== 3));//Link to affiliate
                    TemplateHandler.Assign(ref strTpl, "internalUrl-val", GerUrl(AdminSession.AffUsername, false));//Link to affiliate
                    TemplateHandler.Assign(ref strTpl, "internalUrlKiosk-val", GerUrl(AdminSession.AffUsername, true));//Link to affiliate

                    TemplateHandler.Assign(ref strTpl, "afftypeoption",
            HtmlHelper.DropDownList(AppSettings.BaseAffTypes, "afftypeoption", AdminSession.AffType,false,null,false," onblur='AffTypeTimeout(false);' style='display:none' onchange='UpdateAffType()'"));

                    if (!hideAfftype)
                    {
                        var afftype =  AppSettings.BaseAffTypes.Where(x => x.Id == AdminSession.AffType.ToString()).FirstOrDefault();

                        TemplateHandler.Assign(ref strTpl, "afftype-text", (afftype != null ? afftype.Value : String.Empty));
                        TemplateHandler.Assign(ref strTpl, "afftype-val", AdminSession.AffType);
                    }

                    string _displayURL = dr["siteurl"].ToString().Replace("http://", string.Empty).Replace("www.", string.Empty);
                    _displayURL = (_displayURL.Length <= 23) ? _displayURL : _displayURL.Substring(0, 20) + "...";
                    AdminSession.AffId = CastAide.AsInt32(dr["affid"]);

                    
                    AppSession.Prefix = CastAide.AsString(dr["prefix"], "$");
                    //TemplateHandler.Assign(ref strTpl, "nickname-val", dr["username2"]);
                    TemplateHandler.Assign(ref strTpl, "custid-val", dr["affid"]);
                    TemplateHandler.Assign(ref strTpl, "hdfname-val", dr["firstname"]);
                    TemplateHandler.Assign(ref strTpl, "fname-val", AppCommon.cutLongText(dr["firstname"].ToString(), 23));
                    TemplateHandler.Assign(ref strTpl, "lname-val", AppCommon.cutLongText(dr["lastname"].ToString(), 23));
                    if (DBNull.Value.Equals(dr["gender"]))
                        TemplateHandler.Assign(ref strTpl, "gender-val", string.Empty);
                    else
                        TemplateHandler.Assign(ref strTpl, "gender-val", 
                            Fanex.Affiliate.Resources.Translator.Get((CastAide.AsBoolean(dr["gender"], true) ? "Male" : "Female")));
                    TemplateHandler.Assign(ref strTpl, "number-val", AppCommon.cutLongText(dr["phone"].ToString(), 23));
                    TemplateHandler.Assign(ref strTpl, "mobile-val", AppCommon.cutLongText(dr["mobilephone"].ToString(), 23));
                    TemplateHandler.Assign(ref strTpl, "email-val", dr["email"]);
                    TemplateHandler.Assign(ref strTpl, "language-val", AdminCommon.GerFullTextLang(dr["preferredlanguage"]));
                    TemplateHandler.Assign(ref strTpl, "preferredpayment-val", dr["preferredpayment"]);
                    TemplateHandler.Assign(ref strTpl, "player_username-val", dr["player_username"]);
                    TemplateHandler.Assign(ref strTpl, "lang-val", dr["preferredlanguage"]);
                    TemplateHandler.Assign(ref strTpl, "siteurlorg-val", _displayURL);
                    TemplateHandler.Assign(ref strTpl, "mailoption-val", (CastAide.AsBoolean(dr["mailoption"], true) ? "1" : "0"));
                    if (dr["siteurl"].ToString().IndexOf("http") >= 0)
                    {
                        TemplateHandler.Assign(ref strTpl, "siteurl-val", dr["siteurl"]);
                        TemplateHandler.Assign(ref strTpl, "WebsiteCheckerURL-val", AdminCommon.GetWebsiteCheckerURL(dr["siteurl"].ToString()));
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTpl, "siteurl-val", "http://" + dr["siteurl"].ToString());
                        TemplateHandler.Assign(ref strTpl, "WebsiteCheckerURL-val", AdminCommon.GetWebsiteCheckerURL("http://" + dr["siteurl"].ToString()));
                    }
                    TemplateHandler.Assign(ref strTpl, "lastlogin-val", AdminCommon.IBCDateTimeFormat(dr["lastLoginDate"], DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTpl, "lastloginip-val", dr["lastloginip"]);
                    TemplateHandler.Assign(ref strTpl, "LastLoginIPCheckerURL-val", AdminCommon.GetIPCheckerURL(dr["lastloginip"].ToString()));
                    TemplateHandler.Assign(ref strTpl, "CommGroup-val", dr["groupname"].ToString());
                    TemplateHandler.Assign(ref strTpl, "GroupID-val", dr["groupid"]);

                    if (Convert.ToInt32(dr["groupid"]) > 1000)
                    {

                        TemplateHandler.RemoveBlock(ref strTpl, "PublicGroup");
                    }
                    else
                    {
                        string groupName = dr["groupname"].ToString();
                        groupName = (groupName.Length <= 14) ? groupName : groupName.Substring(0, 11) + "...";
                        TemplateHandler.Assign(ref strTpl, "CommGroup1-val", groupName);
                        TemplateHandler.RemoveBlock(ref strTpl, "PrivateGroup");
                    }

                ////NEW//TemplateHandler.Assign(ref strTpl, "balforward-val", AdminCommon.FormatBalanceNumber(dr["balbroughtforward"], AppSettings.Session.Prefix));
                    TemplateHandler.Assign(ref strTpl, "balforward-val", AdminCommon.FormatBalanceNumber(dr["balbroughtforward"])); //AdminCommon.FormatBalanceNumber(dr["balbroughtforward"]));

                    string strStatus = dr["Status"].ToString();
                    bool isClosed = CastAide.AsBoolean(dr["Closed"]);

                    string lockedOut = dr["IsLocked"].ToString();
                    if (lockedOut == "True")
                    {
                        lockedOut = "Yes";
                    }
                    else
                    {
                        lockedOut = "No";
                        TemplateHandler.ReplaceBlock(ref strTpl, "lockedlink", "<span class='text_box_lock'>No</span>");
                    }
                    TemplateHandler.Assign(ref strTpl, "lockedout-val", lockedOut);

                    if (string.Compare(strStatus, "Approved", true) != 0)
                    {
                        TemplateHandler.Assign(ref strTpl, "spUN", string.Empty);
                        TemplateHandler.Assign(ref strTpl, "lnkUN", "none");
                        TemplateHandler.ReplaceBlock(ref strTpl, "closed", string.Empty);
                        TemplateHandler.ReplaceBlock(ref strTpl, "commission", string.Empty);
                        TemplateHandler.ReplaceBlock(ref strTpl, "onlinelist", string.Empty);
                        TemplateHandler.ReplaceBlock(ref strTpl, "Password", string.Empty);
                        TemplateHandler.RemoveBlock(ref strTpl, "MemberList");
                    }
                    else
                    {
                        TemplateHandler.ReplaceBlock(ref strTpl, "status-val", strStatus);
                        if (isClosed)
                        {
                            TemplateHandler.Assign(ref strTpl, "spUN", string.Empty);
                            TemplateHandler.Assign(ref strTpl, "lnkUN", "none");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref strTpl, "spUN", "none");
                            TemplateHandler.Assign(ref strTpl, "lnkUN", string.Empty);
                        }
                    }
                    TemplateHandler.Assign(ref strTpl, "status-val", strStatus);

                    TemplateHandler.Assign(ref strTpl, "closed-val", (isClosed ? Fanex.Affiliate.Resources.Admin.Yes : Fanex.Affiliate.Resources.Admin.No));                    
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTpl, "noinfo", Fanex.Affiliate.Resources.Admin.NoUser);
                    TemplateHandler.Assign(ref strTpl, "noinfo-val", "1");
                    TemplateHandler.Assign(ref strTpl, "togshow", "none");
                }
                DataTable dtbOnline = result.ResultSet.Tables[1];
                if (dtbOnline.Rows.Count > 0)
                {
                    int rowNo = 0;
                    string strOnlineContent = TemplateHandler.CopyBlock(strTpl, "currlist");
                    StringBuilder strBuild = new StringBuilder();
                    foreach (DataRow dr in dtbOnline.Rows)
                    {
                        rowNo++;
                        string strTmp = strOnlineContent;
                        TemplateHandler.Assign(ref strTmp, "no", rowNo);
                        TemplateHandler.Assign(ref strTmp, "ip-val", dr["ip"]);
                        TemplateHandler.Assign(ref strTmp, "IPCheckerURL-val", AdminCommon.GetIPCheckerURL(dr["ip"].ToString()));
                        TemplateHandler.Assign(ref strTmp, "time", AdminCommon.IBCDateTimeFormat(dr["reload_time"], DTFormatType.DT24h));
                        strBuild.Append(strTmp);
                    }
                    TemplateHandler.ReplaceBlock(ref strTpl, "currlist", strBuild.ToString());
                }
                else
                    TemplateHandler.ReplaceBlock(ref strTpl, "currlist", "<tr><td colspan='3' class='c'>" + Fanex.Affiliate.Resources.Translator.Get("ErrUserOnline") + "</td></tr>");
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "noinfo", Fanex.Affiliate.Resources.Admin.NoUser);
                TemplateHandler.Assign(ref strTpl, "noinfo-val", "1");
                TemplateHandler.Assign(ref strTpl, "togshow", "none");
            }
            TemplateHandler.Assign(ref strTpl, "isAdmin", "0");

            this.HtmlContent = strTpl;
        }


        string GerUrl(string custName,bool isKiosk = false)
        {
            string rndStr = Encryption.RandomString(6);
            string accKey = FormsAuthentication.HashPasswordForStoringInConfigFile(Session.SessionID + rndStr, "MD5");
            string accID = Fanex.Affiliate.Core.AppSession.CustID.ToString();
            string accOnId = AppSession.OnUser.ToString();
            string _url = isKiosk ? AdminSettings.InternalURL_Kiosk :  AdminSettings.InternalURL;
            _url += "?chalengeStr=" + rndStr;
            _url += "&accKey=" + accKey;
            _url += "&custName=" + custName;
            _url += "&accId=" + Fanex.Affiliate.Core.AppSession.CustID.ToString(); ;
            _url += "&accOnId=" + accOnId;
            return _url;
        }
    }
}

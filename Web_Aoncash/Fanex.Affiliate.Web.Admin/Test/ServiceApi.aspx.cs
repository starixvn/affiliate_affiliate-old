using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Core;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.Core.Admin;

namespace Fanex.Web.Aoncash.Admin.Test
{
    public partial class ServiceApi : System.Web.UI.Page
    {
        protected string responseText = null;
        protected string site = AppSettings.SystemName;
        protected string licensee = null;
        protected string btag = null;
        protected double amount = 0;
        protected int dummyacount = 0;
        protected DateTime date = DateTime.Now;
        protected string strDate = null;
        protected string method = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Fanex.Affiliate.Core.Admin.AdminSession.RoleAccess.Contains("[SystemSettings]-1"))
            {
                Response.Redirect("/Default.aspx");
            }
            method = CastAide.AsString(Request["method"], "");
            // site = CastAide.AsString(Request["site"], "");

            switch (method)
            {
                case "GetRegList":
                    {
                        strDate = CastAide.AsString(Request["date"], "");
                        date = CastAide.AsDateTime(Request["date"]);
                        responseText = ServiceWrapper.GetRegList(AppSettings.SystemName, date).ToString();
                        break;
                    }
                case "UpdateBtage":
                    {
                        licensee = CastAide.AsString(Request["licensee"], "");
                        btag = CastAide.AsString(Request["btag"], "");
                        responseText = ServiceWrapper.UpdateBtage(AppSettings.SystemName, licensee, btag).ToString();
                        break;
                    }
                case "GetLicenseeCustInfo":
                    {
                        licensee = CastAide.AsString(Request["licensee"], "");
                        responseText = ServiceWrapper.GetLicenseeCustInfo(AppSettings.SystemName, licensee).ToString();
                        break;
                    }
                case "Transfer":
                    {
                        licensee = CastAide.AsString(Request["licensee"], "");
                        amount = CastAide.AsDouble(Request["amount"], 0);
                        dummyacount = CastAide.AsInt32(Request["dummyacount"], 0);
                        responseText = ServiceWrapper.Transfer(AppSettings.SystemName, licensee, amount, dummyacount).ToString();
                        break;
                    }
                default:
                    responseText = "";
                    break;
            }

            responseText = HttpUtility.HtmlEncode(responseText);
            //



        }
    }
}
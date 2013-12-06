namespace Fanex.Web.ITCBet.Admin.Handler
{
    using System;
    using System.Web;
    using Fanex.Affiliate.DataObjects;
    using Fanex.Affiliate.Resources;
    using Fanex.Affiliate.Core;
    using System.Xml.Linq;
    using DNAide.Core;

    public class AddMemberToAffiliate : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string member = context.Request["member"];
            string affiliate = context.Request["affiliate"];

            /*get system name*/
            var systemName = Settings.GetSetting("SystemName").ResultSet.Tables[0].Rows[0]["SValue"].ToString();
            bool isMemberExist = false;
            int affid  = 0;
            string affStatus = null;

            Member.CheckMemberExist(member, affiliate, ref affid, ref affStatus, ref isMemberExist);

            string message = null;
            if (affid < 1)
            {
                message = Translator.Get("AffNotExist");
            }
            else if (!String.IsNullOrEmpty(affStatus) && !affStatus.Equals("approved"))
            {
                if (affStatus.Equals("closed", StringComparison.OrdinalIgnoreCase))
                    message = Translator.Get("AffClosed");
                else if (!affStatus.Equals("approved", StringComparison.OrdinalIgnoreCase))
                    message = Translator.Get("AffNotApproved");
                else
                    message = "This affiliate is " + affStatus + ".";
            }
            else
            {
                if (isMemberExist)
                {
                    message = Translator.Get("MemberExist");
                }
                else
                {
                    XElement xml = ServiceWrapper.GetLicenseeCustInfo(AppSettings.SystemName, member);

                    if (xml.Element("ReturnCode").Value != "0")
                        message = xml.Element("ReturnMessage").Value;
                    else
                    {
                        long clickid = 0;
                        var btag = xml.Element("btag").Value;

                        if (!String.IsNullOrEmpty(btag))
                        {
                            var array = btag.Split('_');
                            //var licensee = array[0];
                            // var affid = array.Length > 1 ? array[1] : String.Empty;
                            clickid = CastAide.AsInt64Invariant(array.Length > 2 ? array[2] : "0", 0);
                        }
                        DateTime createdate = DateTime.MinValue;
                        DateTime.TryParse(xml.Element("FirstDepositDate").Value,out createdate);

                        message = Member.InsertMember(affiliate,
                            member, null, null, null,
                            xml.Element("Currency").Value,
                            null,
                            0,
                            xml.Element("IBCusername").Value,
                            0,
                            createdate,
                            CastAide.AsDateTime(xml.Element("RegisterDate").Value, DateTime.Now),
                            btag,
                            clickid, Fanex.Affiliate.Core.AppSession.Language
                            );
                        ServiceWrapper.UpdateBtage(AppSettings.SystemName, member,
                             String.Format("{0}_{1}_0", AppSettings.LicenseeID, affid));
                    }
                }
            }

            context.Response.Write(message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
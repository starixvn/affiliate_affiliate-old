using System;
using System.Web;
using DNAide.Web;
using DNAide.Utils;
using Fanex.Affiliate.Core;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*Response.Write("<script type='text/javascript'>alert(\"" + DNAide.Utils.Encryption.GetDeCryptConnectionString("packet size=4096; Data Source=10.40.40.41;Initial Catalog=bodb02;integrated security=false; user id=bodbDownlineNetview; password=TBAw8F+PqCx3u382anlnOXtGnv+Ylef9WzguKrc1cd0=;") + "\");</script>");
        return;*/
        try
        {
            string type = Request["type"];
            string FirstName = Request["txtFirstName"];
            string LastName = Request["txtLastName"];
            string Email = Request["txtEmail"];
            string UserName = Request["txtUserName"];
            bool rs = false;

            switch (type)
            {
                case "1"://Payment Approved
                    rs = ApprovedMail(123456, FirstName, LastName, 15000, DateTime.Now, Email);
                    if (rs)
                        Response.Write("Payment Approved successfully");
                    else
                        Response.Write("Payment Approved Fail");
                    break;
                case "2"://Payment Rejected                 
                    rs = CancelMail(123456, FirstName, LastName, 15000, DateTime.Now, Email);
                    if (rs)
                        Response.Write("Payment Rejected successfully");
                    else
                        Response.Write("Payment Rejected Fail");
                    break;
                case "3"://Approved
                    rs = Approved(FirstName, UserName, Email);
                    if (rs)
                        Response.Write("Approved successfully");
                    else
                        Response.Write("Approved Fail");
                    break;
                case "4"://Disapproved
                    rs = Disapproved(FirstName, UserName, Email);
                    if (rs)
                        Response.Write("Disapproved successfully");
                    else
                        Response.Write("Disapproved Fail");
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + ex.StackTrace);
        }
    }

    #region Email Content of Request Payment
    bool ApprovedMail(int intTransId, string strFName, string strLName, decimal Amt, DateTime dtmWLDate, string strEmail)
    {
        bool rs = false;
        try
        {

            string lang = HttpContext.Current.Request["lang"];
            string FileName = "";
            switch (lang)
            {
                case "E": FileName = "Email/PaymentApp/PaymentAppEmail_en-US.htm"; break;
                case "CS": FileName = "Email/PaymentApp/PaymentAppEmail_zh-CN.htm"; break;
                case "CT": FileName = "Email/PaymentApp/PaymentAppEmail_zh-TW.htm"; break;
                default: FileName = "Email/PaymentApp/PaymentAppEmail_en-US.htm"; break;
            }

            string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");

            TemplateHandler.Assign(ref strNoticeMail, "systemname", AppSettings.SystemNameForMail);
            TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
            //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
            TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "amount", FormatHandler.FormatNumber2(Amt, "en-US"));
            TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
            TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
            string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");

	   

            //rs = SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, "", "", strSubject, strNoticeMail,AppSettings.MailPwd);
            AppCommon.SendEmail(strNoticeMail, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);

	

        }
        catch(Exception ex)
        {
		    Response.Write(ex.Message);
            throw ex;
        }
        return true;
    }
    #endregion

    #region Email Content of Request Payment
    bool CancelMail(int intTransId, string strFName, string strLName, decimal Amt, DateTime dtmWLDate, string strEmail)
    {
        bool rs = false;
        try
        {
            string lang = HttpContext.Current.Request["lang"];
            string FileName = "";
            switch (lang)
            {
                case "E": FileName = "PaymentCanEmail.htm"; break;
                case "CS": FileName = "PaymentCanEmail_cs.htm"; break;
                case "CT": FileName = "PaymentCanEmail_ct.htm"; break;
                default: FileName = "PaymentCanEmail.htm"; break;
            }
            string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
            TemplateHandler.Assign(ref strNoticeMail, "systemname", AppSettings.SystemNameForMail);
            TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
            //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
            TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "amount", FormatHandler.FormatNumber2(Amt, "en-US"));
            TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
            TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
            string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");
            //rs =  SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, "", "", strSubject, strNoticeMail,AppSettings.MailPwd);
            AppCommon.SendEmail(strNoticeMail, strEmail, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
        catch 
        {
            throw;
        }
        return true;
    }
    #endregion

    #region Approved
    bool Approved(string strFname, string strUsername, string Email)
    {
        bool rs = false;
        try
        {
            string[] strTags = { "fname", "username", "pass", "custid", "mailcontact" };
            string[] strValue = { strFname, strUsername, "1234aa", "1234", Email};

            string FileName = "";
            string lang = HttpContext.Current.Request["lang"];
            switch (lang)
            {
                case "E": FileName = "Approved.tpl"; break;
                case "CS": FileName = "Approved_cs.tpl"; break;
                case "CT": FileName = "Approved_ct.tpl"; break;
                default: FileName = "Approved.tpl"; break;
            }

            string strApproved = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
            TemplateHandler.Assign(ref strApproved, "systemname", AppSettings.SystemNameForMail);
            string strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
            //rs = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, Email, AppSettings.FromMail, "", "", strSubject, strApproved,AppSettings.MailPwd);
            AppCommon.SendEmail(strApproved, Email, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
        catch { throw; }
        return true;
    }
    #endregion

    #region Disapproved
    bool Disapproved(string strFname, string strUsername, string Email)
    {
        bool rs = false;
        try
        {
            string[] strTags = { "fname", "mailcontact" };
            string[] strValue = { strFname, Email};

            string FileName = "";
            string lang = HttpContext.Current.Request["lang"];
            switch (lang)
            {
                case "E": FileName = "Disapproved.tpl"; break;
                case "CS": FileName = "Disapproved_cs.tpl"; break;
                case "CT": FileName = "Disapproved_ct.tpl"; break;
                default: FileName = "Disapproved.tpl"; break;
            }

            string strDisapproved = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
            string strSubject = TemplateHandler.ReplaceBlock(ref strDisapproved, "subject", "").Replace("\r\n", "").Trim();
           //rs = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, Email, AppSettings.FromMail, "", "", strSubject, strDisapproved,AppSettings.MailPwd);
            AppCommon.SendEmail(strDisapproved, Email, strSubject, strSubject, AppSettings.BCCMail, AppSettings.CCMail);
        }
        catch { throw; }
        return true;
    }
    #endregion
}


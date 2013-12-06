using System;
using System.Xml;
using System.Text;
using DNAide.Web;
using System.Collections.Generic;
using Fanex.Affiliate.Core;
using Fanex.Affiliate.DataObjects;
using Fanex.Affiliate.Core.Admin;
using Fanex.Core.Utility;
using System.Configuration;

public partial class Header : TplPage
{
    string temp = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        temp = TemplateHandler.LoadTemplate("Header.htm");       

        TemplateHandler.Assign(ref temp, "Logout", Fanex.Affiliate.Resources.Translator.Get("Logout"));
        TemplateHandler.Assign(ref temp, "resetpass", Fanex.Affiliate.Resources.Translator.Get("ChangeMyPwd"));
        TemplateHandler.Assign(ref temp, "AffiliateAdministration", Fanex.Affiliate.Resources.Translator.Get("AffiliateAdministration"));
        TemplateHandler.Assign(ref temp, "Home", Fanex.Affiliate.Resources.Translator.Get("Home"));
        TemplateHandler.Assign(ref temp, "affiliateinfo", Fanex.Affiliate.Resources.Translator.Get("AffiliateInfo"));
        TemplateHandler.Assign(ref temp, "Affiliate", Fanex.Affiliate.Resources.Translator.Get("AffList"));
        TemplateHandler.Assign(ref temp, "member", Fanex.Affiliate.Resources.Translator.Get("MemList"));
        TemplateHandler.Assign(ref temp, "Payment", Fanex.Affiliate.Resources.Translator.Get("PaymentRequest"));
        TemplateHandler.Assign(ref temp, "MediaGallery", Fanex.Affiliate.Resources.Translator.Get("MediaGallery"));
        TemplateHandler.Assign(ref temp, "administratorlist", Fanex.Affiliate.Resources.Translator.Get("AdministratorList")); 
        TemplateHandler.Assign(ref temp, "exchangerate", Fanex.Affiliate.Resources.Translator.Get("ExchangeRate"));
        TemplateHandler.Assign(ref temp, "Configurations", Fanex.Affiliate.Resources.Translator.Get("Configurations"));
        TemplateHandler.Assign(ref temp, "Translations", Fanex.Affiliate.Resources.Translator.Get("Translations"));
        TemplateHandler.Assign(ref temp, "report", Fanex.Affiliate.Resources.Translator.Get("Report"));
        TemplateHandler.Assign(ref temp, "Message", Fanex.Affiliate.Resources.Translator.Get("Message"));
        TemplateHandler.Assign(ref temp, "EmailHistory", Fanex.Affiliate.Resources.Translator.Get("EmailHistory"));
        TemplateHandler.Assign(ref temp, "Theme_Dir",AppSettings.ThemeDir);
        TemplateHandler.Assign(ref temp, "Welcome", Fanex.Affiliate.Resources.Translator.Get("Welcome"));
        TemplateHandler.Assign(ref temp, "CommGroup", Fanex.Affiliate.Resources.Translator.Get("CommGroup"));
        string gmt;
        DateTime dt = ServerDateTime.GetCurrentDBTime(out gmt);
        TemplateHandler.Assign(ref temp, "gmt_offset", gmt);
        TemplateHandler.Assign(ref temp, "year", dt.Year);
        TemplateHandler.Assign(ref temp, "month", dt.Month);
        TemplateHandler.Assign(ref temp, "day", dt.Day);
        TemplateHandler.Assign(ref temp, "hour", dt.Hour);
        TemplateHandler.Assign(ref temp, "minute", dt.Minute);
        TemplateHandler.Assign(ref temp, "second", dt.Second);

        //IList<string> devUsers = Common.GetStringIList(ConfigurationManager.AppSettings["ShowDevMenuFor"], ",");
        //if (!devUsers.Contains(AppSession.UserName.ToLower()))
        //{
        //    TemplateHandler.RemoveBlock(ref temp, "DevMenu");
        //}
        if (AdminSession.RoleID == -1)
        {
            TemplateHandler.RemoveBlock(ref temp, "limit-logout");
            Response.Write("<script>parent.location.href='Signin.aspx';</script>");
        }
        else
        {
            TemplateHandler.Assign(ref temp, "username", AppSession.UserName);
        }
        string strMenuScript = GenListmenu();//Generate listmenu

        this.TplSetContent(temp + GerScriptMsg() + strMenuScript);        
    }

#region Generate list menu
    string GenListmenu()
    {
        string menuItem = TemplateHandler.CopyBlock(temp, "menuitem");

        StringBuilder strListmenu = new StringBuilder();//Generate list menuitem
        StringBuilder strMenuScript = new StringBuilder();//Generate script to focus menu.

        strMenuScript.AppendLine("<script type=\"text/javascript\">");
        strMenuScript.AppendLine("var arrMenu = new Array(");

        XmlDocument xml = new XmlDocument();

        xml.Load(Server.MapPath("~/App_Templates/Menu/ListMenu.xml"));

        XmlNodeList nodeList = xml.SelectNodes("/root/MenuItem");

        string strFirstPage = string.Empty;
        string _roleAccess = AdminSession.RoleAccess;
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlNode node = nodeList[i];
            string Function = node.Attributes["Function"].Value + "-1";
            string[] listLink = node.Attributes["Link"].Value.Split(',');
            if (_roleAccess.IndexOf(Function) >= 0)
            {
                string strTmp = menuItem;
                TemplateHandler.Assign(ref strTmp, "menuid", "menu" + i);
                TemplateHandler.Assign(ref strTmp, "link", listLink[0]);
                TemplateHandler.Assign(ref strTmp, "text", node.Attributes["Text"].Value);
                strListmenu.Append(strTmp);

                if (string.IsNullOrEmpty(strFirstPage))
                    strFirstPage = node.Attributes["Link"].Value;
            }

            if(i == nodeList.Count - 1)//Last item
                strMenuScript.Append("\"menu" + i + "@" + node.Attributes["Link"].Value + "\"");
            else
                strMenuScript.Append("\"menu" + i + "@" + node.Attributes["Link"].Value + "\",");
        }

        TemplateHandler.ReplaceBlock(ref temp, "menuitem", strListmenu.ToString());


        strMenuScript.AppendLine(");");
        strMenuScript.AppendLine("top.fMain.location.href = '" + (string.IsNullOrEmpty(strFirstPage) ? "ChangeAdminPass.aspx" : strFirstPage) + "';");
        strMenuScript.AppendLine("</script>");

        return strMenuScript.ToString();
    }
    #endregion

    string GerScriptMsg()
    {
        StringBuilder strScript = new StringBuilder();
        strScript.AppendLine("<script type=\"text/javascript\">");
        strScript.AppendLine("var Msg = {");
        strScript.AppendLine("CreateAgent:\"" + Fanex.Affiliate.Resources.Translator.Get("Err100") + "\",");
        strScript.AppendLine("ContactNumber:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrContactNumber") + "\",");
        strScript.AppendLine("MobileNumber:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrMobileNumber") + "\",");
        strScript.AppendLine("FName:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrFName") + "\",");
        strScript.AppendLine("LName:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrLName") + "\",");
        strScript.AppendLine("ErrGender:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrGender") + "\",");
        strScript.AppendLine("Email:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrEmail") + "\",");
        strScript.AppendLine("EmailValid:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrEmailValid") + "\",");
        strScript.AppendLine("Address:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrAddress") + "\",");
        strScript.AppendLine("City:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrCity") + "\",");
        strScript.AppendLine("State:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrState") + "\",");
        strScript.AppendLine("PostCode:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrPostCode") + "\",");
        strScript.AppendLine("Country:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrCountry") + "\",");
        strScript.AppendLine("SiteUrl:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrSiteUrl") + "\",");
        strScript.AppendLine("PreferLang:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrPreferLang") + "\",");
        strScript.AppendLine("UserName12Bet:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrUserNamePlayer") + "\",");
        strScript.AppendLine("Neteller:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrNeteller") + "\",");
        strScript.AppendLine("Moneybooker:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrMoneybooker") + "\",");
        strScript.AppendLine("MoneybookerValid:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrMoneybookerValid") + "\",");
        strScript.AppendLine("BankName:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrBankName") + "\",");
        strScript.AppendLine("BankAddress:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrBankAddress") + "\",");
        strScript.AppendLine("BankCode:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrBankCode") + "\",");
        strScript.AppendLine("BankAccName:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrBankAccName") + "\",");
        strScript.AppendLine("BankAccNumber:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrBankAccNumber") + "\",");
        strScript.AppendLine("ChequeFullname:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrChequeFullname") + "\",");
        strScript.AppendLine("ChequeAddress:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrChequeAddress") + "\",");
        strScript.AppendLine("ChequeCity:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrChequeCity") + "\",");
        strScript.AppendLine("ChequeState:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrChequeState") + "\",");
        strScript.AppendLine("ChequePostcode:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrChequePostcode") + "\",");
        strScript.AppendLine("NewPass:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrNewPass") + "\",");
        strScript.AppendLine("RePass:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrRePass") + "\",");
        strScript.AppendLine("Name:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrName") + "\",");
        strScript.AppendLine("InvalidData:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrInvalidData") + "\",");
        strScript.AppendLine("NoteEmpty:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrNoteEmpty") + "\",");
        strScript.AppendLine("NoteAdd:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrNoteAdd") + "\",");
        strScript.AppendLine("UpdateSucc:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrUpdateSucc") + "\",");
        strScript.AppendLine("NoteConfirm:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrNoteConfirm") + "\",");
        strScript.AppendLine("NoteDel:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrNoteDel") + "\",");
        strScript.AppendLine("ChooseItem:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrChooseItem") + "\",");
        strScript.AppendLine("Department:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrDepartment") + "\",");
        strScript.AppendLine("Username:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrUsername") + "\",");
        strScript.AppendLine("PasswordRequired:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrPasswordRequired") + "\",");
        strScript.AppendLine("DepartmentChoose:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrDepartmentChoose") + "\",");
        strScript.AppendLine("Character:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrCharacter") + "\",");
        strScript.AppendLine("ErrDate:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrDate") + "\",");
        strScript.AppendLine("Banner:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrBanner") + "\",");
        strScript.AppendLine("Rate:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrRate") + "\",");
        strScript.AppendLine("RateRequire:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrRateRequire") + "\",");
        strScript.AppendLine("ErrDepartment:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrDepartment") + "\",");
        strScript.AppendLine("MonthlyStatus:\"" + Fanex.Affiliate.Resources.Translator.Get("Err30011") + "\",");
        strScript.AppendLine("ErrValidUserName:\"" + Fanex.Affiliate.Resources.Translator.Get("ErrValidUserName") + "\"};");        
        strScript.AppendLine("</script>");
        return strScript.ToString();
    }
}

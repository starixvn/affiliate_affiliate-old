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
using System.Linq;
using Fanex.Affiliate.Resources;

public partial class Header : TplPage
{
    string temp = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        temp = TemplateHandler.LoadTemplate("Header.htm");

        if (AppSettings.AdminLanguages.Count < 2)
            TemplateHandler.RemoveBlock(ref temp, "language");

        var locale = (string.IsNullOrEmpty(AppSession.Language)) ? AppSettings.AdminLanguages[0].Id : AppSession.Language;
        if (!string.IsNullOrEmpty(AppSession.Language))
        {
            this.Culture = locale;
            this.UICulture = locale;
        }
        else
        {
            AppSession.Language = locale;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(locale);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(locale);
        }
        
        TemplateHandler.Assign(ref temp, "Logout", Fanex.Affiliate.Resources.Admin.Logout);
        TemplateHandler.Assign(ref temp, "ChangePass", Admin.ChangePass);
        TemplateHandler.Assign(ref temp, "AffiliateAdministration", Fanex.Affiliate.Resources.Admin.AffiliateAdministration);
        TemplateHandler.Assign(ref temp, "Home", Fanex.Affiliate.Resources.Admin.Home);
        TemplateHandler.Assign(ref temp, "affiliateinfo", Fanex.Affiliate.Resources.Admin.AffiliateInfo);
        TemplateHandler.Assign(ref temp, "Affiliate", Fanex.Affiliate.Resources.Admin.AffList);
        TemplateHandler.Assign(ref temp, "member", Fanex.Affiliate.Resources.Admin.MemList);
        TemplateHandler.Assign(ref temp, "Payment", Fanex.Affiliate.Resources.Admin.PaymentRequest);
        TemplateHandler.Assign(ref temp, "MediaGallery", Fanex.Affiliate.Resources.Admin.MediaGallery);
        TemplateHandler.Assign(ref temp, "administratorlist", Fanex.Affiliate.Resources.Admin.AdministratorList); 
        TemplateHandler.Assign(ref temp, "exchangerate", Fanex.Affiliate.Resources.Admin.ExchangeRate);
        TemplateHandler.Assign(ref temp, "Configurations", Fanex.Affiliate.Resources.Admin.Configurations);
        TemplateHandler.Assign(ref temp, "Translations", Fanex.Affiliate.Resources.Admin.Translations);
        TemplateHandler.Assign(ref temp, "report", Fanex.Affiliate.Resources.Admin.Report);
        TemplateHandler.Assign(ref temp, "Message", Fanex.Affiliate.Resources.Admin.Message);
        TemplateHandler.Assign(ref temp, "EmailHistory", Fanex.Affiliate.Resources.Admin.EmailHistory);
        TemplateHandler.Assign(ref temp, "Theme_Dir",AppSettings.ThemeDir);
        TemplateHandler.Assign(ref temp, "Welcome", Admin.Welcome);
        TemplateHandler.Assign(ref temp, "CommGroup", Fanex.Affiliate.Resources.Admin.CommissionGroup);
        string gmt;
        DateTime dt = ServerDateTime.GetCurrentDBTime(out gmt);
        TemplateHandler.Assign(ref temp, "gmt_offset", gmt);
        TemplateHandler.Assign(ref temp, "year", dt.Year);
        TemplateHandler.Assign(ref temp, "month", dt.Month);
        TemplateHandler.Assign(ref temp, "day", dt.Day);
        TemplateHandler.Assign(ref temp, "hour", dt.Hour);
        TemplateHandler.Assign(ref temp, "minute", dt.Minute);
        TemplateHandler.Assign(ref temp, "second", dt.Second);


        TemplateHandler.Assign(ref temp, "rooturl", AppSettings.FullDomainName);

        var langtext = AppSettings.BaseLanguages.Where(l => l.Id == AppSession.Language).FirstOrDefault().Value;           

        TemplateHandler.Assign(ref temp, "langcode", AppSession.Language);
        TemplateHandler.Assign(ref temp, "langtext", langtext);
        

        var strLanglist = new StringBuilder();
        foreach (var item in Fanex.Affiliate.Core.AppSettings.AdminLanguages)
        {
            strLanglist.AppendFormat("<a href=\"javascript:ChangeLanguage('{0}');\" class='f{0}'>{1}</a>",
                item.Id, item.Value);            
        }
        TemplateHandler.Assign(ref temp,"langlist", strLanglist);


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
                TemplateHandler.Assign(ref strTmp, "text", Admin.ResourceManager.GetString(node.Attributes["Text"].Value));
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
        strScript.AppendLine("CreateAgent:\"" + Fanex.Affiliate.Resources.Admin.Err100 + "\",");
        strScript.AppendLine("ContactNumber:\"" + Fanex.Affiliate.Resources.Admin.ErrContactNumber + "\",");
        strScript.AppendLine("MobileNumber:\"" + Fanex.Affiliate.Resources.Admin.ErrMobileNumber + "\",");
        strScript.AppendLine("FName:\"" + Fanex.Affiliate.Resources.Admin.ErrFName + "\",");
        strScript.AppendLine("LName:\"" + Fanex.Affiliate.Resources.Admin.ErrLName + "\",");
        strScript.AppendLine("ErrGender:\"" + Fanex.Affiliate.Resources.Admin.ErrGender + "\",");
        strScript.AppendLine("Email:\"" + Fanex.Affiliate.Resources.Admin.ErrEmail + "\",");
        strScript.AppendLine("EmailValid:\"" + Fanex.Affiliate.Resources.Admin.ErrEmailValid+ "\",");
        strScript.AppendLine("Address:\"" + Fanex.Affiliate.Resources.Admin.ErrAddress+ "\",");
        strScript.AppendLine("City:\"" + Fanex.Affiliate.Resources.Admin.ErrCity+ "\",");
        strScript.AppendLine("State:\"" + Fanex.Affiliate.Resources.Admin.ErrState+ "\",");
        strScript.AppendLine("PostCode:\"" + Fanex.Affiliate.Resources.Admin.ErrPostCode+ "\",");
        strScript.AppendLine("Country:\"" + Fanex.Affiliate.Resources.Admin.ErrCountry + "\",");
        strScript.AppendLine("SiteUrl:\"" + Fanex.Affiliate.Resources.Admin.ErrSiteUrl+ "\",");
        strScript.AppendLine("PreferLang:\"" + Fanex.Affiliate.Resources.Admin.ErrPreferLang+ "\",");
        strScript.AppendLine("UserName12Bet:\"" + Fanex.Affiliate.Resources.Admin.ErrUserNamePlayer+ "\",");
        strScript.AppendLine("Neteller:\"" + Fanex.Affiliate.Resources.Admin.ErrNeteller+ "\",");
        strScript.AppendLine("Moneybooker:\"" + Fanex.Affiliate.Resources.Admin.ErrMoneybooker+ "\",");
        strScript.AppendLine("MoneybookerValid:\"" + Fanex.Affiliate.Resources.Admin.ErrMoneybookerValid+ "\",");
        strScript.AppendLine("BankName:\"" + Fanex.Affiliate.Resources.Admin.ErrBankName+ "\",");
        strScript.AppendLine("BankAddress:\"" + Fanex.Affiliate.Resources.Admin.ErrBankAddress+ "\",");
        strScript.AppendLine("BankCode:\"" + Fanex.Affiliate.Resources.Admin.ErrBankCode+ "\",");
        strScript.AppendLine("BankAccName:\"" + Fanex.Affiliate.Resources.Admin.ErrBankAccName+ "\",");
        strScript.AppendLine("BankAccNumber:\"" + Fanex.Affiliate.Resources.Admin.ErrBankAccNumber+ "\",");
        strScript.AppendLine("ChequeFullname:\"" + Fanex.Affiliate.Resources.Admin.ErrChequeFullname+ "\",");
        strScript.AppendLine("ChequeAddress:\"" + Fanex.Affiliate.Resources.Admin.ErrChequeAddress+ "\",");
        strScript.AppendLine("ChequeCity:\"" + Fanex.Affiliate.Resources.Admin.ErrChequeCity+ "\",");
        strScript.AppendLine("ChequeState:\"" + Fanex.Affiliate.Resources.Admin.ErrChequeState+ "\",");
        strScript.AppendLine("ChequePostcode:\"" + Fanex.Affiliate.Resources.Admin.ErrChequePostcode+ "\",");
        strScript.AppendLine("NewPass:\"" + Fanex.Affiliate.Resources.Admin.ErrNewPass+ "\",");
        strScript.AppendLine("RePass:\"" + Fanex.Affiliate.Resources.Admin.ErrRePass+ "\",");
        strScript.AppendLine("Name:\"" + Fanex.Affiliate.Resources.Admin.ErrName+ "\",");
        strScript.AppendLine("InvalidData:\"" + Fanex.Affiliate.Resources.Admin.ErrInvalidData+ "\",");
        strScript.AppendLine("NoteEmpty:\"" + Fanex.Affiliate.Resources.Admin.ErrNoteEmpty+ "\",");
        strScript.AppendLine("NoteAdd:\"" + Fanex.Affiliate.Resources.Admin.ErrNoteAdd+ "\",");
        strScript.AppendLine("UpdateSucc:\"" + Fanex.Affiliate.Resources.Admin.ErrUpdateSucc+ "\",");
        strScript.AppendLine("NoteConfirm:\"" + Fanex.Affiliate.Resources.Admin.ErrNoteConfirm+ "\",");
        strScript.AppendLine("NoteDel:\"" + Fanex.Affiliate.Resources.Admin.ErrNoteDel+ "\",");
        strScript.AppendLine("ChooseItem:\"" + Fanex.Affiliate.Resources.Admin.ErrChooseItem+ "\",");
        strScript.AppendLine("Department:\"" + Fanex.Affiliate.Resources.Admin.ErrDepartment+ "\",");
        strScript.AppendLine("Username:\"" + Fanex.Affiliate.Resources.Admin.ErrUsername+ "\",");
        strScript.AppendLine("PasswordRequired:\"" + Fanex.Affiliate.Resources.Admin.ErrPasswordRequired+ "\",");
        strScript.AppendLine("DepartmentChoose:\"" + Fanex.Affiliate.Resources.Admin.ErrDepartmentChoose+ "\",");
        strScript.AppendLine("Character:\"" + Fanex.Affiliate.Resources.Admin.ErrCharacter+ "\",");
        strScript.AppendLine("ErrDate:\"" + Fanex.Affiliate.Resources.Admin.ErrDate+ "\",");
        strScript.AppendLine("Banner:\"" + Fanex.Affiliate.Resources.Admin.ErrBanner+ "\",");
        strScript.AppendLine("Rate:\"" + Fanex.Affiliate.Resources.Admin.ErrRate+ "\",");
        strScript.AppendLine("RateRequire:\"" + Fanex.Affiliate.Resources.Admin.ErrRateRequire+ "\",");
        strScript.AppendLine("ErrDepartment:\"" + Fanex.Affiliate.Resources.Admin.ErrDepartment+ "\",");
        strScript.AppendLine("MonthlyStatus:\"" + Fanex.Affiliate.Resources.Admin.Err30011+ "\",");
        strScript.AppendLine("SelBannerDel:\"" + Fanex.Affiliate.Resources.Admin.SelBannerDel + "\",");
        strScript.AppendLine("SelTextDel:\"" + Fanex.Affiliate.Resources.Admin.SelTextDel + "\",");
        strScript.AppendLine("UsernameRequired:\"" + Fanex.Affiliate.Resources.Admin.UsernameRequired + "\",");
        strScript.AppendLine("DateFormat:\"" + Fanex.Affiliate.Resources.Admin.DateFormat + "\",");
        strScript.AppendLine("ValidMonth:\"" + Fanex.Affiliate.Resources.Admin.ValidMonth + "\",");
        strScript.AppendLine("ValidDay:\"" + Fanex.Affiliate.Resources.Admin.ValidDay + "\",");
        strScript.AppendLine("ValidYear:\"" + Fanex.Affiliate.Resources.Admin.ValidYear + "\",");
        strScript.AppendLine("ValidDate:\"" + Fanex.Affiliate.Resources.Admin.ValidDate + "\",");
        strScript.AppendLine("ForceLogOut:\"" + Fanex.Affiliate.Resources.Admin.ForceLogOut + "\",");
        strScript.AppendLine("NoInfo:\"" + Fanex.Affiliate.Resources.Admin.NoInfo + "\",");
        strScript.AppendLine("UpdateSucc:\"" + Fanex.Affiliate.Resources.Admin.UpdateSucc + "\",");
        strScript.AppendLine("ChooseAtLeast:\"" + Fanex.Affiliate.Resources.Admin.ChooseAtLeast + "\",");
        strScript.AppendLine("SelBannerDel:\"" + Fanex.Affiliate.Resources.Admin.SelBannerDel + "\",");
        strScript.AppendLine("IdNotNumber:\"" + Fanex.Affiliate.Resources.Admin.IdNotNumber + "\",");
        strScript.AppendLine("FavoriteListEmpty:\"" + Fanex.Affiliate.Resources.Admin.FavoriteListEmpty + "\",");
        strScript.AppendLine("ConfirmUpdateTypeAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmUpdateTypeAff + "\",");
        strScript.AppendLine("ConfirmChangeLanguageAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmChangeLanguageAff + "\",");
        strScript.AppendLine("ConfirmChangeStatusAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmChangeStatusAff + "\",");
        strScript.AppendLine("ConfirmUnlockAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmUnlockAff + "\",");
        strScript.AppendLine("ConfirmLockAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmLockAff + "\",");
        strScript.AppendLine("SelectAffChangeGroup:\"" + Fanex.Affiliate.Resources.Admin.SelectAffChangeGroup + "\",");
        strScript.AppendLine("CopyTierSettingFor:\"" + Fanex.Affiliate.Resources.Admin.CopyTierSettingFor + "\",");
        strScript.AppendLine("ValidUsername:\"" + Fanex.Affiliate.Resources.Admin.ValidUsername + "\",");
        strScript.AppendLine("DelBannerSucc:\"" + Fanex.Affiliate.Resources.Admin.DelBannerSucc + "\",");
        strScript.AppendLine("CancelEditBanner:\"" + Fanex.Affiliate.Resources.Admin.CancelEditBanner + "\",");
        strScript.AppendLine("SelectType:\"" + Fanex.Affiliate.Resources.Admin.SelectType + "\",");
        strScript.AppendLine("InputValueTranslation:\"" + Fanex.Affiliate.Resources.Admin.InputValueTranslation + "\",");

        strScript.AppendLine("NotHandleFrames:\"" + Fanex.Affiliate.Resources.Admin.NotHandleFrames + "\",");
        strScript.AppendLine("SelectBannerDelete:\"" + Fanex.Affiliate.Resources.Admin.SelectBannerDelete + "\",");
        strScript.AppendLine("InputDigitOnly:\"" + Fanex.Affiliate.Resources.Admin.InputDigitOnly + "\",");
        strScript.AppendLine("ConfirmDeleteTypes:\"" + Fanex.Affiliate.Resources.Admin.ConfirmDeleteTypes + "\",");
        strScript.AppendLine("ConfirmCloseAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmCloseAff + "\",");
        strScript.AppendLine("ConfirmOpenAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmOpenAff + "\",");
        strScript.AppendLine("InvalidAmount:\"" + Fanex.Affiliate.Resources.Admin.InvalidAmount + "\",");
        strScript.AppendLine("RequiredAmount:\"" + Fanex.Affiliate.Resources.Admin.RequiredAmount + "\",");
        strScript.AppendLine("ConfirmRemoveFromGroup:\"" + Fanex.Affiliate.Resources.Admin.ConfirmRemoveFromGroup + "\",");
        strScript.AppendLine("SuccAddText:\"" + Fanex.Affiliate.Resources.Admin.SuccAddText + "\",");
        strScript.AppendLine("ConfirmRemoveMem:\"" + Fanex.Affiliate.Resources.Admin.ConfirmRemoveMem + "\",");
        strScript.AppendLine("SuccRemoveMem:\"" + Fanex.Affiliate.Resources.Admin.SuccRemoveMem + "\",");
        strScript.AppendLine("ConfirmCheckAff:\"" + Fanex.Affiliate.Resources.Admin.ConfirmCheckAff + "\",");
        strScript.AppendLine("ConfirmUnlockAffs:\"" + Fanex.Affiliate.Resources.Admin.ConfirmUnlockAffs + "\",");
        strScript.AppendLine("ErrorTryAgain:\"" + Fanex.Affiliate.Resources.Admin.ErrorTryAgain + "\",");
        strScript.AppendLine("CurrencyMustSame:\"" + Fanex.Affiliate.Resources.Admin.CurrencyMustSame + "\",");

        strScript.AppendLine("ConfirmDelDomainSetting:\"" + Fanex.Affiliate.Resources.Admin.ConfirmDelDomainSetting + "\",");
        strScript.AppendLine("ConfirmDelNote:\"" + Fanex.Affiliate.Resources.Admin.ConfirmDelNote + "\",");
        strScript.AppendLine("SelectNoteDel:\"" + Fanex.Affiliate.Resources.Admin.SelectNoteDel + "\",");
        strScript.AppendLine("SelectBannerUpload:\"" + Fanex.Affiliate.Resources.Admin.SelectBannerUpload + "\",");
        strScript.AppendLine("InputTextUpload:\"" + Fanex.Affiliate.Resources.Admin.InputTextUpload + "\",");
        strScript.AppendLine("SelectExcelUpload:\"" + Fanex.Affiliate.Resources.Admin.SelectExcelUpload + "\",");
        strScript.AppendLine("AffDomainNameNotContainChar:\"" + Fanex.Affiliate.Resources.Admin.AffDomainNameNotContainChar + "\",");
        strScript.AppendLine("AffDomainNameAddSucc:\"" + Fanex.Affiliate.Resources.Admin.AffDomainNameAddSucc + "\",");
        strScript.AppendLine("MemDomainNameNotContainChar:\"" + Fanex.Affiliate.Resources.Admin.MemDomainNameNotContainChar + "\",");
        strScript.AppendLine("OnlistListNotUpdate:\"" + Fanex.Affiliate.Resources.Admin.OnlistListNotUpdate + "\",");
        strScript.AppendLine("FieldMustCompleted:\"" + Fanex.Affiliate.Resources.Admin.FieldMustCompleted + "\",");
        strScript.AppendLine("GroupNameNotContainChar:\"" + Fanex.Affiliate.Resources.Admin.GroupNameNotContainChar + "\",");
        strScript.AppendLine("GroupAddSucc:\"" + Fanex.Affiliate.Resources.Admin.GroupAddSucc + "\",");
        strScript.AppendLine("InputWebUrl:\"" + Fanex.Affiliate.Resources.Admin.InputWebUrl + "\",");

        strScript.AppendLine("InputMem:\"" + Fanex.Affiliate.Resources.Admin.InputMem + "\",");
        strScript.AppendLine("InputAff:\"" + Fanex.Affiliate.Resources.Admin.InputAff + "\",");
        strScript.AppendLine("InvalidSettingFor:\"" + Fanex.Affiliate.Resources.Admin.InvalidSettingFor + "\",");
        strScript.AppendLine("MaxTierAllow:\"" + Fanex.Affiliate.Resources.Admin.MaxTierAllow + "\",");
        strScript.AppendLine("LastTierSettingHaveMaxValue:\"" + Fanex.Affiliate.Resources.Admin.LastTierSettingHaveMaxValue + "\",");
        strScript.AppendLine("TierSettingAtLeastTier:\"" + Fanex.Affiliate.Resources.Admin.TierSettingAtLeastTier + "\",");
        strScript.AppendLine("ConfirmUpdateGroup:\"" + Fanex.Affiliate.Resources.Admin.ConfirmUpdateGroup + "\",");

        strScript.AppendLine("ErrValidUserName:\"" + Fanex.Affiliate.Resources.Admin.ValidUsername + "\"};");        
        strScript.AppendLine("</script>");
        return strScript.ToString();
    }
}

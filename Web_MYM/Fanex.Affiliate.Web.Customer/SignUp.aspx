<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="Fanex.Web.MYM.Customer.SignUp" Codebehind="SignUp.aspx.cs" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>


<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>mymbet - Register, Signup sports betting affiliate program</title>
    <meta name="description" content="mymbet - Signup sports betting affiliate program, Registersports betting affiliate program, Open an account with affiliate programme" />
    <meta name="keywords" content="signup affiliate, signup affiliate program, signup sports betting affiliate program, register affiliate program, register sports betting affiliate program, open an account affiliate program" />    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <script src="<%=AppSettings.FullDomainName%>/App_Js/IBCWnd/IBCWnd.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>

    <style type="text/css">
        .payment
        {
            width: 180px;
        }
        .ex
        {
            color: #505050;
            display: none;
            font-size: 85%;
            margin-top: 0.2em;
        }
    </style>

    <script type="text/javascript">
    
    window.onload = function() {
        //ShowMethod();
        //MoreUrl();
        //AgreeTerm();
        $("txtFName").blur();
        $("txtFName").focus(); 
    }
    function ResetClick()
    {
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        document.getElementsByName("radio")[0].checked = true;
        //AgreeTerm();
       // ShowMethod();
    }
    /*function ShowMethod()
    {
        var divs = $('divPlayerAccount', 'divNeteller', 'divMoneyBooker', 'divBankTransfer');
        var radios = document.getElementsByName("radio");
        for(i =0; i < 4; i++)
        {
            divs[i].hide();
            if(radios[i].checked == 1)
            {
                divs[i].show();
                $("hdMethod").value = i;
            }
        }
    }*/
    function AgreeTerm()
    {
        if($('CheckTerm').checked)
        {
            $('btnSubmit').disabled = false;   
            $('btnSubmit').className = "button";
        }
        else
        {
            $('btnSubmit').disabled = true;
            $('btnSubmit').className = "disabled";
        }
    }
    function submitForm()
    {
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        var str = "";
        var firstItem = 0;
         $$("#siteUrl tr td input.sites").each(function (el) {
            if (firstItem == 0)
                firstItem = 1;
            else
                str += el.value + ";";
        });
        if (str.length > 0)
             str = str.substr(0, str.length - 1);

        $("hdMoreurl").value = str;
        if(!CheckSuccess())
            return false;
        return true;
    }
    function CheckSuccess() {

        $$("#Register_form input[type=text]").each(function(el){
            el.value = el.value.trim();
        });
        
        if(isEmpty($('txtFName').value))
            AddError("txtFName", ErrMsg.FName);
        if(isEmpty($('txtLName').value))
            AddError("txtLName", ErrMsg.LName);
        if (!document.getElementsByName("rdGender")[0].checked) {
            if(!document.getElementsByName("rdGender")[1].checked){
                AddError("rdGender", ErrMsg.ErrGender);
            }
        }
        if(isEmpty($('txtEmail').value))
            AddError("txtEmail", ErrMsg.Email);
        else
        {
            if(!checkMail($('txtEmail').value))
                AddError("txtEmail", ErrMsg.EmailValid);
            else
            {
                if($('txtEmail').value != $('txtConfirmEmail').value)
                    AddError("txtConfirmEmail", ErrMsg.EmailMatch);
            }
        }
        if (isEmpty($('txtNumber').value))
            AddError("txtNumber", ErrMsg.ContactNumber);
        if (isEmpty($('txtMobile').value))
            AddError("txtMobile", ErrMsg.MobileNumber);
            
        if(isEmpty($('txtAddress').value))
            AddError("txtAddress", ErrMsg.Address);
        if(isEmpty($('txtCity').value))
            AddError("txtCity", ErrMsg.City);
        if(isEmpty($('txtState').value))
            AddError("txtState", ErrMsg.State);
        if(isEmpty($('txtPost').value))
            AddError("txtPost", ErrMsg.PostCode);
        if(isEmpty($('selCountry').value))
            AddError("selCountry", ErrMsg.Country);
        if(isEmpty($('txtSiteUrl').value))
            AddError("txtSiteUrl", ErrMsg.SiteUrl);
        if(isEmpty($('slPreferLang').value))
            AddError("slPreferLang", ErrMsg.PreferLang);
        if (isEmpty($('slSecurityQuestion').value))
            AddError("slSecurityQuestion", ErrMsg.SecurityQuestion);
        if (isEmpty($('txtAnswer').value))
            AddError("txtAnswer", ErrMsg.YourAnswer);
        if (isEmpty($('txtValidateRegister').value) || $('txtValidateRegister').value == gMsg.entryvalidation)
            AddError("txtValidateRegister", gMsg.Errmsgentryvalidcode);
         
        var listSites = $$(".sites");
        for(var i = 1; i < listSites.length; i++)
        {
            if(listSites[i])
            {
                if(isEmpty(listSites[i].value))
                {
                    AddError(listSites[i].id, "<%=Fanex.Affiliate.Resources.Translator.Get("WebsiteURL") %> " + (i==0 ? "" : (i + 1).toString()) + " <%=Fanex.Affiliate.Resources.Translator.Get("IsRequired")%>");
                    break;
                }  
            }
        }        
        checkPlayerAccount();    

        if(arrItem.length > 0)
        {
            $('errMsg').show();
            ShowError(arrError, arrItem, "error_item");
            return false;
        } 
        return true;
    }
    function checkPlayerAccount()
    {
        if(isEmpty($('txtBet12UserName').value))
            AddError("txtBet12UserName", ErrMsg.UserNameRequire);
    }
    function AddOneMoreURL()
    {
        var listSites = $$(".sites");
        var siteIndex = listSites.length;        
        if(listSites[siteIndex - 1])
            if(isEmpty(listSites[siteIndex - 1].value))
            {
                alert("<%=Fanex.Affiliate.Resources.Translator.Get("WebsiteUrli") %>".replace("{i}", siteIndex.toString()));
                listSites[siteIndex - 1].focus();
                return;
            }
        siteIndex = siteIndex +1;
        var table = $("siteUrl");
        var tr = table.insertRow(table.rows.length);
        var td1 = IBCWnd.createElement("td", tr);
        var td2 = IBCWnd.createElement("td", tr);
        var td3 = IBCWnd.createElement("td", tr);
        tr.setAttribute("id", "tr" + siteIndex);
        td2.className = "cont12oe";
        td2.innerHTML = "*";
        td1.innerHTML = "<%=Fanex.Affiliate.Resources.Translator.Get("WebsiteURL") %> " + siteIndex.toString();
        td3.innerHTML = "<input type='text' size='30' class='sites' id='txtSiteUrl" + siteIndex + "' name='txtSiteUrl" + siteIndex + "' />&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + siteIndex + ");\" class=\"button\" value=\"<%=Fanex.Affiliate.Resources.Translator.Get("Del") %>\" />";
        if($("txtUrl" + siteIndex))
            $("txtUrl" + siteIndex).focus();
    }
    function DelUrl(rowNum)
    {        
        $("siteUrl").deleteRow(rowNum - 1);
        reorderRows(rowNum - 1);
    }
    function reorderRows(starting)
    {
        var tbl = $("siteUrl");
        if (tbl.tBodies[0].rows[starting]) {            
            for(var i = starting; i < tbl.tBodies[0].rows.length; i++)
            {          
                var tr = tbl.tBodies[0].rows[i];
                var tds = tr.getElementsByTagName("td");
                
                var index = i + 1;
                tds[0].innerHTML = "Website URL " + index.toString();
                tds[2].innerHTML = "<input type='text' size='30' value='" + tds[2].childNodes[0].value + "' class='sites' id='txtUrl" + index + "' name='txtUrl" + index + "' />&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + index + ");\" class=\"button\" value=\"<%=Fanex.Affiliate.Resources.Translator.Get("Del") %>\" />";
            }
        }
    }
    </script>
    
    <form id="Register_form" name="Register_form" action="Handler/SignUp.ashx" method="post"
                    onsubmit="return submitForm();" target="frRegister">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>                        
                        <td class="in_top"><asp:Literal ID="ltrRegisForm" runat="server"></asp:Literal></td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <asp:Literal runat="server" ID="ltrCompleteForm"></asp:Literal>
                <br />
                <asp:Literal ID="ltrRequireInfo" runat="server"></asp:Literal>
                <br /><br />
                <input id="CliectAct" name="ClientAct" type="hidden" />
                <div id="errMsg" style="display: none;" class="msg-wid">
                    <div id="divErr">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 40px;" valign="top">
                                    <div class="warning">
                                        &nbsp;</div>
                                </td>
                                <td valign="top">
                                    <div id="error_item">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrContactInfo"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td>
                            <asp:Literal ID="ltrFName" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtFName" maxlength="50" class="inputbox" id="txtFName" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrLName" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtLName"  maxlength="50" class="inputbox" id="txtLName" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrGender" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input id="rdGender" class="noborder" type="radio" value="1" name="rdGender" />
                            <%=Fanex.Affiliate.Resources.Translator.Get("Male") %>
                            <input id="rdGender" class="noborder" type="radio" value="0" name="rdGender" />
                            <%=Fanex.Affiliate.Resources.Translator.Get("Female") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtEmail"  maxlength="50" class="inputbox" id="txtEmail" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; white-space: normal;">
                            <asp:Literal ID="ltrReEmail" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtConfirmEmail" id="txtConfirmEmail" class="inputbox"  maxlength="50"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrContactNumber" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtNumber"  maxlength="50" id="txtNumber" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtMobile" maxlength="50" id="txtMobile" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal ID="ltrMailingInfor" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtAddress" maxlength="80" class="inputbox" id="txtAddress" size="30"
                                type="text" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrCity" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtCity"  maxlength="50" class="inputbox" id="txtCity" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="ltrState"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtState" maxlength="50" id="txtState" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtPost" maxlength="50" id="txtPost" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <%= Fanex.Affiliate.Core.Helper.HtmlHelper.DropDownList(Fanex.Affiliate.Core.AppSettings.BaseCountries, "selCountry", "", Fanex.Affiliate.Resources.Translator.Get("SelectOne"),  null, false)  %>
                            <%--<select name="selCountry" id="selCountry">
                                <option value="">
                                    <%=Fanex.Affiliate.Resources.Translator.Get("SelectOne") %></option>
                                <%foreach (DataRow dr in Fanex.Affiliate.Resources.TextExpressionBuilder.GetListCountry())
                                  { %>    
                                 <option value="<%=dr["ResourceKey"] %>">
                                    <%=dr["ResourceValue"] %>
                                </option>
                                <%} %>
                            </select>--%>
                        </td>
                    </tr>
                </table>
                <br />
                 <div class="box_conts_header">
                    <asp:Literal ID="ltrSiteinfor" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table id="siteUrl" width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal runat="server" ID="ltrSiteurl"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtSiteUrl" maxlength="100" id="txtSiteUrl" class="inputbox sites" size="30"
                                type="text" />
                            &nbsp;&nbsp;&nbsp;<input type="button" class="button" onclick="AddOneMoreURL();" value="<%=Fanex.Affiliate.Resources.Translator.Get("AddUrl")%>" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal ID="ltrPrefer" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                     <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrLang" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>                            
                            <%=languageList %>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrCurrency" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <%=currencyList %>                       
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="ltrMailOption" runat="server"></asp:Literal></td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input type="radio" size="30" value="1" id="rdFormat" name="rdFormat"/>
                            &nbsp;<asp:Literal ID="ltrPlainText" runat="server"></asp:Literal>
                            &nbsp;<input type="radio" size="30" value="0" checked id="Radio1" name="rdFormat"/>
                            &nbsp;<asp:Literal ID="ltrHtmlFormat" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Fanex.Affiliate.Resources.Translator.Get("lblSecureQuestion")%>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <select id="slSecurityQuestion" name="slSecurityQuestion">
                                <option selected="" value=""><%=Fanex.Affiliate.Resources.Translator.Get("SelectOne") %></option>
                                <%=strSecureQuestion%>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Fanex.Affiliate.Resources.Translator.Get("lblYourAnswer")%>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input type="text" autocomplete="off" maxlength="300" class="inputbox"
                                size="30" value="" id="txtAnswer" name="txtAnswer" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Fanex.Affiliate.Resources.Translator.Get("Validation")%>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtValidateRegister" id="txtValidateRegister" type="text" class="inputbox"
                                 style="width: 80px;" align="absmiddle" autocomplete="off" />
                            <img id="validCodeRegister" height="20px" width="40px" alt="Valiation Code" src="Authorization/LoginCaptcha.ashx"
                                align="absmiddle" onclick="ChangeCapchaCode(this);" class="hand" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal ID="ltrPaymentInfor" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="3" class="b">
                            <asp:Literal runat="server" ID="ltrPaymentMethod"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top">
                                    <td valign="top" width="37%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <!--PlayerAccount-->
                                                    <input name="radio" class="noborder" value="0" type="radio" checked />
                                                    <asp:Literal runat="server" ID="ltrAccountPlayerAccount"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" width="63%">
                                        <div id="divPlayerAccount">
                                            <table class="ContentInput">
                                                <tr valign="top">
                                                    <td class="payment">
                                                        <asp:Literal runat="server" ID="ltr12UserName"></asp:Literal>
                                                    </td>
                                                    <td class="cont12oe">
                                                        *
                                                    </td>
                                                    <td>
                                                        <input name="txtBet12UserName" maxlength="30" class="inputbox" id="txtBet12UserName" size="28" type="text" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>                                        
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td colspan="3">
                            <div class="submit-bar">
                                <div align="center">
                                    <input name="btnSubmit" id="btnSubmit" class="button" value="<%=Fanex.Affiliate.Resources.Translator.Get("Submit") %>"
                                        type="submit" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                
            </div>
        </div>        
    </div>
    <input type="hidden" value="12" id="hdMethod" name="hdMethod" />
    <input type="hidden" value="" id="hdMoreurl" name="hdMoreurl" />
    </form>
    <iframe id="frRegister" name="frRegister" class="hide" style="height: 0px; width: 0px;"></iframe>
</asp:Content>

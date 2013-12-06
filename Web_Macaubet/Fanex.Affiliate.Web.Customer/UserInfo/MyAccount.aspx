<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="MyAccount" Codebehind="MyAccount.aspx.cs" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="<%=AppSettings.FullDomainName%>/App_Js/IBCWnd/IBCWnd.js?version=<%=AppSettings.JsVersion%>"></script>
    <style type="text/css">
        .payment
        {
            width: 170px;
        }
    </style>

    <script type="text/javascript">
    var radios = document.getElementsByName("radio");
    var genders = document.getElementsByName("rdGender");
    var formats = document.getElementsByName("rdFormat");
    window.onload=function(){GetMethod();}
    function GetMethod()
    {        
        var index = parseInt($('hdMethod').value);
        radios[index].checked = true;
        <%if(hasGender){ %>
            <%if(isGender){%>
                genders[0].checked = true;
            <%}else{%>
                genders[1].checked = true;
            <%}
        }%>
        
        <%if(Fanex.Affiliate.Core.Customer.AffSession.MailOption){%>
            formats[0].checked = true;
        <%}else{ %>
            formats[1].checked = true;
        <%} %>
        
        $('slPreferLang').value = "<%=strPreferLang %>";
        $('slCurrency').value = "<%=strCurrency %>";
        if(!$('slCurrency').value)
        {            
               $('slCurrency').selectedIndex = 0;
        }
        $('selCountry').value = "<%=strCountry %>";
        $("txtAddress").blur();
        $("txtAddress").focus(); 
        var moreurl = "<%=strMoreSiteUrl %>";
        
    }
    function submitForm()
    {        
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        
        $$("#Update_form input[type=text]").each(function(el){

            el.value = el.value.trim();
        });
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
    function CheckSuccess()
    {      
        
        if (!genders[0].checked) {
            if(!genders[1].checked){
                AddError("rdGender", ErrMsg.ErrGender);
            }
        }
        
        if(isEmpty($("txtAddress").value))
        {
            AddError("txtAddress", ErrMsg.Address);            
        }
        
        //disable nickname
        /*if($('txtNickname'))
        {
            $("spCheckNickname").innerHTML = "";
            if(!isEmpty($('txtNickname').value))
                if(!checkUsername())
                    AddError("txtNickname", ErrMsg.Nickname);
        }*/
        if(isEmpty($('slPreferLang').value))
            AddError("slPreferLang", ErrMsg.PreferLang);
        if(isEmpty($('slCurrency').value))
            AddError("slCurrency", ErrMsg.Currency);
        var listSites = $$(".sites");
        for(var i = 0; i < listSites.length; i++)
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
        // checkPlayerAccount(); 
        if(arrItem.length > 0)
        {
            DisplayMsg(false);
            return false;
        } 
        return true;
    }
    function checkPlayerAccount()
    {
        if(isEmpty($('txtBet12UserName').value))
            AddError("txtBet12UserName", ErrMsg.UserNameRequire);
    }
    function UpdateSuccess()
    {        
        DisplayMsg(true);
        window.scroll(0, 0);
        setTimeout("location.href = location.href;", 3000);
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
                tds[0].innerHTML = "<%=Fanex.Affiliate.Resources.Translator.Get("WebsiteURL") %> " + index.toString();
                tds[2].innerHTML = "<input type='text' size='30' value='" + tds[2].childNodes[0].value + "' class='sites' id='txtUrl" + index + "' name='txtUrl" + index + "' />&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + index + ");\" class=\"button\" value=\"<%=Fanex.Affiliate.Resources.Translator.Get("Del") %>\" />";
            }
        }
    }
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        
                        <td class="in_top">
                            <div class="title"><span><asp:Literal ID="ltrMyAcc" runat="server"></asp:Literal></span></div>
                            <div class="title_detail"></div>
                        </td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
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
                <div id="succMsg" style="display: none;" class="msg-wid">
                    <div id="divSuccess">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 40px;" valign="top">
                                    <div class="icsuccess">
                                        &nbsp;</div>
                                </td>
                                <td valign="top">
                                    <ul>
                                        <li><span class="success" id="succ_item">
                                            <%=Fanex.Affiliate.Resources.Translator.Get("UpdateAccSuccess")%>
                                        </span></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrAccInfo"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <form id="Update_form" name="Update_form" action="../Handler/UpdateAcc.ashx" method="post" onsubmit="return submitForm()" target="frRegister">                
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrUser" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe" style="width: 14px;">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="ltrUsername"></asp:Literal>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Literal ID="ltrPass" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            &nbsp;
                        </td>
                        <td>
                            <a href="ChangePass.aspx">
                                <%=Fanex.Affiliate.Resources.Translator.Get("Change")%></a>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrContactInfo"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <%=Fanex.Affiliate.Resources.Translator.Get("Gender") %>
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
                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtAddress" class="inputbox" id="txtAddress" value="<%=strAddress %>"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrCity" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <input name="txtCity" class="disable" readonly="readonly" disabled="disabled" id="txtCity" value="<%=strCity %>"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="ltrState"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <input name="txtState" id="txtState" class="disable" disabled="disabled" readonly="readonly" value="<%=strState %>"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <input name="txtPost" id="txtPost" disabled="disabled" class="disable" readonly="readonly" value="<%=strPost%>"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <%= Fanex.Affiliate.Core.Helper.HtmlHelper.DropDownList(Fanex.Affiliate.Core.AppSettings.BaseCountries, "selCountry", strCountry, Fanex.Affiliate.Resources.Translator.Get("SelectOne"), null, true)%>
                            <%--<select name="selCountry" id="selCountry" style="size: 30;" disabled="disabled">
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
                    <tr>
                        <td>
                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <input name="txtEmail" class="disable" disabled="disabled" readonly="readonly" id="txtEmail" value="<%=strEmail%>"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrContactNumber" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <input name="txtNumber" id="txtNumber" disabled="disabled" class="disable" readonly="readonly" value="<%=strNumber %>" size="30"
                                type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                            <input name="txtMobile" id="txtMobile"  disabled="disabled" class="disable" readonly="readonly" value="<%=strMobile %>" size="30"
                                type="text" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrSiteinfor"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2" id="siteUrl">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal runat="server" ID="ltrSiteurl"></asp:Literal>
                        </td>
                        <td class="cont12oe">&nbsp;&nbsp;</td>
                        <td>
                            <input name="txtSiteUrl" id="txtSiteUrl" disabled="disabled" class="disable sites" value="<%=strSiteUrl %>"
                                size="30" type="text" readonly="readonly" />
                            &nbsp;&nbsp;&nbsp;<input type="button" class="button" onclick="AddOneMoreURL();" value="<%=Fanex.Affiliate.Resources.Translator.Get("AddUrl")%>" />
                        </td>
                    </tr>
                    <%=strWebsites %>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrPrefer"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrLang" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe" style="width: 14px;">*</td>
                        <td>
                        <%=Fanex.Affiliate.Core.Helper.HtmlHelper.DropDownList(
                            Fanex.Affiliate.Core.AppSettings.BaseLanguages,
                                                        "slPreferLang", strPreferLang, Fanex.Affiliate.Resources.Translator.Get("SelectOne"))%>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrCurrency" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                        </td>
                        <td>
                        <%=Fanex.Affiliate.Core.Helper.HtmlHelper.DropDownList(
                            Fanex.Affiliate.Core.AppSettings.AffCurrencies,
                              "slCurrency", strCurrency,false,"disabled",true)%> 
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrMailOption" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">*</td>
                        <td>
                            <input type="radio" size="30" value="1" id="rdFormat" name="rdFormat"/>
                            &nbsp;<asp:Literal ID="ltrPlainText" runat="server"></asp:Literal>
                            &nbsp;<input type="radio" size="30" value="0" id="Radio1" name="rdFormat"/>
                            &nbsp;<asp:Literal ID="ltrHtmlFormat" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrPaymentInfor"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="3">
                            <asp:Literal runat="server" ID="ltrPaymentMethod"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" id="payment">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top">
                                        <td valign="top" width="37%">
                                            <table class="ContentInput">
                                                <tbody>
                                                    <tr valign="top">
                                                        <td>
                                                            <!--PlayerAccount-->
                                                            <input name="radio" class="noborder" value="0" type="radio" checked />
                                                            <asp:Literal runat="server" ID="ltrAccountPlayerAccount"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td valign="top" width="63%">
                                            <div id="divPlayerAccount" style="padding-top: 3px;">
                                                <table class="ContentInput">
                                                    <tr valign="top">
                                                        <td class="payment">
                                                            <asp:Literal runat="server" ID="ltr12UserName"></asp:Literal>
                                                        </td>
                                                        <td class="cont12oe">
                                                        </td>
                                                        <td>
                                                            <input name="txtBet12UserName" disabled="true" class="inputbox" value="<%=strBet12UserName %>" id="txtBet12UserName"
                                                                size="28" type="text" />
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
                <div class="submit-bar">
                    <div align="center">
                        <input name="btnSubmit" id="btnSubmit" class="button" value="<%=Fanex.Affiliate.Resources.Translator.Get("Submit") %>"
                            type="submit" />
                        <input name="btnCancel" id="btnCancel" value="<%=Fanex.Affiliate.Resources.Translator.Get("Cancel")%>" class="button"
                            type="button" onclick="javascript:window.location.href='Account_Overview.aspx';" />
                    </div>
                </div>
                <input type="hidden" value="<%=strMethod %>" id="hdMethod" name="hdMethod" />
                <input type="hidden" value="<%=strPayment %>" id="hdPayment" name="hdPayment" />
                <input type="hidden" value="<%=strPlayerAccount %>" id="hdPlayerAccount" name="hdPlayerAccount" />                
                <input type="hidden" value="" id="hdMoreurl" name="hdMoreurl" />
                <input type="hidden" value="<%=currencyCode%>" id="hdCurrency" name="hdCurrency" /> 
                </form>
                <iframe id="frRegister" name="frRegister" class="hidden" style="height: 0px; width: 0px; display: none;"></iframe>
            </div>
        </div>
    </div>
</asp:Content>

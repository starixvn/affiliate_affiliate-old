<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="ChangeSecurityQuestion" Codebehind="ChangeSecurityQuestion.aspx.cs" %>

<%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
     <style type="text/css">
         .ex {
                color:#505050;
                display:none;
                font-size:85%;
                margin-top:0.2em;
           }
    </style> 
    <script type="text/javascript" src="<%=AppSettings.FullDomainName%>/App_Js/wz_tooltip.js?version=<%=AppSettings.JsVersion%>
"></script>

    <script type="text/javascript">
    function Reset() {
        arrError.clear();
        arrItem.clear();
        $("errMsg").hide();
    }
    function CheckValid()
    {
        Reset();
        if (isEmpty($('slSecurityQuestion').value))
            AddError("slSecurityQuestion", ErrMsg.SecurityQuestion);
        if (isEmpty($('txtAnswer').value))
            AddError("txtAnswer", ErrMsg.YourAnswer);
        
        if (arrItem.length > 0) {
            $('errMsg').show();
            ShowError(arrError, arrItem, "error_item");
            return false;
        }     
        return true;      
    }
    
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td width="144" class="in_top">
                            <asp:Literal ID="ltrChangeSecureQuestion" runat="server"></asp:Literal>
                        </td>
                        <td class="in_right">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <br />
            <form id="Update_form" name="Update_form" onsubmit="return CheckValid();" action="../Handler/UpdateSecurityQuestion.ashx"
                method="post" target="frChangePass">
                <div id="in_conts">
                    <table width="100%" cellspacing="0" cellpadding="0" style="margin-bottom: 5px">
                        <tr>
                            <td class="box-left">
                            </td>
                            <td valign="top" bgcolor="#fdfdfd">
                                <div class="padd15">
                                    <div id="errMsg" style="display: none; width: 550px; margin-bottom: 10px;" class="msg-wid">
                                        <div id="divErr">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td style="width: 40px;" valign="top">
                                                        <div class="warning">
                                                            &nbsp;
                                                        </div>
                                                    </td>
                                                    <td valign="top">
                                                        <div id="error_item">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="ContentBox">
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td colspan="5" >
                                                        <%=Fanex.Affiliate.Resources.Translator.Get("UpdateSecurityQuestionRemind") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="height: 10px">
                                                       <br />
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <%=Fanex.Affiliate.Resources.Translator.Get("lblSecureQuestion")%>
                                                                  </td>
                                                                <td class="cont12oe">
                                                                    *</td>
                                                                <td>
                                                                  
                                                                    <select id="slSecurityQuestion" name="slSecurityQuestion" tabindex="22">                     
                                                                        <option selected="" value=""><%=Fanex.Affiliate.Resources.Translator.Get("SelectOne") %></option>
                                                                         <%=strSecureQuestion%>                                           
						                                            </select>
                            						                						
						                                         </td>
						                                         <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <%=Fanex.Affiliate.Resources.Translator.Get("lblYourAnswer")%></td>
                                                                <td class="cont12oe">
                                                                    *</td>
                                                                <td>
                                                                   <input type="text" autocomplete="off" tabindex="23" maxlength="100" class="inputbox" size="30" value="" id="txtAnswer" name="txtAnswer"/>                                            
                                                                 </td>
                                                                 <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td>                                                        
                                                                    <input name="btnSubmit" id="btnSubmit" type="submit" class="button" value="<%=Fanex.Affiliate.Resources.Translator.Get("Submit") %>"/>
                                                                    <input name="btnCancel" id="btnCancel" value="<%=Fanex.Affiliate.Resources.Translator.Get("Cancel") %>" class="button" type="reset" onclick="Reset();"/>
                                                                 </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </td>
                            <td class="box-right">
                            </td>
                        </tr>
                        <tr>
                            <td class="box-bottomleft" />
                            <td class="box-bottomcenter" />
                            <td class="box-bottomright" />
                        </tr>
                    </table>
                </div>
                <input type="hidden" value="<%=Fanex.Affiliate.Resources.Translator.Get("PassCheck") %>" id="hdPassCheck" name="hdPassCheck" />
            </form>
        </div>
    </div>    
    <iframe id="frChangePass" name="frChangePass" class="hide" style="height: 0px; width: 0px;"></iframe>
</asp:Content>

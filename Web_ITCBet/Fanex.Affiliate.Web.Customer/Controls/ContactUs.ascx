<%@ Control Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.ITCBet.Customer.Controls_ContactUs" Codebehind="ContactUs.ascx.cs" %>

<script type="text/javascript">
            function ShowError(errMsgs,tagIds,placeHolder)
            {	
	            var errHtml="";	
	            $(placeHolder).style.display=(errMsgs.length==0)?'none':'';
	            for(var i=0;i<errMsgs.length;++i){
	                errHtml+="<li><a class='err-msg' href='' onclick=\"$('"+tagIds[i]+"').focus(); return false;\">"+errMsgs[i]+"</a></li>";
	            }
	            $(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
            }
            function submitForm()
            {
                arrItem.clear();
                arrError.clear();
                $('errMsg').hide();
                if(CheckSuccess())
                    return true;
                return false;
                    //$("ContactUS_form").submit();
            }
            function CheckSuccess()
            {
                if(isEmpty($('txtName').value))
                    AddError("txtName", ErrMsg.Name);
                if(isEmpty($('txtSubject').value))
                    AddError("txtSubject", ErrMsg.Subject);
                if(!checkMail($('txtEmail').value))
                    AddError("txtEmail", ErrMsg.EmailValid);
                if(isEmpty($('txtContent').value))
                    AddError("txtContent", ErrMsg.Comment);
                if (isEmpty($('txtValidateRegister').value) || $('txtValidateRegister').value == '<%=Fanex.Affiliate.Resources.Translator.Get("Validation")%>')
                    AddError("txtValidateRegister", ErrMsg.Errmsgentryvalidcode);
                    
                if(arrItem.length > 0)
                {
                    DisplayMsg(false);
                    return false;
                }              
                return true;
           }
           
           function SendMailSuccess()
           {
                $('errMsg').hide();
                $('dvContact').hide();
                $('dvMessageSentSuccuess').show();
                //$('spHeader').innerHTML = '<%=Fanex.Affiliate.Resources.Translator.Get("MessageSent") %>';
                ChangeCapchaCode($('validCodeRegister'));                             
           }
           
           function ClearFileds()
           {
                $('errMsg').hide();
                var arrID = ['txtName', 'txtSubject', 'txtEmail', 'txtContent', 'txtAffID'];
                for(i = 0; i<arrID.length; i++)
                {
                    $(arrID[i]).value='';
                }
                $(arrID[0]).focus();
           }
           
           function SendMailFail(strErr)
           {
                $('errMsg').style.display='';
                $('dvContact').show();
                 arrError.push(strErr);
                 arrItem.push("txtEmail");
                 ShowError(arrError, arrItem, "error_item");
                 ChangeCapchaCode($('validCodeRegister'));                                 
           }
           
           function tt()
           {
                alert('ok');
            }          
</script>

<iframe id="frmContactUS" name="frmContactUS" style="display: none;" width="0" height="0"
    frameborder="0"></iframe>
<form id="ContactUS_form" name="ContactUS_form" method="post" action="<%=Action %>"
    target="frmContactUS" onsubmit="return submitForm();">
    <div id="errMsg" style="width: 500px; display: none; margin-top: 0px;
        margin-bottom: 0;">
        <div id="divErr" >
            <table style="width: 500px;" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 60px;" rowspan="2" valign="top">
                        <div class="warning">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <div id="error_item">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dvContact" style="width: <%=WidthTable %>;">
        <table class="ContentInput" border="0" cellpadding="3" cellspacing="0" style="width: <%=WidthTable %>;">
            <tr>
                <td colspan="6"><asp:Literal ID="ltrContactTitle" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 65px;"><asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
                <td class="cont12oe" style="width: 3%;">*</td>
                <td>
                    <input class="inputbox" type="text" size="<%=Size %>" id="txtName" name="txtName"
                        value="<%=Name %>" /></td>
                <td style="width: 65px;">
                    <asp:Literal ID="ltrAffiliateID" runat="server"></asp:Literal></td>
                <td style="width: 3%;">
                </td>
                <td>
                    <input class="inputbox" type="text" size="<%=Size %>" id="txtAffID" name="txtAffID"
                        value="<%=AffID %>" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrSubject" runat="server"></asp:Literal></td>
                <td class="cont12oe">
                    *</td>
                <td>
                    <input class="inputbox" type="text" size="<%=Size %>" id="txtSubject" name="txtSubject" /></td>
                <td>
                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal></td>
                <td class="cont12oe">
                    *</td>
                <td>
                    <input class="inputbox" type="text" size="<%=Size %>" id="txtEmail" name="txtEmail"
                        value="<%=Email %>" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
                </td>
                <td class="cont12oe">
                    *</td>
                <td colspan="4">
                    <textarea class="inputbox" id="txtContent" name="txtContent" rows="6" style="width: <%=WidthTextArea %>;"></textarea>
                </td>
            </tr>
             <tr>
                <td>
                    <%=Fanex.Affiliate.Resources.Translator.Get("Validation")%></td>
                <td class="cont12oe">
                    *</td>
                <td style="white-space:nowrap;" colspan="4">
                   <input name="txtValidateRegister" id="txtValidateRegister" type="text" class="inputbox" value="<%=Fanex.Affiliate.Resources.Translator.Get("Validation") %>" style="width: 80px;" align="absmiddle" onfocus="setvaluefocus(this,1,'val1');" onblur="setvaluefocus(this,0,'val1');" autocomplete="off" />
                    <img id="validCodeRegister" height="20px" width="40px" alt="Valiation Code" src="Authorization/LoginCaptcha.ashx" align="absmiddle" onclick="ChangeCapchaCode(this);" class="hand" />
                    <input id="val1" name="val1" type="hidden" value="<%=Fanex.Affiliate.Resources.Translator.Get("Validation") %>"/>
                 </td>
             </tr>
            <tr>
                <td colspan="2">
                </td>
                <td colspan="4" style="text-align: center; line-height: 30px; padding-right: 11px;">
                    <input class="button" type="submit" id="btnSendMsg" name="btnSendMsg" value="<%=Fanex.Affiliate.Resources.Translator.Get("Submit") %>"
                        onclick="submitForm();" />
                    <input class="button" type="Reset" id="btnClear" name="btnClear" value="<%=Fanex.Affiliate.Resources.Translator.Get("Reset") %>"
                        onclick="ClearFileds();" />
                </td>
            </tr>
            <tr style="height: 5px;">
                <td colspan="6">
                </td>
            </tr>
        </table>
    </div>
    <div class="ContentBox" id="dvMessageSentSuccuess" style="display: none;">
        <div class="pad15">
            <asp:Literal ID="ltrSuccess" runat="server"></asp:Literal>
        </div>
        <br />
    </div>
    <%=script%>
</form>

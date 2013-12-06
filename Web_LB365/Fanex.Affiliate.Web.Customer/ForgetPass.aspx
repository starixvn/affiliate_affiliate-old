<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Fanex.Web.LiveBet365.Customer.ForgetPass" Codebehind="ForgetPass.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript">
function ExecRecover()
{     
	arrItem.clear();
	arrError.clear();
	$('errMsg').hide();
		
	$('contentsuc').style.display='none';
	if($('txtemail').value=='') {
		AddError("txtemail", "<%=emailrequire %>");      
	}
	else if (!checkMail($('txtemail').value))
	{
		AddError("txtemail", ErrMsg.EmailValid);
	}
	  
	if ($('txtvalidcode').value == '') {
		AddError("txtemail", gMsg.Errmsgentryvalidcode);
	}
	
	if(arrItem.length > 0)
	{
		$('errMsg').show();
		ShowError(arrError, arrItem, "error_item");
		return false;
	} 
	else
	{
	    var ajax = new Ajax.Request("Handler/CheckForgetPassWord.ashx", {
	        method: 'post',
	        parameters: 'eml=' + $('txtemail').value + '&vcode=' + $("txtvalidcode").value,
	        onComplete: function (datastran) {
	            if (datastran.responseText == 'Ok') {
	                $('contentsuc').style.display = 'block';
	                //$('content').style.display='none';
	                $('errMsg').style.display = 'none';
	                $('tablContent').hide();
	                $('err').innerHTML = '';
	            }
	            else {
	                AddError("error_item", datastran.responseText);
	                $('errMsg').show();
	                ShowError(arrError, arrItem, "error_item");	                
	                $('validCode2').src = 'Authorization/LoginCaptcha.ashx?t=' + new Date().toJSON();
	            }
	        }
	    });       
		
	}    
}
function Reset()
{
	$('txtemail').value='';
	$('errMsg').style.display='none';
}
function EnterEvent(e){        
	e =(e) ?e :window.event;
	var key =(e.keyCode) ?e.keyCode :e.which;
	if (key ==13) ExecRecover();
}
</script>
<div id="contents">
	<div id="box_insite">
		<div class="header_in">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr>					
					<td width="220" class="in_top"><asp:Literal ID="ltrtitelrecoveruspw" runat="server"></asp:Literal></td>
					<td class="in_right">&nbsp;</td>
				</tr>                
			</table>
		</div>
		<div class="in_conts">
			<div style="width: 570px; margin-top: 0px; margin-bottom: 0pt; display:none;" id="errMsg">
				<div id="divErr">
					<table cellspacing="0" cellpadding="0" border="0" style="">
						<tr>
							<td valign="top" rowspan="2" style="width: 60px;">
								<div class="warning">
								</div>
							</td>
						</tr>
						<tr>
							<td valign="top">
								<div id="error_item"><ul id="err"></ul></div>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<div id="contentsuc" name="contentsuc" style="display:none;" class="ContentBox pad15">
				<asp:Literal ID="ltrmsgsucc" runat="server"></asp:Literal>
			</div>
			<table border="0" cellpadding="3" cellspacing="0" style="width: 100%" id="tablContent">      
				<tr>
					<td colspan="2" >                        
						<span class="box_conts_detail"><asp:Literal ID="ltrtitleforget" runat="server"></asp:Literal></span>
					</td>
					<td rowspan="3"></td>
				</tr>
				<tr>
					<td style="width:130px; height: 35px;" class="b">
						<asp:Literal ID="ltryouremail" runat="server"></asp:Literal>                        
					</td>
					<td style="width:320px;">
						<input id="txtemail" name="txtemail" type="text" style="width: 246px" onkeyup="EnterEvent(event)" /></td>
				</tr>
				<tr>
					<td style="height: 35px;" class="b">
						<%=Fanex.Affiliate.Resources.Translator.Get("Validation") %>
					</td>
					<td style="width:320px;">
						<input id="txtvalidcode" name="txtvalidcode" type="text" style="width: 80px" onkeyup="EnterEvent(event)" />
						&nbsp;&nbsp;<img id="validCode2" height="20px" width="40px" alt="Valiation Code" src="Authorization/LoginCaptcha.ashx"
						align="absmiddle" onclick="ChangeCapchaCode(this);" class="hand" />
					</td>
				</tr>                          
				<tr>          
					<td colspan="2" class="c"><input id="Button1" class="button" type="button" value="<%=submit %>" onclick="ExecRecover();" /></td>                           
				</tr>                          
			</table>
		</div>
	</div>
</div>
</asp:Content>


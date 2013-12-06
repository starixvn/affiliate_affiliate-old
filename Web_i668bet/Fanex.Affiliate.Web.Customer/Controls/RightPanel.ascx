<%@ Control Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.i668bet.Customer.Controls_RightPanel" Codebehind="RightPanel.ascx.cs" %>
<%@ Import Namespace="Fanex.Affiliate.Resources" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>

<!--login-->
<div runat="server" id="LoginPanel">
	<div class="box_right_header">
		<%=strTitleLogin %>
	</div>
	<div class="box_right_cons01">
		<form id="frLogin" name="frLogin" onsubmit="return ExcLogin();" method="post" action="">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
			<tr>
				<td width="9%">
					<div class="icuser">
					</div>
				</td>
				<td height="23" colspan="3">
					<%=Translator.Get("Username") %>:                    
				</td>
			</tr>
			<tr>
				<td>
				</td>
				<td height="23" colspan="3">
					<input name="txtusername" id="txtusername" type="text" style="width: 160px;" />
				</td>
			</tr>
			<tr>
				<td>
					<div class="icpass">
					</div>
				</td>
				<td height="23" colspan="3">
					<%=strPwd %>:
				</td>
			</tr>
			<tr>
				<td />
				<td height="23" colspan="3">
					<input name="txtpassword" id="txtpassword" type="password" style="width: 160px;" />
				</td>
			</tr>
			<tr>
				<td />
				<td height="23" width="32%">
					<input name="txtvalidate" id="txtvalidate" type="text" class="login" value="<%=Translator.Get("Validation") %>"
						style="width: 70px;" align="absmiddle" onfocus="setvaluefocus(this,1,'val');"
						onblur="setvaluefocus(this,0,'val');" />
				</td>
				<td height="23" width="19%">
					<img id="validCode" height="20px" width="40px" alt="Valiation Code" src="Authorization/LoginCaptcha.ashx"
						align="absmiddle" onclick="ChangeCapchaCode(this);" class="hand" />
				</td>
				<td height="23" width="40%">
					<input name="btnlogin" id="btnlogin" class="button" type="submit" value="<%=strLogin %>" />
				</td>
			</tr>
			<tr>
				<td />
				<td height="23" colspan="3">
					<a class="link_black" runat="server" href="~/SignUp.aspx">
						<%=strSignup %></a> | <a runat="server" class="link_black" href="~/ForgetPass.aspx">
							<%=strForgetPwd %></a>
				</td>
			</tr>
		</table>
		<input id="val" name="val" type="hidden" value="<%=Translator.Get("Validation") %>"/>
		</form>
	</div>

    <div class="box_right">
        <div class="box_right_header"><%=Translator.Get("Faqs")%></div>
        <div class="box_right_cons02">
            <a id="A1" class="link_white" runat="server" href="~/FAQs.aspx#f1">1. <%=Translator.Get("WhatIsAffiliates")%></a> 
            <a id="A2" class="link_white" runat="server" href="~/FAQs.aspx#f2">2. <%=Translator.Get("HowDoISignUp")%></a> 
            <a id="A3" class="link_white" runat="server" href="~/FAQs.aspx#f3">3. <%=Translator.Get("WhatIfIDontHave")%></a> 
            <a id="A4" class="link_white" runat="server" href="~/FAQs.aspx#f4">4. <%=Translator.Get("WhereDoIGetBanners")%></a> 
            <a id="A5" class="link_white" runat="server" href="~/FAQs.aspx#f5">5. <%=Translator.Get("HowDoesTheSystem")%></a> 
            <a id="A6" class="link_white" runat="server" href="~/FAQs.aspx#f9">6. <%=Translator.Get("HowCanICheck")%></a> 
            <a id="A7" class="link_white" runat="server" href="~/FAQs.aspx#f11">7. <%=Translator.Get("WhatAreTheWithdrawal")%></a> 
            <a id="A8" class="link_white" runat="server" href="~/FAQs.aspx#f13">8. <%=Translator.Get("WhatIsTheMinimumWithdrawal")%></a>
        </div>
    </div>
    <div id="aSignup" style="margin-bottom: 5px;" runat="server">     
    <div id="flashcontent">
        <a href="<%=ResolveUrl("~/SignUp.aspx") %>"><img src="<%=AppSettings.FullDomainName%>/App_Themes/Default/Images/sign-up_<%=strLang%>.jpg?version=<%=AppSettings.JsVersion%>" /></a>
    </div>
</div>
</div>
<!--/login-->
<!--After login-->
<div class="box_right" id="divAfterLogin" runat="server">        
	<div class="box_right_header">
		<div class="icon_top"><asp:Literal ID="ltrUserName" runat="server"></asp:Literal></div>        
	</div>
	<div id="rightmenu">
			<span><a runat="server" href="~/UserInfo/Account_Overview.aspx" id="lnkAccOverview"><%=Translator.Get("AccOverview")%></a></span>
			<span><a runat="server" href="~/UserInfo/StatementHistory.aspx" id="lnkStaHis"><%=Translator.Get("StatementHistory")%></a></span>
			<span><a runat="server" href="~/UserInfo/MyAccount.aspx" id="lnkMyAcc"><%=Translator.Get("MyAcc") %></a></span>
			<span><a runat="server" href="~/UserInfo/PaymentRequest.aspx" id="lnkPayment"><%=Translator.Get("PaymentRequest") %></a></span>
			<span><a href="javascript:void(0);" class="hassub"><%=Translator.Get("MarketingTools")%></a></span>
            <div class="submenu">
                <span>&nbsp;</span><a id="A10" runat="server" href="~/UserInfo/MediaGalleryNew.aspx"><%=Translator.Get("Media") %></a><br />
                <span>&nbsp;</span><a id="lnkTextBox" runat="server" href="~/UserInfo/MediaText.aspx"><%=Translator.Get("Text")%></a>
            </div>			
			<span><a href="javascript:void(0);" class="hassub"><%=Translator.Get("TrafficStatistics")%></a></span>
            <div class="submenu">
                <span>&nbsp;</span><a id="A9" runat="server" href="~/UserInfo/ClickReferrers.aspx"><%=Translator.Get("Referrals") %></a> <br />
                <span>&nbsp;</span><a id="A11" runat="server" href="~/UserInfo/ClicksLandingPages.aspx"><%=Translator.Get("LandingPages") %></a>
            </div>
			<%--<span><a runat="server" href="~/UserInfo/MediaStatistics.aspx" id="lnkMediaStatistics"><%=Lang.TrackingStatistics%></a></span>--%>
	</div>
</div>
<!--/After login-->



<script language="javascript" type="text/javascript">
<%=script %>

</script>


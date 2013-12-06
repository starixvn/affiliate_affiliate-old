<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
	Inherits="Fanex.Web.Macaubet.Customer.FAQs" Codebehind="FAQs.aspx.cs" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>

<%@ Register Src="Controls/Home_Center.ascx" TagName="Home_Center" TagPrefix="uc3" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>Macaubet- Frequently asked questions Affiliate Program, FAQs</title>
    <meta name="description" content="Macaubet - Frequently asked questions in sports betting Affiliate Program, How do I signup with Macaubet affiliate program" />
    <meta name="keywords" content="FAQs affiliate program, Frequently asked questions affiliate bet, FAQs, signup with affiliate program" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">    
	<div id="contents">
		<div id="box_insite">
			<div class="header_in">
				<table width="100%" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td class="in_top">
                            <div class="title"><span><%=Fanex.Affiliate.Resources.Translator.Get("Faqs")%></span></div>
                            <div class="title_detail"></div>
                        </td>
					</tr>                
				</table>
			</div>
			<div class="in_conts">                   
				<uc3:Home_Center ID="Home_Center" runat="server" />
				<div>
                   <%=Fanex.Affiliate.Resources.Translator.Get("FAQs","AffiliateContent")%>					
				</div>         
				<div id="divJoin" runat="server" class="c" style="padding-left: 135px;">
					<a class="signup" href="<%=ResolveUrl("~/SignUp.aspx") %>">
                      <span> <%=Fanex.Affiliate.Resources.Translator.Get("JoinTheAffiliate")%></span>
						
					</a>
				</div>
				<div class="clear"></div>
			</div>
		</div>
	</div>
	<div style='display:none'>
		<div id='licenseecontent' style='padding:10px; background:#fff;'>
			All content and conditions are to be provided by licensee
		</div>
	</div>
	<%--<script type="text/javascript" language="javascript">
		$(document).ready(function(){
			jQuery.noConflict();
			jQuery.colorbox({width:"300px", escKey: false, inline:true, href:"#licenseecontent"})
		});
	</script>--%>
</asp:Content>

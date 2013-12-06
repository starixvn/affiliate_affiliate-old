<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Fanex.Web.ITCBet.Customer.ContactUS" Codebehind="ContactUs.aspx.cs" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<%@ Register Src="Controls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>ITCBet - Comments or questions about affiliate program</title>
    <meta name="description" content="ITCBet - Please direct any comments or questions to support@ITCBet.com" />
    <meta name="keywords" content="Contact us, ITCBet question, ITCBet comment" />    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
  <div id="contents">        
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top">
                            <div class="title"><span><%=Fanex.Affiliate.Resources.Translator.Get("ContactUs") %></span></div>
                            <div class="title_detail"></div>
                        </td>
                        
                    </tr>
                </table>
            </div>
            <div class="padd15">
                <%--<div id="addressForm">
                    <div class="contact-icon">&nbsp;</div>
                    <div class="contact-list">
                        <ul>
                            <li><span><%=Fanex.Affiliate.Resources.Translator.Get("EnglishSupport") %>:</span><a style="color:Black;" href="mailto:csd12b_aff_eng@hotmail.com">csd12b_aff_eng@hotmail.com</a></li>
                            <li><span><%=Fanex.Affiliate.Resources.Translator.Get("ChineseSupport") %>:</span><a style="color:Black;" href="mailto:csd12b_affiliates@hotmail.com">csd12b_affiliates@hotmail.com</a></li>
                            <li><span><%=Fanex.Affiliate.Resources.Translator.Get("ThaiSupport") %>:   </span><a style="color:Black;" href="mailto:csd12bthai_affiliate@hotmail.com">csd12bthai_affiliate@hotmail.com</a></li>
                        </ul>
                    </div>
                    <div class="clear"></div>
                </div>--%>
                <br />
                <uc1:ContactUs ID="ContactUs1" runat="server" />
            </div>
        </div>
    </div>  
</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Fanex.Web.Macaubet.Customer.ContactUS" Codebehind="ContactUs.aspx.cs" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<%@ Register Src="Controls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>Macaubet - Comments or questions about affiliate program</title>
    <meta name="description" content="Macaubet - Please direct any comments or questions to csd@macaubet.com" />
    <meta name="keywords" content="Contact us, Macaubet question, Macaubet comment" />    
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
                <br />
                <uc1:ContactUs ID="ContactUs1" runat="server" />
            </div>
        </div>
    </div>  
</asp:Content>
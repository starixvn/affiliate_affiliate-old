<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="Fanex.Web.Macaubet.Customer.Condition" Codebehind="Condition.aspx.cs" %>
    <%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>Macaubet - affiliate program Terms & Conditions</title>
    <meta name="description" content="Macaubet - affiliate program Terms & Conditions" />
    <meta name="keywords" content="Terms & Conditions, Terms affiliate program, Terms & Conditions Macaubet" />    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top">
                            <div class="title"><span><%=Fanex.Affiliate.Resources.Translator.Get("TermCondition")%></span></div>
                            <div class="title_detail"></div>        
                        </td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <%=Fanex.Affiliate.Resources.Translator.Get("TermConditionContent","AffiliateContent")%>              
            </div>
        </div>
    </div>    
</asp:Content>

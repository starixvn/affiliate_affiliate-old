<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Fanex.Web.i668bet.Customer.Condition"
    CodeBehind="Condition.aspx.cs" %>

<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>i668bet - affiliate program Terms & Conditions</title>
    <meta name="description" content="i668bet - affiliate program Terms & Conditions" />
    <meta name="keywords" content="Terms & Conditions, Terms affiliate program, Terms & Conditions i668bet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top" width="170">
                            <%=Fanex.Affiliate.Resources.Translator.Get("TermCondition")%>
                        </td>
                        <td class="in_right">
                            &nbsp;
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

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" 
    CodeBehind="ProgramDetails.aspx.cs" Inherits="Fanex.Web.LiveBet365.Affiliate.ProgramDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td width="170" class="in_top"><%=Fanex.Affiliate.Resources.Translator.Get("ProgDetails") %></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <%=Fanex.Affiliate.Resources.Translator.Get("Kiosk_ProgramDetails", "AffiliateContent")%>
            </div>
        </div>
    </div>    
    
</asp:Content>


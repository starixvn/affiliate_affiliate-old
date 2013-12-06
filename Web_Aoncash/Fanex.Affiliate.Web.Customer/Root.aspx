<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="Fanex.Web.Aoncash.Customer.Root" Codebehind="Root.aspx.cs" %>

<%@ Register Src="Controls/Home_Center.ascx" TagName="Home_Center" TagPrefix="uc3" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>Aoncash- Sports Betting Affiliate Program,earn up to 35% commission</title>
    <meta name="description" content="Aoncash - Best affiliate program, Earn up to 35% commission share for every customer that you refer with Aoncash sports betting affiiate program" />
    <meta name="keywords" content="Aoncash, affiliate program, affiliate bet, affiliate betting, affiliates program, sports betting affiiate, affiliate programme" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td width="170" class="in_top"><%=Fanex.Affiliate.Resources.Translator.Get("ProgDetails")%></td>        
                        <td class="in_right">&nbsp;</td>                
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <uc3:Home_Center ID="Home_Center" runat="server" />
                <%=Fanex.Affiliate.Resources.Translator.Get("ProgramDetails", "AffiliateContent")%>
            </div>
        </div>
    </div>    
</asp:Content>

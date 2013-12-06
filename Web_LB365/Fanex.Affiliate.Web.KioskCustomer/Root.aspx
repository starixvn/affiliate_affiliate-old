<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="Fanex.Web.LiveBet365.Affiliate.Root" Codebehind="Root.aspx.cs" %>

<%@ Register Src="Controls/Home_Center.ascx" TagName="Home_Center" TagPrefix="uc3" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>Livebet365- Kiosk Affiliate Program</title>
    <meta name="description" content="Livebet365 - Kiosk Affiliate Program" />
    <meta name="keywords" content="livebet365, affiliate program, affiliate bet, affiliate betting, affiliates program, sports betting affiiate, affiliate programme" />
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
                <%=Fanex.Affiliate.Resources.Translator.Get("Kiosk_ProgramDetails", "AffiliateContent")%>
            </div>
        </div>
    </div>    
</asp:Content>

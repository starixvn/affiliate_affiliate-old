<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="Fanex.Web.ITCBet.Customer.Root" Codebehind="Root.aspx.cs" %>

<%@ Register Src="Controls/Home_Center.ascx" TagName="Home_Center" TagPrefix="uc3" %>

<asp:Content ContentPlaceHolderID="title" runat="server" ID="title">
    <title>ITCBet Affiliate</title>
    <meta name="description" content="ITCBet - Best affiliate program, Earn up to 35% commission share for every customer that you refer with ITCBet sports betting affiiate program" />
    <meta name="keywords" content="ITCBet, affiliate program, affiliate bet, affiliate betting, affiliates program, sports betting affiiate, affiliate programme" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_top">
                            <div class="title"><span><%=Fanex.Affiliate.Resources.Translator.Get("ProgDetails")%></span></div>
                            <div class="title_detail"></div>
                        </td>                        
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <uc3:Home_Center ID="Home_Center" runat="server" />
                <%=strContent%>
            </div>
        </div>
    </div>    
</asp:Content>

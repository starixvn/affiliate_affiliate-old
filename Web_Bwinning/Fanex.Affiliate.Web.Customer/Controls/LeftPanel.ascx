<%@ Control Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.Bwinning.Customer.Controls_LeftPanel" Codebehind="LeftPanel.ascx.cs" %>
<div class="leftcontainer">
    <div id="containerMainLeft">
        <div id="subnavleft">
            <div id="subnav-head">
                &nbsp;<asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
            </div>
            <span class="navleft"><a href="Account_Overview.aspx" id="lnkAccOverview">
                <%=Fanex.Affiliate.Resources.Translator.Get("AccOverview")%>
            </a></span><span class="navleft"><a href="StatementHistory.aspx" id="lnkStaHis">
                <%=Fanex.Affiliate.Resources.Translator.Get("StatementHistory")%>
            </a></span><span class="navleft"><a href="MyAccount.aspx" id="lnkMyAcc">
                <%=Fanex.Affiliate.Resources.Translator.Get("MyAcc") %>
            </a></span><span class="navleft"><a href="PaymentRequest.aspx" id="lnkPayment">
                <%=Fanex.Affiliate.Resources.Translator.Get("PaymentRequest") %>
            </a></span><span class="navleft"><a href="MediaGallery.aspx" id="lnkMediaGallery">
                <%=Fanex.Affiliate.Resources.Translator.Get("Media") %>
            </a>              
            </span>
            <span class="navleft"><a href="MediaStatistics.aspx" id="lnkMediaStatistics">
                <%=Fanex.Affiliate.Resources.Translator.Get("TrackingStatistics")%>
            </a></span>
            <div id="subnav-foot">
                &nbsp;</div>
        </div>
        
        <div class="banner_<%=Fanex.Affiliate.Core.AppSession.Flag %>" onclick="document.location.href='../ContactUS.aspx'">
            &nbsp;
        </div>
    </div>
</div>

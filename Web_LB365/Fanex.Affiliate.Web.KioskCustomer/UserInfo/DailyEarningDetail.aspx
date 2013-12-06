<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="DailyEarningDetail.aspx.cs" Inherits="Fanex.Web.LiveBet365.Affiliate.UserInfo.DailyEarningDetail" %>
<%@ Import Namespace="Fanex.Affiliate.Resources" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <script src="../App_Js/Calendar2/calendar-normal.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/date.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/Lang/calendar_<%=AppSession.Language%>.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/calendar-setup.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <link href="../App_Js/Calendar2/Skin/calendar-blue.css?vers=<%Response.Write(AppSettings.JsVersion);%>" media="all" rel="stylesheet" type="text/css" title="Blue" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <%=strTemplate%>
    <script type="text/javascript">
        window.onload = function () {
            Calendar.setup({
                inputField: "monthdate",
                ifFormat: "%m/%d/%Y",
                showsTime: false,
                button: "fdateTrigger",
                singleClick: true,
                showOthers: true,
                step: 1
            });
        }
  </script> 
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeBehind="ClickReferrers.aspx.cs" Inherits="UserInfo_ClickReferrers" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/calendar-normal.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/date.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/Lang/calendar_<%=Fanex.Affiliate.Core.AppSession.Language%>.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/calendar-setup.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <link href="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/Skin/calendar-blue.css?version=<%=AppSettings.JsVersion%>" media="all" rel="stylesheet" type="text/css" title="Blue" />
  <%=statisticsContent %>
  <script type="text/javascript">
        Calendar.setup({
            inputField : "fdate",
            ifFormat: "%m/%d/%Y",
            showsTime : false, 
            button : "fdateTrigger",
            singleClick : true,
            showOthers : true,
            step : 1
        });

        Calendar.setup({
            inputField: "tdate",
            ifFormat: "%m/%d/%Y",
            showsTime: false,
            button: "tdateTrigger",
            singleClick: true,
            showOthers: true,
            step: 1
        });
  </script>
 </asp:Content>

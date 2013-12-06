<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Message" Codebehind="Message.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<%@ Import Namespace="Fanex.Affiliate.Core" %>
<script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/calendar-normal.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
<script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/date.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
<script src=<%=AppSettings.FullDomainName%>/App_Js/Calendar2/Lang/calendar_<%=Fanex.Affiliate.Core.AppSession.Language%>.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
<script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/calendar-setup.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
<link href="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/Skin/calendar-blue.css?version=<%=AppSettings.JsVersion%>" media="all" rel="stylesheet" type="text/css" title="Blue" />
<%=hotNewsContent %>
<script type="text/javascript">
    if($("slSearchOption") && GetDataFromUrl("slSearchOption") != "")
        $("slSearchOption").value = GetDataFromUrl("slSearchOption");
    
    window.onload = function() {EnableDisableSearchDate();};
    if(GetDataFromUrl("chkDate") == "on")
    {
        $("chkDate").checked = true;
        EnableDisableSearchDate();
    }
</script>
</asp:Content>
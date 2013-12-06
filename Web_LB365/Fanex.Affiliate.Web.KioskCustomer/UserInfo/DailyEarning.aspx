<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" Inherits="DailyEarning" Codebehind="DailyEarning.aspx.cs" %>
<%@ Import Namespace="Fanex.Affiliate.Resources" %>
<%@ Import Namespace="Fanex.Affiliate.Core" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/calendar-normal.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/date.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/Lang/calendar_<%=Fanex.Affiliate.Core.AppSession.Language%>.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/calendar-setup.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <link href="<%=AppSettings.FullDomainName%>/App_Js/Calendar2/Skin/calendar-blue.css?version=<%=AppSettings.JsVersion%>" media="all" rel="stylesheet" type="text/css" title="Blue" />
     <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        
                        <td width="144" class="in_top"><asp:Literal runat="server" ID="ltrDailyEarning"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <br />
            <div class="in_conts">
                <form method="get" id="frmData" name="frmData" action="">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td><%=strListProducts %></td>
                            <td><%= Translator.Get("From")%></td>
                            <td class="r" style="width: 100px">
                                <input type="text" id="fdate" name="fdate" size="13" value="<%=fromDate %>" readonly />
                            </td>
                            <td>
                                <img hspace="3" height="20" border="0" width="18" src="../App_Themes/Default/Images/cal.gif"
                                    id="fdateTrigger" />
                            </td>
                            <td><%= Translator.Get("To")%></td>
                            <td style="width: 100px" class="r">
                                <input type="text" id="tdate" name="tdate" size="13" value="<%=toDate %>" readonly />
                            </td>
                            <td>
                                <img hspace="3" height="20" border="0" width="18" src="../App_Themes/Default/Images/cal.gif"
                                    id="tdateTrigger" />
                            </td>   
                            <td>
                                <input type="button" value="<%=Translator.Get("Submit") %>" onclick="LoadData(1);" id="btnSubmit" name="{Submit}"
                                    class="button" />
                            </td>
                            <td width="70px">&nbsp;</td>
                        </tr>
                    </table>
                    
                    <br />
                    <input type="hidden" id="monthdate" name="monthdate" value="<%=Request["monthdate"] %>" />
                </form>
                <asp:Literal runat="server" ID="ltrViewDaily"></asp:Literal>
                
            </div>
        </div>
    </div>  
    <script type="text/javascript">
        Calendar.setup({
            inputField: "fdate",
            ifFormat: "%m/%d/%Y",
            showsTime: false,
            button: "fdateTrigger",
            singleClick: true,
            showOthers: true,
            step: 1
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnderMaintenance.aspx.cs" Inherits="Fanex.Web.Macaubet.Customer.Error.UnderMaintenance" %>

  <%@ Import Namespace="Fanex.Affiliate.Core" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Under Maintenance</title>
    <meta http-equiv="refresh" content="180">
    <link href="App_Themes/undermaintenance/style.css" rel="stylesheet" type="text/css" />
    <script src="<%=AppSettings.FullDomainName%>/App_Js/Common.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
    <script src="<%=AppSettings.FullDomainName%>/App_Js/prototype.js?version=<%=AppSettings.JsVersion%>" type="text/javascript"></script>
</head>
<body>
    <a href="<%=AppSettings.FullDomainName%>"><div id="top">&nbsp;</div></a>
    <div id="center">
        <div id="titleum">
        </div>
        <div id="seperator">
        </div>
        <div id="botcenter">
            <div id="iconum">
            </div>
            <div id="contentum">
                <ul class="cont">
                    <li>We are having server maintenance, website will be unavailable until <span class="cont2">
                        <%= UMTime_To%> (HK Time GMT +8).</span><br>
                    Please accept our apologies for the inconvenience caused!
                    <br>
                    To reach customer service, please <a href="https://server.iad.liveperson.net/hc/34762695/?cmd=file&file=visitorWantsToChat&site=34762695&byhref=1"
                        target="_blank">click here</a> or email to
                    <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></li>
                    
                    <li>Tenemos mantenimiento de servidor, el sitio web estará indisponible hasta que <span
                                        class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    Acepta por favor nuestras disculpas para el inconveniente causado"!
                                    <br>
                                    Para alcanzar servicio de atención al cliente, por favor <a href="https://server.iad.liveperson.net/hc/34762695/?cmd=file&file=visitorWantsToChat&site=34762695&byhref=1"
                                        target="_blank">clic aquí</a> o correo electrónico a

                                    <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></li>
                    
                    <li>本公司正进行伺服器维护，时间至 <span class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    所有网页将无法登入，不便之处有请见谅！<br>
                                    如需要在线帮助，有请<a href="https://server.iad.liveperson.net/hc/34762695/?cmd=file&file=visitorWantsToChat&site=34762695&byhref=1"
                                        target="_blank">点击这里</a>或邮件至

                                    <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a> </li>
                    
                    <li>非常抱歉，本司服務器正在維護中，請稍後嘗試登陸或聯繫在線客服 . </li>
                    
                    <li class="end">本公司正進行伺服器維護，時間至 <span class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    所有網頁將無法登入，不便之處 有請見諒！<br>
                                    如需要在線幫助，有請<a href="https://server.iad.liveperson.net/hc/34762695/?cmd=file&file=visitorWantsToChat&site=34762695&byhref=1"
                                        target="_blank">點擊這裡</a>或郵件至

                                    <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></li>
                </ul>
            </div>
            <div id="redseperator2">
            </div>
        </div>
    </div>
</body>
</html>

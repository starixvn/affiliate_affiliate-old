<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false"
    Inherits="UnderMaintenance" Codebehind="UnderMaintenance.aspx.cs" Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Under Maintenance</title>
    <meta http-equiv="refresh" content="180">
    <link href="App_Themes/undermaintenance/style.css" rel="stylesheet" type="text/css" />
</head>

<body>
	<div id="top">
    	<div class="logo"></div>
    </div>
    <div id="center">
    	<div id="title"></div>
        <div id="seperator"> </div>
        <div id="botcenter">
        	<div id="icon"></div>
            
            <div id="content">
            	<ul class="cont">
                	<li>
                        We are having server maintenance, website will be unavailable until <%= UMTime_To%> (HK Time GMT +8).
                        <br />Please accept our apologies for the inconvenience caused! 
                        <br />To reach customer service, please 
                        <a href="https://sightmax75r2.sightmaxondemand.com/macaubet/SightMaxAgentInterface/PreChatSurvey.aspx?accountID=259&siteID=294&queueID=631"
                        target="_blank">click here</a> or email to 
                        <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%20<%=Fanex.Affiliate.Core.AppSettings.SystemName %>%20website">
                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a>
                    </li>
                    <li>
                        Tenemos mantenimiento de servidor, el sitio web estará indisponible hasta que <%= UMTime_To%> (HK Time GMT +8).
                        <br />Acepta por favor nuestras disculpas para el inconveniente causado! 
                        <br />Para alcanzar servicio de atención al cliente, por favor 
                        <a href="https://sightmax75r2.sightmaxondemand.com/macaubet/SightMaxAgentInterface/PreChatSurvey.aspx?accountID=259&siteID=294&queueID=631"
                        target="_blank">clic aquí</a> o correo electrónico a 
                        <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%20<%=Fanex.Affiliate.Core.AppSettings.SystemName %>%20website">
                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a>
                    </li>
                	<li> 
                        本公司正进行伺服器维护，时间至 <%= UMTime_To%> (HK Time GMT +8).
                        <br />所有网页将无法登入，不便之处有请见谅！
                        <br />如需要在线帮助，有请 
                        <a href="https://sightmax75r2.sightmaxondemand.com/macaubet/SightMaxAgentInterface/PreChatSurvey.aspx?accountID=259&siteID=294&queueID=631"
                        target="_blank">点击这里</a> 或邮件至 
                        <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%20<%=Fanex.Affiliate.Core.AppSettings.SystemName %>%20website">
                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a>           
                    </li>

                    <li class="end"> 
                        本公司正進行伺服器維護，時間至 <%= UMTime_To%> (HK Time GMT +8).
                        <br />所有網頁將無法登入，不便之處 有請見諒！
                        <br />如需要在線幫助，有請 
                        <a href="https://sightmax75r2.sightmaxondemand.com/macaubet/SightMaxAgentInterface/PreChatSurvey.aspx?accountID=259&siteID=294&queueID=631"
                        target="_blank">點擊這裡</a> 或郵件至
                        <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%20<%=Fanex.Affiliate.Core.AppSettings.SystemName %>%20website">
                        <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a>           
                    </li>  
                </ul>
            </div>
        
        <div id="redseperator2"></div>
        </div>
    </div>

</body>
</html>
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
	<div id="main">
        <div id="top">
            <a href="http://itcbet.com"></a>
            <div class="title"></div>
        </div>
        <div id="content">
            <div class="left"></div>
            <div class="right">
                <div>
                    <p>We are having server maintenance, website will be unavailable <br />until <span><%= UMTime_To%> (HK Time GMT +8).</span></p>
                    <p>Please accept our apologies for the inconvenience caused!</p>
                    <p>To reach customer service, please <a href="skype:cs.itcbet?chat">
                            click here</a> or email to <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></p>
                </div>
                <hr style="border-top: 1px dotted rgb(255, 255, 255);" noshade="noshade" size="1">
                <div>
                    <p>本公司正进行伺服器维护, 时间至 <span><%= UMTime_To%> (HK Time GMT +8).</span></p>
                    <p>所有网页将无法登入，不便之处有请见谅！</p>
                    <p>如需要在线帮助，有请 <a href="skype:cs.itcbet?chat">
                            点击这里</a> 或邮件至 <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></p>
                </div>
                <hr style="border-top: 1px dotted rgb(255, 255, 255);" noshade="noshade" size="1">
                <div>
                    <p>本公司正進行伺服器維護，時間至 <span><%= UMTime_To%> (HK Time GMT +8).</span></p>
                    <p>所有網頁將無法登入，不便之處 有請見諒！</p>
                    <p>如需要在線幫助，有請 <a href="skype:cs.itcbet?chat">
                            點擊這裡</a> 或郵件至 <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></p>
                </div>
                <hr style="border-top: 1px dotted rgb(255, 255, 255);" noshade="noshade" size="1">
                <div>
                    <p>Kami sedang melakukan server maintenance, website tidak tersedia <br />hingga pukul <span><%= UMTime_To%> (HK Time GMT +8).</span></p>
                    <p>Mohon maaf untuk ketidaknyamanan ini!</p>
                    <p>Untuk menghubungi Customer Service, mohon <a href="skype:cs.itcbet?chat">
                            klik disini</a> atau email ke <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
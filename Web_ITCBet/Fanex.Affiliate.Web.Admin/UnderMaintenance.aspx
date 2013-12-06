<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false"
    Inherits="UnderMaintenance" Codebehind="UnderMaintenance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Under Maintenance</title>
    <meta http-equiv="refresh" content="180">
    <link href="App_Themes/DefaultV2/undermaintenance/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="main">
        <div id="top">
            <a href="http://affadmin.itcbet.com"></a>
            <div class="title"></div>
        </div>
        <div id="content">
            <div class="left"></div>
            <div class="right">
                <div>
                    <p>We are having server maintenance, website will be unavailable <br />until <span><%= UMTime_To%> (HK Time GMT +8).</span></p>
                    <p>Please accept our apologies for the inconvenience caused!</p>
                    <p>To reach customer service, please <a href="https://server.iad.liveperson.net/hc/34762695/?cmd=file&file=visitorWantsToChat&site=34762695&byhref=1"
                            target="_blank">click here</a> or email to <a href="mailto:<%=Fanex.Affiliate.Core.AppSettings.MailContact %>?subject=Enquiries%20sent%20from%2012BET%20website">
                                <%=Fanex.Affiliate.Core.AppSettings.MailContact %></a></p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

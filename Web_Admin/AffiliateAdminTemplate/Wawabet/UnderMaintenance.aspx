<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" Inherits="UnderMaintenance"
    CodeBehind="UnderMaintenance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Under Maintenance</title>
    <meta http-equiv="refresh" content="180">
    <link href="App_Themes/DefaultV2/undermaintenance/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="top">
        <div class="logo">
            UNDER MAINTENANCE</div>
        <!-- end.logo-->
        <div class="seperator">
        </div>
    </div>
    <!-- end#top-->
    <div id="center">
        <div class="icon">
        </div>
        <!-- end .icon-->
        <div class="content">
            <ul class="text">
                <li>We are having server maintenance,<br />
                    website will be unavailable until
                    <%= UMTime_To%>
                    (HK Time GMT +8).<br />
                    <br />
                    Sorry for the inconveniences caused! </li>
                <li class="end">本公司正進行伺服器維護，時間至
                    <%= UMTime_To%>
                    (HK Time GMT +8).
                    <br />
                    所有網頁將無法登入，不便之處 有請見諒 </li>
            </ul>
        </div>
        <!-- end .content-->
    </div>
</body>
</html>

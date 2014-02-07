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
                <li>서버 점검이 진행되어 웹사이트를
                    <%= UMTime_To%>
                    (홍콩시간 GMT +8)까지 사용하실 수 없습니다.
                    <br />
                    <br />
                    점검으로 인한 불편을 드려 죄송하며 양해부탁드립니다.</li>
                <li>We are having server maintenance,<br />
                    website will be unavailable until
                    <%= UMTime_To%>
                    (HK Time GMT +8).<br />
                    <br />
                    Please accept our apologies for the inconvenience caused! </li>
                <li>本公司正進行伺服器維護，<br />
                    時間至
                    <%= UMTime_To%>
                    (HK Time GMT +8).<br />
                    <br />
                    所有網頁將無法登入，不便之處 有請見諒！</li>
                <li class="end">本公司正进行伺服器维护<br />
                    时间至
                    <%= UMTime_To%>
                    (HK Time GMT +8).<br />
                    <br />
                    所有网页将无法登入，不便之处有请见谅！ </li>
            </ul>
        </div>
        <!-- end .content-->
    </div>
</body>
</html>

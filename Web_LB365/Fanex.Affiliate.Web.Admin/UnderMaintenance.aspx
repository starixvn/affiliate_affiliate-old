<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false"
    Inherits="UnderMaintenance" Codebehind="UnderMaintenance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Under Maintenance</title>
    <meta http-equiv="refresh" content="180">
    <style type="text/css">
        body
        {
            background:url(App_Themes/DefaultV2/images/bgMain.jpg) repeat-x scroll center top #000000;
            color: #333333;
            font-family:"Myriad Pro", Tahoma, REZ, Arial;
            font-size: 11px;
            margin: 0;
            padding: 0;
        }
        a
        {
            color: #eec110 !important;
        }
        #top
        {
	        width:960px;
	        height:60px;
	        margin:auto;
	        background:url(App_Themes/DefaultV2/images/logo.png) top left no-repeat;
	        margin-bottom: 30px;
        }

        #center
        {
	        width:850px;
	        height: 400px;
	        margin:auto;
        }

        #title
        {
	        background:url(App_Themes/DefaultV2/images/sry.png) right no-repeat;
	        width:620px;
	        height:44px;
	        margin:auto;
        }
        #seperator
        {
	        border-top: 1px solid #FF9900;
	        border-bottom: 1px solid #460e0f;
	        width: 780px;
	        height:1px;
	        margin:auto;
        }
        #botcenter
        {
	        width: 850px;
	        height: 318px;
        }
        #icon
        {
	        background:url(App_Themes/DefaultV2/images/icon.png) left bottom no-repeat;
	        width:303px;
	        height: 237px;
	        float:left;
        }
        #content
        {
	        font-size:18px;
	        color:#FFF;
	        float:left;
	        width: 510px;
        }

        .cont
        {
	        margin: 0;
	        padding: 0;
        }
        .cont li
        {
	        border-bottom: #fff dotted 1px;
	        list-style-type: none;
	        padding:10px;
		
        }
        .cont li.end
        {
		        border: 0;
        }
    </style>
</head>
<body>
    <a href="http://affadmin.livebet365.com"><div id="top">&nbsp;</div></a>
    <div id="center">
        <div id="title">
        </div>
        <div id="seperator">
        </div>
        <div id="botcenter">
            <div id="icon">
            </div>
            <div id="content">
                <ul class="cont">
                    <li>We are having server maintenance, website will be unavailable until <span class="cont2">
                        <%= UMTime_To%> (HK Time GMT +8).</span><br>
                    Sorry for the inconvenience caused!  </li>
                    <li class="end">本公司正進行伺服器維護，時間至 <span class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    所有網頁將無法登入，不便之處 有請見諒！<br>                
                </ul>
            </div>
            <div id="redseperator2">
            </div>
        </div>
    </div>
</body>
</html>

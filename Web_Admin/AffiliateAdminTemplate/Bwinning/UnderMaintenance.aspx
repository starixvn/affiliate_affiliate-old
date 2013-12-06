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
    <div id="top">
    	<div class="logo">
        </div>
    </div>
    <div id="center">
    	<div class="icon"></div>
        <div class="text">
            <div id="title"></div>
            <div class="seperator">
            </div>
            <div id="content">
                <ul class="cont">
                <li>We are having server maintenance,<br />
				website will be unavailable until <%= UMTime_To%> (HK Time GMT +8).<br />
				<br />Sorry for the inconveniences caused!</li>
                </ul>
            </div>
      	</div>
    </div>
</body>
</html>
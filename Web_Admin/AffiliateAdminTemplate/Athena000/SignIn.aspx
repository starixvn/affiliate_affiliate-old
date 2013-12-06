<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignIn" CodeBehind="SignIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Athena Affiliate Login</title>
    <link runat="server" id="IconICO" rel="shortcut icon" />
    <![if ! IE]>
    <link runat="server" id="IconPNG" rel="shortcut icon" />
    <![endif]>
    <link href="App_Themes/Default/login.css" type="text/css" rel="stylesheet" />
    <script src="App_Js/prototype.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/login.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/Common.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <div id="header">
        <div class="logo">
        </div>
        <div class="seperator">
        </div>
    </div>
    <div id="container">
        <div class="panel">
            <input type="text" name="txtun" id="txtun" class="user" placeholder="Username" onkeyup="PreLogin(event);" />
            <input type="password" class="pass" name="txtpw" placeholder="Password" id="txtpw"
                onkeyup="PreLogin(event);" />
            <div id="submit-div">
                <input type="button" onclick="ExcLogin()" class="submit-button" id="submit" value="Submit" />
            </div>
            <div id="errMsg" style="display: none;">
                <div class="err-msg" id="error_msg"></div>
            </div>
        </div>
    </div>
    <script src="App_Js/placeholder_fixer.js" language="javascript" type="text/javascript"></script>
</body>
</html>
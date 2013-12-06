<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignIn" CodeBehind="SignIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>i668bet Affiliate Admin</title>
    <link runat="server" id="IconICO" rel="shortcut icon" />
    <![if ! IE]>
    <link runat="server" id="IconPNG" rel="shortcut icon" />
    <![endif]>
    <link href='App_Themes/DefaultV2/common.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/content.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/footer.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/header.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/header/style.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/left.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/login/login.css' type='text/css' rel='stylesheet' />
    <style type="text/css">
        body
        {
            font-family: arial,tahoma,verdana,helvetica,sans-serif;
            font-size: 11px;
            margin: 0px;
            background: #cdd1d8 url(App_Themes/DefaultV2/images/login-background.jpg) repeat;
        }
    </style>
    <!--js-->
    <script src="App_js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="App_Js/prototype.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/login.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/Common.js" language="javascript" type="text/javascript"></script>
    <!--/js-->
</head>
<body>
    <div id="wrapper">
        <div id="container">
            <div id="header">
                Admin Login</div>
            <div id="divun" class="label">
                Username
            </div>
            <div id="username_input">
                <input type="text" name="txtun" id="txtun" placeholder="Username" onkeyup="PreLogin(event);" />
            </div>
            <div id="divpw" class="label">
                Password
            </div>
            <div id="password_input">
                <input type="password" name="txtpw" placeholder="Password" id="txtpw" onkeyup="PreLogin(event);" />
            </div>
            <div id="submit-div">
                <div id="language" style="display: none">
                    <span id="selectLang" class="fen-US">English</span> <span class="arrUp" id="arrow">&nbsp;</span>
                </div>
                <div id="langlist" style="display: none">
                    <div>
                        <a href="javascript:ChangeLanguage('zh-TW');" class="fzh-TW">中文繁体</a></div>
                    <div>
                        <a href="javascript:ChangeLanguage('zh-CN');" class="fzh-CN">中文简体</a></div>
                    <div>
                        <a href="javascript:ChangeLanguage('en-US');" class="fen-US">English</a></div>
                </div>
                <input type="button" onclick="ExcLogin()" class="submit-button" id="submit" value="Submit" />
            </div>
            <div id="errMsg" style="display: none;">
                <div id="error_item">
                    <ul>
                        <li><a class="err-msg" id="error_msg"></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script src="App_Js/placeholder_fixer.js" language="javascript" type="text/javascript"></script>
</body>
</html>

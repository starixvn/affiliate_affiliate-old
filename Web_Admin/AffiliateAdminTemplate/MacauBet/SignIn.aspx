<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignIn" CodeBehind="SignIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Macaubet Affiliate Admin</title>
    <link runat="server" id="IconICO" rel="shortcut icon" />
    <![if ! IE]>
    <link runat="server" id="IconPNG" rel="shortcut icon" />
    <![endif]>
    <link href='App_Themes/DefaultV2/common.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/login/login.css' type='text/css' rel='stylesheet' />
    <!--js-->
    <script src="App_Js/prototype.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/login.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/Common.js" language="javascript" type="text/javascript"></script>
    
    <!--/js-->
</head>
<body>
    <div id="wrapper">
        <div id="main">
            <span class="logo"></span>
            <div class="under-logo"></div>
            <div id="content">
                <div class="login">
                    <div class="ad-text"></div>
                    <div class="ad-line"></div>
                    
                    <div class="userbox">
                        <input type="text" name="txtun" id="txtun" onkeyup="PreLogin(event);"/>
                    </div>

                    <div class="keybox">
                        <input type="password" name="txtpw" id="txtpw" onkeyup="PreLogin(event);"/>
                    </div>

                    <div class="loginbox">
                        <!-- <input type="button" onclick="ExcLogin()" class="submit-button" id="submit" value="Submit"/> -->
                        <button class="submit-button" id="submit" onclick="ExcLogin();">Submit</button>
                    </div>

                </div>
            </div>
            <div id="errMsg" style="display: none; ">
                <div id="error_item">
                    <ul>
                        <li><a class="err-msg" id="error_msg"></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

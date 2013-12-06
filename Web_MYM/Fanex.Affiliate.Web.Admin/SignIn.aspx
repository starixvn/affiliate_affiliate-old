<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignIn" CodeBehind="SignIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>mymbet Affiliate Admin</title>
    <link runat="server" id="IconICO" rel="shortcut icon" />
    <![if ! IE]>
    <link runat="server" id="IconPNG" rel="shortcut icon" />
    <![endif]>
    
    <link href='App_Themes/DefaultV2/content.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/footer.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/header.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/left.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/login/login.css' type='text/css' rel='stylesheet' />
    <style type="text/css">
        body
        {
            font-family: arial,tahoma,verdana,helvetica,sans-serif;
            font-size: 11px;
            margin: 0px;
            background: #cdd1d8 url(App_Themes/DefaultV2/images/login-background.png) repeat;
        }
    </style>
    <!--js-->
    <script src="App_Js/prototype.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/login.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/Common.js" language="javascript" type="text/javascript"></script>
    
    <!--/js-->
</head>
<body>
   
	<div id="wrapper">
		<div id="wrappertop"></div>

		<div id="wrappermiddle">
			

			<div id="username_input">

				<div id="username_inputleft"></div>

				<div id="username_inputmiddle">				
					<input type="text" name="txtun" id="txtun" placeholder="Username"  onkeyup="PreLogin(event);">				
				</div>

				<div id="username_inputright"></div>

			</div>

			<div id="password_input">

				<div id="password_inputleft"></div>
				<div id="password_inputmiddle">				
					<input type="password" name="txtpw" placeholder="Password" id="txtpw" onkeyup="PreLogin(event);">				
				</div>

				<div id="password_inputright"></div>

			</div>

			<div id="submit-div">
				<input type="button" onclick="ExcLogin()" class="submit-button" id="submit" value="">			
				
			</div>
            <div id="errMsg" style="display: none; ">
                        <div id="error_item">
                            <ul>
                                <li>
                                <img style="vertical-align:middle" src="App_Themes/DefaultV2/Images/warning.png" />
                                <a style="padding-top:1px" class="err-msg" id="error_msg"></a>
                                </li>
                            </ul>
                        </div>
                    </div>

		</div>

		<div id="wrapperbottom"></div>
		<!--
		<div id="powered">
		<p>Powered by <a href="http://www.mymbet.com">mymbet.com</a></p>
		</div>-->
	</div>
    <script src="App_Js/placeholder_fixer.js" language="javascript" type="text/javascript"></script>
</body>
</html>

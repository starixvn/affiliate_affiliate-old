<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignIn" CodeBehind="SignIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LIVEBET365 Affiliate Admin</title>
    <link runat="server" id="IconICO" rel="shortcut icon" />
    <![if ! IE]>
    <link runat="server" id="IconPNG" rel="shortcut icon" />
    <![endif]>
    <link href='App_Themes/DefaultV2/common.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/content.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/footer.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/header.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/left.css' type='text/css' rel='stylesheet' />
    <link href='App_Themes/DefaultV2/login.css' type='text/css' rel='stylesheet' />
    <style type="text/css">
        body
        {
            font-family: arial,tahoma,verdana,helvetica,sans-serif;
            font-size: 11px;
            margin: 0px;
            background: #cdd1d8 url(App_Themes/DefaultV2/images/lgBg.jpg) repeat-x;
        }
    </style>
    <!--js-->
    <script src="App_Js/prototype.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/login.js" language="javascript" type="text/javascript"></script>
    <script src="App_Js/Common.js" language="javascript" type="text/javascript"></script>
    <!--/js-->
</head>
<body>
    <div id="table_main">
        <div class="contents_box">
            <div class="lgRp">
                <div class="war_box">
                    <div id="errMsg" style="display: none; width: 274px;">
                        <div id="error_item">
                            <ul>
                                <li><a class="err-msg" id="error_msg"></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="form_log">
                    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" style="height: 5px;">
                            </td>
                        </tr>
                        <tr>
                            <td width="91" class="icUser">
                                &nbsp;
                            </td>
                            <td width="77" height="39" class="log_text">
                                Username
                            </td>
                            <td width="222">
                                <input type="text" name="txtun" id="txtun" style="width: 180px; height: 20px;" onkeyup="PreLogin(event);"></input>
                            </td>
                        </tr>
                        <tr>
                            <td class="icPass">
                                &nbsp;
                            </td>
                            <td height="39" class="log_text">
                                Password
                            </td>
                            <td>
                                <input type="password" name="txtpw" id="txtpw" style="width: 180px; height: 20px;"
                                    onkeyup="PreLogin(event);"></input>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td height="35">
                                &nbsp;
                            </td>
                            <td>
                                <div id="btnlogin">
                                    <a href="javascript:ExcLogin()" class="btLg">LOGON</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom">
                                &nbsp;
                            </td>
                            <td valign="bottom">
                                &nbsp;
                            </td>
                            <td height="15" valign="bottom">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <%--<tr>
                            <td valign="bottom">
                                &nbsp;
                            </td>
                            <td valign="bottom">
                                &nbsp;
                            </td>
                            <td valign="middle">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="29%">
                                            Best in view:
                                        </td>
                                        <td width="28%" class="icFirefox">
                                            &nbsp;
                                        </td>
                                        <td width="43%" class="icIE">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </div>
            <div class="lgRight">             
                &copy; Copyright @ www.livebet365.com</div>
        </div>
    </div>
</body>
</html>

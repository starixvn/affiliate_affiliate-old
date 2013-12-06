<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="System.Threading" %>
<script runat="server">
    void Page_Load()
    {
        byte[] delay = new byte[1];
        RandomNumberGenerator prng = new RNGCryptoServiceProvider();

        prng.GetBytes(delay);
        Thread.Sleep((int)delay[0]);

        IDisposable disposable = prng as IDisposable;
        if (disposable != null) { disposable.Dispose(); }
    }
</script>
<html>
<head id="Head1" runat="server">
    <title>Error</title>
    <link href="App_Themes/DefaultV2/error/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="top">
    </div>
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
                    <li>We are sorry. The system is currently unavailable.<br />
                        Please try again later or contact administrator. </li>
                    <li>非常抱歉，本司服务器正在维护中，请稍后尝试登陆或联系在线客服。 </li>
                    <li class="end">非常抱歉，本司服務器正在維護中，請稍後嘗試登陸或聯繫在線客服 . </li>
                </ul>
            </div>
            <div id="redseperator2">
            </div>
        </div>
    </div>
</body>
</html>

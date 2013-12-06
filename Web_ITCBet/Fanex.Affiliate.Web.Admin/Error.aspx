<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Security.Cryptography" %>
<%@ Import Namespace="System.Threading" %>

<script runat="server">
   void Page_Load() {
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
	<div id="main">
        <div id="top">
            <a href="http://affadmin.itcbet.com"></a>
            <div class="title"></div>
        </div>
        <div id="content">
            <div class="left"></div>
            <div class="right">
                <div>
                    <p>We are sorry. The system is currently unavailable. Please try again later or contact administrator.</p>
                </div>
                <div>
                    <p>Chúng tôi thành thật xin lỗi ,vì hệ thống đang tiến hành bảo trì. Xin vui lòng thử lại trong giây lát sau hoặc liên hệ hỗ trợ trực tuyến .</p>
                </div>
                <div>
                    <p>ขออภัยในความไม่สะดวก ขณะนี้ระบบยังไม่สามารถใช้งานได้ชั่วคราว อีกสักครู่ กรุณาเข้าระบบใหม่อีกครั้ง หรือติดต่อเจ้าหน้าที่เพื่อให้คำแนะนำ </p>
                </div>
                <div>
                    <p>非常抱歉，本司服务器正在维护中，请稍后尝试登陆或联系在线客服.</p>
                </div>
                <div>
                    <p>非常抱歉，本司服務器正在維護中，請稍後嘗試登陸或聯繫在線客服.</p>
                </div>
                <div>
                    <p>Mohon maaf. Saat ini sistem tidak tersedia. Silahkan mencoba dilain waktu atau hubungi administrator.</p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
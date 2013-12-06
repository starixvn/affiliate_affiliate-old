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
	<div id="top">
    </div>
    <div id="seperator"> 
    </div>
    <div id="center">
    	<div id="title">
        </div>
              
        
        <div id="botcenter">
        	<div id="icon">
            </div>
            
            <div id="content">
            	<ul class="cont">
                	<li>
                    	We are sorry. The system is currently unavailable.<br />
						Please try again later or contact administrator.
                    </li>
                	
                    <li>
                    Chúng tôi thành thật xin lỗi ,vì hệ thống đang tiến hành bảo trì.
					<br />Xin vui lòng thử lại trong giây lát sau hoặc liên hệ hỗ trợ trực tuyến 
                	</li>
                    
                    <li>
                    ขออภัยในความไม่สะดวก ขณะนี้ระบบยังไม่สามารถใช้งานได้ชั่วคราว อีกสักครู่
					<br />กรุณาเข้าระบบใหม่อีกครั้ง หรือติดต่อเจ้าหน้าที่เพื่อให้คำแนะนำ 
                    </li>
                    
                    <li>
                    非常抱歉，本司服务器正在维护中，请稍后尝试登陆或联系在线客服。 
                    </li>
                    
                    <li>
                    非常抱歉，本司服務器正在維護中，請稍後嘗試登陸或聯繫在線客服 .
                    </li>
                    
                    <li class="end">
                    Mohon maaf. Saat ini sistem tidak tersedia.
					<br />Silahkan mencoba dilain waktu atau hubungi administrator.
                    </li>
                    
                    
                    
                </ul>
            </div>
        
        <div id="redseperator2">
        </div>
        
        </div>
    </div>
</body>
</html>
</html>
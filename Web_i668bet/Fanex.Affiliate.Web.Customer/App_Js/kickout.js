function KickedOut() {
    alert("Kickout.js");
   var ajax= new Ajax.Request("Authorization/ReCheckIn.ashx",{
            method:'get',              
            onComplete:function(datastran)
            {
                if(datastran.responseText=='Expired')
                {
                    
                    alert("Another session has logged in or your session is expired. Force to logout!");
                    ExcLogout();
                }
            }
        }); 
}
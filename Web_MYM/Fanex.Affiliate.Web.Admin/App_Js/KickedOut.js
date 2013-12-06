function ExcLogout()
{
    parent.location.href="Handler/Logout.ashx";
}

function KickedOut() {    

   var ajax= new Ajax.Request("Auth/ReCheckIn.ashx",{
            method:'get',              
            onComplete:function(datastran)
            {
                if(datastran.responseText=='Expired')
                {
                    alert("Another session has logged in or your session is expired. Force to logout!");
                    //parent.location.href='SignIn.aspx';
                    ExcLogout();
                }                
            }
        }); 
}
window.onload =function() { setInterval('KickedOut();', 15000);top.fMenu.ActiveMenuHead('menu1');}
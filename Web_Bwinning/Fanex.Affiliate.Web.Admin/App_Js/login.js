// JScript File

function setvaluefocus(e,isok,id)
{          
    if(isok==1) 
    {       
        if(e.value==$(id).value)
        {
                        
            e.value='';            
        }
    }
    else
    {   
        if(e.value=='')     
        e.value=$(id).value;
    }
}

function ExcLogin() {

    var username = $('txtun').value.trim();

    if (username == '' || username == 'Username')
    {
        //$('errMsg').show();
        //$('error_msg').className = "war_box comment";        
        ErrAlter('User Name is empty. Please check!');
        //$('error_msg').innerHTML='User Name is empty. Please check!';
        $('txtun').focus();
        return;
    }
    
    var pwd = $('txtpw').value.trim();

    if (pwd == '' || pwd == 'Password')
    {
        //$('errMsg').show();
        //$('error_msg').className = "war_box comment";        
        ErrAlter('Password is empty. Please check!');
        //$('error_msg').innerHTML='Password is empty. Please check!';
        $('txtpw').focus();
        return;
    }
    else
    {
        //$('errMsg').hide();
        $('error_msg').innerHTML='';
        var ajax= new Ajax.Request("Auth/ProcessLogin.ashx",{
            method:'post',
            parameters: 'un=' + username + '&pw=' + encodeURIComponent(pwd),
            onComplete:AlertMsg               
        });        
    }
}
function setdisable()
{
    if($('txtun').value!='' || $('txtpw').value!='')
    {
        $('btnlogin').disabled=false;
    }
    else {
        $('btnlogin').disabled=true;
    }
}
function AlertMsg(res)
{
    if (res.responseText == 'OK') {

        parent.location.href = 'Default.aspx';
    }
    else if (res.responseText == 'UM') {
        location.href = 'UnderMaintenance.aspx';
    }
    else {
        $('errMsgLogin').show();
        //$("errMsg").style.display = "";
        $('error_msg').className = "comment";
        $('error_msg').innerHTML = res.responseText;
    }
            
}

function PreLogin(e){        
    e =(e) ?e :window.event;
	var key =(e.keyCode) ?e.keyCode :e.which;
	if (key ==13)			
	ExcLogin();
}

if (top != window) {
    top.location.href = location.href;
}
// JScript File
var isLogin = true;

function setvaluefocus(e, isok, id) {
    if (isok == 1) {
        if (e.value == $(id).value) {
            e.focus()
            e.value = '';

        }
    }
    else {
        if (e.value == '') {
            e.value = $(id).value;
        }
    }
}

function ExcLogin() {

    var v = $('txtusername').value.trim();
    $('txtusername').value = v;
    
    if (v=='') {
        alert(gMsg.Errmsgentryusername);
        $('txtusername').focus();
        isLogin = false;
        return false;
    }
    if (checkInjecton("txtusername"))
        return false;

    v = $('txtpassword').value.trim();
    
    if (v == '') {
        alert(gMsg.Errmsgentrypassword);
        $('txtpassword').focus();
        isLogin = false;
        return false;
    }
    //if (checkInjecton("txtpassword"))
    //    return false;

    v = $('txtvalidate').value.trim();
    $('txtvalidate').value = v;
    if (v == '' || v == gMsg.entryvalidation) {
        alert(gMsg.Errmsgentryvalidcode);
        $('txtvalidate').focus();
        isLogin = false;
        return false;
    }
    if (checkInjecton("txtvalidate"))
        return false;
    var password = $('txtpassword').value.trim();
    var userName = $('txtusername').value.trim();
    var capcha = $('txtvalidate').value;
    var para = 'un=' + userName + '&pw=' + encodeURIComponent(password) + '&vcode=' + capcha;

    var ajax = new Ajax.Request("Authorization/ProcessLogin.ashx", {
        method: 'post',
        parameters: para,
        onComplete: Login
    });
    return false;
}
function checkInjecton(elementid) {
    var v = $(elementid).value;
    var err = Common.ValidateHTMLAndSQLInjection(v);

    if (err.ErrCode) {
        alert(err.ErrMsg);
        $(elementid).select();
        return true;
    }
    return false;
}
function Login(rs) {
    var err = rs.responseText.split(':');

    if (err[0] == 100) {
        parent.location.href = 'UserInfo/Account_Overview.aspx';
    }
    else {
        isLogin = false;
        $('validCode').src = 'Authorization/LoginCaptcha.ashx?t=' + new Date().toJSON();
        if (err[0] == 101) {
            alert(err[1]);
            $('txtvalidate').value = '';
            $('txtvalidate').focus();
        }
        else {
            alert(err[1]);
            $('txtvalidate').value = '';
            $('txtpassword').value = '';
            $('txtpassword').focus();
        }
    }
}



function PreLogin(e) {
    e = (e) ? e : window.event;
    var key = (e.keyCode) ? e.keyCode : e.which;
    if (key == 13) {
        if (navigator.appName.indexOf("Netscape") >= 0)
            if (!isLogin) {
            isLogin = true;
            return;
        }
        ExcLogin();
    }
}
function ExcLogout() {
    window.location.href = "Authorization/Logout.ashx";
}



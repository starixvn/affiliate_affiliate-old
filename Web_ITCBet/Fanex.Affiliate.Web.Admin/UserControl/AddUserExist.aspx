<%@ Page Language="C#" AutoEventWireup="true" Inherits="AddUserExist" Codebehind="AddUserExist.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    window.onload = Focus;
    function Focus() {
        $('username').blur();
        $('username').focus();
    } 
    function CheckInput()
    {
        if($('username').value=='')
        {
            ErrAlter(top.fMenu.Msg.Username);                                 
            $('username').focus();
            return false;
        }       
        else if($('dep').value==0 || $('dep').value=='')
        {
            ErrAlter(top.fMenu.Msg.DepartmentChoose);
            $('dep').focus();
            return false;
        }
        return true;
    }
    function Exec_NewUser(isexist)
    {
       
        if(CheckInput())
        {
            var ajax = new Ajax.Request('../Handler/AddUser.ashx', {
                method: 'post',
                parameters: { us: $('username').value, gpid: $('dep').value, isex: isexist },
                onComplete: function(strdata) {
                    if (strdata.responseText == 'Ok') {
                        /*location.href='../Alert.aspx?code=2&user=' + $('username').value + '&link=UserControl/ListUser.aspx';       */
                        ClosePopup();
                        location.href = '../Alert.aspx?code=2&user=' + $('username').value + '&link=UserControl/ListUser.aspx';
                    }
                    if (strdata.responseText != 'Ok') {
                        ErrAlter(strdata.responseText);
                    }
                }
            });  
        }
    }
    function EnterExecUser(e)
    {        
        e =(e) ?e :window.event;
	    var key =(e.keyCode) ?e.keyCode :e.which;
	    if (key ==13) Exec_NewUser(1);
    }
    function Reset()
    {
        parent.ibcWnd.close();
    }

</script>
<!--/js-->
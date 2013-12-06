<%@ Page Language="C#" AutoEventWireup="true" Inherits="AddUser" Codebehind="AddUser.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function CheckInput() {
        var username = $('username').value.trim();
        if(!Common.isValidUserName(username)){
            ErrAlter(top.fMenu.Msg.ErrValidUserName);
            $('username').focus();
            return false;
        }

        if (username == '')
        {
            ErrAlter(top.fMenu.Msg.Username);
            $('username').focus();
            return false;
        }
        else if($('password').value=='')
        {
            ErrAlter(top.fMenu.Msg.PasswordRequired);          
            $('password').focus();
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
        var param='';
        if(CheckInput()==false)
        {
            return;
        }
        else if (CheckSpecialChar()==false)
        {
            return;
        }
        else
        {

            var ajax = new Ajax.Request('../Handler/AddUser.ashx', {
                method: 'post',
                parameters: { us: $('username').value.trim(), pw: $('password').value, ln: $('lastname').value, fn: $('firstname').value, gpid: $('dep').value, isex: isexist },
                onComplete: function(strdata) {

                if (strdata.responseText == 'Ok') {
                        var arrError = new Array();
                        var arrItem = new Array();
                        //location.href = '../Alert.aspx?code=2&user=AA' + $('username').value + '&link=UserControl/ListUser.aspx';

                        //var url = parent.location.href;
                        $("errMsg").className = "done";
                        arrError.push("Administrator has been added successfully!");
                        arrItem.push("errMsg");
                        $("errMsg").show();
                        ShowError(arrError, arrItem, "error_item");
                        DelayReloadPage1(); 
                        setTimeout("ClosePopup();", 3000);

                    }
                    if (strdata.responseText != 'Ok') {
                        ErrAlter(strdata.responseText);
                    }

                }
            });      
               
            
        }
    }
    window.onload=function()
        {
            $('username').focus();
        }
        function removeextra(element) { // remove special character from input

            var initVal = element.value;

            if (!initVal)
                return;

            outputVal = initVal.replace(/[^0-9a-z]/g, "");
            if (initVal != outputVal) {
                element.value = outputVal;
            }
        };
        function PreExec(e) {            
          e =(e) ?e :window.event;
          var key = (e.keyCode) ? e.keyCode : e.which;          
	      if (key == 13) {
	          $('btnadd').click();

	      } else if (key == 8 || // backspace
           (key >= 48 && key <= 57) || // 0-9
            //(key >= 65 && key <= 90) || // A-Z
            (key >= 97 && key <= 122))  // a-z
	          return true;
	      return false;
     }
     function Reset() {
         parent.ibcWnd.close();
     }  
     function CheckSpecialChar()
     {
        var iChars = "!@#$%^&*()+=-[]\';,./{}|\":<>?";
        for (var i = 0; i < $('username').value.length; i++)
        {
  	        if (iChars.indexOf($('username').value.charAt(i)) != -1) 
  	        {
  	            ErrAlter(top.fMenu.Msg.Character);
  	            return false;
  		    }
  		    else
	            return true;
	     }

	 }
</script>
<!--/js-->
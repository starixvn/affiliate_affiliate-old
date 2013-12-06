<%@ Page Language="C#" AutoEventWireup="true" Inherits="AddDepartment" Codebehind="AddDepartment.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    window.onload = Focus;
    function Focus() {
        $('txtdep').blur();
        $('txtdep').focus();
    } 
   function checkinput()
   {
        if($('txtdep').value=='')
        {         
            ErrAlter(top.fMenu.Msg.Department);
            $('txtdep').focus();
            return false;
        }
        return true;
   }
   function AddDepartment() {
       var arrError = new Array();
       var arrItem = new Array();
        if(checkinput())
        {
            var ajax = new Ajax.Request('../Handler/UpdateDepartment.ashx', {
                method: 'post',
                parameters: { depname: $('txtdep').value, depdesc: $('txtdesc').value, sta: 'Add' },
                onComplete: function(strdata) {

                    if (strdata.responseText == 'Ok') {
                        //location.href = '../Alert.aspx?code=2&user=' + $('txtdep').value + '&link=UserControl/ListDepartment.aspx';
                        $("errMsg").className = "done";
                        arrError.push("Group has been added successfully!");
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
    function PreExec1(e){        
        e =(e) ?e :window.event;
        var key =(e.keyCode) ?e.keyCode :e.which;
        if (key == 13) {
            AddDepartment();
        }
        else if (key == 39 || key == 34) {
            alert("Group name can not contain this character");
            return false;
        }
    }
    function PreExec(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            AddDepartment();
        }
    }
     function Reset()
     {
        $('txtdep').value="";
        $('txtdesc').value="";
    }
</script>
<!--/js-->
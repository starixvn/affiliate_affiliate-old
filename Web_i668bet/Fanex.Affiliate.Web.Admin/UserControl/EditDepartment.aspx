<%@ Page Language="C#" AutoEventWireup="true" Inherits="EditDepartment" Codebehind="EditDepartment.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/prototype-full.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var isUpdate = false;
   function EditDepartment(depid)
   {   
        if($("txtdep").value.trim() == "")
        {
            ErrAlter(top.fMenu.Msg.ErrDepartment);
            return;
        }
        var ajax = new Ajax.Request('../Handler/UpdateDepartment.ashx', {
            method: 'post',
            parameters: { depid: depid, depname: $('txtdep').value, depdesc: $('txtdesc').value, sta: 'Edit' },
            onComplete: function(strdata) {
                if (strdata.responseText == 'Ok') {
                    isUpdate = true;
                    $("errMsg").className = "done";
                    SuccAlter(top.fMenu.Msg.UpdateSucc);
                    setTimeout("parent.ibcWnd.close();", 2000);
                }
                else {
                    ErrAlter(strdata.responseText);
                }
            }
        });  
   }
    function PreExec(e,depid){        
            e =(e) ?e :window.event;
	        var key =(e.keyCode) ?e.keyCode :e.which;
	        if (key ==13)
	            EditDepartment(depid);
	        else if (key == 39 || key == 34) {
	            alert(top.fMenu.Msg.GroupNameNotContainChar);
	            return false;
	        }
        }
    window.onunload = function(){if(isUpdate) parent.location.reload();};
</script>
<!--/js-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit12BetDomain.aspx.cs" Inherits="Fanex.Web.i668bet.Admin.Domain.Edit12BetDomain" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/prototype-full.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script language="javascript" type="text/javascript">
    var isUpdate = false;

    window.onload = InitEvent;
    function InitEvent() {
        CheckBox_AddEvent();
        $('txt12betdomain').blur();
        $('txt12betdomain').focus();
    }

   function Edit12BetDomain(_12BetDomainId)
   {
       if ($("txt12betdomain").value.trim() == "")
        {
            ErrAlter(top.fMenu.Msg.ErrAffDomain);
            return;
        }
        var ajax = new Ajax.Request('../Handler/UpdateMemberDomain.ashx', {
            method: 'post',
            parameters: { _12BetDomainId: _12BetDomainId, _12BetDomainName: $('txt12betdomain').value, sta: 'Edit' },
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
    function PreExec(e,domainid){        
            e =(e) ?e :window.event;
	        var key =(e.keyCode) ?e.keyCode :e.which;
	        if (key ==13)
	            Edit12BetDomain(domainid);
	        else if (key == 39 || key == 34) {
	            alert(top.fMenu.Msg.MemDomainNameNotContainChar);
	            return false;
	        }
        }
    window.onunload = function(){if(isUpdate) parent.location.reload();};
</script>
<!--/js-->
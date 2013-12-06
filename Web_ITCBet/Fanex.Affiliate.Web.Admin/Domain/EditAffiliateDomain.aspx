<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAffiliateDomain.aspx.cs" Inherits="Fanex.Web.ITCBet.Admin.Domain.EditAffiliateDomain" %>
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
        $('txtaffdomain').blur();
        $('txtaffdomain').focus();
    }

   function EditAffDomain(affDomainId)
   {
       if ($("txtaffdomain").value.trim() == "")
        {
            ErrAlter(top.fMenu.Msg.ErrAffDomain);
            return;
        }
        var ajax = new Ajax.Request('../Handler/UpdateAffDomain.ashx', {
            method: 'post',
            parameters: { affDomainId: affDomainId, affDomainName: $('txtaffdomain').value, sta: 'Edit' },
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
	            EditAffDomain(domainid);
	        else if (key == 39 || key == 34) {
	            alert("Affiliate Domain name can not contain this character");
	            return false;
	        }
        }
    window.onunload = function(){if(isUpdate) parent.location.reload();};
</script>
<!--/js-->
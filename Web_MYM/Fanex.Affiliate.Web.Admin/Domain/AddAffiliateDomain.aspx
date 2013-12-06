<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAffiliateDomain.aspx.cs" Inherits="Fanex.Web.MYM.Admin.Domain.AddAffiliateDomain" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    window.onload = Focus;
    function Focus() {
        $('txtaffdomain').blur();
        $('txtaffdomain').focus();
    }
    
   function checkinput()
   {
       if ($('txtaffdomain').value == '')
        {
            ErrAlter(top.fMenu.Msg.AffDomain);
            $('txtaffdomain').focus();
            return false;
        }
        return true;
   }
   function AddAffDomain() {
       var arrError = new Array();
       var arrItem = new Array();       
        if(checkinput())
        {
            var ajax = new Ajax.Request('../Handler/UpdateAffDomain.ashx', {
                method: 'post',
                parameters: { affDomainName: $('txtaffdomain').value, sta: 'Add' },
                onComplete: function(strdata) {

                    if (strdata.responseText == 'Ok') {                        
                        $("errMsg").className = "done";
                        arrError.push(top.fMenu.Msg.AffDomainNameAddSucc);
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
            AddAffDomain();
        }
        else if (key == 39 || key == 34) {
            alert(top.fMenu.Msg.AffDomainNameNotContainChar);
            return false;
        }
    }
    function PreExec(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            AddAffDomain();
        }
    }
     function Reset()
     {
         $('txtaffdomain').value = "";        
    }
</script>
<!--/js-->
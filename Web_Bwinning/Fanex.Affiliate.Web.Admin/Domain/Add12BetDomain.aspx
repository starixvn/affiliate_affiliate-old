<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add12BetDomain.aspx.cs" Inherits="Fanex.Web.Bwinning.Admin.Domain.Add12BetDomain" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    window.onload = Focus;
    function Focus() {        
        $('txt12betdomain').blur();
        $('txt12betdomain').focus();
    }
    
   function checkinput()
   {
       if ($('txt12betdomain').value == '')
        {
            ErrAlter(top.fMenu.Msg._12BetDomain);
            $('txt12betdomain').focus();
            return false;
        }
        return true;
   }
   function Add12BetDomain() {
       var arrError = new Array();
       var arrItem = new Array();
        if(checkinput())
        {
            var ajax = new Ajax.Request('../Handler/UpdateMemberDomain.ashx', {
                method: 'post',
                parameters: { _12BetDomainName: $('txt12betdomain').value, sta: 'Add' },
                onComplete: function(strdata) {

                    if (strdata.responseText == 'Ok') {                        
                        $("errMsg").className = "done";
                        arrError.push("member domain has been added successfully!");
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
            Add12BetDomain();
        }
        else if (key == 39 || key == 34) {
            alert("Affiliate Domain name can not contain this character");
            return false;
        }
    }
    function PreExec(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            Add12BetDomain();
        }
    }
     function Reset()
     {
         $('txt12betdomain').value = "";        
    }
</script>
<!--/js-->
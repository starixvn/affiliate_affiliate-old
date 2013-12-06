<%@ Page Language="C#" AutoEventWireup="true" Inherits="EditUser" Codebehind="EditUser.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
   function EditUser(custid,depname)
   {
       var ajax = new Ajax.Request('../Handler/UpdateUser.ashx', {
           method: 'post',
           parameters: { custid: custid, fn: $('txtfirstname').value, ln: $('txtlastname').value, depid: $('SelDep').value },
           onComplete: function(strdata) {
               if (strdata.responseText == 'Ok') {
                   DelayReloadPage1();
                   arrError.push($('AlertSucc').value);
                   arrItem.push("errMsg");
                   $("errMsg").className = "done";
                   $("errMsg").show();
                   ShowError(arrError, arrItem, "error_item");
                   if (parent.$("isReload"))
                       parent.$("isReload").value = "1"; //Reload parent window when update Data
                   if (depname == "") {
                       setTimeout("parent.$('btnsearch').click();", 2500);
                   }
                   else {
                       setTimeout("ClosePopup();", 2500);
                   }
               }
               else {
                   ErrAlter(strdata.responseText);
               }

           }
       });  
   }
  function PreExec(e,custid,depname){        
            e =(e) ?e :window.event;
	        var key =(e.keyCode) ?e.keyCode :e.which;
	        if (key ==13)			
	        EditUser(custid,depname);
        }

</script>
<!--/js-->
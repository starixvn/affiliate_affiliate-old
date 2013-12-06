<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.LiveBet365.AffiliateAdmin.Exchange.AffEditRate" Codebehind="AffEditRate.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
    var isUpdate = false;
    function getParam(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return results[1];
    }
    function updateRate(exid)
    {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();
        if(isEmpty($("txtRate").value))
        {
              arrError.push(top.fMenu.Msg.RateRequire);            
              arrItem.push("errMsg");
              $('errMsg').show();
              ShowError(arrError, arrItem, "error_item");  
              return false;
        }
        var Rate = parseFloat($("txtRate").value);
        if(Rate <= 0)
        {
            arrError.push(top.fMenu.Msg.Rate);
            arrItem.push("errMsg");
            $('errMsg').show();
            ShowError(arrError, arrItem, "error_item");  
            return false;
	    }
	    DelayReloadPage1();
	    var myajax = new Ajax.Request('../Handler/EditRate.ashx', {
	        method: 'post',
	        parameters: { id: exid, rate: $("txtRate").value, tocurrency: getParam('tocurrency') },
	        onComplete: function(res) {
	            if (res.responseText == "1") {
	                $("errMsg").className = "done";
	                arrError.push("Update Successfully!");
	                arrItem.push("errMsg");
	                isUpdate = true;	                
	                $("errMsg").show();
	                ShowError(arrError, arrItem, "error_item");
	                setTimeout("DelayReloadPage1();parent.ibcWnd.close();", 2000);
	            }
	            else {
	                arrError.push(res.responseText);
	                arrItem.push("txtRate");
	                $('errMsg').show();
	                ShowError(arrError, arrItem, "error_item");
	                HideDelayReloadPage();
	            }
	        }
	    });
        return false;
    }
    
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46)) {
            alert("Please input digit only.");
            return false;
        }

        return true;
    }
    
    window.onunload=function(){if(isUpdate) parent.location.reload();}
</script>
<!--/js-->
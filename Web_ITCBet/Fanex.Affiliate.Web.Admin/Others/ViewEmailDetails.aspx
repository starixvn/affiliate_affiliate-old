<%@ Page Language="C#" AutoEventWireup="true" Inherits="Fanex.Web.ITCBet.Admin.Others.ViewEmailDetails" Codebehind="ViewEmailDetails.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrErr = new Array();
    
    function CheckForm()
    {
        return true;
    } 
    function UpdateSuccess(Message)
    {      
        arrErr.push(Message);
        arrItem.push("errMsg");
        $("errMsg").show();
        ShowError(arrErr, arrItem, "error_item");                        
        setTimeout("closePopup();", 2500);  
    }
    function UpdateFail(Message)
    {      
        arrErr.push(Message);
        ErrAlter(arrErr);
    }
    
    var paththeme = "{paththeme}";
    function DeleteSelectedMessage() {
        if (confirm($("ConfirmDeleteEmail").value)) {
            var params = "";
            for (var i = minNo; i <= maxNo; i++) {
                if ($("chk" + i).checked) {
                    params += $("td" + i).innerHTML;
                    params += ",";
                }
            }
            DeleteMessage(params);
        }
    }
    function DeleteMessage(msgList) {
        try {
            var params;
            params = "params=" + msgList;

            var ajax = new Ajax.Request("../Handler/DeleteMailList.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {
                if (data.responseText != "") {
                        alert(data.responseText);
                    }
                ClosePopup();
                }
            });

        }
        catch (e) {
            alert(e);
        }

    }
    function DeleteCurrentMessage() {
        if (confirm($("ConfirmDeleteEmail").value)) {
            DeleteMessage($("mid").value + ",");
        }
    }

    function LoadMessageListData(curPage) {
        try {
            var page = parseInt(curPage);

            var params;
            if (option != -1) {
                text = $('SearchText').value;
            }

            params = "params=" + option + ";" + text + ";" + fromdate + ";" + todate + ";" + curPage + ";";  //+ page;

            var ajax = new Ajax.Request("../Handler/GetEmailHistoryList.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {
                    ProcessResult(data.responseText);
                }
            });
        }
        catch (e) {
            alert(e);
        }
    }
</script>
<!--/js-->

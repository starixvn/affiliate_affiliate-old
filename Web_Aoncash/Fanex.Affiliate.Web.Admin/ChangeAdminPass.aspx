<%@ Page Language="C#" AutoEventWireup="true" Inherits="ChangeAdminPass" Codebehind="ChangeAdminPass.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="App_Js/Common.js"></script>
<script type="text/javascript" src="App_Js/prototype.js"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrErr = new Array();
    function CheckForm()
    {
        arrItem.clear();
        arrErr.clear();
        if(isEmpty($("OldPass").value))
        {
            arrItem.push('OldPass');
            arrErr.push($("hdOldPass").value);
        }
        else
        {
            if(isEmpty($("NewPassword").value))
            {
                arrItem.push('NewPassword');
                arrErr.push($("hdPassRequire").value);
            }
            else
            {
                if($("ConfirmPassword").value != $("NewPassword").value)
                {
                    arrItem.push('ConfirmPassword');
                    arrErr.push($("hdPassConfirm").value);
                }
            }
        }
        if(arrItem.length > 0)
        {
            $('errMsg').show();
            ShowError(arrErr, arrItem, "error_item");
            return false;
        }
        return true;
    }  
    function UpdateFail(Msg)
    {
        arrErr.push(Msg);
        arrItem.push("OldPass");
        $('errMsg').show();
        ShowError(arrErr, arrItem, "error_item");
        ErrAlter(Msg);
    }
    function UpdateSucess(Msg) {
        SuccAlter(Msg);
    }
    function ErrAlter(msg) {
        $('errMsg').className = "";
        $('errMsg').show();
        $('error_item').innerHTML = msg;
        window.scroll(0, 0);
        setTimeout("HideErr()", 5000);
    }
    function SuccAlter(msg) {
        $("errMsg").className = "done";
        $('errMsg').show();
        $('error_item').innerHTML = msg;
        window.scroll(0, 0);
        setTimeout("HideErr()", 3000);
    }
     window.onload=function()
        {
            $('OldPass').focus();
        }
</script>
<!--/js-->
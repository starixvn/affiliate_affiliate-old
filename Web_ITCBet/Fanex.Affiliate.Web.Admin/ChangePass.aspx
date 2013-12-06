<%@ Page Language="C#" AutoEventWireup="true" Inherits="ChangePass" Codebehind="ChangePass.aspx.cs" %>
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
        if(isEmpty($("NewPassword").value))
        {               
            arrErr.push($("hdPassRequire").value);
            ErrAlter(arrErr);
            $("NewPassword").focus();
            return false;
        }
        else
        {
            if($("ConfirmPassword").value != $("NewPassword").value)
            {                              
                arrErr.push($("hdPassConfirm").value);                
                ErrAlter(arrErr);
                $("ConfirmPassword").focus();
                return false;
            }
        }
        return true;
    } 
    function UpdateSuccess(Message)
    {      
        arrErr.push(Message);
        arrItem.push("errMsg");
        $("errMsg").className = "done";
        $("errMsg").show();        
        ShowError(arrErr, arrItem, "error_item");
        setTimeout("closePopup();", 2500);  
    }
    function UpdateFail(Message)
    {   
        $("errMsg").className = "";   
        arrErr.push(Message);
        ErrAlter(arrErr);
    }
    function closePopup(){
        if(parent.ibcWnd)
            parent.ibcWnd.close();
        else
            window.close();
    }
</script>
<!--/js-->

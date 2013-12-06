<%@ Page Language="C#" AutoEventWireup="true" Inherits="PaymentUpdate" Codebehind="PaymentUpdate.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
    function ValidForm()
    {
        $('errMsg').hide();
        arrError.clear();
        arrItem.clear();
        var radios = document.getElementsByName("rdApprove");
        if(radios[0].checked == false && radios[1].checked == false)
        {
            arrError.push($("AppErr").value);                       
            ErrAlter(arrError);
            return false;
        }
        if(radios[0].checked)
            $("hdApprove").value = "Approved";
        if(radios[1].checked)
            $("hdApprove").value = "Rejected";
        DelayReloadPage1();
        return true;
    }
    function EnableUpdate(isLoad)
    {
        var radios = document.getElementsByName("rdApprove");
        if(radios[0].checked == false && radios[1].checked == false)
        {
            $("btnUpdate").disable();
            $("btnUpdate").className = "disabled";
            return;
        }
        if(!isLoad)
        {
            $("btnUpdate").enable();
            $("btnUpdate").className = "buttonlogin";
        }
    }
    function UpdateSuccess(msg)
    {
        scroll(0, 0);
        ErrAlter(msg);
        $("errMsg").className = "done";
        setTimeout("parent.DelayReloadPage1();parent.location.href = parent.location.href.replace(/#/, '')", 2000); 
    }
    function ReloadPage(msg)
    {
        var fInfo = top.document.getElementById("fInfo");
        if(fInfo)
        {
            fInfo.$("isReload").value = "3";
            fInfo.$("frmSearch").submit();
        }        
    }
    function UpdateErr(msg)
    {
        ErrAlter(msg);
        HideDelayReloadPage();
    }
</script>    
<!--/js-->